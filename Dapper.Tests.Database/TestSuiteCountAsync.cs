using System.Linq;

using Dapper.Database.Extensions;
using Xunit;
using System.Threading.Tasks;

#if NET452
using System.Transactions;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlServerCe;
#endif


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        [ProviderFact]
        [Trait("Category", "CountAsync")]
        public async Task CountAllAsync()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.Equal(295, await connection.CountAsync<Product>());
            }
        }

        [ProviderFact]
        [Trait("Category", "CountAsync")]
        public async Task CountWithWhereClauseAsync()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.Equal(89, await connection.CountAsync<Product>("where Color = 'Black'" ));
            }
        }


        [ProviderFact]
        [Trait("Category", "CountAsync")]
        public async Task CountWithWhereClauseParameterAsync()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.Equal(89, await connection.CountAsync<Product>("where Color = @Color", new { Color = "Black" }));
            }
        }

        [ProviderFact]
        [Trait("Category", "CountAsync")]
        public async Task CountWithSelectClauseAsync()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.Equal(89, await connection.CountAsync<Product>("select * from Product where Color = 'Black'"));
            }
        }

        [ProviderFact]
        [Trait("Category", "CountAsync")]
        public async Task CountWithSelectClauseParameterAsync()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.Equal(89, await connection.CountAsync<Product>("select * from Product where Color = @Color", new { Color = "Black" }));
            }
        }

        [ProviderFact]
        [Trait("Category", "CountAsync")]
        public async Task CountShortCircuitAsync()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.Equal(89, await connection.CountAsync<Product>(";select count(*) from Product where Color = @Color", new { Color = "Black" }));
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Data;
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
        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsAsync()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new CustomerProxy {FirstName= "Exists" };
                Assert.True(await connection.InsertAsync(u1).ConfigureAwait(false));
                Assert.True(await connection.ExistsAsync<CustomerProxy>(u1).ConfigureAwait(false));
                Assert.True(await connection.ExistsAsync<CustomerProxy>(u1.Id).ConfigureAwait(false));
                Assert.False(await connection.ExistsAsync<CustomerProxy>(-100).ConfigureAwait(false));

            }
        }

        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsClauseQueryAsync()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new CustomerProxy { FirstName = "FetchMe" };
                Assert.True(await connection.InsertAsync(u1).ConfigureAwait(false));
                Assert.True(await connection.ExistsAsync<CustomerProxy>("[FirstName] = @FirstName", new { FirstName = "FetchMe" }).ConfigureAwait(false));
                Assert.False(await connection.ExistsAsync<CustomerProxy>("[FIrstName] = @FirstName", new { FirstName = "junk" }).ConfigureAwait(false));

            }
        }

    }
}

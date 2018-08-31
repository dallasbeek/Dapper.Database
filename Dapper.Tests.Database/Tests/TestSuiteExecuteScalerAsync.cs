using System.Threading.Tasks;
using Xunit;

using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "ExecuteScalerAsync")]
        public async Task ExecuteScalerAsyncSql()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(102.29m, await db.ExecuteScalerAsync<decimal>("select listprice from Product where productid = 806"));
            }
        }


        [Fact]
        [Trait("Category", "ExecuteScalerAsync")]
        public async Task ExecuteScalerAsyncSqlWithParameter()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(102.29m, await db.ExecuteScalerAsync<decimal>($"select listprice from Product where productid = {P}ProductId", new { ProductId = 806 }));
            }
        }

    }
}

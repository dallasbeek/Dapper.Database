using Xunit;

using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "ExecuteScaler")]
        public void ExecuteScalerSql()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(102.29m, db.ExecuteScaler<decimal>("select listprice from Product where productid = 806"));
            }
        }


        [Fact]
        [Trait("Category", "ExecuteScaler")]
        public void ExecuteScalerSqlWithParameter()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(102.29m, db.ExecuteScaler<decimal>($"select listprice from Product where productid = {P}ProductId", new { ProductId = 806 }));
            }
        }

    }
}

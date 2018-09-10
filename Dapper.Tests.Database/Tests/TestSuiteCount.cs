using Xunit;

using FactAttribute = Xunit.SkippableFactAttribute;


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        [Fact]
        [Trait("Category", "Count")]
        public void CountAll()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(295, db.Count<Product>());
            }
        }

        [Fact]
        [Trait("Category", "Count")]
        public void CountWithWhereClause()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, db.Count<Product>("where Color = 'Black'"));
            }
        }


        [Fact]
        [Trait("Category", "Count")]
        public void CountWithWhereClauseParameter()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, db.Count<Product>($"where Color = {P}Color", new { Color = "Black" }));
            }
        }

        [Fact]
        [Trait("Category", "Count")]
        public void CountWithSelectClause()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, db.Count<Product>("select * from Product where Color = 'Black'"));
            }
        }

        [Fact]
        [Trait("Category", "Count")]
        public void CountWithSelectClauseParameter()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, db.Count<Product>($"select * from Product where Color = {P}Color", new { Color = "Black" }));
            }
        }

        [Fact]
        [Trait("Category", "Count")]
        public void CountShortCircuit()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, db.Count<Product>($";select count(*) from Product where Color = {P}Color", new { Color = "Black" }));
            }
        }


    }
}

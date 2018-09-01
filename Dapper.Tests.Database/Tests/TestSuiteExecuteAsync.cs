using System.Threading.Tasks;
using Xunit;

using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "ExecuteAsync")]
        public async Task ExecuteAsyncSql()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, await db.ExecuteAsync("update Product set color = 'Black' where Color = 'Black'"));
            }
        }


        [Fact]
        [Trait("Category", "ExecuteAsync")]
        public async Task ExecuteAsyncSqlWithParameter()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, await db.ExecuteAsync($"update Product set color = {P}Color where Color = {P}Color", new { Color = "Black" }));
            }
        }

    }
}

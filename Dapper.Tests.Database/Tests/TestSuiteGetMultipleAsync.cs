using System.Linq;
using System.Threading.Tasks;
using Xunit;

using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "GetMultipleAsync")]
        public async Task GetMultipleAsync()
        {
            if (GetProvider() != Provider.SqlServer)
            {
                CheckSkip();
                return;
            }

            using (var db = GetSqlDatabase())
            {
                using (var trans = db.GetTransaction())
                {
                    var dt = await db.GetMultipleAsync(@"
                    select * from Product where Color = 'Black';
                    select * from ProductCategory where productcategoryid = '21';");
                    Assert.Equal(89, dt.Read(typeof(Product)).Count());

                    var pc = (ProductCategory)dt.ReadSingle(typeof(ProductCategory));
                    ValidateProductCategory21(pc);
                    trans.Complete();
                }
            }
        }


        [Fact]
        [Trait("Category", "GetMultipleAsync")]
        public async Task GetMultipleAsyncWithParameter()
        {
            if (GetProvider() != Provider.SqlServer)
            {
                CheckSkip();
                return;
            }

            using (var db = GetSqlDatabase())
            {
                using (var trans = db.GetTransaction())
                {
                    var dt = await db.GetMultipleAsync($@"
                    select * from Product where Color = {P}Color;
                    select * from ProductCategory where productcategoryid = {P}ProductCategoryId;",
                        new { Color = "Black", ProductCategoryId = 21 });
                    Assert.Equal(89, dt.Read(typeof(Product)).Count());

                    var pc = (ProductCategory)dt.ReadSingle(typeof(ProductCategory));
                    ValidateProductCategory21(pc);
                    trans.Complete();
                }
            }
        }

    }
}

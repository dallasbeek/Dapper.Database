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
        [Trait("Category", "GetMultipleAsync")]
        public async Task GetMultipleAsync()
        {
            if (Provider != Provider.SQLite)
            {
                using (var db = GetSqlDatabase())
                {
                    var dt = await db.GetMultipleAsync(@"
                        select * from product where Color = 'Black';
                        select * from productcategory where productcategoryid = '21';");
                    Assert.Equal(89, dt.Read(typeof(Product)).Count());

                    var pc = (ProductCategory) dt.ReadSingle(typeof(ProductCategory));
                    ValidateProductCategory21(pc);
                }
            }
        }


        [Fact]
        [Trait("Category", "GetMultipleAsync")]
        public async Task GetMultipleAsyncWithParameter()
        {
            if (Provider != Provider.SQLite)
            {
                using (var db = GetSqlDatabase())
                {
                    var dt =await db.GetMultipleAsync(@"
                        select * from product where Color = @Color;
                        select * from productcategory where productcategoryid = @ProductCategoryId;",
                            new { Color = "Black", ProductCategoryId = 21 });
                    Assert.Equal(89, dt.Read(typeof(Product)).Count());

                    var pc = (ProductCategory)dt.ReadSingle(typeof(ProductCategory));
                    ValidateProductCategory21(pc);
                }
            }
        }

    }
}

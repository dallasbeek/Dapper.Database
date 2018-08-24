using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Dapper.Database.Extensions;
using Xunit;

#if NET452
using System.Transactions;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlServerCe;
#endif
using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "GetMultiple")]
        public void GetMultiple()
        {
            if (GetProvider() == Provider.SqlServer)    
            {
             
                using (var db = GetSqlDatabase())
                {
                    using (var trans = db.GetTransaction())
                    {
                        var dt = db.GetMultiple(@"
                        select * from product where Color = 'Black';
                        select * from productcategory where productcategoryid = '21';");
                        Assert.Equal(89, dt.Read(typeof(Product)).Count());

                        var pc = (ProductCategory)dt.ReadSingle(typeof(ProductCategory));
                        ValidateProductCategory21(pc);
                        trans.Complete();
                    }
                }
            }
        }


        [Fact]
        [Trait("Category", "GetMultiple")]
        public void GetMultipleWithParameter()
        {
            if (GetProvider() == Provider.SqlServer)
            {
                using (var db = GetSqlDatabase())
                {
                    using (var trans = db.GetTransaction())
                    {
                        var dt = db.GetMultiple($@"
                        select * from product where Color = {P}Color;
                        select * from productcategory where productcategoryid = {P}ProductCategoryId;",
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
}

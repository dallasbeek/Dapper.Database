using System;
using Dapper.Database.Extensions;
using Xunit;
using System.Linq;
using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {


        [Fact]
        [Trait("Category", "GetFirst")]
        public void GetFirstWithWhereClause()
        {
            using ( var db = GetSqlDatabase() )
            {
                var item = db.GetFirst<Product>("where Color = 'Black' and ProductId >= 816 order by ProductId");
                ValidateProduct816(item);
            }
        }


        [Fact]
        [Trait("Category", "GetFirst")]
        public void GetFirstWithWhereClauseParameter()
        {
            using ( var db = GetSqlDatabase() )
            {
                var item = db.GetFirst<Product>($"where Color = {P}Color and ProductId >= {P}ProductId order by ProductId", new { Color = "Black", ProductId = 816 });
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetFirst")]
        public void GetFirstWithSelectClause()
        {
            using ( var db = GetSqlDatabase() )
            {
                var item = db.GetFirst<Product>("select p.*, p.rowguid as GuidId from Product p where p.Color = 'Black' and p.ProductId >= 816 order by p.ProductId");
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetFirst")]
        public void GetFirstWithSelectClauseParameter()
        {
            using ( var db = GetSqlDatabase() )
            {
                var item = db.GetFirst<Product>($"select p.*, p.rowguid as GuidId from Product p where p.Color = {P}Color and p.ProductId >= {P}ProductId order by p.ProductId", new { Color = "Black", ProductId = 816 });
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetFirst")]
        public void GetFirstShortCircuit()
        {
            using ( var db = GetSqlDatabase() )
            {
                var item = db.GetFirst<Product>($";select p.*, p.rowguid as GuidId from Product p where p.Color = {P}Color and p.ProductId >= {P}ProductId order by p.ProductId", new { Color = "Black", ProductId = 816 });
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetFirst")]
        public void GetFirstPartialBySelect()
        {
            using ( var db = GetSqlDatabase() )
            {
                var item = db.GetFirst<Product>($"select p.ProductId, p.rowguid AS GuidId, Name from Product p where p.Color = {P}Color and p.ProductId >= {P}ProductId order by p.ProductId", new { Color = "Black", ProductId = 816 });
                Assert.Equal(816, item.ProductID);
                Assert.Equal("ML Mountain Front Wheel", item.Name);
                Assert.Null(item.ProductNumber);
                Assert.Equal(new Guid("5E3E5033-9A77-4DCA-8B7F-DFED78EFA08A"), item.GuidId);
            }
        }

        [Fact]
        [Trait("Category", "GetFirst")]
        public void GetFirstOneJoinUnmapped()
        {
            using ( var db = GetSqlDatabase() )
            {
                var item = db.GetFirst<Product, ProductCategory>(
                    getFirstTwoParamQuery, new { Color = "Black", ProductId = 816 }, "ProductCategoryId");
                ValidateProduct816(item);
                if ( GetProvider() != Provider.SQLite )
                {
                    ValidateProductCategory21(item.ProductCategory);
                }
            }
        }

        [Fact]
        [Trait("Category", "GetFirst")]
        public void GetFirstOneJoinMapped()
        {
            using ( var db = GetSqlDatabase() )
            {
                var item = db.GetFirst<Product, ProductCategory, Product>(
                    ( pr, pc ) =>
                    {
                        pr.ProductCategory = pc;
                        return pr;
                    },
                    getFirstTwoParamQuery, new { Color = "Black", ProductId = 816 }, "ProductCategoryId");
                ValidateProduct816(item);
                if ( GetProvider() != Provider.SQLite )
                {
                    ValidateProductCategory21(item.ProductCategory);
                }
            }
        }

        [Fact]
        [Trait("Category", "GetFirst")]
        public void GetFirstTwoJoinsUnmapped()
        {
            using ( var db = GetSqlDatabase() )
            {
                var item = db.GetFirst<Product, ProductCategory, ProductModel>(
                    getFirstThreeParamQuery, new { Color = "Black", ProductId = 816 }, "ProductCategoryId,ProductModelId");
                ValidateProduct816(item);
                if ( GetProvider() != Provider.SQLite )
                {
                    ValidateProductCategory21(item.ProductCategory);
                    ValidateProductModel45(item.ProductModel);
                }
            }
        }

        [Fact]
        [Trait("Category", "GetFirst")]
        public void GetFirstTwoJoinsMapped()
        {
            using ( var db = GetSqlDatabase() )
            {
                var item = db.GetFirst<Product, ProductCategory, ProductModel, Product>(
                    ( pr, pc, pm ) =>
                    {
                        pr.ProductCategory = pc;
                        pr.ProductModel = pm;
                        return pr;
                    },
                    getFirstThreeParamQuery, new { Color = "Black", ProductId = 816 }, "ProductCategoryId,ProductModelId");
                ValidateProduct816(item);
                if ( GetProvider() != Provider.SQLite )
                {
                    ValidateProductCategory21(item.ProductCategory);
                    ValidateProductModel45(item.ProductModel);
                }
            }
        }

        private string getFirstTwoParamQuery
        {
            get
            {
                return

            $@"select  P.*, P.rowguid as GuidId, PC.* 
            from Product P
            join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
            where Color = {P}Color and ProductId >= {P}ProductId order by ProductId";

            }
        }

        private string getFirstThreeParamQuery
        {
            get
            {
                return
                    $@"select  P.*, P.rowguid as GuidId, PC.*, PM.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where Color = {P}Color and ProductId >= {P}ProductId order by ProductId";
            }
        }

    }
}

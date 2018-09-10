using System;
using Dapper.Database.Extensions;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {


        [Fact]
        [Trait("Category", "Get")]
        public void GetByEntity()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new Product { ProductID = 806, GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") };
                ValidateProduct806(db.Get(p));
            }
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetByIntegerId()
        {
            using (var db = GetSqlDatabase())
            {
                ValidateProduct806(db.Get<Product>(806));
            }
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetByAliasIntegerId()
        {
            using (var db = GetSqlDatabase())
            {
                var item = db.Get<ProductAlias>(806);
                Assert.Equal(806, item.Id);
                Assert.Equal("ML Headset", item.Name);
                Assert.Equal("HS-2451", item.ProductNumber);
                Assert.Null(item.Color);
            }
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetByGuidIdWhereClause()
        {
            using (var db = GetSqlDatabase())
            {
                if (GetProvider() == Provider.SQLite)
                {
                    return;
                    //ValidateProduct806(db.Get<Product>($"where rowguid = {P}GuidId", new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
                }
                else if (GetProvider() == Provider.Firebird)
                {
                    ValidateProduct806(db.Get<Product>($"where rowguid = {P}GuidId", new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
                }
                else
                {
                    ValidateProduct806(db.Get<Product>($"WHERE rowguid = {P}GuidId", new { GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") }));
                }
            }
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetPartialBySelect()
        {
            using (var db = GetSqlDatabase())
            {
                var p = db.Get<Product>($"select ProductId, rowguid AS GuidId, Name from Product where ProductId = {P}Id", new { Id = 806 });
                Assert.NotNull(p);
                Assert.Equal(806, p.ProductID);
                Assert.Equal("ML Headset", p.Name);
                Assert.Null(p.ProductNumber);
                Assert.Equal(new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6"), p.GuidId);
            }
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetStarBySelect()
        {
            using (var db = GetSqlDatabase())
            {
                ValidateProduct806(db.Get<Product>($"select p.*, p.rowguid AS GuidId  from Product p where p.ProductId = {P}Id", new { Id = 806 }));
            }
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetShortCircuitSemiColon()
        {
            using (var db = GetSqlDatabase())
            {
                var tsql = "; select 23 AS ProductId";
                switch (GetProvider())
                {
                    case Provider.Firebird:
                        tsql += " from RDB$Database";
                        break;
                    case Provider.Oracle:
                        tsql += " from dual";
                        break;
                }
                var p = db.Get<Product>(tsql, new { });
                Assert.Equal(23, p.ProductID);
            }
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetOneJoinUnmapped()
        {
            using (var db = GetSqlDatabase())
            {
                var p = db.Get<Product, ProductCategory>(
                    getMultiTwoParamQuery, new { ProductId = 806 }, "ProductCategoryId");
                ValidateProduct806(p);
                ValidateProductCategory15(p.ProductCategory);
            }
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetOneJoinMapped()
        {
            using (var db = GetSqlDatabase())
            {
                var p = db.Get<Product, ProductCategory, Product>(
                    (pr, pc) =>
                    {
                        pr.ProductCategory = pc;
                        return pr;
                    },
                    getMultiTwoParamQuery, new { ProductId = 806 }, "ProductCategoryId");
                ValidateProduct806(p);
                ValidateProductCategory15(p.ProductCategory);
            }
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetTwoJoinsUnmapped()
        {
            using (var db = GetSqlDatabase())
            {
                var p = db.Get<Product, ProductCategory, ProductModel>(
                    getMultiThreeParamQuery, new { ProductId = 806 }, "ProductCategoryId,ProductModelId");
                ValidateProduct806(p);
                ValidateProductCategory15(p.ProductCategory);
                ValidateProductModel60(p.ProductModel);
            }
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetTwoJoinsMapped()
        {
            using (var db = GetSqlDatabase())
            {
                var p = db.Get<Product, ProductCategory, ProductModel, Product>(
                    (pr, pc, pm) =>
                    {
                        pr.ProductCategory = pc;
                        pr.ProductModel = pm;
                        return pr;
                    },
                    getMultiThreeParamQuery, new { ProductId = 806 }, "ProductCategoryId,ProductModelId");
                ValidateProduct806(p);
                ValidateProductCategory15(p.ProductCategory);
                ValidateProductModel60(p.ProductModel);
            }
        }

        private void ValidateProduct806(Product p)
        {
            Assert.NotNull(p);
            Assert.Equal(806, p.ProductID);
            Assert.Equal("ML Headset", p.Name);
            Assert.Equal("HS-2451", p.ProductNumber);
            Assert.Null(p.Color);
            Assert.Equal(45.4168m, p.StandardCost);
            Assert.Equal(102.29m, p.ListPrice);
            Assert.Null(p.Size);
            Assert.Null(p.Weight);
            Assert.Equal(15, p.ProductCategoryID);
            Assert.Equal(60, p.ProductModelID);
            Assert.Equal(new DateTime(2002, 7, 1), p.SellStartDate.Date);
            Assert.Equal(new DateTime(2003, 6, 30), p.SellEndDate.Value.Date);
            Assert.Null(p.DiscontinuedDate);
            Assert.Equal("no_image_available_small.gif", p.ThumbnailPhotoFileName);
            Assert.Equal(new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6"), p.GuidId);
            Assert.Equal(new DateTime(2004, 3, 11), p.ModifiedDate.Date);
        }

        private void ValidateProductCategory15(ProductCategory p)
        {
            Assert.NotNull(p);
            Assert.Equal(15, p.ProductCategoryID);
            //Assert.Equal(2, p.ParentProductCategoryID);
            Assert.Equal("Headsets", p.Name);
            //Assert.Equal(new Guid("7C782BBE-5A16-495A-AA50-10AFE5A84AF2"), p.GuidId);
        }

        private void ValidateProductModel60(ProductModel p)
        {
            Assert.NotNull(p);
            Assert.Equal(60, p.ProductModelID);
            Assert.Equal("ML Headset", p.Name);
            Assert.Null(p.CatalogDescription);
            Assert.Equal(new DateTime(2002, 6, 1), p.ModifiedDate.Date);
            //Assert.Equal(new Guid("6BA9F3B6-E08B-4AC2-A725-B41114C2A283"), p.GuidId);
        }

        private void ValidateProduct816(Product p)
        {
            Assert.NotNull(p);
            Assert.Equal(816, p.ProductID);
            Assert.Equal("ML Mountain Front Wheel", p.Name);
            Assert.Equal("FW-M762", p.ProductNumber);
            Assert.Equal("Black", p.Color);
            Assert.Equal(92.8071m, p.StandardCost);
            Assert.Equal(209.025m, p.ListPrice);
            Assert.Null(p.Size);
            Assert.Null(p.Weight);
            Assert.Equal(45, p.ProductModelID);
            Assert.Equal(new DateTime(2002, 7, 1), p.SellStartDate.Date);
            Assert.Equal(new DateTime(2003, 6, 30), p.SellEndDate.Value.Date);
            Assert.Null(p.DiscontinuedDate);
            Assert.Equal("wheel_small.gif", p.ThumbnailPhotoFileName);
            Assert.Equal(new Guid("5E3E5033-9A77-4DCA-8B7F-DFED78EFA08A"), p.GuidId);
            Assert.Equal(new DateTime(2004, 3, 11), p.ModifiedDate.Date);
        }

        private void ValidateProductCategory21(ProductCategory p)
        {
            Assert.NotNull(p);
            Assert.Equal(21, p.ProductCategoryID);
            //Assert.Equal(2, p.ParentProductCategoryID);
            Assert.Equal("Wheels", p.Name);
            //Assert.Equal(new Guid("7C782BBE-5A16-495A-AA50-10AFE5A84AF2"), p.GuidId);
        }

        private void ValidateProductModel45(ProductModel p)
        {
            Assert.NotNull(p);
            Assert.Equal(45, p.ProductModelID);
            Assert.Equal("ML Mountain Front Wheel", p.Name);
            Assert.Null(p.CatalogDescription);
            Assert.Equal(new DateTime(2002, 6, 1), p.ModifiedDate.Date);
            //Assert.Equal(new Guid("6BA9F3B6-E08B-4AC2-A725-B41114C2A283"), p.GuidId);
        }

        private string getMultiTwoParamQuery
        {
            get
            {
                return
                    $@"select  P.*, P.rowguid as GuidId, PC.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    where P.ProductID = {P}ProductId";
            }
        }

        private string getMultiThreeParamQuery
        {
            get
            {
                return
            $@"select  P.*, P.rowguid as GuidId, PC.*, PM.*
            from Product P
            join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
            join ProductModel PM on PM.ProductModelID = P.ProductModelID
           where P.ProductID = {P}ProductId";
            }
        }

    }
}

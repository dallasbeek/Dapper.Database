using System;
using Dapper.Database.Extensions;
using Xunit;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [ProviderFact]
        [Trait("Category", "Get")]
        public void GetByEntity()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new Product { ProductID = 806, GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") };
                ValidateProduct806(connection.Get(p));
            }
        }

        [ProviderFact]
        [Trait("Category", "Get")]
        public void GetByIntegerId()
        {
            using (var connection = GetOpenConnection())
            {
                ValidateProduct806(connection.Get<Product>(806));
            }
        }

        [ProviderFact]
        [Trait("Category", "Get")]
        public void GetByGuidIdWhereClause()
        {
            using (var connection = GetOpenConnection())
            {
                ValidateProduct806(connection.Get<Product>("WHERE rowguid = @GuidId", new { GuidId ="23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
            }
        }

        [ProviderFact]
        [Trait("Category", "Get")]
        public void GetPartialBySelect()
        {
            using (var connection = GetOpenConnection())
            {
                var p = connection.Get<Product>("select ProductId, rowguid AS GuidId, Name from Product where ProductId = @Id", new { Id = 806 });
                Assert.NotNull(p);
                Assert.Equal(806, p.ProductID);
                Assert.Equal("ML Headset", p.Name);
                Assert.Null(p.ProductNumber);
                Assert.Equal(new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6"), p.GuidId);
            }
        }

        [ProviderFact]
        [Trait("Category", "Get")]
        public void GetStarBySelect()
        {
            using (var connection = GetOpenConnection())
            {
                ValidateProduct806(connection.Get<Product>("select *, rowguid AS GuidId  from Product where ProductId = @Id", new { Id = 806 }));
            }
        }

        [ProviderFact]
        [Trait("Category", "Get")]
        public void GetShortCircuitSemiColon()
        {
            using (var connection = GetOpenConnection())
            {
                var p = connection.Get<Product>("; select 23 AS ProductId", new { });
                Assert.Equal(23, p.ProductID);
            }
        }

        [ProviderFact]
        [Trait("Category", "Get")]
        public void GetOneJoinUnmapped()
        {
            using (var connection = GetConnection())
            {
                var p = connection.Get<Product, ProductCategory>(
                    @"select  P.ProductID, P.Name, P.ProductNumber, P.Color, P.StandardCost, P.ListPrice, P.Size, 
                    P.Weight, P.ProductModelID, P.SellStartDate, P.SellEndDate, P.DiscontinuedDate, 
                    P.ThumbNailPhoto, P.ThumbnailPhotoFileName, P.rowguid as GuidId, P.ModifiedDate, PC.ProductCategoryID, 
                    PC.ParentProductCategoryID
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    where P.ProductID = @ProductId", new { ProductId = 806 });
                ValidateProduct806(p);
                ValidateProductCategory15(p.ProductCategory);
            }
        }

        [ProviderFact]
        [Trait("Category", "Get")]
        public void GetOneJoinMapped()
        {
            using (var connection = GetConnection())
            {
                var p = connection.Get<Product, ProductCategory, Product>(
                    (pr, pc) =>
                    {
                        pr.ProductCategory = pc;
                        return pr;
                    },
                    @"select  P.ProductID, P.Name, P.ProductNumber, P.Color, P.StandardCost, P.ListPrice, P.Size, 
                    P.Weight, P.ProductModelID, P.SellStartDate, P.SellEndDate, P.DiscontinuedDate, 
                    P.ThumbNailPhoto, P.ThumbnailPhotoFileName, P.rowguid as GuidId, P.ModifiedDate, PC.ProductCategoryID, 
                    PC.ParentProductCategoryID
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    where P.ProductID = @ProductId", new {ProductId = 806 });
                ValidateProduct806(p);
                ValidateProductCategory15(p.ProductCategory);
            }
        }

        [ProviderFact]
        [Trait("Category", "Get")]
        public void GetTwoJoinsUnmapped()
        {
            using (var connection = GetConnection())
            {
                var p = connection.Get<Product, ProductCategory, ProductModel>(
                    @"select  P.ProductID, P.Name, P.ProductNumber, P.Color, P.StandardCost, P.ListPrice, P.Size, 
                    P.Weight, P.ProductModelID, P.SellStartDate, P.SellEndDate, P.DiscontinuedDate, 
                    P.ThumbNailPhoto, P.ThumbnailPhotoFileName, P.rowguid as GuidId, P.ModifiedDate, PC.ProductCategoryID, 
                    PC.ParentProductCategoryID, PM.ProductModelID, PM.CatalogDescription
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where P.ProductID = @ProductId", new { ProductId = 806 });
                ValidateProduct806(p);
                ValidateProductCategory15(p.ProductCategory);
                ValidateProductModel60(p.ProductModel);
            }
        }

        [ProviderFact]
        [Trait("Category", "Get")]
        public void GetTwoJoinsMapped()
        {
            using (var connection = GetConnection())
            {
                var p = connection.Get<Product, ProductCategory, ProductModel, Product>(
                    (pr, pc, pm) =>
                    {
                        pr.ProductCategory = pc;
                        pr.ProductModel = pm;
                        return pr;
                    },
                    @"select  P.ProductID, P.Name, P.ProductNumber, P.Color, P.StandardCost, P.ListPrice, P.Size, 
                    P.Weight, P.ProductModelID, P.SellStartDate, P.SellEndDate, P.DiscontinuedDate, 
                    P.ThumbNailPhoto, P.ThumbnailPhotoFileName, P.rowguid as GuidId, P.ModifiedDate, PC.ProductCategoryID, 
                    PC.ParentProductCategoryID, PM.ProductModelID, PM.CatalogDescription
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where P.ProductID = @ProductId", new { ProductId = 806 });
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
            //Assert.Equal(15, p.ProductCategoryID);
            //Assert.Equal(60, p.ProductModelID);
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
            //Assert.Equal("Headsets", p.Name);
            //Assert.Equal(new Guid("7C782BBE-5A16-495A-AA50-10AFE5A84AF2"), p.GuidId);
        }

        private void ValidateProductModel60(ProductModel p)
        {
            Assert.NotNull(p);
            Assert.Equal(60, p.ProductModelID);
            //Assert.Equal("ML Headset", p.Name);
            Assert.Null(p.CatalogDescription);
            //Assert.Equal(new DateTime(2002,6,1), p.ModifiedDate.Date);
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
            //Assert.Equal(45, p.ProductModelID);
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
            //Assert.Equal("Wheels", p.Name);
            //Assert.Equal(new Guid("7C782BBE-5A16-495A-AA50-10AFE5A84AF2"), p.GuidId);
        }

        private void ValidateProductModel45(ProductModel p)
        {
            Assert.NotNull(p);
            Assert.Equal(45, p.ProductModelID);
            //Assert.Equal("ML Mountain Front Wheel", p.Name);
            Assert.Null(p.CatalogDescription);
            //Assert.Equal(new DateTime(2002, 6, 1), p.ModifiedDate.Date);
            //Assert.Equal(new Guid("6BA9F3B6-E08B-4AC2-A725-B41114C2A283"), p.GuidId);
        }

    }
}

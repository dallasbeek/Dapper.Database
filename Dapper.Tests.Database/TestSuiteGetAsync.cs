using System;
using System.Threading.Tasks;
using Dapper.Database.Extensions;
using Xunit;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [ProviderFact]
        [Trait("Category", "GetAsync")]
        public async Task GetByEntityAsync()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new Product { ProductID = 806, GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") };
                ValidateProduct806(await connection.GetAsync(p));
            }
        }

        [ProviderFact]
        [Trait("Category", "GetAsync")]
        public async Task GetByIntegerIdAsync()
        {
            using (var connection = GetOpenConnection())
            {
                ValidateProduct806(await connection.GetAsync<Product>(806));
            }
        }

        [ProviderFact]
        [Trait("Category", "GetAsync")]
        public async Task GetByGuidIdWhereClauseAsync()
        {
            using (var connection = GetOpenConnection())
            {
                ValidateProduct806(await connection.GetAsync<Product>("WHERE rowguid = @GuidId", new { GuidId ="23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
            }
        }

        [ProviderFact]
        [Trait("Category", "GetAsync")]
        public async Task GetPartialBySelectAsync()
        {
            using (var connection = GetOpenConnection())
            {
                var p = await connection.GetAsync<Product>("select ProductId, rowguid AS GuidId, Name from Product where ProductId = @Id", new { Id = 806 });
                Assert.NotNull(p);
                Assert.Equal(806, p.ProductID);
                Assert.Equal("ML Headset", p.Name);
                Assert.Null(p.ProductNumber);
                Assert.Equal(new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6"), p.GuidId);
            }
        }

        [ProviderFact]
        [Trait("Category", "GetAsync")]
        public async Task GetStarBySelectAsync()
        {
            using (var connection = GetOpenConnection())
            {
                ValidateProduct806(await connection.GetAsync<Product>("select *, rowguid AS GuidId  from Product where ProductId = @Id", new { Id = 806 }));
            }
        }

        [ProviderFact]
        [Trait("Category", "GetAsync")]
        public async Task GetShortCircuitSemiColonAsync()
        {
            using (var connection = GetOpenConnection())
            {
                var p = await connection.GetAsync<Product>("; select 23 AS ProductId", new { });
                Assert.Equal(23, p.ProductID);
            }
        }

        [ProviderFact]
        [Trait("Category", "GetAsync")]
        public async Task GetOneJoinUnmappedAsync()
        {
            using (var connection = GetConnection())
            {
                var p = await connection.GetAsync<Product, ProductCategory>(
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
        [Trait("Category", "GetAsync")]
        public async Task GetOneJoinMappedAsync()
        {
            using (var connection = GetConnection())
            {
                var p = await connection.GetAsync<Product, ProductCategory, Product>(
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
        [Trait("Category", "GetAsync")]
        public async Task GetTwoJoinsUnmappedAsync()
        {
            using (var connection = GetConnection())
            {
                var p = await connection.GetAsync<Product, ProductCategory, ProductModel>(
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
        [Trait("Category", "GetAsync")]
        public async Task GetTwoJoinsMappedAsync()
        {
            using (var connection = GetConnection())
            {
                var p = await connection.GetAsync<Product, ProductCategory, ProductModel, Product>(
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

    }
}

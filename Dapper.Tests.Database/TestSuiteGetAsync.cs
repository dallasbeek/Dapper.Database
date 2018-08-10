using System;
using System.Threading.Tasks;
using Dapper.Database.Extensions;
using Xunit;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "GetAsync")]
        public async Task GetByEntityAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = new Product { ProductID = 806, GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") };
                ValidateProduct806(await connection.GetAsync(p));
            }
        }

        [Fact]
        [Trait("Category", "GetAsync")]
        public async Task GetByIntegerIdAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                ValidateProduct806(await connection.GetAsync<Product>(806));
            }
        }

        [Fact]
        [Trait("Category", "GetAsync")]
        public async Task GetByGuidIdWhereClauseAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                if (Provider == Provider.SQLite)
                {
                    ValidateProduct806(await connection.GetAsync<Product>("WHERE rowguid = @GuidId", new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
                }
                else
                {
                    ValidateProduct806(await connection.GetAsync<Product>("WHERE rowguid = @GuidId", new { GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") }));
                }
            }
        }

        [Fact]
        [Trait("Category", "GetAsync")]
        public async Task GetPartialBySelectAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = await connection.GetAsync<Product>("select ProductId, rowguid AS GuidId, Name from Product where ProductId = @Id", new { Id = 806 });
                Assert.NotNull(p);
                Assert.Equal(806, p.ProductID);
                Assert.Equal("ML Headset", p.Name);
                Assert.Null(p.ProductNumber);
                Assert.Equal(new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6"), p.GuidId);
            }
        }

        [Fact]
        [Trait("Category", "GetAsync")]
        public async Task GetStarBySelectAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                ValidateProduct806(await connection.GetAsync<Product>("select *, rowguid AS GuidId  from Product where ProductId = @Id", new { Id = 806 }));
            }
        }

        [Fact]
        [Trait("Category", "GetAsync")]
        public async Task GetShortCircuitSemiColonAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = await connection.GetAsync<Product>("; select 23 AS ProductId", new { });
                Assert.Equal(23, p.ProductID);
            }
        }

        [Fact]
        [Trait("Category", "GetAsync")]
        public async Task GetOneJoinUnmappedAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var p = await db.GetAsync<Product, ProductCategory>(
                    getMultiTwoParamQuery, new { ProductId = 806 }, "ProductCategoryId");
                ValidateProduct806(p);
                ValidateProductCategory15(p.ProductCategory);
            }
        }

        [Fact]
        [Trait("Category", "GetAsync")]
        public async Task GetOneJoinMappedAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var p = await db.GetAsync<Product, ProductCategory, Product>(
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
        [Trait("Category", "GetAsync")]
        public async Task GetTwoJoinsUnmappedAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var p = await db.GetAsync<Product, ProductCategory, ProductModel>(
                    getMultiThreeParamQuery, new { ProductId = 806 }, "ProductCategoryId,ProductModelId");
                ValidateProduct806(p);
                ValidateProductCategory15(p.ProductCategory);
                ValidateProductModel60(p.ProductModel);
            }
        }

        [Fact]
        [Trait("Category", "Get")]
        public async Task GetTwoJoinsMappedAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var p = await db.GetAsync<Product, ProductCategory, ProductModel, Product>(
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

    }
}

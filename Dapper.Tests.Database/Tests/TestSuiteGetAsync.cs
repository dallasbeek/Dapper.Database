using System;
using System.Threading.Tasks;
using Dapper.Database.Extensions;
using Xunit;
using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;


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
        public async Task GetByAliasIntegerIdAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var item = await connection.GetAsync<ProductAlias>(806);
                Assert.Equal(806, item.Id);
                Assert.Equal("ML Headset", item.Name);
                Assert.Equal("HS-2451", item.ProductNumber);
                Assert.Null(item.Color);

            }
        }

        [Fact]
        [Trait("Category", "GetAsync")]
        public async Task GetByGuidIdWhereClauseAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                if (GetProvider() == Provider.SQLite)
                {
                    return;
                    //ValidateProduct806(await connection.GetAsync<Product>($"where rowguid = {P}GuidId", new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
                }
                else if ( GetProvider() == Provider.Firebird )
                {
                    ValidateProduct806(await connection.GetAsync<Product>($"where rowguid = {P}GuidId", new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
                }
                else

                {
                    ValidateProduct806(await connection.GetAsync<Product>($"WHERE rowguid = {P}GuidId", new { GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") }));
                }
            }
        }

        [Fact]
        [Trait("Category", "GetAsync")]
        public async Task GetPartialBySelectAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = await connection.GetAsync<Product>($"select p.ProductId, p.rowguid AS GuidId, p.Name from Product p where p.ProductId = {P}Id", new { Id = 806 });
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
                ValidateProduct806(await connection.GetAsync<Product>($"select p.*, p.rowguid AS GuidId  from Product p where p.ProductId = {P}Id", new { Id = 806 }));
            }
        }

        [Fact]
        [Trait("Category", "GetAsync")]
        public async Task GetShortCircuitSemiColonAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var tsql = "; select 23 AS ProductId";
                switch ( GetProvider() )
                {
                    case Provider.Firebird:
                        tsql += " from RDB$Database";
                        break;
                    case Provider.Oracle:
                        tsql += " from dual";
                        break;
                }
                var p = await connection.GetAsync<Product>(tsql, new { });
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

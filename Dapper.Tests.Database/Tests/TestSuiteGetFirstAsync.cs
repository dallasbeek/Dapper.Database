using System;
using Dapper.Database.Extensions;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {


        [Fact]
        [Trait("Category", "GetFirstAsync")]
        public async Task GetFirstAsyncWithWhereClause()
        {
            using (var db = GetSqlDatabase())
            {
                var item = await db.GetFirstAsync<Product>("where Color = 'Black' and ProductId >= 816 order by ProductId");
                ValidateProduct816(item);
            }
        }


        [Fact]
        [Trait("Category", "GetFirstAsync")]
        public async Task GetFirstAsyncWithWhereClauseParameter()
        {
            using (var db = GetSqlDatabase())
            {
                var item = await db.GetFirstAsync<Product>($"where Color = {P}Color and ProductId >= {P}ProductId order by ProductId", new { Color = "Black", ProductId = 816 });
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetFirstAsync")]
        public async Task GetFirstAsyncWithSelectClause()
        {
            using (var db = GetSqlDatabase())
            {
                var item = await db.GetFirstAsync<Product>("select p.*, p.rowguid as GuidId from Product p where p.Color = 'Black' and p.ProductId >= 816 order by p.ProductId");
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetFirstAsync")]
        public async Task GetFirstAsyncWithSelectClauseParameter()
        {
            using (var db = GetSqlDatabase())
            {
                var item = await db.GetFirstAsync<Product>($"select p.*, p.rowguid as GuidId from Product p where p.Color = {P}Color and p.ProductId >= {P}ProductId order by p.ProductId", new { Color = "Black", ProductId = 816 });
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetFirstAsync")]
        public async Task GetFirstAsyncShortCircuit()
        {
            using (var db = GetSqlDatabase())
            {
                var item = await db.GetFirstAsync<Product>($";select p.*, p.rowguid as GuidId from Product p where p.Color = {P}Color and p.ProductId >= {P}ProductId order by p.ProductId", new { Color = "Black", ProductId = 816 });
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetFirstAsync")]
        public async Task GetFirstAsyncPartialBySelect()
        {
            using (var db = GetSqlDatabase())
            {
                var item = await db.GetFirstAsync<Product>($"select p.ProductId, p.rowguid AS GuidId, Name from Product p where p.Color = {P}Color and p.ProductId >= {P}ProductId order by p.ProductId", new { Color = "Black", ProductId = 816 });
                Assert.Equal(816, item.ProductID);
                Assert.Equal("ML Mountain Front Wheel", item.Name);
                Assert.Null(item.ProductNumber);
                Assert.Equal(new Guid("5E3E5033-9A77-4DCA-8B7F-DFED78EFA08A"), item.GuidId);
            }
        }

        [Fact]
        [Trait("Category", "GetFirstAsync")]
        public async Task GetFirstAsyncOneJoinUnmapped()
        {
            using (var db = GetSqlDatabase())
            {
                var item = await db.GetFirstAsync<Product, ProductCategory>(
                    getFirstTwoParamQuery, new { Color = "Black", ProductId = 816 }, "ProductCategoryId");
                ValidateProduct816(item);
                if (GetProvider() != Provider.SQLite)
                {
                    ValidateProductCategory21(item.ProductCategory);
                }
            }
        }

        [Fact]
        [Trait("Category", "GetFirstAsync")]
        public async Task GetFirstAsyncOneJoinMapped()
        {
            using (var db = GetSqlDatabase())
            {
                var item = await db.GetFirstAsync<Product, ProductCategory, Product>(
                    (pr, pc) =>
                    {
                        pr.ProductCategory = pc;
                        return pr;
                    },
                    getFirstTwoParamQuery, new { Color = "Black", ProductId = 816 }, "ProductCategoryId");
                ValidateProduct816(item);
                if (GetProvider() != Provider.SQLite)
                {
                    ValidateProductCategory21(item.ProductCategory);
                }
            }
        }

        [Fact]
        [Trait("Category", "GetFirstAsync")]
        public async Task GetFirstAsyncTwoJoinsUnmapped()
        {
            using (var db = GetSqlDatabase())
            {
                var item = await db.GetFirstAsync<Product, ProductCategory, ProductModel>(
                    getFirstThreeParamQuery, new { Color = "Black", ProductId = 816 }, "ProductCategoryId,ProductModelId");
                ValidateProduct816(item);
                if (GetProvider() != Provider.SQLite)
                {
                    ValidateProductCategory21(item.ProductCategory);
                    ValidateProductModel45(item.ProductModel);
                }
            }
        }

        [Fact]
        [Trait("Category", "GetFirstAsync")]
        public async Task GetFirstAsyncTwoJoinsMapped()
        {
            using (var db = GetSqlDatabase())
            {
                var item = await db.GetFirstAsync<Product, ProductCategory, ProductModel, Product>(
                    (pr, pc, pm) =>
                    {
                        pr.ProductCategory = pc;
                        pr.ProductModel = pm;
                        return pr;
                    },
                    getFirstThreeParamQuery, new { Color = "Black", ProductId = 816 }, "ProductCategoryId,ProductModelId");
                ValidateProduct816(item);
                if (GetProvider() != Provider.SQLite)
                {
                    ValidateProductCategory21(item.ProductCategory);
                    ValidateProductModel45(item.ProductModel);
                }
            }
        }

    }
}

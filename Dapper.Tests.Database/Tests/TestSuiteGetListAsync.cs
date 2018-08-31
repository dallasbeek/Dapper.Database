using System;
using Dapper.Database.Extensions;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "GetListAsync")]
        public async Task GetListAllAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var lst = await connection.GetListAsync<Product>();
                Assert.Equal(295, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }


        [Fact]
        [Trait("Category", "GetListAsync")]
        public async Task GetListWithWhereClauseAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var lst = await connection.GetListAsync<Product>("where Color = 'Black'");
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }


        [Fact]
        [Trait("Category", "GetListAsync")]
        public async Task GetListWithWhereClauseParameterAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var lst = await connection.GetListAsync<Product>($"where Color = {P}Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetListAsync")]
        public async Task GetListWithSelectClauseAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var lst = await connection.GetListAsync<Product>("select p.*, p.rowguid as GuidId from Product p where p.Color = 'Black'");
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetListAsync")]
        public async Task GetListWithSelectClauseParameterAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var lst = await connection.GetListAsync<Product>($"select p.*, p.rowguid as GuidId from Product p where p.Color = {P}Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetListAsync")]
        public async Task GetListShortCircuitAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var lst = await connection.GetListAsync<Product>($";select p.*, p.rowguid as GuidId from Product p where p.Color = {P}Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetListAsync")]
        public async Task GetListPartialBySelectAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var lst = await connection.GetListAsync<Product>($"select ProductId, rowguid AS GuidId, Name from Product where Color = {P}Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var p = lst.Single(a => a.ProductID == 816);
                Assert.Equal(816, p.ProductID);
                Assert.Equal("ML Mountain Front Wheel", p.Name);
                Assert.Null(p.ProductNumber);
                Assert.Equal(new Guid("5E3E5033-9A77-4DCA-8B7F-DFED78EFA08A"), p.GuidId);
            }
        }

        [Fact]
        [Trait("Category", "GetListAsync")]
        public async Task GetListOneJoinUnmappedAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = await db.GetListAsync<Product, ProductCategory>(
                    getListMultiTwoParamQuery, new { Color = "Black" }, "ProductCategoryId");
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);

                //There must be a bug with mapping on SqlLite when fetching many records
                if (GetProvider() != Provider.SQLite)
                {
                    ValidateProductCategory21(item.ProductCategory);
                }
            }
        }

        [Fact]
        [Trait("Category", "GetListAsync")]
        public async Task GetListOneJoinMappedAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = await db.GetListAsync<Product, ProductCategory, Product>(
                    (pr, pc) =>
                    {
                        pr.ProductCategory = pc;
                        return pr;
                    },
                   getListMultiTwoParamQuery, new { Color = "Black" }, "ProductCategoryId");
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);

                //There must be a bug with mapping on SqlLite when fetching many records
                if (GetProvider() != Provider.SQLite)
                {
                    ValidateProductCategory21(item.ProductCategory);
                }
            }
        }

        [Fact]
        [Trait("Category", "GetListAsync")]
        public async Task GetListTwoJoinsUnmappedAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = await db.GetListAsync<Product, ProductCategory, ProductModel>(
                    getListMultiThreeParamQuery, new { Color = "Black" }, "ProductCategoryId,ProductModelId");
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);

                //There must be a bug with mapping on SqlLite when fetching many records
                if (GetProvider() != Provider.SQLite)
                {
                    ValidateProductCategory21(item.ProductCategory);
                    ValidateProductModel45(item.ProductModel);
                }
            }
        }

        [Fact]
        [Trait("Category", "GetListAsync")]
        public async Task GetListTwoJoinsMappedAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = await db.GetListAsync<Product, ProductCategory, ProductModel, Product>(
                    (pr, pc, pm) =>
                    {
                        pr.ProductCategory = pc;
                        pr.ProductModel = pm;
                        return pr;
                    },
                    getListMultiThreeParamQuery, new { Color = "Black" }, "ProductCategoryId,ProductModelId");
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);

                //There must be a bug with mapping on SqlLite when fetching many records
                if (GetProvider() != Provider.SQLite)
                {
                    ValidateProductCategory21(item.ProductCategory);
                    ValidateProductModel45(item.ProductModel);
                }
            }
        }

    }
}

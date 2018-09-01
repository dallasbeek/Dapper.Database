using System;

using Dapper.Database.Extensions;
using Xunit;
using System.Threading.Tasks;
using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsNoArgsAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                Assert.True(await connection.ExistsAsync<Product>());
            }
        }


        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsByEntityAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = new Product { ProductID = 806, GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") };
                Assert.True(await connection.ExistsAsync(p));

                p.ProductID = -1;
                Assert.False(await connection.ExistsAsync(p));
            }
        }

        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsByIntegerIdAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                Assert.True(await connection.ExistsAsync<Product>(806));
                Assert.False(await connection.ExistsAsync<Product>(-1));
            }
        }

        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsByAliasIntegerIdAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                Assert.True(await connection.ExistsAsync<ProductAlias>(806));
                Assert.False(await connection.ExistsAsync<ProductAlias>(-1));
            }
        }

        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsByGuidIdWhereClauseAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                if (GetProvider() == Provider.SQLite)
                {
                    return;
                    //Assert.True(await connection.ExistsAsync<Product>($"where rowguid = {P}GuidId", new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
                    //Assert.False(await connection.ExistsAsync<Product>($"where rowguid = {P}GuidId", new { GuidId = "1115D52B-8C29-4059-B899-75C53B5EE2E6" }));
                }
                else if (GetProvider() == Provider.Firebird)
                {
                    Assert.True(await connection.ExistsAsync<Product>($"where rowguid = {P}GuidId", new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
                    Assert.False(await connection.ExistsAsync<Product>($"where rowguid = {P}GuidId", new { GuidId = "1115D52B-8C29-4059-B899-75C53B5EE2E6" }));
                }
                else
                {
                    Assert.True(await connection.ExistsAsync<Product>($"where rowguid = {P}GuidId", new { GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") }));
                    Assert.False(await connection.ExistsAsync<Product>($"where rowguid = {P}GuidId", new { GuidId = new Guid("1115D52B-8C29-4059-B899-75C53B5EE2E6") }));

                }
            }
        }

        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsPartialBySelectAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                Assert.True(await connection.ExistsAsync<Product>($"select p.ProductId, p.rowguid AS GuidId, p.Name from Product p where p.ProductId = {P}Id", new { Id = 806 }));
                Assert.False(await connection.ExistsAsync<Product>($"select p.ProductId, p.rowguid AS GuidId, p.Name from Product p where p.ProductId = {P}Id", new { Id = -1 }));
            }
        }

        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsBySelectAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                Assert.True(await connection.ExistsAsync<Product>($"select p.*, p.rowguid AS GuidId  from Product p where p.ProductId = {P}Id", new { Id = 806 }));
                Assert.False(await connection.ExistsAsync<Product>($"select p.*, p.rowguid AS GuidId  from Product p where p.ProductId = {P}Id", new { Id = -1 }));
            }
        }

        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsShortCircuitSemiColonAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var tsql = "; select 1 AS ProductId";
                var fsql = "; select 0 AS ProductId";
                switch (GetProvider())
                {
                    case Provider.Firebird:
                        tsql += " from RDB$Database";
                        fsql += " from RDB$Database";
                        break;
                    case Provider.Oracle:
                        tsql += " from dual";
                        fsql += " from dual";
                        break;
                }

                Assert.True(await connection.ExistsAsync<Product>(tsql));
                Assert.False(await connection.ExistsAsync<Product>(fsql));
            }
        }

    }
}

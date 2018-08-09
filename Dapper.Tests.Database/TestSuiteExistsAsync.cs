using System;

using Dapper.Database.Extensions;
using Xunit;
using System.Threading.Tasks;

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
        public async Task ExistsByGuidIdWhereClauseAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                if (Provider == Provider.SQLite)
                {
                    Assert.True(await connection.ExistsAsync<Product>("where rowguid = @GuidId", new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
                    Assert.False(await connection.ExistsAsync<Product>("where rowguid = @GuidId", new { GuidId = "1115D52B-8C29-4059-B899-75C53B5EE2E6" }));
                }
                else
                {
                    Assert.True(await connection.ExistsAsync<Product>("where rowguid = @GuidId", new { GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") }));
                    Assert.False(await connection.ExistsAsync<Product>("where rowguid = @GuidId", new { GuidId = new Guid("1115D52B-8C29-4059-B899-75C53B5EE2E6") }));

                }
            }
        }

        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsPartialBySelectAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                Assert.True(await connection.ExistsAsync<Product>("select ProductId, rowguid AS GuidId, Name from Product where ProductId = @Id", new { Id = 806 }));
                Assert.False(await connection.ExistsAsync<Product>("select ProductId, rowguid AS GuidId, Name from Product where ProductId = @Id", new { Id = -1 }));
            }
        }

        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsBySelectAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                Assert.True(await connection.ExistsAsync<Product>("select *, rowguid AS GuidId  from Product where ProductId = @Id", new { Id = 806 }));
                Assert.False(await connection.ExistsAsync<Product>("select *, rowguid AS GuidId  from Product where ProductId = @Id", new { Id = -1 }));
            }
        }

        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsShortCircuitSemiColonAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                Assert.True(await connection.ExistsAsync<Product>("; select 1 AS ProductId"));
                Assert.False(await connection.ExistsAsync<Product>("; select 0 AS ProductId"));
            }
        }

    }
}

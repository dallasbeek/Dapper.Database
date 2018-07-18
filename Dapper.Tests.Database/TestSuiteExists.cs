using System;
using Dapper.Database.Extensions;
using Xunit;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        [Fact]
        [Trait("Category", "Exists")]
        public void ExistsNoArgs()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.True(db.Exists<Product>());
            }
        }


        [Fact]
        [Trait("Category", "Exists")]
        public void ExistsByEntity()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new Product { ProductID = 806, GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") };
                Assert.True(db.Exists(p));

                p.ProductID = -1;
                Assert.False(db.Exists(p));
            }
        }

        [Fact]
        [Trait("Category", "Exists")]
        public void ExistsByIntegerId()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.True(db.Exists<Product>(806));
                Assert.False(db.Exists<Product>(-1));
            }
        }

        [Fact]
        [Trait("Category", "Exists")]
        public void ExistsByGuidIdWhereClause()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.True(db.Exists<Product>("where rowguid = @GuidId", new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
                Assert.False(db.Exists<Product>("where rowguid = @GuidId", new { GuidId = "1115D52B-8C29-4059-B899-75C53B5EE2E6" }));
            }
        }

        [Fact]
        [Trait("Category", "Exists")]
        public void ExistsPartialBySelect()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.True(db.Exists<Product>("select ProductId, rowguid AS GuidId, Name from Product where ProductId = @Id", new { Id = 806 }));
                Assert.False(db.Exists<Product>("select ProductId, rowguid AS GuidId, Name from Product where ProductId = @Id", new { Id = -1 }));
            }
        }

        [Fact]
        [Trait("Category", "Exists")]
        public void ExistsBySelect()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.True(db.Exists<Product>("select *, rowguid AS GuidId  from Product where ProductId = @Id", new { Id = 806 }));
                Assert.False(db.Exists<Product>("select *, rowguid AS GuidId  from Product where ProductId = @Id", new { Id = -1 }));
            }
        }

        [Fact]
        [Trait("Category", "Exists")]
        public void ExistsShortCircuitSemiColon()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.True( db.Exists<Product>("; select 1 AS ProductId"));
                Assert.False(db.Exists<Product>("; select 0 AS ProductId"));
            }
        }

    }
}

using System;
using Dapper.Database.Extensions;
using Xunit;
using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;

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
        public void ExistsByAliasIntegerId()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.True(db.Exists<ProductAlias>(806));
                Assert.False(db.Exists<ProductAlias>(-1));
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

                if (GetProvider() == Provider.SQLite)
                {
                    return;
                    //Assert.True(db.Exists<Product>($"where rowguid = {P}GuidId", new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
                    //Assert.False(db.Exists<Product>($"where rowguid = {P}GuidId", new { GuidId = "1115D52B-8C29-4059-B899-75C53B5EE2E6" }));
                }
                else if (GetProvider() == Provider.Firebird)
                {
                    Assert.True(db.Exists<Product>($"where rowguid = {P}GuidId", new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
                    Assert.False(db.Exists<Product>($"where rowguid = {P}GuidId", new { GuidId = "1115D52B-8C29-4059-B899-75C53B5EE2E6" }));
                }
                else
                {
                    Assert.True(db.Exists<Product>($"where rowguid = {P}GuidId", new { GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") }));
                    Assert.False(db.Exists<Product>($"where rowguid = {P}GuidId", new { GuidId = new Guid("1115D52B-8C29-4059-B899-75C53B5EE2E6") }));
                }
            }
        }

        [Fact]
        [Trait("Category", "Exists")]
        public void ExistsPartialBySelect()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.True(db.Exists<Product>($"select p.ProductId, p.rowguid AS GuidId, Name from Product p where p.ProductId = {P}Id", new { Id = 806 }));
                Assert.False(db.Exists<Product>($"select p.ProductId, p.rowguid AS GuidId, Name from Product p where p.ProductId = {P}Id", new { Id = -1 }));
            }
        }

        [Fact]
        [Trait("Category", "Exists")]
        public void ExistsBySelect()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.True(db.Exists<Product>($"select p.*, p.rowguid AS GuidId  from Product p where p.ProductId = {P}Id", new { Id = 806 }));
                Assert.False(db.Exists<Product>($"select p.*, p.rowguid AS GuidId  from Product p where p.ProductId = {P}Id", new { Id = -1 }));
            }
        }

        [Fact]
        [Trait("Category", "Exists")]
        public void ExistsShortCircuitSemiColon()
        {
            using (var db = GetSqlDatabase())
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
                Assert.True(db.Exists<Product>(tsql));
                Assert.False(db.Exists<Product>(fsql));
            }
        }

    }
}

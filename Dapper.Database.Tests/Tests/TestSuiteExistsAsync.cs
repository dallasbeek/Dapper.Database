using System;
using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "ExistsAsync")]
    public async Task ExistsNoArgsAsync()
    {
        using var db = GetSqlDatabase();
        Assert.True(await db.ExistsAsync<Product>());
    }

    [Fact]
    [Trait("Category", "ExistsAsync")]
    public async Task ExistsByEntityAsync()
    {
        using var db = GetSqlDatabase();
        var p = new Product { ProductID = 806, GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") };
        Assert.True(await db.ExistsAsync(p));

        p.ProductID = -1;
        Assert.False(await db.ExistsAsync(p));
    }

    [Fact]
    [Trait("Category", "ExistsAsync")]
    public async Task ExistsByIntegerIdAsync()
    {
        using var db = GetSqlDatabase();
        Assert.True(await db.ExistsAsync<Product>(806));
        Assert.False(await db.ExistsAsync<Product>(-1));
    }

    [Fact]
    [Trait("Category", "ExistsAsync")]
    public async Task ExistsByAliasIntegerIdAsync()
    {
        using var db = GetSqlDatabase();
        Assert.True(await db.ExistsAsync<ProductAlias>(806));
        Assert.False(await db.ExistsAsync<ProductAlias>(-1));
    }

    [Fact]
    [Trait("Category", "ExistsAsync")]
    public async Task ExistsByGuidIdWhereClauseAsync()
    {
        using var db = GetSqlDatabase();
        if (GetProvider() == Provider.Firebird || GetProvider() == Provider.SQLite)
        {
            Assert.True(await db.ExistsAsync<Product>($"where rowguid = {P}GuidId",
                new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
            Assert.False(await db.ExistsAsync<Product>($"where rowguid = {P}GuidId",
                new { GuidId = "1115D52B-8C29-4059-B899-75C53B5EE2E6" }));
        }
        else
        {
            Assert.True(await db.ExistsAsync<Product>($"where rowguid = {P}GuidId",
                new { GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") }));
            Assert.False(await db.ExistsAsync<Product>($"where rowguid = {P}GuidId",
                new { GuidId = new Guid("1115D52B-8C29-4059-B899-75C53B5EE2E6") }));
        }
    }

    [Fact]
    [Trait("Category", "ExistsAsync")]
    public async Task ExistsPartialBySelectAsync()
    {
        using var db = GetSqlDatabase();
        Assert.True(await db.ExistsAsync<Product>(
            $"select p.ProductId, p.rowguid AS GuidId, p.Name from Product p where p.ProductId = {P}Id",
            new { Id = 806 }));
        Assert.False(await db.ExistsAsync<Product>(
            $"select p.ProductId, p.rowguid AS GuidId, p.Name from Product p where p.ProductId = {P}Id",
            new { Id = -1 }));
    }

    [Fact]
    [Trait("Category", "ExistsAsync")]
    public async Task ExistsBySelectAsync()
    {
        using var db = GetSqlDatabase();
        Assert.True(await db.ExistsAsync<Product>(
            $"select p.*, p.rowguid AS GuidId  from Product p where p.ProductId = {P}Id", new { Id = 806 }));
        Assert.False(await db.ExistsAsync<Product>(
            $"select p.*, p.rowguid AS GuidId  from Product p where p.ProductId = {P}Id", new { Id = -1 }));
    }

    [Fact]
    [Trait("Category", "ExistsAsync")]
    public async Task ExistsShortCircuitSemiColonAsync()
    {
        using var db = GetSqlDatabase();
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

        Assert.True(await db.ExistsAsync<Product>(tsql));
        Assert.False(await db.ExistsAsync<Product>(fsql));
    }
}

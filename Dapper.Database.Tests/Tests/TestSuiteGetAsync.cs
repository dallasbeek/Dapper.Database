using System;
using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "GetAsync")]
    public async Task GetByEntityAsync()
    {
        using var db = GetSqlDatabase();
        var p = new Product { ProductID = 806, GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") };
        ValidateProduct806(await db.GetAsync(p));
    }

    [Fact]
    [Trait("Category", "GetAsync")]
    public async Task GetByIntegerIdAsync()
    {
        using var db = GetSqlDatabase();
        ValidateProduct806(await db.GetAsync<Product>(806));
    }

    [Fact]
    [Trait("Category", "GetAsync")]
    public async Task GetByAliasIntegerIdAsync()
    {
        using var db = GetSqlDatabase();
        var item = await db.GetAsync<ProductAlias>(806);
        Assert.Equal(806, item.Id);
        Assert.Equal("ML Headset", item.Name);
        Assert.Equal("HS-2451", item.ProductNumber);
        Assert.Null(item.Color);
    }

    [Fact]
    [Trait("Category", "GetAsync")]
    public async Task GetByGuidIdWhereClauseAsync()
    {
        using var db = GetSqlDatabase();
        //if (GetProvider() == Provider.SQLite)
        //{
        //    return;
        //}
        //else 
        if (GetProvider() == Provider.Firebird || GetProvider() == Provider.SQLite)
            ValidateProduct806(await db.GetAsync<Product>($"where rowguid = {P}GuidId",
                new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
        else
            ValidateProduct806(await db.GetAsync<Product>($"WHERE rowguid = {P}GuidId",
                new { GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") }));
    }

    [Fact]
    [Trait("Category", "GetAsync")]
    public async Task GetPartialBySelectAsync()
    {
        using var db = GetSqlDatabase();
        var p = await db.GetAsync<Product>(
            $"select p.ProductId, p.rowguid AS GuidId, p.Name from Product p where p.ProductId = {P}Id",
            new { Id = 806 });
        Assert.NotNull(p);
        Assert.Equal(806, p.ProductID);
        Assert.Equal("ML Headset", p.Name);
        Assert.Null(p.ProductNumber);
        Assert.Equal(new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6"), p.GuidId);
    }

    [Fact]
    [Trait("Category", "GetAsync")]
    public async Task GetStarBySelectAsync()
    {
        using var db = GetSqlDatabase();
        ValidateProduct806(await db.GetAsync<Product>(
            $"select p.*, p.rowguid AS GuidId  from Product p where p.ProductId = {P}Id", new { Id = 806 }));
    }

    [Fact]
    [Trait("Category", "GetAsync")]
    public async Task GetShortCircuitSemiColonAsync()
    {
        using var db = GetSqlDatabase();
        var tsql = "; select 23 AS ProductId";
        switch (GetProvider())
        {
            case Provider.Firebird:
                tsql += " from RDB$Database";
                break;
            case Provider.Oracle:
                tsql += " from dual";
                break;
        }

        var p = await db.GetAsync<Product>(tsql, new { });
        Assert.Equal(23, p.ProductID);
    }

    [Fact]
    [Trait("Category", "GetAsync")]
    public async Task GetOneJoinUnmappedAsync()
    {
        using var db = GetSqlDatabase();
        var p = await db.GetAsync<Product, ProductCategory>(
            GetMultiTwoParamQuery, new { ProductId = 806 }, "ProductCategoryId");
        ValidateProduct806(p);
        ValidateProductCategory15(p.ProductCategory);
    }

    [Fact]
    [Trait("Category", "GetAsync")]
    public async Task GetOneJoinMappedAsync()
    {
        using var db = GetSqlDatabase();
        var p = await db.GetAsync<Product, ProductCategory, Product>(
            (pr, pc) =>
            {
                pr.ProductCategory = pc;
                return pr;
            },
            GetMultiTwoParamQuery, new { ProductId = 806 }, "ProductCategoryId");
        ValidateProduct806(p);
        ValidateProductCategory15(p.ProductCategory);
    }

    [Fact]
    [Trait("Category", "GetAsync")]
    public async Task GetTwoJoinsUnmappedAsync()
    {
        using var db = GetSqlDatabase();
        var p = await db.GetAsync<Product, ProductCategory, ProductModel>(
            GetMultiThreeParamQuery, new { ProductId = 806 }, "ProductCategoryId,ProductModelId");
        ValidateProduct806(p);
        ValidateProductCategory15(p.ProductCategory);
        ValidateProductModel60(p.ProductModel);
    }

    [Fact]
    [Trait("Category", "Get")]
    public async Task GetTwoJoinsMappedAsync()
    {
        using var db = GetSqlDatabase();
        var p = await db.GetAsync<Product, ProductCategory, ProductModel, Product>(
            (pr, pc, pm) =>
            {
                pr.ProductCategory = pc;
                pr.ProductModel = pm;
                return pr;
            },
            GetMultiThreeParamQuery, new { ProductId = 806 }, "ProductCategoryId,ProductModelId");
        ValidateProduct806(p);
        ValidateProductCategory15(p.ProductCategory);
        ValidateProductModel60(p.ProductModel);
    }
}

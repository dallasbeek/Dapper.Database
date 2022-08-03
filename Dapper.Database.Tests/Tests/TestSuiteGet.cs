using System;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "Get")]
    public void GetByEntity()
    {
        using var db = GetSqlDatabase();
        var p = new Product { ProductID = 806, GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") };
        ValidateProduct806(db.Get(p));
    }

    [Fact]
    [Trait("Category", "Get")]
    public void GetByIntegerId()
    {
        using var db = GetSqlDatabase();
        ValidateProduct806(db.Get<Product>(806));
    }

    [Fact]
    [Trait("Category", "Get")]
    public void GetByAliasIntegerId()
    {
        using var db = GetSqlDatabase();
        var item = db.Get<ProductAlias>(806);
        Assert.Equal(806, item.Id);
        Assert.Equal("ML Headset", item.Name);
        Assert.Equal("HS-2451", item.ProductNumber);
        Assert.Null(item.Color);
    }

    [Fact]
    [Trait("Category", "Get")]
    public void GetByGuidIdWhereClause()
    {
        using var db = GetSqlDatabase();
        if (GetProvider() == Provider.Firebird || GetProvider() == Provider.SQLite)
            ValidateProduct806(db.Get<Product>($"where rowguid = {P}GuidId",
                new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
        else
            ValidateProduct806(db.Get<Product>($"WHERE rowguid = {P}GuidId",
                new { GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") }));
    }

    [Fact]
    [Trait("Category", "Get")]
    public void GetPartialBySelect()
    {
        using var db = GetSqlDatabase();
        var p = db.Get<Product>($"select ProductId, rowguid AS GuidId, Name from Product where ProductId = {P}Id",
            new { Id = 806 });
        Assert.NotNull(p);
        Assert.Equal(806, p.ProductID);
        Assert.Equal("ML Headset", p.Name);
        Assert.Null(p.ProductNumber);
        Assert.Equal(new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6"), p.GuidId);
    }

    [Fact]
    [Trait("Category", "Get")]
    public void GetStarBySelect()
    {
        using var db = GetSqlDatabase();
        ValidateProduct806(db.Get<Product>(
            $"select p.*, p.rowguid AS GuidId  from Product p where p.ProductId = {P}Id", new { Id = 806 }));
    }

    [Fact]
    [Trait("Category", "Get")]
    public void GetShortCircuitSemiColon()
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

        var p = db.Get<Product>(tsql, new { });
        Assert.Equal(23, p.ProductID);
    }

    [Fact]
    [Trait("Category", "Get")]
    public void GetOneJoinUnmapped()
    {
        using var db = GetSqlDatabase();
        var p = db.Get<Product, ProductCategory>(
            GetMultiTwoParamQuery, new { ProductId = 806 }, "ProductCategoryId");
        ValidateProduct806(p);
        ValidateProductCategory15(p.ProductCategory);
    }

    [Fact]
    [Trait("Category", "Get")]
    public void GetOneJoinMapped()
    {
        using var db = GetSqlDatabase();
        var p = db.Get<Product, ProductCategory, Product>(
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
    [Trait("Category", "Get")]
    public void GetTwoJoinsUnmapped()
    {
        using var db = GetSqlDatabase();
        var p = db.Get<Product, ProductCategory, ProductModel>(
            GetMultiThreeParamQuery, new { ProductId = 806 }, "ProductCategoryId,ProductModelId");
        ValidateProduct806(p);
        ValidateProductCategory15(p.ProductCategory);
        ValidateProductModel60(p.ProductModel);
    }

    [Fact]
    [Trait("Category", "Get")]
    public void GetTwoJoinsMapped()
    {
        using var db = GetSqlDatabase();
        var p = db.Get<Product, ProductCategory, ProductModel, Product>(
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

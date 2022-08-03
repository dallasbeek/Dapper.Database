using System;
using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "GetFirstAsync")]
    public async Task GetFirstWithWhereClauseAsync()
    {
        using var db = GetSqlDatabase();
        var item = await db.GetFirstAsync<Product>("where Color = 'Black' and ProductId >= 816 order by ProductId");
        ValidateProduct816(item);
    }

    [Fact]
    [Trait("Category", "GetFirstAsync")]
    public async Task GetFirstWithWhereClauseParameterAsync()
    {
        using var db = GetSqlDatabase();
        var item = await db.GetFirstAsync<Product>(
            $"where Color = {P}Color and ProductId >= {P}ProductId order by ProductId",
            new { Color = "Black", ProductId = 816 });
        ValidateProduct816(item);
    }

    [Fact]
    [Trait("Category", "GetFirstAsync")]
    public async Task GetFirstWithSelectClauseAsync()
    {
        using var db = GetSqlDatabase();
        var item = await db.GetFirstAsync<Product>(
            "select p.*, p.rowguid as GuidId from Product p where p.Color = 'Black' and p.ProductId >= 816 order by p.ProductId");
        ValidateProduct816(item);
    }

    [Fact]
    [Trait("Category", "GetFirstAsync")]
    public async Task GetFirstWithSelectClauseParameterAsync()
    {
        using var db = GetSqlDatabase();
        var item = await db.GetFirstAsync<Product>(
            $"select p.*, p.rowguid as GuidId from Product p where p.Color = {P}Color and p.ProductId >= {P}ProductId order by p.ProductId",
            new { Color = "Black", ProductId = 816 });
        ValidateProduct816(item);
    }

    [Fact]
    [Trait("Category", "GetFirstAsync")]
    public async Task GetFirstShortCircuitAsync()
    {
        using var db = GetSqlDatabase();
        var item = await db.GetFirstAsync<Product>(
            $";select p.*, p.rowguid as GuidId from Product p where p.Color = {P}Color and p.ProductId >= {P}ProductId order by p.ProductId",
            new { Color = "Black", ProductId = 816 });
        ValidateProduct816(item);
    }

    [Fact]
    [Trait("Category", "GetFirstAsync")]
    public async Task GetFirstPartialBySelectAsync()
    {
        using var db = GetSqlDatabase();
        var item = await db.GetFirstAsync<Product>(
            $"select p.ProductId, p.rowguid AS GuidId, Name from Product p where p.Color = {P}Color and p.ProductId >= {P}ProductId order by p.ProductId",
            new { Color = "Black", ProductId = 816 });
        Assert.Equal(816, item.ProductID);
        Assert.Equal("ML Mountain Front Wheel", item.Name);
        Assert.Null(item.ProductNumber);
        Assert.Equal(new Guid("5E3E5033-9A77-4DCA-8B7F-DFED78EFA08A"), item.GuidId);
    }

    [Fact]
    [Trait("Category", "GetFirstAsync")]
    public async Task GetFirstOneJoinUnmappedAsync()
    {
        using var db = GetSqlDatabase();
        var item = await db.GetFirstAsync<Product, ProductCategory>(
            GetFirstTwoParamQuery, new { Color = "Black", ProductId = 816 }, "ProductCategoryId");
        ValidateProduct816(item);
        if (GetProvider() != Provider.SQLite) ValidateProductCategory21(item.ProductCategory);
    }

    [Fact]
    [Trait("Category", "GetFirstAsync")]
    public async Task GetFirstOneJoinMappedAsync()
    {
        using var db = GetSqlDatabase();
        var item = await db.GetFirstAsync<Product, ProductCategory, Product>(
            (pr, pc) =>
            {
                pr.ProductCategory = pc;
                return pr;
            },
            GetFirstTwoParamQuery, new { Color = "Black", ProductId = 816 }, "ProductCategoryId");
        ValidateProduct816(item);
        if (GetProvider() != Provider.SQLite) ValidateProductCategory21(item.ProductCategory);
    }

    [Fact]
    [Trait("Category", "GetFirstAsync")]
    public async Task GetFirstTwoJoinsUnmappedAsync()
    {
        using var db = GetSqlDatabase();
        var item = await db.GetFirstAsync<Product, ProductCategory, ProductModel>(
            GetFirstThreeParamQuery, new { Color = "Black", ProductId = 816 }, "ProductCategoryId,ProductModelId");
        ValidateProduct816(item);
        if (GetProvider() != Provider.SQLite)
        {
            ValidateProductCategory21(item.ProductCategory);
            ValidateProductModel45(item.ProductModel);
        }
    }

    [Fact]
    [Trait("Category", "GetFirstAsync")]
    public async Task GetFirstTwoJoinsMappedAsync()
    {
        using var db = GetSqlDatabase();
        var item = await db.GetFirstAsync<Product, ProductCategory, ProductModel, Product>(
            (pr, pc, pm) =>
            {
                pr.ProductCategory = pc;
                pr.ProductModel = pm;
                return pr;
            },
            GetFirstThreeParamQuery, new { Color = "Black", ProductId = 816 }, "ProductCategoryId,ProductModelId");
        ValidateProduct816(item);
        if (GetProvider() != Provider.SQLite)
        {
            ValidateProductCategory21(item.ProductCategory);
            ValidateProductModel45(item.ProductModel);
        }
    }
}

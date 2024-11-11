using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{

    [Fact]
    [Trait("Category", "UpdateManyAsync")]
    public async Task UpdateManyAllAsync()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(847, await db.UpdateManyAsync<Customer>(null, new[] { "Phone" }, new { Phone = "555-555-5555" }));
    }

    [Fact]
    [Trait("Category", "UpdateManyAsync")]
    public async Task UpdateManySqlAsync()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(89, await db.UpdateManyAsync<Product>("WHERE color = 'Black'", new[] { "Color" }, new { Color = "Black" }));
    }

    [Fact]
    [Trait("Category", "UpdateManyAsync")]
    public async Task UpdateManySqlWithParameterAsync()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(89, await db.UpdateManyAsync<Product>($"WHERE color = {P}Color", ["Color"], new { Color = "Black" }));
    }

    [Fact]
    [Trait("Category", "UpdateManyAsync")]
    public async Task UpdateManySqlWithSplitParameterAsync()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(89, await db.UpdateManyAsync<Product>($"WHERE color = {P}Filter", ["Color"], new { Color = "Black", Filter = "Black" }));
    }

}

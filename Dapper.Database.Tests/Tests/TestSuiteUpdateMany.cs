using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{

    [Fact]
    [Trait("Category", "UpdateManyAsync")]
    public void UpdateManyAll()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(847, db.UpdateMany<Customer>(null, new[] { "Phone" }, new { Phone = "555-555-5555" }));
    }


    [Fact]
    [Trait("Category", "UpdateManyAsync")]
    public void UpdateManySql()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(89, db.UpdateMany<Product>("WHERE color = 'Black'", new[] { "Color" }, new { Color = "Black" }));
    }

    [Fact]
    [Trait("Category", "UpdateManyAsync")]
    public void UpdateManySqlWithParameter()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(89, db.UpdateMany<Product>($"WHERE color = {P}Color", ["Color"], new { Color = "Black" }));
    }

    [Fact]
    [Trait("Category", "UpdateManyAsync")]
    public void UpdateManySqlWithSplitParameter()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(89, db.UpdateMany<Product>($"WHERE color = {P}Filter", ["Color"], new { Color = "Black", Filter = "Black" }));
    }

}

using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "GetDataTable")]
    public void GetDataTable()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite specified method is not supported.");

        using var db = GetSqlDatabase();
        var dt = db.GetDataTable("select * from Product where Color = 'Black'");
        Assert.Equal(89, dt.Rows.Count);
    }

    [Fact]
    [Trait("Category", "GetDataTable")]
    public void GetDataTableWithParameter()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite specified method is not supported.");

        using var db = GetSqlDatabase();
        var dt = db.GetDataTable($"select * from Product where Color = {P}Color", new { Color = "Black" });
        Assert.Equal(89, dt.Rows.Count);
    }
}

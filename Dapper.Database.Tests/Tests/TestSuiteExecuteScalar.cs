using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "ExecuteScalar")]
    public void ExecuteScalarSql()
    {
        using var db = GetSqlDatabase();
        // ReSharper disable StringLiteralTypo
        Assert.Equal(102.29m, db.ExecuteScalar<decimal>("select listprice from Product where productid = 806"));
        // ReSharper restore StringLiteralTypo
    }

    [Fact]
    [Trait("Category", "ExecuteScalar")]
    public void ExecuteScalarSqlWithParameter()
    {
        using var db = GetSqlDatabase();
        // ReSharper disable StringLiteralTypo
        Assert.Equal(102.29m,
            db.ExecuteScalar<decimal>($"select listprice from Product where productid = {P}ProductId",
                new { ProductId = 806 }));
        // ReSharper restore StringLiteralTypo
    }
}

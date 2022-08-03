using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "ExecuteScalarAsync")]
    public async Task ExecuteScalarSqlAsync()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(102.29m,
            await db.ExecuteScalarAsync<decimal>("select listprice from Product where productid = 806"));
    }

    [Fact]
    [Trait("Category", "ExecuteScalarAsync")]
    public async Task ExecuteScalarSqlWithParameterAsync()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(102.29m,
            await db.ExecuteScalarAsync<decimal>($"select listprice from Product where productid = {P}ProductId",
                new { ProductId = 806 }));
    }
}

using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "ExecuteAsync")]
    public async Task ExecuteSqlAsync()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(89, await db.ExecuteAsync("update Product set color = 'Black' where Color = 'Black'"));
    }

    [Fact]
    [Trait("Category", "ExecuteAsync")]
    public async Task ExecuteSqlWithParameterAsync()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(89,
            await db.ExecuteAsync($"update Product set color = {P}Color where Color = {P}Color",
                new { Color = "Black" }));
    }
}

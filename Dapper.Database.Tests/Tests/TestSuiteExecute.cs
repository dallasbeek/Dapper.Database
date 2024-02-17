﻿using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "Execute")]
    public void ExecuteSql()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(89, db.Execute("update Product set color = 'Black' where Color = 'Black'"));
    }

    [Fact]
    [Trait("Category", "Execute")]
    public void ExecuteSqlWithParameter()
    {
        using var db = GetSqlDatabase();
        Assert.Equal(89,
            db.Execute($"update Product set color = {P}Color where Color = {P}Color", new { Color = "Black" }));
    }
}

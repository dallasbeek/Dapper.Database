using System;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "Timeouts")]
    public void GlobalTimeout()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite does not implement Timeout.");

        using var db = GetSqlDatabase();
        //can't force it to timeout since it's in seconds so verify it's set invalid
        db.CommandTimeout = -1;
        if (GetProvider() == Provider.Postgres)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => db.Count<Product>());
            AssertTimeoutExceptionMessage(ex);
        }
        else
        {
            var ex = Assert.Throws<ArgumentException>(() => db.Count<Product>());
            AssertTimeoutExceptionMessage(ex);
        }
    }

    [Fact]
    [Trait("Category", "Timeouts")]
    public void OneTimeTimeout()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite does not implement Timeout.");

        using var db = GetSqlDatabase();
        //can't force it to timeout since it's in seconds so verify it's set invalid
        db.OneTimeCommandTimeout = -1;
        if (GetProvider() == Provider.Postgres)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => db.Count<Product>());
            AssertTimeoutExceptionMessage(ex);
        }
        else
        {
            var ex = Assert.Throws<ArgumentException>(() => db.Count<Product>());
            AssertTimeoutExceptionMessage(ex);
        }
    }

    [Fact]
    [Trait("Category", "Timeouts")]
    public void GlobalAndOneTimeTimeout()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite does not implement Timeout.");

        using var db = GetSqlDatabase();
        //can't force it to timeout since it's in seconds so verify it's set invalid
        db.CommandTimeout = -1;
        if (GetProvider() == Provider.Postgres)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => db.Count<Product>());
            AssertTimeoutExceptionMessage(ex);
        }
        else
        {
            var ex = Assert.Throws<ArgumentException>(() => db.Count<Product>());
            AssertTimeoutExceptionMessage(ex);
        }

        db.OneTimeCommandTimeout = 0;
        Assert.Equal(295, db.Count<Product>());

        if (GetProvider() == Provider.Postgres)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => db.Count<Product>());
            AssertTimeoutExceptionMessage(ex);
        }
        else
        {
            var ex = Assert.Throws<ArgumentException>(() => db.Count<Product>());
            AssertTimeoutExceptionMessage(ex);
        }
    }

    private void AssertTimeoutExceptionMessage(Exception ex)
    {
        switch (GetProvider())
        {
            case Provider.SqlCE:
                Assert.StartsWith("SqlCeCommand.CommandTimeout does not support non-zero values.", ex.Message);
                break;
            case Provider.MySql:
                Assert.StartsWith("Command timeout must not be negative", ex.Message);
                break;
            case Provider.Postgres:
                Assert.StartsWith("CommandTimeout can't be less than zero.", ex.Message);
                break;
            case Provider.Firebird:
                Assert.StartsWith("The property value assigned is less than 0", ex.Message);
                break;
            case Provider.Oracle:
                // Verified this is correct for ODP.NET 12.2.1100 and ODP.NET Core 2.12.0-beta3.
                Assert.StartsWith("Value does not fall within the expected range.", ex.Message);
                break;
            default:
                Assert.StartsWith("Invalid CommandTimeout value -1;", ex.Message);
                break;
        }
    }
}

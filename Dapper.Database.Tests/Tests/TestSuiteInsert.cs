﻿using System;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "Insert")]
    public void InsertIdentity()
    {
        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        Assert.True(db.Insert(p));
        Assert.True(p.IdentityId > 0);
        var gp = db.Get<PersonIdentity>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "Insert")]
    public void InsertUniqueIdentifier()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        Assert.True(db.Insert(p));
        var gp = db.Get<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "Insert")]
    public void InsertUniqueIdentifierWithAliases()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifierWithAliases { GuidId = Guid.NewGuid(), First = "Alice", Last = "Jones" };
        Assert.True(db.Insert(p));
        var gp = db.Get<PersonUniqueIdentifierWithAliases>(p.GuidId);

        Assert.Equal(p.First, gp.First);
        Assert.Equal(p.Last, gp.Last);
    }

    [Fact]
    [Trait("Category", "Insert")]
    public void InsertPersonCompositeKey()
    {
        using var db = GetSqlDatabase();
        var gid = Guid.NewGuid();
        var p = new PersonCompositeKey { GuidId = gid, StringId = "test", FirstName = "Alice", LastName = "Jones" };
        Assert.True(db.Insert(p));
        var gp = db.Get<PersonCompositeKey>($"where GuidId = {P}GuidId and StringId = {P}StringId", p);

        Assert.Equal(p.StringId, gp.StringId);
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "Insert")]
    public void InsertComputed()
    {
        var now = DateTime.UtcNow;
        using var db = GetSqlDatabase();
        var p = new PersonExcludedColumns
        {
            FirstName = "Alice",
            LastName = "Jones",
            Notes = "Hello",
            CreatedOn = now,
            UpdatedOn = now
        };
        Assert.True(db.Insert(p));

        if (p.FullName != null) Assert.Equal("Alice Jones", p.FullName);

        var gp = db.Get<PersonExcludedColumns>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Null(gp.Notes);
        Assert.Null(gp.NoDbColumn);
        Assert.Null(gp.UpdatedOn);
        Assert.NotNull(gp.CreatedOn);
        Assert.InRange(gp.CreatedOn.Value, now.AddSeconds(-1),
            now.AddSeconds(
                1)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most.)
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);

    }
}

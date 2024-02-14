﻿using System;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "Update")]
    public void UpdateIdentity()
    {
        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        Assert.True(db.Insert(p));
        Assert.True(p.IdentityId > 0);

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(db.Update(p));

        var gp = db.Get<PersonIdentity>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "Update")]
    public void UpdateUniqueIdentifier()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        Assert.True(db.Insert(p));

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(db.Update(p));

        var gp = db.Get<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "Update")]
    public void UpdateUniqueIdentifierWithAliases()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifierWithAliases { GuidId = Guid.NewGuid(), First = "Alice", Last = "Jones" };
        Assert.True(db.Insert(p));

        p.First = "Greg";
        p.Last = "Smith";
        Assert.True(db.Update(p));

        var gp = db.Get<PersonUniqueIdentifierWithAliases>(p.GuidId);

        Assert.Equal(p.First, gp.First);
        Assert.Equal(p.Last, gp.Last);
    }

    [Fact]
    [Trait("Category", "Update")]
    public void UpdatePersonCompositeKey()
    {
        using var db = GetSqlDatabase();
        var p = new PersonCompositeKey
        {
            GuidId = Guid.NewGuid(), StringId = "test", FirstName = "Alice", LastName = "Jones"
        };
        Assert.True(db.Insert(p));

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(db.Update(p));

        var gp = db.Get<PersonCompositeKey>($"where GuidId = {P}GuidId and StringId = {P}StringId", p);

        Assert.Equal(p.StringId, gp.StringId);
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "Update")]
    public void UpdateComputed()
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

        p.FirstName = "Greg";
        p.LastName = "Smith";
        p.CreatedOn = DateTime.UtcNow;
        Assert.True(db.Update(p));
        if (p.FullName != null) Assert.Equal("Greg Smith", p.FullName);

        var gp = db.Get<PersonExcludedColumns>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Null(gp.Notes);
        Assert.Null(gp.NoDbColumn);
        Assert.NotNull(gp.CreatedOn);
        Assert.InRange(gp.CreatedOn.Value, now.AddSeconds(-1),
            now.AddSeconds(
                1)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most). 
        Assert.NotNull(gp.UpdatedOn);
        Assert.InRange(gp.UpdatedOn.Value, now.AddMinutes(-1),
            now.AddMinutes(1)); // to cover clock skew, delay in DML, etc.
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "Update")]
    public void UpdateComputedAlias()
    {
        using var db = GetSqlDatabase();
        var p = new PersonIdentityAlias { First = "Alice", Last = "Jones" };
        Assert.True(db.Insert(p));

        if (p.Name != null) Assert.Equal("Alice Jones", p.Name);

        p.First = "Greg";
        p.Last = "Smith";

        Assert.True(db.Update(p));
        if (p.Name != null) Assert.Equal("Greg Smith", p.Name);

        var gp = db.Get<PersonIdentityAlias>(p.Id);

        Assert.Equal(p.Id, gp.Id);
        Assert.Equal(p.First, gp.First);
        Assert.Equal(p.Last, gp.Last);
        Assert.Equal(p.Name, gp.Name);

        Assert.True(db.Delete<PersonIdentityAlias>(p.Id));

        var dp = db.Get(p);
        Assert.Null(dp);
    }

    [Fact]
    [Trait("Category", "Update")]
    public void UpdatePartial()
    {
        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        Assert.True(db.Insert(p));
        Assert.True(p.IdentityId > 0);

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(db.Update(p, new[] { "LastName" }));

        var gp = db.Get<PersonIdentity>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal("Smith", gp.LastName);
    }

    [Fact]
    [Trait("Category", "Update")]
    public void UpdateConcurrencyCheckNotModified()
    {
        Skip.If(GetProvider() == Provider.SqlCE, "SqlCE doesn't handle null concurrency field");

        using var db = GetSqlDatabase();
        var p = new PersonConcurrencyCheck
        {
            GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones", StringId = "abc"
        };
        Assert.True(db.Insert(p));

        Assert.Equal("abc", p.StringId);
        Assert.Null(p.UpdatedOn);

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(db.Update(p), "Concurrent fields unchanged");

        var gp = db.Get<PersonConcurrencyCheck>(p.GuidId);

        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
        Assert.Equal(p.StringId, gp.StringId);
        Assert.Equal(p.UpdatedOn, gp.UpdatedOn);
    }

    [Fact]
    [Trait("Category", "Update")]
    public void UpdateConcurrencyCheckModified()
    {
        using var db = GetSqlDatabase();
        var p = new PersonConcurrencyCheck
        {
            GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones", StringId = "abc"
        };
        Assert.True(db.Insert(p));

        // Modify one of the concurrency-check columns to simulate it changing out from underneath us.
        db.Execute("update Person set StringId = 'xyz' where GuidId = @GuidId", p);

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.ThrowsAny<OptimisticConcurrencyException>(() => db.Update(p));

        var gp = db.Get<PersonConcurrencyCheck>(p.GuidId);

        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal("Jones", gp.LastName);
    }
}

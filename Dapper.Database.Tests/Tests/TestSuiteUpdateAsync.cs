﻿using System;
using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "UpdateAsync")]
    public async Task UpdateIdentityAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        Assert.True(await db.InsertAsync(p));
        Assert.True(p.IdentityId > 0);

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(await db.UpdateAsync(p));

        var gp = await db.GetAsync<PersonIdentity>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateAsync")]
    public async Task UpdateUniqueIdentifierAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        Assert.True(await db.InsertAsync(p));

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(await db.UpdateAsync(p));

        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateAsync")]
    public async Task UpdateUniqueIdentifierWithAliasesAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifierWithAliases { GuidId = Guid.NewGuid(), First = "Alice", Last = "Jones" };
        Assert.True(await db.InsertAsync(p));

        p.First = "Greg";
        p.Last = "Smith";
        Assert.True(await db.UpdateAsync(p));

        var gp = await db.GetAsync<PersonUniqueIdentifierWithAliases>(p.GuidId);

        Assert.Equal(p.First, gp.First);
        Assert.Equal(p.Last, gp.Last);
    }

    [Fact]
    [Trait("Category", "UpdateAsync")]
    public async Task UpdatePersonCompositeKeyAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonCompositeKey
        {
            GuidId = Guid.NewGuid(), StringId = "test", FirstName = "Alice", LastName = "Jones"
        };
        Assert.True(await db.InsertAsync(p));

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(await db.UpdateAsync(p));

        var gp = await db.GetAsync<PersonCompositeKey>($"where GuidId = {P}GuidId and StringId = {P}StringId", p);

        Assert.Equal(p.StringId, gp.StringId);
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateAsync")]
    public async Task UpdateComputedAsync()
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
        Assert.True(await db.InsertAsync(p));

        if (p.FullName != null) Assert.Equal("Alice Jones", p.FullName);

        p.FirstName = "Greg";
        p.LastName = "Smith";
        p.CreatedOn = DateTime.UtcNow;
        Assert.True(await db.UpdateAsync(p));
        if (p.FullName != null) Assert.Equal("Greg Smith", p.FullName);

        var gp = await db.GetAsync<PersonExcludedColumns>(p.IdentityId);

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
    [Trait("Category", "UpdateAsync")]
    public async Task UpdateComputedAliasAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonIdentityAlias { First = "Alice", Last = "Jones" };
        Assert.True(await db.InsertAsync(p));

        if (p.Name != null) Assert.Equal("Alice Jones", p.Name);

        p.First = "Greg";
        p.Last = "Smith";

        Assert.True(await db.UpdateAsync(p));
        if (p.Name != null) Assert.Equal("Greg Smith", p.Name);

        var gp = await db.GetAsync<PersonIdentityAlias>(p.Id);

        Assert.Equal(p.Id, gp.Id);
        Assert.Equal(p.First, gp.First);
        Assert.Equal(p.Last, gp.Last);
        Assert.Equal(p.Name, gp.Name);

        Assert.True(await db.DeleteAsync<PersonIdentityAlias>(p.Id));

        var dp = await db.GetAsync(p);
        Assert.Null(dp);
    }

    [Fact]
    [Trait("Category", "UpdateAsync")]
    public async Task UpdatePartialAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        Assert.True(await db.InsertAsync(p));
        Assert.True(p.IdentityId > 0);

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(await db.UpdateAsync(p, new[] { "LastName" }));

        var gp = await db.GetAsync<PersonIdentity>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal("Smith", gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateAsync")]
    public async Task UpdateConcurrencyCheckNotModifiedAsync()
    {
        Skip.If(GetProvider() == Provider.SqlCE, "SqlCE doesn't handle null concurrency field");

        using var db = GetSqlDatabase();
        var p = new PersonConcurrencyCheck
        {
            GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones", StringId = "abc"
        };
        Assert.True(await db.InsertAsync(p));

        Assert.Equal("abc", p.StringId);
        Assert.Null(p.UpdatedOn);

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(await db.UpdateAsync(p), "Concurrent fields unchanged");

        var gp = await db.GetAsync<PersonConcurrencyCheck>(p.GuidId);

        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
        Assert.Equal(p.StringId, gp.StringId);
        Assert.Equal(p.UpdatedOn, gp.UpdatedOn);
    }

    [Fact]
    [Trait("Category", "UpdateAsync")]
    public async Task UpdateConcurrencyCheckModifiedAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonConcurrencyCheck
        {
            GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones", StringId = "abc"
        };
        Assert.True(await db.InsertAsync(p));

        // Modify one of the concurrency-check columns to simulate it changing out from underneath us.
        await db.ExecuteAsync("update Person set StringId = 'xyz' where GuidId = @GuidId", p);

        p.FirstName = "Greg";
        p.LastName = "Smith";
        await Assert.ThrowsAnyAsync<OptimisticConcurrencyException>(() => db.UpdateAsync(p));

        var gp = await db.GetAsync<PersonConcurrencyCheck>(p.GuidId);

        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal("Jones", gp.LastName);
    }
}

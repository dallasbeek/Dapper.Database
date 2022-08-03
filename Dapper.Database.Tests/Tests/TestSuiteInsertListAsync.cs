using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "InsertListAsync")]
    public async Task InsertEmptyListAsync()
    {
        using var db = GetSqlDatabase();
        Assert.False(await db.InsertListAsync(new List<PersonUniqueIdentifier>()));
    }

    [Fact]
    [Trait("Category", "InsertListAsync")]
    public async Task InsertListNoComputedAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };
        Assert.True(await db.InsertListAsync(new List<PersonUniqueIdentifier> { p, q, r }));
        //Assert.True(p.IdentityId > 0);
        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "InsertListAsync")]
    public async Task InsertListNoComputedThrowsExceptionAsync()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier
        {
            GuidId = Guid.NewGuid(), FirstName = "a".PadRight(101, 'a'), LastName = "Padilla"
        };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };
        await Assert.ThrowsAnyAsync<Exception>(() =>
            db.InsertListAsync(new List<PersonUniqueIdentifier> { p, q, r }));

        Assert.Null(await db.GetAsync<PersonUniqueIdentifier>(p.GuidId));
    }

    [Fact]
    [Trait("Category", "InsertListAsync")]
    public async Task InsertListIdentityAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        var q = new PersonIdentity { FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonIdentity { FirstName = "Lidia", LastName = "Bain" };
        Assert.True(await db.InsertListAsync(new List<PersonIdentity> { p, q, r }));
        Assert.True(p.IdentityId > 0);
        var gp = await db.GetAsync<PersonIdentity>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "InsertListAsync")]
    public async Task InsertListIdentityThrowsExceptionAsync()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        var q = new PersonIdentity { FirstName = "a".PadRight(101, 'a'), LastName = "Padilla" };
        var r = new PersonIdentity { FirstName = "Lidia", LastName = "Bain" };
        await Assert.ThrowsAnyAsync<Exception>(() => db.InsertListAsync(new List<PersonIdentity> { p, q, r }));

        Assert.Null(await db.GetAsync<PersonIdentity>(p.IdentityId));
    }

    [Fact]
    [Trait("Category", "InsertListAsync")]
    public async Task InsertListComputedAsync()
    {
        var dnow = DateTime.UtcNow;
        using var db = GetSqlDatabase();
        var p = new PersonExcludedColumns
        {
            FirstName = "Alice",
            LastName = "Jones",
            Notes = "Hello",
            CreatedOn = dnow,
            UpdatedOn = dnow
        };
        var q = new PersonExcludedColumns
        {
            FirstName = "Raj",
            LastName = "Padilla",
            Notes = "Hello",
            CreatedOn = dnow,
            UpdatedOn = dnow
        };
        var r = new PersonExcludedColumns
        {
            FirstName = "Lidia",
            LastName = "Bain",
            Notes = "Hello",
            CreatedOn = dnow,
            UpdatedOn = dnow
        };

        Assert.True(await db.InsertListAsync(new List<PersonExcludedColumns> { p, q, r }));

        if (p.FullName != null)
        {
            Assert.Equal("Alice Jones", p.FullName);
            Assert.Equal("Raj Padilla", q.FullName);
            Assert.Equal("Lidia Bain", r.FullName);
        }

        var gp = await db.GetAsync<PersonExcludedColumns>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Null(gp.Notes);
        Assert.Null(gp.UpdatedOn);
        Assert.InRange(gp.CreatedOn.Value, dnow.AddSeconds(-1),
            dnow.AddSeconds(
                1)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most. )
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "InsertListAsync")]
    public async Task InsertListTransactionNoComputedAsync()
    {
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

        using var db = GetSqlDatabase();
        using (var t = db.GetTransaction())
        {
            Assert.True(await db.InsertListAsync(new List<PersonUniqueIdentifier> { p, q, r }));
            t.Complete();
        }

        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "InsertListAsync")]
    public async Task InsertListTransactionRollbackNoComputedAsync()
    {
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

        using var db = GetSqlDatabase();
        using (var t = db.GetTransaction())
        {
            Assert.True(await db.InsertListAsync(new List<PersonUniqueIdentifier> { p, q, r }));
            t.Dispose();
        }

        Assert.Null(await db.GetAsync<PersonUniqueIdentifier>(p.GuidId));
    }

    [Fact]
    [Trait("Category", "InsertListAsync")]
    public async Task InsertListTransactionNoComputedThrowsExceptionAsync()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier
        {
            GuidId = Guid.NewGuid(), FirstName = "a".PadRight(101, 'a'), LastName = "Padilla"
        };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

        using var db = GetSqlDatabase();
        using (var t = db.GetTransaction())
        {
            await Assert.ThrowsAnyAsync<Exception>(() =>
                db.InsertListAsync(new List<PersonUniqueIdentifier> { p, q, r }));
        }

        Assert.Null(await db.GetAsync<PersonUniqueIdentifier>(p.GuidId));
    }
}

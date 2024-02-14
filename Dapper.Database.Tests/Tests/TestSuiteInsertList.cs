using System;
using System.Collections.Generic;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "InsertList")]
    public void InsertEmptyList()
    {
        using var db = GetSqlDatabase();
        Assert.False(db.InsertList(new List<PersonUniqueIdentifier>()));
    }

    [Fact]
    [Trait("Category", "InsertList")]
    public void InsertListNoComputed()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };
        Assert.True(db.InsertList(new List<PersonUniqueIdentifier> { p, q, r }));
        //Assert.True(p.IdentityId > 0);
        var gp = db.Get<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "InsertList")]
    public void InsertListNoComputedThrowsException()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier
        {
            GuidId = Guid.NewGuid(), FirstName = "a".PadRight(101, 'a'), LastName = "Padilla"
        };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };
        Assert.ThrowsAny<Exception>(() => db.InsertList(new List<PersonUniqueIdentifier> { p, q, r }));

        Assert.Null(db.Get<PersonUniqueIdentifier>(p.GuidId));
    }

    [Fact]
    [Trait("Category", "InsertList")]
    public void InsertListIdentity()
    {
        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        var q = new PersonIdentity { FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonIdentity { FirstName = "Lidia", LastName = "Bain" };
        Assert.True(db.InsertList(new List<PersonIdentity> { p, q, r }));
        Assert.True(p.IdentityId > 0);
        var gp = db.Get<PersonIdentity>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "InsertList")]
    public void InsertListIdentityThrowsException()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        var q = new PersonIdentity { FirstName = "a".PadRight(101, 'a'), LastName = "Padilla" };
        var r = new PersonIdentity { FirstName = "Lidia", LastName = "Bain" };
        Assert.ThrowsAny<Exception>(() => db.InsertList(new List<PersonIdentity> { p, q, r }));

        Assert.Null(db.Get<PersonIdentity>(p.IdentityId));
    }

    [Fact]
    [Trait("Category", "Insert")]
    public void InsertListComputed()
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
        var q = new PersonExcludedColumns
        {
            FirstName = "Raj",
            LastName = "Padilla",
            Notes = "Hello",
            CreatedOn = now,
            UpdatedOn = now
        };
        var r = new PersonExcludedColumns
        {
            FirstName = "Lidia",
            LastName = "Bain",
            Notes = "Hello",
            CreatedOn = now,
            UpdatedOn = now
        };

        Assert.True(db.InsertList(new List<PersonExcludedColumns> { p, q, r }));

        if (p.FullName != null)
        {
            Assert.Equal("Alice Jones", p.FullName);
            Assert.Equal("Raj Padilla", q.FullName);
            Assert.Equal("Lidia Bain", r.FullName);
        }

        var gp = db.Get<PersonExcludedColumns>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Null(gp.Notes);
        Assert.Null(gp.NoDbColumn);
        Assert.Null(gp.UpdatedOn);
        Assert.NotNull(gp.CreatedOn);
        Assert.InRange(gp.CreatedOn.Value, now.AddSeconds(-1),
            now.AddSeconds(
                1)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most). 
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "InsertList")]
    public void InsertListTransactionNoComputed()
    {
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

        using var db = GetSqlDatabase();
        using (var t = db.GetTransaction())
        {
            Assert.True(db.InsertList(new List<PersonUniqueIdentifier> { p, q, r }));
            t.Complete();
        }

        var gp = db.Get<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "InsertList")]
    public void InsertListTransactionRollbackNoComputed()
    {
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

        using var db = GetSqlDatabase();
        using (db.GetTransaction())
        {
            Assert.True(db.InsertList(new List<PersonUniqueIdentifier> { p, q, r }));
        }

        Assert.Null(db.Get<PersonUniqueIdentifier>(p.GuidId));
    }

    [Fact]
    [Trait("Category", "InsertList")]
    public void InsertListTransactionNoComputedThrowsException()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier
        {
            GuidId = Guid.NewGuid(), FirstName = "a".PadRight(101, 'a'), LastName = "Padilla"
        };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

        using var db = GetSqlDatabase();
        using (db.GetTransaction())
        {
            Assert.ThrowsAny<Exception>(() => db.InsertList(new List<PersonUniqueIdentifier> { p, q, r }));
        }

        Assert.Null(db.Get<PersonUniqueIdentifier>(p.GuidId));
    }
}

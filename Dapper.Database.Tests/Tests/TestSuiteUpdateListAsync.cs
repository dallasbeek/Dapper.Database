﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "UpdateListAsync")]
    public async Task UpdateEmptyListAsync()
    {
        using var db = GetSqlDatabase();
        Assert.False(await db.UpdateListAsync(new List<PersonUniqueIdentifier>()));
    }

    [Fact]
    [Trait("Category", "UpdateListAsync")]
    public async Task UpdateListNoComputedAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

        var lst = new List<PersonUniqueIdentifier> { p, q, r };

        Assert.True(await db.InsertListAsync(lst));

        p.FirstName = "Emily";
        q.FirstName = "Jim";
        r.FirstName = "Laura";

        Assert.True(await db.UpdateListAsync(lst));

        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal("Emily", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateListAsync")]
    public async Task UpdateListNoComputedPartialAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

        var lst = new List<PersonUniqueIdentifier> { p, q, r };

        Assert.True(await db.InsertListAsync(lst));

        p.FirstName = "Emily";
        p.LastName = "Swank";
        q.FirstName = "Jim";
        r.FirstName = "Laura";

        Assert.True(await db.UpdateListAsync(lst, new[] { "LastName" }));

        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal("Swank", gp.LastName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateListAsync")]
    public async Task UpdateListNoComputedThrowsExceptionAsync()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

        var lst = new List<PersonUniqueIdentifier> { p, q, r };

        Assert.True(await db.InsertListAsync(lst));

        p.FirstName = "Emily";
        q.FirstName = "a".PadRight(101, 'a');
        r.FirstName = "Laura";

        await Assert.ThrowsAnyAsync<Exception>(() => db.UpdateListAsync(lst));

        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateListAsync")]
    public async Task UpdateListIdentityAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        var q = new PersonIdentity { FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonIdentity { FirstName = "Lidia", LastName = "Bain" };

        var lst = new List<PersonIdentity> { p, q, r };

        Assert.True(await db.InsertListAsync(lst));

        p.FirstName = "Emily";
        q.FirstName = "Jim";
        r.FirstName = "Laura";

        Assert.True(await db.UpdateListAsync(lst));

        var gp = await db.GetAsync<PersonIdentity>(p.IdentityId);

        Assert.Equal("Emily", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateListAsync")]
    public async Task UpdateListIdentityThrowsExceptionAsync()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        var q = new PersonIdentity { FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonIdentity { FirstName = "Lidia", LastName = "Bain" };

        var lst = new List<PersonIdentity> { p, q, r };

        Assert.True(await db.InsertListAsync(lst));

        p.FirstName = "Emily";
        q.FirstName = "a".PadRight(101, 'a');
        r.FirstName = "Laura";

        await Assert.ThrowsAnyAsync<Exception>(() => db.UpdateListAsync(lst));

        var gp = await db.GetAsync<PersonIdentity>(p.IdentityId);

        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateListAsync")]
    public async Task UpdateListComputedAsync()
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

        var lst = new List<PersonExcludedColumns> { p, q, r };

        Assert.True(await db.InsertListAsync(lst));

        p.FirstName = "Emily";
        q.FirstName = "Jim";
        r.FirstName = "Laura";

        Assert.True(await db.UpdateListAsync(lst));

        if (p.FullName != null)
        {
            Assert.Equal("Emily Jones", p.FullName);
            Assert.Equal("Jim Padilla", q.FullName);
            Assert.Equal("Laura Bain", r.FullName);
        }

        var gp = await db.GetAsync<PersonExcludedColumns>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Null(gp.Notes);
        Assert.Null(gp.NoDbColumn);
        Assert.NotNull(gp.UpdatedOn);
        Assert.InRange(gp.UpdatedOn.Value, now.AddMinutes(-1),
            now.AddMinutes(1)); // to cover clock skew, delay in DML, etc.
        Assert.NotNull(gp.CreatedOn);
        Assert.InRange(gp.CreatedOn.Value, now.AddSeconds(-1),
            now.AddSeconds(
                1)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most). 
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateListAsync")]
    public async Task UpdateListTransactionNoComputedAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

        var lst = new List<PersonUniqueIdentifier> { p, q, r };

        Assert.True(await db.InsertListAsync(lst));

        using (var t = db.GetTransaction())
        {
            p.FirstName = "Emily";
            q.FirstName = "Jim";
            r.FirstName = "Laura";

            Assert.True(await db.UpdateListAsync(lst));

            t.Complete();
        }

        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal("Emily", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateListAsync")]
    public async Task UpdateListTransactionRollbackNoComputedAsync()
    {
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

        var lst = new List<PersonUniqueIdentifier> { p, q, r };

        using var db = GetSqlDatabase();
        Assert.True(await db.InsertListAsync(lst));

        using (db.GetTransaction())
        {
            p.FirstName = "Emily";
            q.FirstName = "Jim";
            r.FirstName = "Laura";

            Assert.True(await db.UpdateListAsync(lst));
        }

        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateListAsync")]
    public async Task UpdateListTransactionNoComputedThrowsExceptionAsync()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

        var lst = new List<PersonUniqueIdentifier> { p, q, r };

        using var db = GetSqlDatabase();
        Assert.True(await db.InsertListAsync(lst));

        using (db.GetTransaction())
        {
            p.FirstName = "Emily";
            q.FirstName = "a".PadRight(101, 'a');
            r.FirstName = "Laura";
            await Assert.ThrowsAnyAsync<Exception>(() => db.UpdateListAsync(lst));
        }


        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateListAsync")]
    public async Task UpdateListConcurrencyCheckNotModifiedAsync()
    {
        Skip.If(GetProvider() == Provider.SqlCE, "SqlCE doesn't handle null concurrency field");

        using var db = GetSqlDatabase();
        var p = new PersonConcurrencyCheck
        {
            GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones", StringId = "aj"
        };
        var q = new PersonConcurrencyCheck
        {
            GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla", StringId = "rp"
        };
        var r = new PersonConcurrencyCheck
        {
            GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain", StringId = "lb"
        };

        var lst = new[] { p, q, r };

        Assert.True(await db.InsertListAsync(lst));

        p.FirstName = "Emily";
        q.FirstName = "Jim";
        r.FirstName = "Laura";

        Assert.True(await db.UpdateListAsync(lst));

        var gp = await db.GetAsync<PersonConcurrencyCheck>(p.GuidId);

        Assert.Equal("Emily", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
    }

    [Fact]
    [Trait("Category", "UpdateListAsync")]
    public async Task UpdateListConcurrencyCheckModifiedAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonConcurrencyCheck
        {
            GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones", StringId = "aj"
        };
        var q = new PersonConcurrencyCheck
        {
            GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla", StringId = "rp"
        };
        var r = new PersonConcurrencyCheck
        {
            GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain", StringId = "lb"
        };

        var lst = new[] { p, q, r };

        Assert.True(await db.InsertListAsync(lst));

        p.FirstName = "Emily";
        q.FirstName = "Jim";
        r.FirstName = "Laura";

        // Simulate the data changing out from underneath us by modifying the second item.
        q.StringId += "X";

        await Assert.ThrowsAnyAsync<OptimisticConcurrencyException>(() => db.UpdateListAsync(lst));

        var gp = await db.GetAsync<PersonConcurrencyCheck>(p.GuidId);
        var gq = await db.GetAsync<PersonConcurrencyCheck>(q.GuidId);
        var gr = await db.GetAsync<PersonConcurrencyCheck>(r.GuidId);

        // None of the items should have changed.
        Assert.NotEqual(p.FirstName, gp.FirstName);
        Assert.NotEqual(q.FirstName, gq.FirstName);
        Assert.NotEqual(r.FirstName, gr.FirstName);
    }
}

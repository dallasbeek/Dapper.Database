using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "UpsertListAsync")]
    public async Task UpsertEmptyListAsync()
    {
        using var db = GetSqlDatabase();
        Assert.False(await db.UpsertListAsync(new List<PersonUniqueIdentifier>()));
    }

    [Fact]
    [Trait("Category", "UpsertListAsync")]
    public async Task UpsertListNoComputedAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };
        var s = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Derren", LastName = "Southern" };

        var lst = new List<PersonUniqueIdentifier> { p, q, r };

        Assert.True(await db.UpsertListAsync(lst));

        p.FirstName = "Emily";
        q.FirstName = "Jim";
        r.FirstName = "Laura";
        lst.Add(s);

        Assert.True(await db.UpsertListAsync(lst));

        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal("Emily", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);

        var gs = await db.GetAsync<PersonUniqueIdentifier>(s.GuidId);

        Assert.Equal("Derren", gs.FirstName);
        Assert.Equal("Southern", gs.LastName);
    }

    [Fact]
    [Trait("Category", "UpsertListAsync")]
    public async Task UpsertListNoComputedPartialAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };
        var s = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Derren", LastName = "Southern" };

        var lst = new List<PersonUniqueIdentifier> { p, q, r };

        Assert.True(await db.UpsertListAsync(lst));

        p.FirstName = "Emily";
        p.LastName = "Swank";
        q.FirstName = "Jim";
        r.FirstName = "Laura";
        lst.Add(s);

        Assert.True(await db.UpsertListAsync(lst, new[] { "LastName" }));

        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal("Swank", gp.LastName);

        var gs = await db.GetAsync<PersonUniqueIdentifier>(s.GuidId);

        Assert.Equal("Derren", gs.FirstName);
        Assert.Equal("Southern", gs.LastName);
    }

    [Fact]
    [Trait("Category", "UpsertListAsync")]
    public async Task UpsertListNoComputedThrowsExceptionAsync()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };
        var s = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Derren", LastName = "Southern" };

        var lst = new List<PersonUniqueIdentifier> { p, q, r };

        Assert.True(await db.UpsertListAsync(lst));

        p.FirstName = "Emily";
        q.FirstName = "a".PadRight(101, 'a');
        r.FirstName = "Laura";
        lst.Add(s);

        await Assert.ThrowsAnyAsync<Exception>(() => db.UpsertListAsync(lst));

        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);

        var gs = await db.GetAsync<PersonUniqueIdentifier>(s.GuidId);

        Assert.Null(gs);
    }

    [Fact]
    [Trait("Category", "UpsertListAsync")]
    public async Task UpsertListIdentityAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        var q = new PersonIdentity { FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonIdentity { FirstName = "Lidia", LastName = "Bain" };
        var s = new PersonIdentity { FirstName = "Derren", LastName = "Southern" };

        var lst = new List<PersonIdentity> { p, q, r };

        Assert.True(await db.UpsertListAsync(lst));

        p.FirstName = "Emily";
        q.FirstName = "Jim";
        r.FirstName = "Laura";
        lst.Add(s);

        Assert.True(await db.UpsertListAsync(lst));

        var gp = await db.GetAsync<PersonIdentity>(p.IdentityId);

        Assert.Equal("Emily", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);

        var gs = await db.GetAsync<PersonIdentity>(s.IdentityId);

        Assert.Equal("Derren", gs.FirstName);
        Assert.Equal("Southern", gs.LastName);
    }

    [Fact]
    [Trait("Category", "UpsertListAsync")]
    public async Task UpsertListIdentityThrowsExceptionAsync()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        var q = new PersonIdentity { FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonIdentity { FirstName = "Lidia", LastName = "Bain" };
        var s = new PersonIdentity { FirstName = "Derren", LastName = "Southern" };

        var lst = new List<PersonIdentity> { p, q, r };

        Assert.True(await db.UpsertListAsync(lst));

        p.FirstName = "Emily";
        q.FirstName = "a".PadRight(101, 'a');
        r.FirstName = "Laura";
        lst.Add(s);

        await Assert.ThrowsAnyAsync<Exception>(() => db.UpsertListAsync(lst));

        var gp = await db.GetAsync<PersonIdentity>(p.IdentityId);

        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);

        var gs = await db.GetAsync<PersonIdentity>(s.IdentityId);

        Assert.Null(gs);
    }

    [Fact]
    [Trait("Category", "UpsertListAsync")]
    public async Task UpsertListComputedAsync()
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
        var s = new PersonExcludedColumns
        {
            FirstName = "Derren",
            LastName = "Southern",
            Notes = "Hello",
            CreatedOn = dnow,
            UpdatedOn = dnow
        };

        var lst = new List<PersonExcludedColumns> { p, q, r };

        Assert.True(await db.UpsertListAsync(lst));

        p.FirstName = "Emily";
        q.FirstName = "Jim";
        r.FirstName = "Laura";
        lst.Add(s);

        Assert.True(await db.UpsertListAsync(lst));

        if (p.FullName != null)
        {
            Assert.Equal("Emily Jones", p.FullName);
            Assert.Equal("Jim Padilla", q.FullName);
            Assert.Equal("Laura Bain", r.FullName);
            Assert.Equal("Derren Southern", s.FullName);
        }

        var gp = await db.GetAsync<PersonExcludedColumns>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Null(gp.Notes);
        Assert.InRange(gp.UpdatedOn.Value, dnow.AddMinutes(-1),
            dnow.AddMinutes(1)); // to cover clock skew, delay in DML, etc.
        Assert.InRange(gp.CreatedOn.Value, dnow.AddSeconds(-1),
            dnow.AddSeconds(
                1)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most. )
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);

        var gs = await db.GetAsync<PersonExcludedColumns>(s.IdentityId);
        Assert.Equal(s.IdentityId, gs.IdentityId);
        Assert.Null(gs.Notes);
        Assert.Null(gs.UpdatedOn); // to cover clock skew, delay in DML, etc.
        Assert.InRange(gs.CreatedOn.Value, dnow.AddSeconds(-1),
            dnow.AddSeconds(
                1)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most. )
        Assert.Equal(s.FirstName, gs.FirstName);
        Assert.Equal(s.LastName, gs.LastName);
    }

    [Fact]
    [Trait("Category", "UpsertListAsync")]
    public async Task UpsertListTransactionNoComputedAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };
        var s = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Derren", LastName = "Southern" };

        var lst = new List<PersonUniqueIdentifier> { p, q, r };

        Assert.True(await db.UpsertListAsync(lst));

        using (var t = db.GetTransaction())
        {
            p.FirstName = "Emily";
            q.FirstName = "Jim";
            r.FirstName = "Laura";
            lst.Add(s);

            Assert.True(await db.UpsertListAsync(lst));

            t.Complete();
        }

        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal("Emily", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);

        var gs = await db.GetAsync<PersonUniqueIdentifier>(s.GuidId);

        Assert.Equal("Derren", gs.FirstName);
        Assert.Equal("Southern", gs.LastName);
    }

    [Fact]
    [Trait("Category", "UpsertListAsync")]
    public async Task UpsertListComputedCallbacksAsync()
    {
        var dnow = DateTime.UtcNow;
        using var db = GetSqlDatabase();
        var p = new PersonExcludedColumns { FirstName = "Alice", LastName = "Jones", Notes = "Hello" };
        var q = new PersonExcludedColumns { FirstName = "Raj", LastName = "Padilla", Notes = "Hello" };
        var r = new PersonExcludedColumns { FirstName = "Lidia", LastName = "Bain", Notes = "Hello" };
        var s = new PersonExcludedColumns { FirstName = "Derren", LastName = "Southern", Notes = "Hello" };

        var lst = new List<PersonExcludedColumns> { p, q, r };

        var uc = 0;
        var ic = 0;

        Assert.True(await db.UpsertListAsync(
            lst,
            i =>
            {
                i.CreatedOn = DateTime.UtcNow;
                ic++;
            },
            u =>
            {
                u.UpdatedOn = DateTime.UtcNow;
                uc++;
            }
        ));

        Assert.Equal(3, ic);
        Assert.Equal(0, uc);

        p.FirstName = "Emily";
        q.FirstName = "Jim";
        r.FirstName = "Laura";
        lst.Add(s);

        Assert.True(await db.UpsertListAsync(
            lst,
            i =>
            {
                i.CreatedOn = DateTime.UtcNow;
                ic++;
            },
            u =>
            {
                u.UpdatedOn = DateTime.UtcNow;
                uc++;
            }
        ));

        Assert.Equal(4, ic);
        Assert.Equal(3, uc);

        if (p.FullName != null)
        {
            Assert.Equal("Emily Jones", p.FullName);
            Assert.Equal("Jim Padilla", q.FullName);
            Assert.Equal("Laura Bain", r.FullName);
            Assert.Equal("Derren Southern", s.FullName);
        }

        var gp = await db.GetAsync<PersonExcludedColumns>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Null(gp.Notes);
        Assert.InRange(gp.UpdatedOn.Value, dnow.AddMinutes(-1),
            dnow.AddMinutes(1)); // to cover clock skew, delay in DML, etc.
        Assert.InRange(gp.CreatedOn.Value, dnow.AddSeconds(-3),
            dnow.AddSeconds(
                3)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most. )
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);

        var gs = await db.GetAsync<PersonExcludedColumns>(s.IdentityId);
        Assert.Equal(s.IdentityId, gs.IdentityId);
        Assert.Null(gs.Notes);
        Assert.Null(gs.UpdatedOn); // to cover clock skew, delay in DML, etc.
        Assert.InRange(gs.CreatedOn.Value, dnow.AddSeconds(-3),
            dnow.AddSeconds(
                3)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most. )
        Assert.Equal(s.FirstName, gs.FirstName);
        Assert.Equal(s.LastName, gs.LastName);
    }


    [Fact]
    [Trait("Category", "UpsertListAsync")]
    public async Task UpsertListComputedCallbacksPartialUpdateAsync()
    {
        var dnow = DateTime.UtcNow;
        using var db = GetSqlDatabase();
        var p = new PersonExcludedColumns { FirstName = "Alice", LastName = "Jones", Notes = "Hello" };
        var q = new PersonExcludedColumns { FirstName = "Raj", LastName = "Padilla", Notes = "Hello" };
        var r = new PersonExcludedColumns { FirstName = "Lidia", LastName = "Bain", Notes = "Hello" };
        var s = new PersonExcludedColumns { FirstName = "Derren", LastName = "Southern", Notes = "Hello" };

        var lst = new List<PersonExcludedColumns> { p, q, r };

        var uc = 0;
        var ic = 0;

        Assert.True(await db.UpsertListAsync(
            lst,
            i =>
            {
                i.CreatedOn = DateTime.UtcNow;
                ic++;
            },
            u =>
            {
                u.UpdatedOn = DateTime.UtcNow;
                uc++;
            }
        ));

        Assert.Equal(3, ic);
        Assert.Equal(0, uc);

        p.FirstName = "Emily";
        p.LastName = "Smith";
        q.FirstName = "Jim";
        q.LastName = "Jones";
        r.FirstName = "Laura";
        r.LastName = "Williams";
        lst.Add(s);

        Assert.True(await db.UpsertListAsync(
            lst,
            new[] { "FirstName", "CreatedOn", "UpdatedOn" },
            i =>
            {
                i.CreatedOn = DateTime.UtcNow;
                ic++;
            },
            u =>
            {
                u.UpdatedOn = DateTime.UtcNow;
                uc++;
            }
        ));

        Assert.Equal(4, ic);
        Assert.Equal(3, uc);

        if (p.FullName != null)
        {
            Assert.Equal("Emily Jones", p.FullName);
            Assert.Equal("Jim Padilla", q.FullName);
            Assert.Equal("Laura Bain", r.FullName);
            Assert.Equal("Derren Southern", s.FullName);
        }

        var gp = await db.GetAsync<PersonExcludedColumns>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Null(gp.Notes);
        Assert.InRange(gp.UpdatedOn.Value, dnow.AddMinutes(-1),
            dnow.AddMinutes(1)); // to cover clock skew, delay in DML, etc.
        Assert.InRange(gp.CreatedOn.Value, dnow.AddSeconds(-3),
            dnow.AddSeconds(
                3)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most. )
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal("Jones", gp.LastName);

        var gs = await db.GetAsync<PersonExcludedColumns>(s.IdentityId);
        Assert.Equal(s.IdentityId, gs.IdentityId);
        Assert.Null(gs.Notes);
        Assert.Null(gs.UpdatedOn); // to cover clock skew, delay in DML, etc.
        Assert.InRange(gs.CreatedOn.Value, dnow.AddSeconds(-3),
            dnow.AddSeconds(
                3)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most. )
        Assert.Equal(s.FirstName, gs.FirstName);
        Assert.Equal(s.LastName, gs.LastName);
    }

    [Fact]
    [Trait("Category", "UpsertListAsync")]
    public async Task UpsertListTransactionRollbackNoComputedAsync()
    {
        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };
        var s = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Derren", LastName = "Southern" };

        var lst = new List<PersonUniqueIdentifier> { p, q, r };

        using var db = GetSqlDatabase();
        Assert.True(await db.UpsertListAsync(lst));

        using (var t = db.GetTransaction())
        {
            p.FirstName = "Emily";
            q.FirstName = "Jim";
            r.FirstName = "Laura";
            lst.Add(s);

            Assert.True(await db.UpsertListAsync(lst));
            t.Dispose();
        }

        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);


        var gs = await db.GetAsync<PersonUniqueIdentifier>(s.GuidId);

        Assert.Null(gs);
    }

    [Fact]
    [Trait("Category", "UpsertListAsync")]
    public async Task UpsertListTransactionNoComputedThrowsExceptionAsync()
    {
        Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

        var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
        var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };
        var s = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Derren", LastName = "Southern" };

        var lst = new List<PersonUniqueIdentifier> { p, q, r };

        using var db = GetSqlDatabase();
        Assert.True(await db.UpsertListAsync(lst));

        using (var t = db.GetTransaction())
        {
            p.FirstName = "Emily";
            q.FirstName = "a".PadRight(101, 'a');
            r.FirstName = "Laura";
            lst.Add(s);

            await Assert.ThrowsAnyAsync<Exception>(() => db.UpsertListAsync(lst));
        }


        var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

        Assert.Equal("Alice", gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);

        var gs = await db.GetAsync<PersonUniqueIdentifier>(s.GuidId);

        Assert.Null(gs);
    }
}

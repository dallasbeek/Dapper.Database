﻿using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "Transaction")]
    public void TransactionDispose()
    {
        //if (GetProvider() == Provider.SQLite) return;

        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        Assert.True(db.Insert(p));

        using (db.GetTransaction())
        {
            var gp = db.Get<PersonIdentity>(p.IdentityId);
            gp.FirstName = "Sally";
            db.Update(gp);
        }

        var agp = db.Get<PersonIdentity>(p.IdentityId); //updates should have been rolled back
        Assert.Equal("Alice", agp.FirstName);
    }

    [Fact]
    [Trait("Category", "Transaction")]
    public void TransactionAutoDispose()
    {
        //if (GetProvider() == Provider.SQLite) return;

        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        Assert.True(db.Insert(p));

        using (db.GetTransaction())
        {
            var gp = db.Get<PersonIdentity>(p.IdentityId);
            gp.FirstName = "Sally";
            db.Update(gp);
        }

        var agp = db.Get<PersonIdentity>(p.IdentityId); //updates should have been rolled back
        Assert.Equal("Alice", agp.FirstName);
    }

    [Fact]
    [Trait("Category", "Transaction")]
    public void TransactionCommit()
    {
        //if (GetProvider() == Provider.SQLite) return;

        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        Assert.True(db.Insert(p));
        using (var tran = db.GetTransaction())
        {
            var gp = db.Get<PersonIdentity>(p.IdentityId);
            gp.FirstName = "Sally";
            db.Update(gp);
            tran.Complete();
        }

        var agp = db.Get<PersonIdentity>(p.IdentityId); //updates should have been rolled back
        Assert.Equal("Sally", agp.FirstName);
    }

    [Fact]
    [Trait("Category", "Transaction")]
    public void NestedTransactionCommitInnerRollbackOuter()
    {
        //if (GetProvider() == Provider.SQLite) return;

        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
        Assert.True(db.Insert(p));
        using (db.GetTransaction())
        {
            using (var tran = db.GetTransaction())
            {
                var gp = db.Get<PersonIdentity>(p.IdentityId);
                gp.FirstName = "Sally";
                db.Update(gp);
                tran.Complete();
            }
        }

        var agp = db.Get<PersonIdentity>(p.IdentityId); //updates should have been rolled back
        Assert.Equal("Alice", agp.FirstName);
    }
}

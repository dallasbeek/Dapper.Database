using System;
using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "DeleteAsync")]
    public async Task DeleteIdentityEntityAsync()
    {
        using var db = GetSqlDatabase();
        var pOther = new PersonIdentity { FirstName = "OtherAliceAsync", LastName = "OtherJones" };
        var p = new PersonIdentity { FirstName = "AliceAsync", LastName = "Jones" };
        Assert.True(await db.InsertAsync(p));
        Assert.True(p.IdentityId > 0);
        Assert.True(await db.InsertAsync(pOther));
        Assert.True(pOther.IdentityId > 0);

        Assert.True(await db.DeleteAsync(p));

        var gp = await db.GetAsync<PersonIdentity>(p.IdentityId);
        var gpOther = await db.GetAsync<PersonIdentity>(pOther.IdentityId);
        Assert.Null(gp);
        Assert.NotNull(gpOther);
    }

    [Fact]
    [Trait("Category", "DeleteAsync")]
    public async Task DeleteUniqueIdentifierEntityAsync()
    {
        using var db = GetSqlDatabase();
        var pOther = new PersonUniqueIdentifier
        {
            GuidId = Guid.NewGuid(), FirstName = "OtherAliceAsync", LastName = "OtherJones"
        };
        var p = new PersonUniqueIdentifier
        {
            GuidId = Guid.NewGuid(), FirstName = "AliceAsync", LastName = "Jones"
        };
        Assert.True(await db.InsertAsync(p));
        Assert.True(await db.InsertAsync(pOther));
        Assert.True(await db.DeleteAsync(p));

        var gp = await db.GetAsync(p);
        var gpOther = await db.GetAsync(pOther);
        Assert.Null(gp);
        Assert.NotNull(gpOther);
    }

    [Fact]
    [Trait("Category", "DeleteAsync")]
    public async Task DeleteUniqueIdentifierWithAliasesAsync()
    {
        using var db = GetSqlDatabase();
        var pOther = new PersonUniqueIdentifierWithAliases
        {
            GuidId = Guid.NewGuid(), First = "OtherAliceAsync", Last = "OtherJones"
        };
        var p = new PersonUniqueIdentifierWithAliases
        {
            GuidId = Guid.NewGuid(), First = "AliceAsync", Last = "Jones"
        };
        Assert.True(await db.InsertAsync(p));
        Assert.True(await db.InsertAsync(pOther));
        Assert.True(await db.DeleteAsync(p));

        var gp = await db.GetAsync(p);
        var gpOther = await db.GetAsync(pOther);
        Assert.Null(gp);
        Assert.NotNull(gpOther);
    }

    [Fact]
    [Trait("Category", "DeleteAsync")]
    public async Task DeleteIdentityAsync()
    {
        using var db = GetSqlDatabase();
        var pOther = new PersonIdentity { FirstName = "OtherAliceAsync", LastName = "OtherJones" };
        var p = new PersonIdentity { FirstName = "AliceAsync", LastName = "Jones" };
        Assert.True(await db.InsertAsync(p));
        Assert.True(p.IdentityId > 0);
        Assert.True(await db.InsertAsync(pOther));
        Assert.True(pOther.IdentityId > 0);

        Assert.True(await db.DeleteAsync<PersonIdentity>(p.IdentityId));

        var gp = await db.GetAsync(p);
        var gpOther = await db.GetAsync(pOther);
        Assert.Null(gp);
        Assert.NotNull(gpOther);
    }

    [Fact]
    [Trait("Category", "DeleteAsync")]
    public async Task DeleteAliasIdentityAsync()
    {
        using var db = GetSqlDatabase();
        var pOther = new PersonIdentityAlias { First = "OtherAliceAsync", Last = "OtherJones" };
        var p = new PersonIdentityAlias { First = "AliceAsync", Last = "Jones" };
        Assert.True(await db.InsertAsync(p));
        Assert.True(p.Id > 0);
        Assert.True(await db.InsertAsync(pOther));
        Assert.True(pOther.Id > 0);

        Assert.True(await db.DeleteAsync<PersonIdentityAlias>(p.Id));

        var gp = await db.GetAsync(p);
        var gpOther = await db.GetAsync(pOther);
        Assert.Null(gp);
        Assert.NotNull(gpOther);
    }

    [Fact]
    [Trait("Category", "DeleteAsync")]
    public async Task DeleteUniqueIdentifierAsync()
    {
        using var db = GetSqlDatabase();
        var pOther = new PersonUniqueIdentifier
        {
            GuidId = Guid.NewGuid(), FirstName = "OtherAliceAsync", LastName = "OtherJones"
        };
        var p = new PersonUniqueIdentifier
        {
            GuidId = Guid.NewGuid(), FirstName = "AliceAsync", LastName = "Jones"
        };
        Assert.True(await db.InsertAsync(p));
        Assert.True(await db.InsertAsync(pOther));
        Assert.True(await db.DeleteAsync<PersonUniqueIdentifier>(p.GuidId));

        var gp = await db.GetAsync(p);
        var gpOther = await db.GetAsync(pOther);
        Assert.Null(gp);
        Assert.NotNull(gpOther);
    }

    [Fact]
    [Trait("Category", "DeleteAsync")]
    public async Task DeletePersonCompositeKeyAsync()
    {
        using var db = GetSqlDatabase();
        var pOther = new PersonCompositeKey
        {
            GuidId = Guid.NewGuid(),
            StringId = "testOther",
            FirstName = "OtherAliceAsync",
            LastName = "OtherJones"
        };
        var p = new PersonCompositeKey
        {
            GuidId = Guid.NewGuid(), StringId = "test", FirstName = "AliceAsync", LastName = "Jones"
        };
        Assert.True(await db.InsertAsync(p));
        Assert.True(await db.InsertAsync(pOther));

        Assert.True(await db.DeleteAsync<PersonCompositeKey>($"where GuidId = {P}GuidId and StringId = {P}StringId",
            p));

        var gp = await db.GetAsync(p);
        var gpOther = await db.GetAsync(pOther);
        Assert.Null(gp);
        Assert.NotNull(gpOther);
    }

    [Fact]
    [Trait("Category", "DeleteAsync")]
    public async Task DeletePersonCompositeKeyWithAliasesAsync()
    {
        using var db = GetSqlDatabase();
        var sharedGuidId = Guid.NewGuid();
        var pOther = new PersonCompositeKeyWithAliases
        {
            GuidId = sharedGuidId, StringId = "Other P", First = "Other AliceAsync", Last = "Other Jones"
        };
        var p = new PersonCompositeKeyWithAliases
        {
            GuidId = sharedGuidId, StringId = "P", First = "AliceAsync", Last = "Jones"
        };
        Assert.True(await db.InsertAsync(pOther));
        Assert.True(await db.InsertAsync(p));
        Assert.True(await db.DeleteAsync(p));
        var gp = await db.GetAsync(p);
        var gpOther = await db.GetAsync(pOther);
        Assert.Null(gp);
        Assert.NotNull(gpOther);
    }

    [Fact]
    [Trait("Category", "DeleteAsync")]
    public async Task DeleteAllAsync()
    {
        using var db = GetSqlDatabase();
        var pOther = new PersonCompositeKey
        {
            GuidId = Guid.NewGuid(),
            StringId = "testOther",
            FirstName = "OtherAliceAsync",
            LastName = "OtherJones"
        };
        var p = new PersonCompositeKey
        {
            GuidId = Guid.NewGuid(), StringId = "test", FirstName = "AliceAsync", LastName = "Jones"
        };
        Assert.True(await db.InsertAsync(p));
        Assert.True(await db.InsertAsync(pOther));

        Assert.True(await db.DeleteAllAsync<PersonCompositeKey>());

        Assert.Equal(0, await db.CountAsync<PersonCompositeKey>());
    }

    [Fact]
    [Trait("Category", "DeleteAsync")]
    public async Task DeleteWhereClauseAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonIdentity { FirstName = "DeleteAsync", LastName = "Me" };

        for (var i = 0; i < 10; i++) Assert.True(await db.InsertAsync(p));
        var pOther = new PersonIdentity { FirstName = "DeleteOtherAsync", LastName = "MeOther" };

        Assert.True(await db.InsertAsync(pOther));

        Assert.Equal(10, await db.CountAsync<PersonIdentity>("where FirstName = 'DeleteAsync'"));
        Assert.Equal(1, await db.CountAsync<PersonIdentity>("where FirstName = 'DeleteOtherAsync'"));

        Assert.True(await db.DeleteAsync<PersonIdentity>("where FirstName = 'DeleteAsync'"));

        Assert.Equal(0, await db.CountAsync<PersonIdentity>("where FirstName = 'DeleteAsync'"));
        //Ensure that this did not delete rows it shouldn't have from the database.
        Assert.Equal(1, await db.CountAsync<PersonIdentity>("where FirstName = 'DeleteOtherAsync'"));
    }
}

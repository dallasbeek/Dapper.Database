using System;
using System.Threading.Tasks;
using Dapper.Database.Extensions;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteIdentityEntityAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var pOther = new PersonIdentity { FirstName = "OtherAlice", LastName = "OtherJones" };
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(p.IdentityId > 0);
                Assert.True(await connection.InsertAsync(pOther));
                Assert.True(pOther.IdentityId > 0);

                Assert.True(await connection.DeleteAsync(p));

                var gp = await connection.GetAsync<PersonIdentity>(p.IdentityId);
                Assert.Null(gp);
                var gpOther = await connection.GetAsync(pOther);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteUniqueIdentifierEntityAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var pOther = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "OtherAlice", LastName = "OtherJones" };
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(await connection.InsertAsync(pOther));
                Assert.True(await connection.DeleteAsync(p));

                var gp = await connection.GetAsync(p);
                Assert.Null(gp);
                var gpOther = await connection.GetAsync(pOther);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteUniqueIdentifierWithAliasesAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var pOther = new PersonUniqueIdentifierWithAliases { GuidId = Guid.NewGuid(), First = "OtherAlice", Last = "OtherJones" };
                var p = new PersonUniqueIdentifierWithAliases { GuidId = Guid.NewGuid(), First = "Alice", Last = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(await connection.InsertAsync(pOther));
                Assert.True(await connection.DeleteAsync(p));

                var gp = await connection.GetAsync(p);
                Assert.Null(gp);
                var gpOther = await connection.GetAsync(pOther);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteIdentityAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var pOther = new PersonIdentity { FirstName = "OtherAlice", LastName = "OtherJones" };
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(p.IdentityId > 0);
                Assert.True(await connection.InsertAsync(pOther));
                Assert.True(pOther.IdentityId > 0);

                Assert.True(await connection.DeleteAsync<PersonIdentity>(p.IdentityId));

                var gp = await connection.GetAsync(p);
                Assert.Null(gp);
                var gpOther = await connection.GetAsync(pOther);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteAliasIdentityAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var pOther = new PersonIdentityAlias { First = "OtherAlice", Last = "OtherJones" };
                var p = new PersonIdentityAlias { First = "Alice", Last = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(p.Id > 0);
                Assert.True(await connection.InsertAsync(pOther));
                Assert.True(pOther.Id > 0);

                Assert.True(await connection.DeleteAsync<PersonIdentityAlias>(p.Id));

                var gp = await connection.GetAsync(p);
                Assert.Null(gp);
                var gpOther = await connection.GetAsync(pOther);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteUniqueIdentifierAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var pOther = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "OtherAlice", LastName = "OtherJones" };
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(await connection.InsertAsync(pOther));
                Assert.True(await connection.DeleteAsync<PersonUniqueIdentifier>(p.GuidId));

                var gp = await connection.GetAsync(p);
                Assert.Null(gp);
                var gpOther = await connection.GetAsync(pOther);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeletePersonCompositeKeyAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var pOther = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "testOther", FirstName = "OtherAlice", LastName = "OtherJones" };
                var p = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "test", FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(await connection.InsertAsync(pOther));

                Assert.True(await connection.DeleteAsync<PersonCompositeKey>($"where GuidId = {P}GuidId and StringId = {P}StringId", p));

                var gp = await connection.GetAsync(p);
                Assert.Null(gp);
                var gpOther = await connection.GetAsync(pOther);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeletePersonCompositeKeyWithAliasesAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var sharedGuidId = Guid.NewGuid();
                var pOther = new PersonCompositeKeyWithAliases
                {
                    GuidId = sharedGuidId,
                    StringId = "Other P",
                    First = "Other Alice",
                    Last = "Other Jones"
                };
                var p = new PersonCompositeKeyWithAliases
                {
                    GuidId = sharedGuidId,
                    StringId = "P",
                    First = "Alice",
                    Last = "Jones"
                };
                Assert.True(await db.InsertAsync(pOther));
                Assert.True(await db.InsertAsync(p));
                Assert.True(await db.DeleteAsync(p));
                var gp = db.Get(p);
                var gpOther = db.Get(pOther);
                Assert.Null(gp);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteAllAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var pOther = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "testOther", FirstName = "OtherAlice", LastName = "OtherJones" };
                var p = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "test", FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(await connection.InsertAsync(pOther));

                Assert.True(await connection.DeleteAllAsync<PersonCompositeKey>());

                Assert.Equal(0, await connection.CountAsync<PersonCompositeKey>());
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteWhereClauseAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = new PersonIdentity { FirstName = "DeleteAsync", LastName = "Me" };

                for (var i = 0; i < 10; i++)
                {
                    Assert.True(await connection.InsertAsync(p));
                }
                var pOther = new PersonIdentity { FirstName = "DeleteOtherAsync", LastName = "MeOther" };

                Assert.True(await connection.InsertAsync(pOther));

                Assert.Equal(10, await connection.CountAsync<PersonIdentity>("where FirstName = 'DeleteAsync'"));
                Assert.Equal(1, await connection.CountAsync<PersonIdentity>("where FirstName = 'DeleteOtherAsync'"));

                Assert.True(await connection.DeleteAsync<PersonIdentity>("where FirstName = 'DeleteAsync'"));

                Assert.Equal(0, await connection.CountAsync<PersonIdentity>("where FirstName = 'DeleteAsync'"));
                //Ensure that this did not delete rows it shouldn't have from the database.
                Assert.Equal(1, await connection.CountAsync<PersonIdentity>("where FirstName = 'DeleteOtherAsync'"));
            }
        }

    }
}

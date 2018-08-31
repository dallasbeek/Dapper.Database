using System;
using System.Threading.Tasks;
using Dapper.Database.Extensions;
using Xunit;
using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;

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
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(p.IdentityId > 0);

                Assert.True(await connection.DeleteAsync(p));

                var gp = await connection.GetAsync<PersonIdentity>(p.IdentityId);

                Assert.Null(gp);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteUniqueIdentifierEntityAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(await connection.DeleteAsync(p));

                var gp = await connection.GetAsync(p);
                Assert.Null(gp);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteUniqueIdentifierWithAliasesAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifierWithAliases { GuidId = Guid.NewGuid(), First = "Alice", Last = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(await connection.DeleteAsync(p));

                var gp = await connection.GetAsync(p);
                Assert.Null(gp);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteIdentityAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(p.IdentityId > 0);

                Assert.True(await connection.DeleteAsync<PersonIdentity>(p.IdentityId));

                var gp = await connection.GetAsync(p);
                Assert.Null(gp);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteAliasIdentityAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = new PersonIdentityAlias { First = "Alice", Last = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(p.Id > 0);

                Assert.True(await connection.DeleteAsync<PersonIdentityAlias>(p.Id));

                var gp = await connection.GetAsync(p);
                Assert.Null(gp);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteUniqueIdentifierAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(await connection.DeleteAsync<PersonIdentity>(p.GuidId));

                var gp = await connection.GetAsync(p);
                Assert.Null(gp);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeletePersonCompositeKeyAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "test", FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));

                Assert.True(await connection.DeleteAsync<PersonCompositeKey>($"where GuidId = {P}GuidId and StringId = {P}StringId", p));

                var gp = await connection.GetAsync(p);
                Assert.Null(gp);
            }
        }


        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteAllAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "test", FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));

                Assert.True(await connection.DeleteAsync<PersonCompositeKey>());

                Assert.Equal(0, await connection.CountAsync<PersonCompositeKey>());
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteWhereClauseAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = new PersonIdentity { FirstName = "Delete", LastName = "Me" };

                for (var i = 0; i < 10; i++)
                {
                    Assert.True(await connection.InsertAsync(p));
                }

                Assert.Equal(10, await connection.CountAsync<PersonIdentity>("where FirstName = 'Delete'"));

                Assert.True(await connection.DeleteAsync<PersonIdentity>("where FirstName = 'Delete'"));

                Assert.Equal(0, await connection.CountAsync<PersonIdentity>("where FirstName = 'Delete'"));
            }
        }

    }
}

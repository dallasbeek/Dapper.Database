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
        [Trait("Category", "UpdateAsync")]
        public async Task UpdateIdentityAsync()
        {
            using (var db = GetSqlDatabase())
            {
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
        }

        [Fact]
        [Trait("Category", "UpdateAsync")]
        public async Task UpdateUniqueIdentifierAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(await db.InsertAsync(p));

                p.FirstName = "Greg";
                p.LastName = "Smith";
                Assert.True(await db.UpdateAsync(p));

                var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateAsync")]
        public async Task UpdateUniqueIdentifierWithAliasesAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifierWithAliases { GuidId = Guid.NewGuid(), First = "Alice", Last = "Jones" };
                Assert.True(await db.InsertAsync(p));

                p.First = "Greg";
                p.Last = "Smith";
                Assert.True(await db.UpdateAsync(p));

                var gp = await db.GetAsync<PersonUniqueIdentifierWithAliases>(p.GuidId);

                Assert.Equal(p.First, gp.First);
                Assert.Equal(p.Last, gp.Last);
            }
        }

        [Fact]
        [Trait("Category", "UpdateAsync")]
        public async Task UpdatePersonCompositeKeyAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "test", FirstName = "Alice", LastName = "Jones" };
                Assert.True(await db.InsertAsync(p));

                p.FirstName = "Greg";
                p.LastName = "Smith";
                Assert.True(await db.UpdateAsync(p));

                var gp = await db.GetAsync<PersonCompositeKey>($"where GuidId = {P}GuidId and StringId = {P}StringId", p);

                Assert.Equal(p.StringId, gp.StringId);
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateAsync")]
        public async Task UpdateComputedAsync()
        {

            var dnow = DateTime.UtcNow;
            using (var db = GetSqlDatabase())
            {
                var p = new PersonExcludedColumns { FirstName = "Alice", LastName = "Jones", Notes = "Hello", CreatedOn = dnow, UpdatedOn = dnow };
                Assert.True(await db.InsertAsync(p));

                if (p.FullName != null)
                {
                    Assert.Equal("Alice Jones", p.FullName);
                }

                p.FirstName = "Greg";
                p.LastName = "Smith";
                p.CreatedOn = DateTime.UtcNow;
                Assert.True(await db.UpdateAsync(p));
                if (p.FullName != null)
                {
                    Assert.Equal("Greg Smith", p.FullName);
                }

                var gp = await db.GetAsync<PersonExcludedColumns>(p.IdentityId);

                Assert.Equal(p.IdentityId, gp.IdentityId);
                Assert.Null(gp.Notes);
                Assert.InRange(gp.CreatedOn.Value, dnow.AddSeconds(-1), dnow.AddSeconds(1)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most. )
                Assert.InRange(gp.UpdatedOn.Value, dnow.AddMinutes(-1), dnow.AddMinutes(1)); // to cover clock skew, delay in DML, etc.
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateAsync")]
        public async Task UpdateComputedAliasAsync()
        {

            var dnow = DateTime.UtcNow;
            using (var db = GetSqlDatabase())
            {
                var p = new PersonIdentityAlias { First = "Alice", Last = "Jones" };
                Assert.True(await db.InsertAsync(p));

                if (p.Name != null)
                {
                    Assert.Equal("Alice Jones", p.Name);
                }

                p.First = "Greg";
                p.Last = "Smith";

                Assert.True(await db.UpdateAsync(p));
                if (p.Name != null)
                {
                    Assert.Equal("Greg Smith", p.Name);
                }

                var gp = await db.GetAsync<PersonIdentityAlias>(p.Id);

                Assert.Equal(p.Id, gp.Id);
                Assert.Equal(p.First, gp.First);
                Assert.Equal(p.Last, gp.Last);
                Assert.Equal(p.Name, gp.Name);

                Assert.True(await db.DeleteAsync<PersonIdentityAlias>(p.Id));

                var dp = await db.GetAsync(p);
                Assert.Null(dp);

            }
        }

        [Fact]
        [Trait("Category", "UpdateAsync")]
        public async Task UpdatePartialAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(await db.InsertAsync(p));
                Assert.True(p.IdentityId > 0);

                p.FirstName = "Greg";
                p.LastName = "Smith";
                Assert.True(await db.UpdateAsync(p, new string[] { "LastName" }));

                var gp = await db.GetAsync<PersonIdentity>(p.IdentityId);

                Assert.Equal(p.IdentityId, gp.IdentityId);
                Assert.Equal("Alice", gp.FirstName);
                Assert.Equal("Smith", gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateAsync")]
        public async Task UpdateConcurrencyCheckAsync()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonConcurrencyCheck { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones", StringId = "abc" };
                Assert.True(await db.InsertAsync(p));

                p.FirstName = "Greg";
                p.LastName = "Smith";
                Assert.True(await db.UpdateAsync(p), "StringId unchanged");

                // Modify one of the concurrency-check columns to simulate it changing out from underneath us.
                await db.ExecuteAsync("update Person set StringId = 'xyz' where GuidId = @GuidId", p);

                p.FirstName = "Alice";
                p.LastName = "Jones";
                Assert.False(await db.UpdateAsync(p), "StringId changed elsewhere, update not permitted");

                var gp = await db.GetAsync<PersonConcurrencyCheck>(p.GuidId);

                Assert.Equal("Greg", gp.FirstName);
                Assert.Equal("Smith", gp.LastName);
            }
        }
    }
}

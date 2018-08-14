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
        [Trait("Category", "InsertAsync")]
        public async Task InsertIdentityAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                Assert.True(p.IdentityId > 0);
                var gp = await connection.GetAsync<PersonIdentity>(p.IdentityId);

                Assert.Equal(p.IdentityId, gp.IdentityId);
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "InsertAsync")]
        public async Task InsertUniqueIdentifierAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                var gp = await connection.GetAsync<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "InsertAsync")]
        public async Task InsertPersonCompositeKeyAsync()
        {
            using (var connection = GetSqlDatabase())
            {
                var gid = Guid.NewGuid();
                var p = new PersonCompositeKey { GuidId = gid, StringId = "test", FirstName = "Alice", LastName = "Jones" };
                Assert.True(await connection.InsertAsync(p));
                var gp = await connection.GetAsync<PersonCompositeKey>("where GuidId = @GuidId and StringId = @StringId", p);

                Assert.Equal(p.StringId, gp.StringId);
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "InsertAsync")]
        public async Task InsertComputedAsync()
        {

            var dnow = DateTime.UtcNow;
            using (var connection = GetSqlDatabase())
            {
                var p = new PersonExcludedColumns { FirstName = "Alice", LastName = "Jones", Notes = "Hello", CreatedOn = dnow, UpdatedOn = dnow };
                Assert.True(await connection.InsertAsync(p));

                if (p.FullName != null)
                {
                    Assert.Equal("Alice Jones", p.FullName);
                }

                var gp = await connection.GetAsync<PersonExcludedColumns>(p.IdentityId);

                Assert.Equal(p.IdentityId, gp.IdentityId);
                Assert.Null(gp.Notes);
                Assert.Null(gp.UpdatedOn);
                Assert.Equal(dnow.ToString("yyyy-MM-dd HH:mm"), gp.CreatedOn.Value.ToString("yyyy-MM-dd HH:mm"));
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }
    }
}

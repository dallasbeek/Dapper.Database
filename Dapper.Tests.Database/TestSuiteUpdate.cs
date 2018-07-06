using System;
using Dapper.Database.Extensions;
using Xunit;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "Update")]
        public void UpdateIdentity()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(connection.Insert(p));
                Assert.True(p.IdentityId > 0);

                p.FirstName = "Greg";
                p.LastName = "Smith";
                Assert.True(connection.Update(p));

                var gp = connection.Get<PersonIdentity>(p.IdentityId);

                Assert.Equal(p.IdentityId, gp.IdentityId);
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "Update")]
        public void UpdateUniqueIdentifier()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(connection.Insert(p));

                p.FirstName = "Greg";
                p.LastName = "Smith";
                Assert.True(connection.Update(p));

                var gp = connection.Get<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "Update")]
        public void UpdatePersonCompositeKey()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "test", FirstName = "Alice", LastName = "Jones" };
                Assert.True(connection.Insert(p));

                p.FirstName = "Greg";
                p.LastName = "Smith";
                Assert.True(connection.Update(p));

                var gp = connection.Get<PersonCompositeKey>("where GuidId = @GuidId and StringId = @StringId", p);

                Assert.Equal(p.StringId, gp.StringId);
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "Update")]
        public void UpdateComputed()
        {

            var dnow = DateTime.UtcNow;
            using (var connection = GetOpenConnection())
            {
                var p = new PersonExcludedColumns {FirstName = "Alice", LastName = "Jones", Notes = "Hello", CreatedOn = dnow, UpdatedOn = dnow};
                Assert.True(connection.Insert(p));

                if (p.FullName != null)
                {
                    Assert.Equal("Alice Jones", p.FullName);
                }

                p.FirstName = "Greg";
                p.LastName = "Smith";
                p.CreatedOn = DateTime.UtcNow;
                Assert.True(connection.Update(p));
                if (p.FullName != null)
                {
                    Assert.Equal("Greg Smith", p.FullName);
                }

                var gp = connection.Get<PersonExcludedColumns>(p.IdentityId);

                Assert.Equal(p.IdentityId, gp.IdentityId);
                Assert.Null(gp.Notes);
                Assert.Equal(dnow.ToString("yyyy-MM-dd HH:mm"), gp.UpdatedOn.Value.ToString("yyyy-MM-dd HH:mm"));
                Assert.Equal(dnow.ToString("yyyy-MM-dd HH:mm"), gp.CreatedOn.Value.ToString("yyyy-MM-dd HH:mm"));
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "Update")]
        public void UpdatePartial()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(connection.Insert(p));
                Assert.True(p.IdentityId > 0);

                p.FirstName = "Greg";
                p.LastName = "Smith";
                Assert.True(connection.Update(p, new string[] { "LastName" }));

                var gp = connection.Get<PersonIdentity>(p.IdentityId);

                Assert.Equal(p.IdentityId, gp.IdentityId);
                Assert.Equal("Alice", gp.FirstName);
                Assert.Equal("Smith", gp.LastName);
            }
        }


    }
}

using System;
using Dapper.Database.Extensions;
using Xunit;


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "Insert")]
        public void InsertIdentity()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(connection.Insert(p));
                Assert.True(p.IdentityId > 0);
                var gp = connection.Get<PersonIdentity>(p.IdentityId);

                Assert.Equal(p.IdentityId, gp.IdentityId);
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "Insert")]
        public void InsertUniqueIdentifier()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(connection.Insert(p));
                var gp = connection.Get<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal(p.GuidId, gp.GuidId);
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "Insert")]
        public void InsertPersonCompositeKey()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "test", FirstName = "Alice", LastName = "Jones" };
                Assert.True(connection.Insert(p));
                var gp = connection.Get<PersonCompositeKey>("where GuidId = @GuidId and StringId = @StringId", p);

                Assert.Equal(p.GuidId, gp.GuidId);
                Assert.Equal(p.StringId, gp.StringId);
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "Insert")]
        public void InsertComputed()
        {

            var dnow = DateTime.Now;
            using (var connection = GetOpenConnection())
            {
                var p = new PersonExcludedColumns {FirstName = "Alice", LastName = "Jones", Notes = "Hello", CreatedOn = dnow, UpdatedOn = dnow};
                Assert.True(connection.Insert(p));

                Assert.Equal("Alice Jones", p.FullName);
                var gp = connection.Get<PersonExcludedColumns>(p.IdentityId);

                Assert.Equal(p.IdentityId, gp.IdentityId);
                Assert.Null(gp.Notes);
                Assert.Null(gp.UpdatedOn);
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }
    }
}

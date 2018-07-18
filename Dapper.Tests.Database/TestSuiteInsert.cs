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
            using (var db = GetSqlDatabase())
            {
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(db.Insert(p));
                Assert.True(p.IdentityId > 0);
                var gp = db.Get<PersonIdentity>(p.IdentityId);

                Assert.Equal(p.IdentityId, gp.IdentityId);
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "Insert")]
        public void InsertUniqueIdentifier()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(db.Insert(p));
                var gp = db.Get<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "Insert")]
        public void InsertPersonCompositeKey()
        {
            using (var db = GetSqlDatabase())
            {
                var gid = new Guid("45441d7b-867d-2c4a-9cb0-1d82b5ef11f2");
                var p = new PersonCompositeKey { GuidId = gid, StringId = "test", FirstName = "Alice", LastName = "Jones" };
                Assert.True(db.Insert(p));
                var gp = db.Get<PersonCompositeKey>("where GuidId = @GuidId and StringId = @StringId", p);

                Assert.Equal(p.StringId, gp.StringId);
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "Insert")]
        public void InsertComputed()
        {

            var dnow = DateTime.UtcNow;
            using (var db = GetSqlDatabase())
            {
                var p = new PersonExcludedColumns {FirstName = "Alice", LastName = "Jones", Notes = "Hello", CreatedOn = dnow, UpdatedOn = dnow};
                Assert.True(db.Insert(p));

                if (p.FullName != null)
                {
                    Assert.Equal("Alice Jones", p.FullName);
                }

                var gp = db.Get<PersonExcludedColumns>(p.IdentityId);

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

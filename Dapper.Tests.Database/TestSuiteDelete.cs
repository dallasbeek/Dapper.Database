using System;
using Dapper.Database.Extensions;
using Xunit;


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteIdentityEntity()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(connection.Insert(p));
                Assert.True(p.IdentityId > 0);

                Assert.True(connection.Delete(p));

                var gp = connection.Get<PersonIdentity>(p.IdentityId);

                Assert.Null(gp);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteUniqueIdentifierEntity()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(connection.Insert(p));
                Assert.True(connection.Delete(p));

                var gp = connection.Get(p);
                Assert.Null(gp);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteIdentity()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(connection.Insert(p));
                Assert.True(p.IdentityId > 0);

                Assert.True(connection.Delete<PersonIdentity>(p.IdentityId));

                var gp = connection.Get(p);
                Assert.Null(gp);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteUniqueIdentifier()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(connection.Insert(p));
                Assert.True(connection.Delete<PersonIdentity>(p.GuidId));

                var gp = connection.Get(p);
                Assert.Null(gp);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeletePersonCompositeKey()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "test", FirstName = "Alice", LastName = "Jones" };
                Assert.True(connection.Insert(p));

                Assert.True(connection.Delete<PersonCompositeKey>("where GuidId = @GuidId and StringId = @StringId", p));

                var gp = connection.Get(p);
                Assert.Null(gp);
            }
        }


        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteAll()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "test", FirstName = "Alice", LastName = "Jones" };
                Assert.True(connection.Insert(p));

                Assert.True(connection.Delete<PersonCompositeKey>());

                Assert.Equal(0, connection.Count<PersonCompositeKey>());
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteWhereClause()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new PersonIdentity { FirstName = "Delete", LastName = "Me" };

                for(var i = 0; i < 10; i++)
                {
                    Assert.True(connection.Insert(p));
                }

                Assert.Equal(10, connection.Count<PersonIdentity>("where FirstName = 'Delete'"));

                Assert.True(connection.Delete<PersonIdentity>("where FirstName = 'Delete'"));

                Assert.Equal(0, connection.Count<PersonIdentity>("where FirstName = 'Delete'"));
            }
        }

    }
}

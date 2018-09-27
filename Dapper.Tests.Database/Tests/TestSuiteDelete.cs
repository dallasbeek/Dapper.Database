﻿using System;
using Dapper.Database.Extensions;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteIdentityEntity()
        {
            using (var db = GetSqlDatabase())
            {
                var pOther = new PersonIdentity { FirstName = "OtherAlice", LastName = "OtherJones" };
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(db.Insert(p));
                Assert.True(p.IdentityId > 0);
                Assert.True(db.Insert(pOther));
                Assert.True(pOther.IdentityId > 0);

                Assert.True(db.Delete(p));

                var gp = db.Get<PersonIdentity>(p.IdentityId);
                var gpOther = db.Get<PersonIdentity>(pOther.IdentityId);
                Assert.Null(gp);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteUniqueIdentifierEntity()
        {
            using (var db = GetSqlDatabase())
            {
                var pOther = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "OtherAlice", LastName = "OtherJones" };
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(db.Insert(p));
                Assert.True(db.Insert(pOther));
                Assert.True(db.Delete(p));

                var gp = db.Get(p);
                var gpOther = db.Get(pOther);
                Assert.Null(gp);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteUniqueIdentifierWithAliases()
        {
            using (var db = GetSqlDatabase())
            {
                var pOther = new PersonUniqueIdentifierWithAliases { GuidId = Guid.NewGuid(), First = "OtherAlice", Last = "OtherJones" };
                var p = new PersonUniqueIdentifierWithAliases { GuidId = Guid.NewGuid(), First = "Alice", Last = "Jones" };
                Assert.True(db.Insert(p));
                Assert.True(db.Insert(pOther));
                Assert.True(db.Delete(p));

                var gp = db.Get(p);
                var gpOther = db.Get(pOther);
                Assert.Null(gp);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteIdentity()
        {
            using (var db = GetSqlDatabase())
            {
                var pOther = new PersonIdentity { FirstName = "OtherAlice", LastName = "OtherJones" };
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                Assert.True(db.Insert(p));
                Assert.True(p.IdentityId > 0);
                Assert.True(db.Insert(pOther));
                Assert.True(pOther.IdentityId > 0);

                Assert.True(db.Delete<PersonIdentity>(p.IdentityId));

                var gp = db.Get(p);
                var gpOther = db.Get(pOther);
                Assert.Null(gp);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteAliasIdentity()
        {
            using (var db = GetSqlDatabase())
            {
                var pOther = new PersonIdentityAlias { First = "OtherAlice", Last = "OtherJones" };
                var p = new PersonIdentityAlias { First = "Alice", Last = "Jones" };
                Assert.True(db.Insert(p));
                Assert.True(p.Id > 0);
                Assert.True(db.Insert(pOther));
                Assert.True(pOther.Id > 0);

                Assert.True(db.Delete<PersonIdentityAlias>(p.Id));

                var gp = db.Get(p);
                var gpOther = db.Get(pOther);
                Assert.Null(gp);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteUniqueIdentifier()
        {
            using (var db = GetSqlDatabase())
            {
                var pOther = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "OtherAlice", LastName = "OtherJones" };
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(db.Insert(p));
                Assert.True(db.Insert(pOther));
                Assert.True(db.Delete<PersonUniqueIdentifier>(p.GuidId));

                var gp = db.Get(p);
                var gpOther = db.Get(pOther);
                Assert.Null(gp);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeletePersonCompositeKey()
        {
            using (var db = GetSqlDatabase())
            {
                var pOther = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "testOther", FirstName = "OtherAlice", LastName = "OtherJones" };
                var p = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "test", FirstName = "Alice", LastName = "Jones" };
                Assert.True(db.Insert(p));
                Assert.True(db.Insert(pOther));

                Assert.True(db.Delete<PersonCompositeKey>($"where GuidId = {P}GuidId and StringId = {P}StringId", p));

                var gp = db.Get(p);
                var gpOther = db.Get(pOther);
                Assert.Null(gp);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeletePersonCompositeKeyWithAliases()
        {
            using (var db = GetSqlDatabase())
            {
                var sharedGuidId = Guid.NewGuid();
                var pOther = new PersonCompositeKeyWithAliases { GuidId = sharedGuidId, StringId = "Other P", First = "Other Alice", Last = "Other Jones" };
                var p = new PersonCompositeKeyWithAliases { GuidId = sharedGuidId, StringId = "P", First = "Alice", Last = "Jones" };
                Assert.True(db.Insert(pOther));
                Assert.True(db.Insert(p));
                Assert.True(db.Delete(p));

                var gp = db.Get(p);
                var gpOther = db.Get(pOther);
                Assert.Null(gp);
                Assert.NotNull(gpOther);
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteAll()
        {
            using (var db = GetSqlDatabase())
            {
                var pOther = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "testOther", FirstName = "OtherAlice", LastName = "OtherJones" };
                var p = new PersonCompositeKey { GuidId = Guid.NewGuid(), StringId = "test", FirstName = "Alice", LastName = "Jones" };
                Assert.True(db.Insert(p));
                Assert.True(db.Insert(pOther));

                Assert.True(db.DeleteAll<PersonCompositeKey>());

                Assert.Equal(0, db.Count<PersonCompositeKey>());
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteWhereClause()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonIdentity { FirstName = "Delete", LastName = "Me" };

                for (var i = 0; i < 10; i++)
                {
                    Assert.True(db.Insert(p));
                }
                var pOther = new PersonIdentity { FirstName = "DeleteOther", LastName = "MeOther" };
                Assert.True(db.Insert(pOther));

                Assert.Equal(10, db.Count<PersonIdentity>("where FirstName = 'Delete'"));
                Assert.Equal(1, db.Count<PersonIdentity>("where FirstName = 'DeleteOther'"));

                Assert.True(db.Delete<PersonIdentity>("where FirstName = 'Delete'"));

                Assert.Equal(0, db.Count<PersonIdentity>("where FirstName = 'Delete'"));
                //Ensure that this did not delete rows it shouldn't have from the database.
                Assert.Equal(1, db.Count<PersonIdentity>("where FirstName = 'DeleteOther'"));
            }
        }
    }
}

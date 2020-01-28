using System;
using System.Collections.Generic;
using Dapper.Database.Extensions;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        [Fact]
        [Trait("Category", "UpdateList")]
        public void UpdateEmptyList()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.False(db.UpdateList(new List<PersonUniqueIdentifier>()));
            }
        }

        [Fact]
        [Trait("Category", "UpdateList")]
        public void UpdateListNoComputed()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
                var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

                var lst = new List<PersonUniqueIdentifier> { p, q, r };

                Assert.True(db.InsertList(lst));

                p.FirstName = "Emily";
                q.FirstName = "Jim";
                r.FirstName = "Laura";

                Assert.True(db.UpdateList(lst));

                var gp = db.Get<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal("Emily", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);

            }
        }

        [Fact]
        [Trait("Category", "UpdateList")]
        public void UpdateListNoComputedPartial()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
                var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

                var lst = new List<PersonUniqueIdentifier> { p, q, r };

                Assert.True(db.InsertList(lst));

                p.FirstName = "Emily";
                p.LastName = "Swank";
                q.FirstName = "Jim";
                r.FirstName = "Laura";

                Assert.True(db.UpdateList(lst, new[] { "LastName" }));

                var gp = db.Get<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal("Alice", gp.FirstName);
                Assert.Equal("Swank", gp.LastName);
                Assert.Equal(p.LastName, gp.LastName);

            }
        }

        [Fact]
        [Trait("Category", "UpdateList")]
        public void UpdateListNoComputedThrowsException()
        {
            Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

            using (var db = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
                var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

                var lst = new List<PersonUniqueIdentifier> { p, q, r };

                Assert.True(db.InsertList(lst));

                p.FirstName = "Emily";
                q.FirstName = "a".PadRight(101, 'a');
                r.FirstName = "Laura";

                Assert.ThrowsAny<Exception>(() => db.UpdateList(lst));

                var gp = db.Get<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal("Alice", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateList")]
        public void UpdateListIdentity()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                var q = new PersonIdentity { FirstName = "Raj", LastName = "Padilla" };
                var r = new PersonIdentity { FirstName = "Lidia", LastName = "Bain" };

                var lst = new List<PersonIdentity> { p, q, r };

                Assert.True(db.InsertList(lst));

                p.FirstName = "Emily";
                q.FirstName = "Jim";
                r.FirstName = "Laura";

                Assert.True(db.UpdateList(lst));

                var gp = db.Get<PersonIdentity>(p.IdentityId);

                Assert.Equal("Emily", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateList")]
        public void UpdateListIdentityThrowsException()
        {
            Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

            using (var db = GetSqlDatabase())
            {
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                var q = new PersonIdentity { FirstName = "Raj", LastName = "Padilla" };
                var r = new PersonIdentity { FirstName = "Lidia", LastName = "Bain" };

                var lst = new List<PersonIdentity> { p, q, r };

                Assert.True(db.InsertList(lst));

                p.FirstName = "Emily";
                q.FirstName = "a".PadRight(101, 'a');
                r.FirstName = "Laura";

                Assert.ThrowsAny<Exception>(() => db.UpdateList(lst));

                var gp = db.Get<PersonIdentity>(p.IdentityId);

                Assert.Equal("Alice", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateList")]
        public void UpdateListComputed()
        {

            var dnow = DateTime.UtcNow;
            using (var db = GetSqlDatabase())
            {
                var p = new PersonExcludedColumns { FirstName = "Alice", LastName = "Jones", Notes = "Hello", CreatedOn = dnow, UpdatedOn = dnow };
                var q = new PersonExcludedColumns { FirstName = "Raj", LastName = "Padilla", Notes = "Hello", CreatedOn = dnow, UpdatedOn = dnow };
                var r = new PersonExcludedColumns { FirstName = "Lidia", LastName = "Bain", Notes = "Hello", CreatedOn = dnow, UpdatedOn = dnow };

                var lst = new List<PersonExcludedColumns> { p, q, r };

                Assert.True(db.InsertList(lst));

                p.FirstName = "Emily";
                q.FirstName = "Jim";
                r.FirstName = "Laura";

                Assert.True(db.UpdateList(lst));

                if (p.FullName != null)
                {
                    Assert.Equal("Emily Jones", p.FullName);
                    Assert.Equal("Jim Padilla", q.FullName);
                    Assert.Equal("Laura Bain", r.FullName);
                }

                var gp = db.Get<PersonExcludedColumns>(p.IdentityId);

                Assert.Equal(p.IdentityId, gp.IdentityId);
                Assert.Null(gp.Notes);
                Assert.InRange(gp.UpdatedOn.Value, dnow.AddMinutes(-1), dnow.AddMinutes(1)); // to cover clock skew, delay in DML, etc.
                Assert.InRange(gp.CreatedOn.Value, dnow.AddSeconds(-1), dnow.AddSeconds(1)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most. )
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateList")]
        public void UpdateListTransactionNoComputed()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
                var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

                var lst = new List<PersonUniqueIdentifier> { p, q, r };

                Assert.True(db.InsertList(lst));

                using (var t = db.GetTransaction())
                {
                    p.FirstName = "Emily";
                    q.FirstName = "Jim";
                    r.FirstName = "Laura";

                    Assert.True(db.UpdateList(lst));

                    t.Complete();
                }

                var gp = db.Get<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal("Emily", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateList")]
        public void UpdateListTransactionRollbackNoComputed()
        {
            var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
            var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
            var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

            var lst = new List<PersonUniqueIdentifier> { p, q, r };

            using (var db = GetSqlDatabase())
            {
                Assert.True(db.InsertList(lst));

                using (var t = db.GetTransaction())
                {
                    p.FirstName = "Emily";
                    q.FirstName = "Jim";
                    r.FirstName = "Laura";

                    Assert.True(db.UpdateList(lst));
                    t.Dispose();
                }

                var gp = db.Get<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal("Alice", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateList")]
        public void UpdateListTransactionNoComputedThrowsException()
        {
            Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

            var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
            var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
            var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

            var lst = new List<PersonUniqueIdentifier> { p, q, r };

            using (var db = GetSqlDatabase())
            {
                Assert.True(db.InsertList(lst));

                using (var t = db.GetTransaction())
                {
                    p.FirstName = "Emily";
                    q.FirstName = "a".PadRight(101, 'a');
                    r.FirstName = "Laura";
                    Assert.ThrowsAny<Exception>(() => db.UpdateList(lst));
                }


                var gp = db.Get<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal("Alice", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateList")]
        public void UpdateListConcurrencyCheck()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonConcurrencyCheck { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones", StringId = "aj" };
                var q = new PersonConcurrencyCheck { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla", StringId = "rp" };
                var r = new PersonConcurrencyCheck { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain", StringId = "lb" };

                var lst = new[] { p, q, r };

                Assert.True(db.InsertList(lst));

                p.FirstName = "Emily";
                q.FirstName = "Jim";
                r.FirstName = "Laura";

                Assert.True(db.UpdateList(lst));

                var gp = db.Get<PersonConcurrencyCheck>(p.GuidId);

                Assert.Equal("Emily", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateList")]
        public void UpdateListConcurrencyCheckModified()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonConcurrencyCheck { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones", StringId = "aj" };
                var q = new PersonConcurrencyCheck { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla", StringId = "rp" };
                var r = new PersonConcurrencyCheck { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain", StringId = "lb" };

                var lst = new[] { p, q, r };

                Assert.True(db.InsertList(lst));

                p.FirstName = "Emily";
                q.FirstName = "Jim";
                r.FirstName = "Laura";

                // Simulate the data changing out from underneath us by modifying the second item.
                q.StringId += "X";

                Assert.False(db.UpdateList(lst));

                var gp = db.Get<PersonConcurrencyCheck>(p.GuidId);
                var gq = db.Get<PersonConcurrencyCheck>(q.GuidId);
                var gr = db.Get<PersonConcurrencyCheck>(r.GuidId);

                // first item changed, second/third did not.
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.NotEqual(q.FirstName, gq.FirstName);
                Assert.NotEqual(r.FirstName, gr.FirstName);
            }
        }
    }
}

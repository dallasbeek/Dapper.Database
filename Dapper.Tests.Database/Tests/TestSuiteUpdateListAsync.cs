using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Database.Extensions;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        [Fact]
        [Trait("Category", "UpdateListAsync")]
        public async Task UpdateListAsyncNoComputed()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
                var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

                var lst = new List<PersonUniqueIdentifier> { p, q, r };

                Assert.True(await db.InsertListAsync(lst));

                p.FirstName = "Emily";
                q.FirstName = "Jim";
                r.FirstName = "Laura";

                Assert.True(await db.UpdateListAsync(lst));

                var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal("Emily", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);

            }
        }

        [Fact]
        [Trait("Category", "UpdateListAsync")]
        public async Task UpdateListAsyncNoComputedPartial()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
                var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

                var lst = new List<PersonUniqueIdentifier> { p, q, r };

                Assert.True(await db.InsertListAsync(lst));

                p.FirstName = "Emily";
                p.LastName = "Swank";
                q.FirstName = "Jim";
                r.FirstName = "Laura";

                Assert.True(await db.UpdateListAsync(lst, new[] { "LastName" }));

                var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal("Alice", gp.FirstName);
                Assert.Equal("Swank", gp.LastName);
                Assert.Equal(p.LastName, gp.LastName);

            }
        }

        [Fact]
        [Trait("Category", "UpdateListAsync")]
        public async Task UpdateListAsyncNoComputedThrowsException()
        {
            Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

            using (var db = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
                var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

                var lst = new List<PersonUniqueIdentifier> { p, q, r };

                Assert.True(await db.InsertListAsync(lst));

                p.FirstName = "Emily";
                q.FirstName = "a".PadRight(101, 'a');
                r.FirstName = "Laura";

                await Assert.ThrowsAnyAsync<Exception>(() => db.UpdateListAsync(lst));

                var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal("Alice", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateListAsync")]
        public async Task UpdateListAsyncIdentity()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                var q = new PersonIdentity { FirstName = "Raj", LastName = "Padilla" };
                var r = new PersonIdentity { FirstName = "Lidia", LastName = "Bain" };

                var lst = new List<PersonIdentity> { p, q, r };

                Assert.True(await db.InsertListAsync(lst));

                p.FirstName = "Emily";
                q.FirstName = "Jim";
                r.FirstName = "Laura";

                Assert.True(await db.UpdateListAsync(lst));

                var gp = await db.GetAsync<PersonIdentity>(p.IdentityId);

                Assert.Equal("Emily", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateListAsync")]
        public async Task UpdateListAsyncIdentityThrowsException()
        {
            Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

            using (var db = GetSqlDatabase())
            {
                var p = new PersonIdentity { FirstName = "Alice", LastName = "Jones" };
                var q = new PersonIdentity { FirstName = "Raj", LastName = "Padilla" };
                var r = new PersonIdentity { FirstName = "Lidia", LastName = "Bain" };

                var lst = new List<PersonIdentity> { p, q, r };

                Assert.True(await db.InsertListAsync(lst));

                p.FirstName = "Emily";
                q.FirstName = "a".PadRight(101, 'a');
                r.FirstName = "Laura";

                await Assert.ThrowsAnyAsync<Exception>(() => db.UpdateListAsync(lst));

                var gp = await db.GetAsync<PersonIdentity>(p.IdentityId);

                Assert.Equal("Alice", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateListAsync")]
        public async Task UpdateListAsyncComputed()
        {

            var dnow = DateTime.UtcNow;
            using (var db = GetSqlDatabase())
            {
                var p = new PersonExcludedColumns { FirstName = "Alice", LastName = "Jones", Notes = "Hello", CreatedOn = dnow, UpdatedOn = dnow };
                var q = new PersonExcludedColumns { FirstName = "Raj", LastName = "Padilla", Notes = "Hello", CreatedOn = dnow, UpdatedOn = dnow };
                var r = new PersonExcludedColumns { FirstName = "Lidia", LastName = "Bain", Notes = "Hello", CreatedOn = dnow, UpdatedOn = dnow };

                var lst = new List<PersonExcludedColumns> { p, q, r };

                Assert.True(await db.InsertListAsync(lst));

                p.FirstName = "Emily";
                q.FirstName = "Jim";
                r.FirstName = "Laura";

                Assert.True(await db.UpdateListAsync(lst));

                if (p.FullName != null)
                {
                    Assert.Equal("Emily Jones", p.FullName);
                    Assert.Equal("Jim Padilla", q.FullName);
                    Assert.Equal("Laura Bain", r.FullName);
                }

                var gp = await db.GetAsync<PersonExcludedColumns>(p.IdentityId);

                Assert.Equal(p.IdentityId, gp.IdentityId);
                Assert.Null(gp.Notes);
                Assert.InRange(gp.UpdatedOn.Value, dnow.AddMinutes(-1), dnow.AddMinutes(1)); // to cover clock skew, delay in DML, etc.
                Assert.InRange(gp.CreatedOn.Value, dnow.AddSeconds(-1), dnow.AddSeconds(1)); // to cover fractional seconds rounded up/down (amounts supported between databases vary, but should all be ±1 second at most. )
                Assert.Equal(p.FirstName, gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateListAsync")]
        public async Task UpdateListAsyncTransactionNoComputed()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
                var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

                var lst = new List<PersonUniqueIdentifier> { p, q, r };

                Assert.True(await db.InsertListAsync(lst));

                using (var t = db.GetTransaction(GetProvider() == Provider.SQLite ? System.Data.IsolationLevel.Serializable : System.Data.IsolationLevel.ReadCommitted))
                {
                    p.FirstName = "Emily";
                    q.FirstName = "Jim";
                    r.FirstName = "Laura";

                    Assert.True(await db.UpdateListAsync(lst));

                    t.Complete();
                }

                var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal("Emily", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateListAsync")]
        public async Task UpdateListAsyncTransactionRollbackNoComputed()
        {
            var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
            var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
            var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

            var lst = new List<PersonUniqueIdentifier> { p, q, r };

            using (var db = GetSqlDatabase())
            {
                Assert.True(await db.InsertListAsync(lst));

                using (var t = db.GetTransaction(GetProvider() == Provider.SQLite ? System.Data.IsolationLevel.Serializable : System.Data.IsolationLevel.ReadCommitted))
                {
                    p.FirstName = "Emily";
                    q.FirstName = "Jim";
                    r.FirstName = "Laura";

                    Assert.True(await db.UpdateListAsync(lst));
                    t.Dispose();
                }

                var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal("Alice", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }

        [Fact]
        [Trait("Category", "UpdateListAsync")]
        public async Task UpdateListAsyncTransactionNoComputedThrowsException()
        {
            Skip.If(GetProvider() == Provider.SQLite, "Sqlite doesn't enforce size limit");

            var p = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
            var q = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Raj", LastName = "Padilla" };
            var r = new PersonUniqueIdentifier { GuidId = Guid.NewGuid(), FirstName = "Lidia", LastName = "Bain" };

            var lst = new List<PersonUniqueIdentifier> { p, q, r };

            using (var db = GetSqlDatabase())
            {
                Assert.True(await db.InsertListAsync(lst));

                using (var t = db.GetTransaction())
                {
                    p.FirstName = "Emily";
                    q.FirstName = "a".PadRight(101, 'a');
                    r.FirstName = "Laura";
                    await Assert.ThrowsAnyAsync<Exception>(() => db.UpdateListAsync(lst));
                }


                var gp = await db.GetAsync<PersonUniqueIdentifier>(p.GuidId);

                Assert.Equal("Alice", gp.FirstName);
                Assert.Equal(p.LastName, gp.LastName);
            }
        }
    }
}

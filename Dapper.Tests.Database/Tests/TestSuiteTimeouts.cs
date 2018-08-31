using Xunit;
using System;

#if NET452
using System.Transactions;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlServerCe;
#endif

using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        [Fact]
        [Trait("Category", "Timeouts")]
        public void GlobalTimeout()
        {
            if (GetProvider() == Provider.SQLite) return;

            using (var db = GetSqlDatabase())
            {
                //can't force it to timeout since it's in seconds so verify it's set invalid
                db.CommandTimeout = -1;
                if (GetProvider() == Provider.MySql || GetProvider() == Provider.Postgres)
                {
                    var ex = Assert.Throws<ArgumentOutOfRangeException>(() => db.Count<Product>());
                    AssertTimeoutExceptionMessage(ex);
                }
                else
                {
                    var ex = Assert.Throws<ArgumentException>(() => db.Count<Product>());
                    AssertTimeoutExceptionMessage(ex);
                }
            }
        }

        [Fact]
        [Trait("Category", "Timeouts")]
        public void OneTimeTimeout()
        {
            if (GetProvider() == Provider.SQLite) return;

            using (var db = GetSqlDatabase())
            {
                //can't force it to timeout since it's in seconds so verify it's set invalid
                db.OneTimeCommandTimeout = -1;
                if (GetProvider() == Provider.MySql || GetProvider() == Provider.Postgres)
                {
                    var ex = Assert.Throws<ArgumentOutOfRangeException>(() => db.Count<Product>());
                    AssertTimeoutExceptionMessage(ex);
                }
                else
                {
                    var ex = Assert.Throws<ArgumentException>(() => db.Count<Product>());
                    AssertTimeoutExceptionMessage(ex);
                }
            }
        }


        [Fact]
        [Trait("Category", "Timeouts")]
        public void GlobalAndOneTimeTimeout()
        {
            if (GetProvider() == Provider.SQLite) return;

            using (var db = GetSqlDatabase())
            {
                //can't force it to timeout since it's in seconds so verify it's set invalid
                db.CommandTimeout = -1;
                if (GetProvider() == Provider.MySql || GetProvider() == Provider.Postgres)
                {
                    var ex = Assert.Throws<ArgumentOutOfRangeException>(() => db.Count<Product>());
                    AssertTimeoutExceptionMessage(ex);
                }
                else
                {
                    var ex = Assert.Throws<ArgumentException>(() => db.Count<Product>());
                    AssertTimeoutExceptionMessage(ex);
                }

                db.OneTimeCommandTimeout = 0;
                Assert.Equal(295, db.Count<Product>());

                if (GetProvider() == Provider.MySql || GetProvider() == Provider.Postgres)
                {
                    var ex = Assert.Throws<ArgumentOutOfRangeException>(() => db.Count<Product>());
                    AssertTimeoutExceptionMessage(ex);
                }
                else
                {
                    var ex = Assert.Throws<ArgumentException>(() => db.Count<Product>());
                    AssertTimeoutExceptionMessage(ex);
                }


            }
        }

        private void AssertTimeoutExceptionMessage(Exception ex)
        {
            if (GetProvider() == Provider.SqlCE)
            {
                Assert.StartsWith("SqlCeCommand.CommandTimeout does not support non-zero values.", ex.Message);
            }
            else if (GetProvider() == Provider.MySql)
            {
                Assert.StartsWith("Timeout can be only be set to 'System.Threading.Timeout.Infinite'", ex.Message);
            }
            else if (GetProvider() == Provider.Postgres)
            {
                Assert.StartsWith("CommandTimeout can't be less than zero.", ex.Message);
            }
            else if (GetProvider() == Provider.Firebird)
            {
                Assert.StartsWith("The property value assigned is less than 0.", ex.Message);
            }
            else
            {
                Assert.StartsWith("Invalid CommandTimeout value -1;", ex.Message);
            }

        }
    }
}

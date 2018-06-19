using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Dapper.Database.Extensions;
using Xunit;

#if NET452
using System.Transactions;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlServerCe;
#endif


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        [Fact]
        [Trait("Category", "Exists")]
        public void Exists()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new User {Name= "Exists" };
                Assert.True(connection.Insert(u1));
                Assert.True(connection.Exists<User>(u1));
                Assert.True(connection.Exists<User>(u1.Id));
                Assert.False(connection.Exists<User>(-100));

            }
        }

        [Fact]
        [Trait("Category", "Exists")]
        public void ExistsClauseQuery()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new User { Name = "FetchMe" };
                Assert.True(connection.Insert(u1));
                Assert.True(connection.Exists<User>("[Name] = @Name", new { Name = "FetchMe" }));
                Assert.False(connection.Exists<User>("[Name] = @Name", new { Name = "junk" }));

            }
        }

    }
}

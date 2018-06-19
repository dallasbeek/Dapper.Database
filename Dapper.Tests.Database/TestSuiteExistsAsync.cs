using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Dapper.Database.Extensions;
using Xunit;
using System.Threading.Tasks;

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
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsAsync()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new User {Name= "Exists" };
                Assert.True(await connection.InsertAsync(u1).ConfigureAwait(false));
                Assert.True(await connection.ExistsAsync<User>(u1).ConfigureAwait(false));
                Assert.True(await connection.ExistsAsync<User>(u1.Id).ConfigureAwait(false));
                Assert.False(await connection.ExistsAsync<User>(-100).ConfigureAwait(false));

            }
        }

        [Fact]
        [Trait("Category", "ExistsAsync")]
        public async Task ExistsClauseQueryAsync()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new User { Name = "FetchMe" };
                Assert.True(await connection.InsertAsync(u1).ConfigureAwait(false));
                Assert.True(await connection.ExistsAsync<User>("[Name] = @Name", new { Name = "FetchMe" }).ConfigureAwait(false));
                Assert.False(await connection.ExistsAsync<User>("[Name] = @Name", new { Name = "junk" }).ConfigureAwait(false));

            }
        }

    }
}

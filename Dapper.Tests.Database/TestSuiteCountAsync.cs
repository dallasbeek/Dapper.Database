
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
        [Trait( "Category", "CountAsync" )]
        public async Task CountEnumerableAsync()
        {
            const int numberOfEntities = 10;

            var users = new List<User>();
            for (var i = 0; i < numberOfEntities; i++)
                users.Add(new User { Name = "User " + i, Age = i });

            using (var connection = GetOpenConnection())
            {
                await connection.DeleteAllAsync<User>().ConfigureAwait(false);
                var total = await connection.InsertAsync(users).ConfigureAwait(false);

                Assert.Equal(numberOfEntities, connection.Count<User>("1 = 1", null));
                Assert.Equal(5, await connection.CountAsync<User>("Age > @age", new { age = 4 }).ConfigureAwait(false));
            }

        }
    }
}

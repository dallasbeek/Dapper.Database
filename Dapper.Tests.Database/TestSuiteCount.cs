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
        [Trait( "Category", "Count" )]
        public void CountEnumerable()
        {
            const int numberOfEntities = 10;

            var users = new List<User>();
            for (var i = 0; i < numberOfEntities; i++)
                users.Add(new User { Name = "User " + i, Age = i });

            using (var connection = GetOpenConnection())
            {
                connection.DeleteAll<User>();
                var total = connection.Insert(users);

                Assert.Equal(numberOfEntities, connection.Count<User>("1 = 1", null));
                Assert.Equal(5, connection.Count<User>("Age > @age", new { age = 4 }));
            }

        }
    }
}

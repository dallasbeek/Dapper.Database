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

            var users = new List<CustomerProxy>();
            for (var i = 0; i < numberOfEntities; i++)
                users.Add(new CustomerProxy { FirstName = "User " + i, Age = i });

            using (var connection = GetOpenConnection())
            {
                connection.DeleteAll<CustomerProxy>();
                var total = connection.Insert(users);

                Assert.Equal(numberOfEntities, connection.Count<CustomerProxy>("1 = 1", null));
                Assert.Equal(5, connection.Count<CustomerProxy>("Age > @age", new { age = 4 }));
            }

        }
    }
}

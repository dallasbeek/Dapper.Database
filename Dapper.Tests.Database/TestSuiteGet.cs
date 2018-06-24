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
        [Trait("Category", "Get")]
        public void Get()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new CustomerProxy { FirstName = "Adama", Age = 10 };
                Assert.True(connection.Insert(u1));
                var ur1 = connection.Get<CustomerProxy>(u1);
                Assert.Equal(u1.Id, ur1.Id);
                Assert.Equal(u1.FirstName, ur1.FirstName);
                Assert.Equal(u1.Age, ur1.Age);

                var ur2 = connection.Get<CustomerProxy>(u1.Id);
                Assert.Equal(u1.Id, ur2.Id);
                Assert.Equal(u1.FirstName, ur2.FirstName);
                Assert.Equal(u1.Age, ur2.Age);

                Assert.Null(connection.Get<CustomerProxy>(-100));

                connection.Delete(u1);
            }
        }


        [Fact]
        [Trait("Category", "Exists")]
        public void GetClauseQuery()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new CustomerProxy { FirstName = "ZippoQ" };
                Assert.True(connection.Insert(u1));

                var ur1 = connection.Get<CustomerProxy>("[FirstName] = @FirstName", new { FirstName = "ZippoQ" });
                Assert.Equal(u1.Id, ur1.Id);
                Assert.Equal(u1.FirstName, ur1.FirstName);
                Assert.Equal(u1.Age, ur1.Age);
                connection.Delete(u1);

            }
        }

    }
}

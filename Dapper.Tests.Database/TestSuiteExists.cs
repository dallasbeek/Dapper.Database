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
                var u1 = new CustomerProxy {FirstName= "Exists" };
                Assert.True(connection.Insert(u1));
                Assert.True(connection.Exists<CustomerProxy>(u1));
                Assert.True(connection.Exists<CustomerProxy>(u1.Id));
                Assert.False(connection.Exists<CustomerProxy>(-100));

            }
        }

        [Fact]
        [Trait("Category", "Exists")]
        public void ExistsClauseQuery()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new CustomerProxy { FirstName = "FetchMe" };
                Assert.True(connection.Insert(u1));
                Assert.True(connection.Exists<CustomerProxy>("[FirstName] = @FirstName", new { FirstName = "FetchMe" }));
                Assert.False(connection.Exists<CustomerProxy>("[FirstName] = @FirstName", new { FirstName = "junk" }));

            }
        }

        [Fact]
        [Trait("Category", "Exists")]
        public void ExistsComposite()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new CustomerComposite {  IId = 8, GId = Guid.NewGuid() };
                Assert.True(connection.Insert(u1));
                Assert.True(connection.Exists<CustomerComposite>(u1));
            }
        }

    }
}

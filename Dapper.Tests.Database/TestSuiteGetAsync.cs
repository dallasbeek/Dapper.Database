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
        [Trait("Category", "GetAsync")]
        public async Task GetAsync()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new CustomerProxy {FirstName= "Get" };
                Assert.True(await connection.InsertAsync(u1).ConfigureAwait(false));
                var ur1 = await connection.GetAsync<CustomerProxy>(u1).ConfigureAwait(false);
                Assert.Equal(u1.Id, ur1.Id);
                Assert.Equal(u1.FirstName, ur1.FirstName);
                Assert.Equal(u1.Age, ur1.Age);

                var ur2 = await connection.GetAsync<CustomerProxy>(u1.Id).ConfigureAwait(false);
                Assert.Equal(u1.Id, ur2.Id);
                Assert.Equal(u1.FirstName, ur2.FirstName);
                Assert.Equal(u1.Age, ur2.Age);

                Assert.Null(await connection.GetAsync<CustomerProxy>(-100).ConfigureAwait(false));

                await connection.DeleteAsync(u1).ConfigureAwait(false);
            }
        }

        [Fact]
        [Trait("Category", "GetAsync")]
        public async Task GetClauseQueryAsync()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new CustomerProxy { FirstName = "FetchMe" };
                Assert.True(await connection.InsertAsync(u1).ConfigureAwait(false));
                var ur1 = await connection.GetAsync<CustomerProxy>("[FirstName] = @FirstName", new { FirstName = "FetchMe" }).ConfigureAwait(false);
                Assert.Equal(u1.Id, ur1.Id);
                Assert.Equal(u1.FirstName, ur1.FirstName);
                Assert.Equal(u1.Age, ur1.Age);


                Assert.Null(await connection.GetAsync<CustomerProxy>("[FirstName] = @FirstName", new { FirstName = "junk" }).ConfigureAwait(false));

               await connection.DeleteAsync(u1).ConfigureAwait(false);

            }
        }

        [Fact]
        [Trait("Category", "GetAsync")]
        public async Task GetCompositeAsync()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new CustomerComposite { IId = 8, GId = Guid.NewGuid() };
                Assert.True(await connection.InsertAsync(u1).ConfigureAwait(false));
                var ur1 = await connection.GetAsync<CustomerComposite>(u1).ConfigureAwait(false);
                Assert.Equal(u1.IId, ur1.IId);
                Assert.Equal(u1.GId, ur1.GId);
                Assert.Equal(u1.FirstName, ur1.FirstName);
                Assert.Equal(u1.Age, ur1.Age);

                await connection.DeleteAsync(u1).ConfigureAwait(false);
            }
        }

    }
}

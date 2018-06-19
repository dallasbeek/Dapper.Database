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
        [Trait( "Category", "DeleteAsync")]
        public async Task DeleteAllAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new User { Name = "Alice", Age = 32 };
                var u2 = new User { Name = "Bob", Age = 33 };

                Assert.True(await connection.InsertAsync( u1 ).ConfigureAwait(false) );
                Assert.True(await connection.InsertAsync( u2 ).ConfigureAwait(false));
                Assert.True(await connection.DeleteAllAsync<User>().ConfigureAwait(false) );
                Assert.Null(await connection.GetAsync<User>( u1.Id ).ConfigureAwait(false));
                Assert.Null(await connection.GetAsync<User>( u2.Id ).ConfigureAwait(false));
            }
        }


        [Fact]
        [Trait( "Category", "DeleteAsync")]
        public async Task DeleteEnumerableAsync()
        {
            await DeleteHelperAsync( src => src.AsEnumerable() ).ConfigureAwait(false); ;
        }

        [Fact]
        [Trait( "Category", "DeleteAsync" )]
        public async Task DeleteArrayAsync()
        {
           await  DeleteHelperAsync( src => src.ToArray() ).ConfigureAwait(false); ;
        }

        [Fact]
        [Trait( "Category", "DeleteAsync")]
        public async Task DeleteListAsync()
        {
            await DeleteHelperAsync( src => src.ToList() ).ConfigureAwait(false); ;
        }

        [Fact]
        [Trait( "Category", "DeleteAsync")]
        public async Task DeleteEntityAsync()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new User { Name = "DeleteMe", Age = 33 };

                Assert.True( connection.Insert( u1 ) );

                await connection.DeleteAsync( u1 ).ConfigureAwait(false); ;

                Assert.False( connection.Update( u1 ) );
            }
        }

        [Fact]
        [Trait("Category", "DeleteAsync")]
        public async Task DeleteByIdAsync()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new User { Name = "DeleteMe", Age = 33 };

                Assert.True(connection.Insert(u1));

                await connection.DeleteAsync<User>(u1.Id).ConfigureAwait(false);

                Assert.False(connection.Update(u1));
            }
        }

        [Fact]
        [Trait("Category", "DeleteAsync")]
        public async Task DeleteByClauseAsync()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new User { Name = "DeleteMe", Age = 33 };
                Assert.True(connection.Insert(u1));

                await  connection.DeleteAsync<User>("Age = @a", new { a = 33 }).ConfigureAwait(false);

                Assert.False(connection.Update(u1));
            }
        }

        //[Fact]
        //[Trait( "Category", "Delete" )]
        //public void DeleteByIds ()
        //{
        //    using ( var connection = GetOpenConnection() )
        //    {
        //        var u1 = new User { Name = "DeleteMe", Age = 11 };
        //        var u2 = new User { Name = "DeleteMe", Age = 12 };
        //        var u3 = new User { Name = "DeleteMe", Age = 13 };

        //        Assert.True( connection.Insert( u1 ) );
        //        Assert.True( connection.Insert( u2 ) );
        //        Assert.True( connection.Insert( u3 ) );

        //        var ids = new { u1, u2, u3 };

        //        connection.Delete<User>( ids );

        //        Assert.False( connection.Update( u1 ) );
        //    }
        //}

        //[Fact]
        //[Trait( "Category", "Delete" )]
        //public void DeleteCompositeKey ()
        //{
        //    using ( var connection = GetOpenConnection() )
        //    {
        //        var u1 = new User { Name = "DeleteMe", Age = 11 };
        //        var u2 = new User { Name = "DeleteMe", Age = 12 };
        //        var u3 = new User { Name = "DeleteMe", Age = 13 };

        //        Assert.True( connection.Insert( u1 ) );
        //        Assert.True( connection.Insert( u2 ) );
        //        Assert.True( connection.Insert( u3 ) );

        //        var ids = new { u1, u2, u3 };

        //        connection.Delete<User>( ids );

        //        Assert.False( connection.Update( u1 ) );
        //    }
        //}

        private async Task DeleteHelperAsync<T>(Func<IEnumerable<User>, T> helper)
            where T : class
        {
            const int numberOfEntities = 10;

            var users = new List<User>();
            for (var i = 0; i < numberOfEntities; i++)
                users.Add(new User { Name = "User " + i, Age = i });

            using (var connection = GetOpenConnection())
            {
                await connection.DeleteAllAsync<User>().ConfigureAwait(false);

                Assert.True(await connection.InsertAsync(helper(users)).ConfigureAwait(false));

                users = connection.Query<User>("select * from Users").ToList();
                Assert.Equal(users.Count, numberOfEntities);

                var usersToDelete = users.Take(10).ToList();
                await connection.DeleteAsync(helper(usersToDelete)).ConfigureAwait(false);
                users = connection.Query<User>("select * from Users").ToList();
                Assert.Equal(users.Count, numberOfEntities - 10);
            }
        }
    }
}

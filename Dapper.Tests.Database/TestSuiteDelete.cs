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
        [Trait( "Category", "Delete" )]
        public void DeleteAll ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerProxy { FirstName = "Alice", Age = 32 };
                var u2 = new CustomerProxy { FirstName = "Bob", Age = 33 };

                Assert.True( connection.Insert( u1 ) );
                Assert.True( connection.Insert( u2 ) );
                Assert.True( connection.DeleteAll<CustomerProxy>() );
                Assert.Null( connection.Get<CustomerProxy>( u1.Id ) );
                Assert.Null( connection.Get<CustomerProxy>( u2.Id ) );
            }
        }


        [Fact]
        [Trait( "Category", "Delete" )]
        public void DeleteEnumerable ()
        {
            DeleteHelper( src => src.AsEnumerable() );
        }

        [Fact]
        [Trait( "Category", "Delete" )]
        public void DeleteArray ()
        {
            DeleteHelper( src => src.ToArray() );
        }

        [Fact]
        [Trait( "Category", "Delete" )]
        public void DeleteList ()
        {
            DeleteHelper( src => src.ToList() );
        }

        [Fact]
        [Trait( "Category", "Delete" )]
        public void DeleteEntity ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerProxy { FirstName = "DeleteMe", Age = 33 };

                Assert.True( connection.Insert( u1 ) );

                connection.Delete( u1 );

                Assert.False( connection.Update( u1 ) );
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteById()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new CustomerProxy { FirstName = "DeleteMe", Age = 33 };

                Assert.True(connection.Insert(u1));

                connection.Delete<CustomerProxy>(u1.Id);

                Assert.False(connection.Update(u1));
            }
        }

        [Fact]
        [Trait("Category", "Delete")]
        public void DeleteByClause()
        {
            using (var connection = GetOpenConnection())
            {
                var u1 = new CustomerProxy { FirstName = "DeleteMe", Age = 33 };
                Assert.True(connection.Insert(u1));

                connection.Delete<CustomerProxy>("Age = @a", new { a = 33 });

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

        private void DeleteHelper<T> ( Func<IEnumerable<CustomerProxy>, T> helper )
            where T : class
        {
            const int numberOfEntities = 10;

            var users = new List<CustomerProxy>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new CustomerProxy { FirstName = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<CustomerProxy>();

                var total = connection.Insert( helper( users ) );
                //Assert.Equal(total, numberOfEntities);
                users = connection.Query<CustomerProxy>( "select * from Customers" ).ToList();
                Assert.Equal( users.Count, numberOfEntities );

                var usersToDelete = users.Take( 10 ).ToList();
                connection.Delete( helper( usersToDelete ) );
                users = connection.Query<CustomerProxy>( "select * from Customers" ).ToList();
                Assert.Equal( users.Count, numberOfEntities - 10 );
            }
        }
    }
}

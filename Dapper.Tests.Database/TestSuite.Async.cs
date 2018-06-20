using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dapper.Database.Extensions;
//using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;
using Xunit;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        [Fact]
        public async Task TypeWithGenericParameterCanBeInsertedAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<GenericType<string>>();
                var objectToInsert = new GenericType<string>
                {
                    SId = Guid.NewGuid().ToString(),
                    FirstName = "something"
                };
                await connection.InsertAsync( objectToInsert );

                Assert.Single( connection.GetAll<GenericType<string>>() );

                var objectsToInsert = new List<GenericType<string>>
                {
                    new GenericType<string>
                    {
                        SId = Guid.NewGuid().ToString(),
                        FirstName = "1",
                    },
                    new GenericType<string>
                    {
                        SId = Guid.NewGuid().ToString(),
                        FirstName = "2",
                    }
                };

                await connection.InsertAsync( objectsToInsert );
                var list = connection.GetAll<GenericType<string>>();
                Assert.Equal( 3, list.Count() );
            }
        }

        [Fact]
        public async Task TypeWithGenericParameterCanBeUpdatedAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var objectToInsert = new GenericType<string>
                {
                    SId = Guid.NewGuid().ToString(),
                    FirstName = "something"
                };
                await connection.InsertAsync( objectToInsert );

                objectToInsert.FirstName = "somethingelse";
                await connection.UpdateAsync( objectToInsert );

                var updatedObject = connection.Get<GenericType<string>>( objectToInsert.SId );
                Assert.Equal( objectToInsert.FirstName, updatedObject.FirstName );
            }
        }

        [Fact]
        public async Task TypeWithGenericParameterCanBeDeletedAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var objectToInsert = new GenericType<string>
                {
                    SId = Guid.NewGuid().ToString(),
                    FirstName = "something"
                };
                await connection.InsertAsync( objectToInsert );

                bool deleted = await connection.DeleteAsync( objectToInsert );
                Assert.True( deleted );
            }
        }

        /// <summary>
        /// Tests for issue #351 
        /// </summary>
        [Fact]
        public async Task InsertGetUpdateDeleteWithExplicitKeyAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var guid = Guid.NewGuid().ToString();
                var o1 = new CustomerStringId { SId = guid, FirstName = "Foo" };
                var originalxCount = ( await connection.QueryAsync<int>( "Select Count(*) From Customers" ).ConfigureAwait( false ) ).First();
                await connection.InsertAsync( o1 ).ConfigureAwait( false );
                var list1 = ( await connection.QueryAsync<CustomerStringId>( "select * from Customers" ).ConfigureAwait( false ) ).ToList();
                Assert.Equal( list1.Count, originalxCount + 1 );
                o1 = await connection.GetAsync<CustomerStringId>( guid ).ConfigureAwait( false );
                Assert.Equal( o1.SId, guid );
                o1.FirstName = "Bar";
                await connection.UpdateAsync( o1 ).ConfigureAwait( false );
                o1 = await connection.GetAsync<CustomerStringId>( guid ).ConfigureAwait( false );
                Assert.Equal( "Bar", o1.FirstName );
                await connection.DeleteAsync( o1 ).ConfigureAwait( false );
                o1 = await connection.GetAsync<CustomerStringId>( guid ).ConfigureAwait( false );
                Assert.Null( o1 );

                const int id = 42;
                var o2 = new CustomerIntegerId { IId = id, FirstName = "Foo" };
                var originalyCount = connection.Query<int>( "Select Count(*) From Customers" ).First();
                await connection.InsertAsync( o2 ).ConfigureAwait( false );
                var list2 = ( await connection.QueryAsync<CustomerIntegerId>( "select * from Customers" ).ConfigureAwait( false ) ).ToList();
                Assert.Equal( list2.Count, originalyCount + 1 );
                o2 = await connection.GetAsync<CustomerIntegerId>( id ).ConfigureAwait( false );
                Assert.Equal( o2.IId, id );
                o2.FirstName = "Bar";
                await connection.UpdateAsync( o2 ).ConfigureAwait( false );
                o2 = await connection.GetAsync<CustomerIntegerId>( id ).ConfigureAwait( false );
                Assert.Equal( "Bar", o2.FirstName );
                await connection.DeleteAsync( o2 ).ConfigureAwait( false );
                o2 = await connection.GetAsync<CustomerIntegerId>( id ).ConfigureAwait( false );
                Assert.Null( o2 );
            }
        }

        [Fact]
        public async Task TableNameAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                // tests against "Automobiles" table (Table attribute)
                var c1 = new Car { FirstName = "VolvoAsync" };
                var c2 = new Car { FirstName = "SaabAsync" };
                Assert.True( await connection.InsertAsync( c1 ).ConfigureAwait( false ) );
                var car = await connection.GetAsync<Car>( c1.Id ).ConfigureAwait( false );
                Assert.NotNull( car );
                Assert.Equal( "VolvoAsync", car.FirstName );
                c2.Id = c1.Id;
                Assert.True( await connection.UpdateAsync( c2 ).ConfigureAwait( false ) );
                Assert.Equal( "SaabAsync", ( await connection.GetAsync<Car>( c1.Id ).ConfigureAwait( false ) ).FirstName );
                Assert.True( await connection.DeleteAsync( new Car { Id = c1.Id } ).ConfigureAwait( false ) );
                Assert.Null( await connection.GetAsync<Car>( c1.Id ).ConfigureAwait( false ) );
            }
        }

        [Fact]
        public async Task TestSimpleGetAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerProxy { FirstName = "Adama", Age = 10 };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );
                var user = await connection.GetAsync<CustomerProxy>( u1.Id ).ConfigureAwait( false );
                Assert.True( user.Id > 0 );
                Assert.Equal( "Adama", user.FirstName );
                await connection.DeleteAsync( user ).ConfigureAwait( false );
            }
        }

        [Fact]
        public async Task InsertGetUpdateAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                Assert.Null( await connection.GetAsync<CustomerProxy>( 30 ).ConfigureAwait( false ) );

                var originalCount = ( await connection.QueryAsync<int>( "select Count(*) from Customers" ).ConfigureAwait( false ) ).First();

                var u1 = new CustomerProxy { FirstName = "Adam", Age = 10 };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                //get a user with "isdirty" tracking
                var user = await connection.GetAsync<ICustomer>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Adam", user.FirstName );
                Assert.False( await connection.UpdateAsync( user ).ConfigureAwait( false ) ); //returns false if not updated, based on tracking
                user.FirstName = "Bob";
                Assert.True( await connection.UpdateAsync( user ).ConfigureAwait( false ) ); //returns true if updated, based on tracking
                user = await connection.GetAsync<ICustomer>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Bob", user.FirstName );

                //get a user with no tracking
                var notrackedUser = await connection.GetAsync<CustomerProxy>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Bob", notrackedUser.FirstName );
                Assert.True( await connection.UpdateAsync( notrackedUser ).ConfigureAwait( false ) );
                //returns true, even though user was not changed
                notrackedUser.FirstName = "Cecil";
                Assert.True( await connection.UpdateAsync( notrackedUser ).ConfigureAwait( false ) );
                Assert.Equal( "Cecil", ( await connection.GetAsync<CustomerProxy>( u1.Id ).ConfigureAwait( false ) ).FirstName );

                Assert.Equal( ( await connection.QueryAsync<CustomerProxy>( "select * from Customers" ).ConfigureAwait( false ) ).Count(), originalCount + 1 );
                Assert.True( await connection.DeleteAsync( user ).ConfigureAwait( false ) );
                Assert.Equal( ( await connection.QueryAsync<CustomerProxy>( "select * from Customers" ).ConfigureAwait( false ) ).Count(), originalCount );

                Assert.False( await connection.UpdateAsync( notrackedUser ).ConfigureAwait( false ) ); //returns false, user not found

                Assert.True( await connection.InsertAsync( new CustomerProxy { FirstName = "Adam", Age = 10 } ).ConfigureAwait( false ) );
                Assert.Equal( ( await connection.QueryAsync<CustomerProxy>( "select * from Customers" ).ConfigureAwait( false ) ).Count(), originalCount + 1 );
            }
        }

        [Fact]
        public async Task InsertCheckKeyAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<CustomerProxy>().ConfigureAwait( false );

                Assert.Null( await connection.GetAsync<ICustomer>( 3 ).ConfigureAwait( false ) );
                var user = new CustomerProxy { FirstName = "Adamb", Age = 10 };
                var id = await connection.InsertAsync( user ).ConfigureAwait( false );
                Assert.True( user.Id > 0 );
            }
        }

        [Fact]
        public async Task BuilderSelectClauseAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<CustomerProxy>().ConfigureAwait( false );

                var rand = new Random( 8675309 );
                var data = new List<CustomerProxy>();
                for ( var i = 0; i < 100; i++ )
                {
                    var nU = new CustomerProxy { Age = rand.Next( 70 ), Id = i, FirstName = Guid.NewGuid().ToString() };
                    data.Add( nU );
                    Assert.True( await connection.InsertAsync( nU ).ConfigureAwait( false ) );
                }

                var builder = new SqlBuilder();
                var justId = builder.AddTemplate( "SELECT /**select**/ FROM Customers" );
                var all = builder.AddTemplate( "SELECT FirstName, /**select**/, Age FROM Customers" );

                builder.Select( "Id" );

                var ids = await connection.QueryAsync<int>( justId.RawSql, justId.Parameters ).ConfigureAwait( false );
                var users = await connection.QueryAsync<CustomerProxy>( all.RawSql, all.Parameters ).ConfigureAwait( false );

                foreach ( var u in data )
                {
                    if ( !ids.Any( i => u.Id == i ) ) throw new Exception( "Missing ids in select" );
                    if ( !users.Any( a => a.Id == u.Id && a.FirstName == u.FirstName && a.Age == u.Age ) )
                        throw new Exception( "Missing users in select" );
                }
            }
        }

        [Fact]
        public async Task BuilderTemplateWithoutCompositionAsync ()
        {
            var builder = new SqlBuilder();
            var template = builder.AddTemplate( "SELECT COUNT(*) FROM Customers WHERE Age = @age", new { age = 5 } );

            if ( template.RawSql == null ) throw new Exception( "RawSql null" );
            if ( template.Parameters == null ) throw new Exception( "Parameters null" );

            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<CustomerProxy>().ConfigureAwait( false );

                await connection.InsertAsync( new CustomerProxy { Age = 5, FirstName = "Testy McTestington" } ).ConfigureAwait( false );

                if ( ( await connection.QueryAsync<int>( template.RawSql, template.Parameters ).ConfigureAwait( false ) ).Single() != 1 )
                    throw new Exception( "Query failed" );
            }
        }

        [Fact]
        public async Task InsertEnumerableAsync ()
        {
            await InsertHelperAsync( src => src.AsEnumerable() ).ConfigureAwait( false );
        }

        [Fact]
        public async Task InsertArrayAsync ()
        {
            await InsertHelperAsync( src => src.ToArray() ).ConfigureAwait( false );
        }

        [Fact]
        public async Task InsertListAsync ()
        {
            await InsertHelperAsync( src => src.ToList() ).ConfigureAwait( false );
        }

        private async Task InsertHelperAsync<T> ( Func<IEnumerable<CustomerProxy>, T> helper )
            where T : class
        {
            const int numberOfEntities = 10;

            var users = new List<CustomerProxy>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new CustomerProxy { FirstName = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<CustomerProxy>().ConfigureAwait( false );

                Assert.True( await connection.InsertAsync( helper( users ) ).ConfigureAwait( false ) );
                users = connection.Query<CustomerProxy>( "select * from Customers" ).ToList();
                Assert.Equal( users.Count, numberOfEntities );
            }
        }

        [Fact]
        public async Task UpdateEnumerableAsync ()
        {
            await UpdateHelperAsync( src => src.AsEnumerable() ).ConfigureAwait( false );
        }

        [Fact]
        public async Task UpdateArrayAsync ()
        {
            await UpdateHelperAsync( src => src.ToArray() ).ConfigureAwait( false );
        }

        [Fact]
        public async Task UpdateListAsync ()
        {
            await UpdateHelperAsync( src => src.ToList() ).ConfigureAwait( false );
        }

        private async Task UpdateHelperAsync<T> ( Func<IEnumerable<CustomerProxy>, T> helper )
            where T : class
        {
            const int numberOfEntities = 10;

            var users = new List<CustomerProxy>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new CustomerProxy { FirstName = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<CustomerProxy>().ConfigureAwait( false );

                Assert.True( await connection.InsertAsync( helper( users ) ).ConfigureAwait( false ) );
                users = connection.Query<CustomerProxy>( "select * from Customers" ).ToList();
                Assert.Equal( users.Count, numberOfEntities );
                foreach ( var user in users )
                {
                    user.FirstName += " updated";
                }
                await connection.UpdateAsync( helper( users ) ).ConfigureAwait( false );
                var name = connection.Query<CustomerProxy>( "select * from Customers" ).First().FirstName;
                Assert.Contains( "updated", name );
            }
        }

        [Fact]
        public async Task GetAllAsync ()
        {
            const int numberOfEntities = 10;

            var users = new List<CustomerProxy>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new CustomerProxy { FirstName = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<CustomerProxy>().ConfigureAwait( false );

                Assert.True( await connection.InsertAsync( users ).ConfigureAwait( false ) );

                users = ( List<CustomerProxy> ) await connection.GetAllAsync<CustomerProxy>().ConfigureAwait( false );
                Assert.Equal( users.Count, numberOfEntities );
                var iusers = await connection.GetAllAsync<ICustomer>().ConfigureAwait( false );
                Assert.Equal( iusers.ToList().Count, numberOfEntities );
            }
        }

        /// <summary>
        /// Test for issue #933
        /// </summary>
        [Fact]
        public async void GetAsyncAndGetAllAsyncWithNullableValues ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var d1 = new CustomerProxy { UpdatedOn = new DateTime( 2011, 07, 14 ) };
                var d2 = new CustomerProxy { UpdatedOn = null };

                Assert.True( connection.Insert( d1 ) );
                Assert.True( connection.Insert( d2 ) );

                var value1 = await connection.GetAsync<ICustomer>( d1.Id ).ConfigureAwait( false );
                Assert.Equal( new DateTime( 2011, 07, 14 ), value1.UpdatedOn.Value );

                var value2 = await connection.GetAsync<ICustomer>( d2.Id ).ConfigureAwait( false );
                Assert.True( value2.UpdatedOn == null );

                var value3 = await connection.GetAllAsync<ICustomer>().ConfigureAwait( false );
                var valuesList = value3.ToList();
                Assert.Equal( new DateTime( 2011, 07, 14 ), value3.ToList().Find( c => c.Id == d1.Id ).UpdatedOn.Value );
                Assert.True( value3.ToList().Find( c => c.Id == d2.Id ).UpdatedOn == null );
            }
        }

        [Fact]
        public async Task InsertFieldWithReservedNameAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var r1 = new Result { FirstName = "Adam", Age = 1 };
                await connection.DeleteAllAsync<CustomerProxy>().ConfigureAwait( false );
                Assert.True( await connection.InsertAsync( r1).ConfigureAwait( false ));

                var result = await connection.GetAsync<Result>( r1.Id ).ConfigureAwait( false );
                Assert.Equal( 1, result.Age );
            }
        }

        [Fact]
        public async Task IgnoreInsertAttributeAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerAttribute { FirstName = "This should be ignored" };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                var obj = connection.Get<CustomerAttribute>( u1.Id );
                Assert.Null( obj.FirstName );

                u1.FirstName = "This should now be changed";
                Assert.True( await connection.UpdateAsync( u1 ).ConfigureAwait( false ) );

                obj = connection.Get<CustomerAttribute>( u1.Id );
                Assert.Equal( "This should now be changed", obj.FirstName );

            }
        }

        [Fact]
        public async Task IgnoreUpdateAttributeAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerAttribute { LastName = "Set On Insert" };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                var obj = await connection.GetAsync<CustomerAttribute>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Set On Insert", obj.LastName );

                u1.LastName = "This should not be changed";
                Assert.True( await connection.UpdateAsync( u1 ).ConfigureAwait( false ) );

                obj = await connection.GetAsync<CustomerAttribute>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Set On Insert", obj.LastName );
            }
        }


        [Fact]
        public async Task IgnoreSelectAttributeAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerAttribute { Age = 100 };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                var obj = await connection.GetAsync<CustomerAttribute>( u1.Id ).ConfigureAwait( false );
                Assert.Null( obj.Age );
            }
        }

        [Fact]
        public async Task ComputedAttributeAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerAttribute { FirstName = "Jim", LastName = "Bob", FullName = "Ignored On Insert or Update" };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                var obj = await connection.GetAsync<CustomerAttribute>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( " Bob", obj.FullName );

                u1.LastName = "This should not be changed";
                Assert.True( await connection.UpdateAsync( u1 ).ConfigureAwait( false ) );

                obj = await connection.GetAsync<CustomerAttribute>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Jim Bob", obj.FullName );
            }
        }

        [Fact]
        public async Task ReadOnlyAttributeAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerAttribute { GId = Guid.NewGuid() };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                var obj = await connection.GetAsync<CustomerAttribute>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( Guid.Empty, obj.GId );

                u1.LastName = "This should not be changed";
                Assert.True( await connection.UpdateAsync( u1 ).ConfigureAwait( false ) );

                obj = await connection.GetAsync<CustomerAttribute>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( Guid.Empty, obj.GId );
            }
        }

        [Fact]
        public async Task UpdateColumnsSpecifiedAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerProxy { FirstName = "ValueA", Age = 33 };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                u1.FirstName = "ValueB";
                u1.Age = 43; // should not be updated.

                await connection.UpdateAsync( u1, new string[] { "FirstName" } ).ConfigureAwait( false );

                var uf = await connection.GetAsync<CustomerProxy>( u1.Id ).ConfigureAwait( false );

                Assert.Equal( "ValueB", uf.FirstName );
                Assert.Equal( 33, uf.Age );

            }
        }

    }
}

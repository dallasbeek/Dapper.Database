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
                    Id = Guid.NewGuid().ToString(),
                    Name = "something"
                };
                await connection.InsertAsync( objectToInsert );

                Assert.Single( connection.GetAll<GenericType<string>>() );

                var objectsToInsert = new List<GenericType<string>>
                {
                    new GenericType<string>
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "1",
                    },
                    new GenericType<string>
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "2",
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
                    Id = Guid.NewGuid().ToString(),
                    Name = "something"
                };
                await connection.InsertAsync( objectToInsert );

                objectToInsert.Name = "somethingelse";
                await connection.UpdateAsync( objectToInsert );

                var updatedObject = connection.Get<GenericType<string>>( objectToInsert.Id );
                Assert.Equal( objectToInsert.Name, updatedObject.Name );
            }
        }

        [Fact]
        public async Task TypeWithGenericParameterCanBeDeletedAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var objectToInsert = new GenericType<string>
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "something"
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
                var o1 = new ObjectX { ObjectXId = guid, Name = "Foo" };
                var originalxCount = ( await connection.QueryAsync<int>( "Select Count(*) From ObjectX" ).ConfigureAwait( false ) ).First();
                await connection.InsertAsync( o1 ).ConfigureAwait( false );
                var list1 = ( await connection.QueryAsync<ObjectX>( "select * from ObjectX" ).ConfigureAwait( false ) ).ToList();
                Assert.Equal( list1.Count, originalxCount + 1 );
                o1 = await connection.GetAsync<ObjectX>( guid ).ConfigureAwait( false );
                Assert.Equal( o1.ObjectXId, guid );
                o1.Name = "Bar";
                await connection.UpdateAsync( o1 ).ConfigureAwait( false );
                o1 = await connection.GetAsync<ObjectX>( guid ).ConfigureAwait( false );
                Assert.Equal( "Bar", o1.Name );
                await connection.DeleteAsync( o1 ).ConfigureAwait( false );
                o1 = await connection.GetAsync<ObjectX>( guid ).ConfigureAwait( false );
                Assert.Null( o1 );

                const int id = 42;
                var o2 = new ObjectY { ObjectYId = id, Name = "Foo" };
                var originalyCount = connection.Query<int>( "Select Count(*) From ObjectY" ).First();
                await connection.InsertAsync( o2 ).ConfigureAwait( false );
                var list2 = ( await connection.QueryAsync<ObjectY>( "select * from ObjectY" ).ConfigureAwait( false ) ).ToList();
                Assert.Equal( list2.Count, originalyCount + 1 );
                o2 = await connection.GetAsync<ObjectY>( id ).ConfigureAwait( false );
                Assert.Equal( o2.ObjectYId, id );
                o2.Name = "Bar";
                await connection.UpdateAsync( o2 ).ConfigureAwait( false );
                o2 = await connection.GetAsync<ObjectY>( id ).ConfigureAwait( false );
                Assert.Equal( "Bar", o2.Name );
                await connection.DeleteAsync( o2 ).ConfigureAwait( false );
                o2 = await connection.GetAsync<ObjectY>( id ).ConfigureAwait( false );
                Assert.Null( o2 );
            }
        }

        [Fact]
        public async Task TableNameAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                // tests against "Automobiles" table (Table attribute)
                var c1 = new Car { Name = "VolvoAsync" };
                var c2 = new Car { Name = "SaabAsync" };
                Assert.True( await connection.InsertAsync( c1 ).ConfigureAwait( false ) );
                var car = await connection.GetAsync<Car>( c1.Id ).ConfigureAwait( false );
                Assert.NotNull( car );
                Assert.Equal( "VolvoAsync", car.Name );
                c2.Id = c1.Id;
                Assert.True( await connection.UpdateAsync( c2 ).ConfigureAwait( false ) );
                Assert.Equal( "SaabAsync", ( await connection.GetAsync<Car>( c1.Id ).ConfigureAwait( false ) ).Name );
                Assert.True( await connection.DeleteAsync( new Car { Id = c1.Id } ).ConfigureAwait( false ) );
                Assert.Null( await connection.GetAsync<Car>( c1.Id ).ConfigureAwait( false ) );
            }
        }

        [Fact]
        public async Task TestSimpleGetAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new User { Name = "Adama", Age = 10 };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );
                var user = await connection.GetAsync<User>( u1.Id ).ConfigureAwait( false );
                Assert.True( user.Id > 0 );
                Assert.Equal( "Adama", user.Name );
                await connection.DeleteAsync( user ).ConfigureAwait( false );
            }
        }

        [Fact]
        public async Task InsertGetUpdateAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                Assert.Null( await connection.GetAsync<User>( 30 ).ConfigureAwait( false ) );

                var originalCount = ( await connection.QueryAsync<int>( "select Count(*) from Users" ).ConfigureAwait( false ) ).First();

                var u1 = new User { Name = "Adam", Age = 10 };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                //get a user with "isdirty" tracking
                var user = await connection.GetAsync<IUser>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Adam", user.Name );
                Assert.False( await connection.UpdateAsync( user ).ConfigureAwait( false ) ); //returns false if not updated, based on tracking
                user.Name = "Bob";
                Assert.True( await connection.UpdateAsync( user ).ConfigureAwait( false ) ); //returns true if updated, based on tracking
                user = await connection.GetAsync<IUser>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Bob", user.Name );

                //get a user with no tracking
                var notrackedUser = await connection.GetAsync<User>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Bob", notrackedUser.Name );
                Assert.True( await connection.UpdateAsync( notrackedUser ).ConfigureAwait( false ) );
                //returns true, even though user was not changed
                notrackedUser.Name = "Cecil";
                Assert.True( await connection.UpdateAsync( notrackedUser ).ConfigureAwait( false ) );
                Assert.Equal( "Cecil", ( await connection.GetAsync<User>( u1.Id ).ConfigureAwait( false ) ).Name );

                Assert.Equal( ( await connection.QueryAsync<User>( "select * from Users" ).ConfigureAwait( false ) ).Count(), originalCount + 1 );
                Assert.True( await connection.DeleteAsync( user ).ConfigureAwait( false ) );
                Assert.Equal( ( await connection.QueryAsync<User>( "select * from Users" ).ConfigureAwait( false ) ).Count(), originalCount );

                Assert.False( await connection.UpdateAsync( notrackedUser ).ConfigureAwait( false ) ); //returns false, user not found

                Assert.True( await connection.InsertAsync( new User { Name = "Adam", Age = 10 } ).ConfigureAwait( false ) );
                Assert.Equal( ( await connection.QueryAsync<User>( "select * from Users" ).ConfigureAwait( false ) ).Count(), originalCount + 1 );
            }
        }

        [Fact]
        public async Task InsertCheckKeyAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<User>().ConfigureAwait( false );

                Assert.Null( await connection.GetAsync<IUser>( 3 ).ConfigureAwait( false ) );
                var user = new User { Name = "Adamb", Age = 10 };
                var id = await connection.InsertAsync( user ).ConfigureAwait( false );
                Assert.True( user.Id > 0 );
            }
        }

        [Fact]
        public async Task BuilderSelectClauseAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<User>().ConfigureAwait( false );

                var rand = new Random( 8675309 );
                var data = new List<User>();
                for ( var i = 0; i < 100; i++ )
                {
                    var nU = new User { Age = rand.Next( 70 ), Id = i, Name = Guid.NewGuid().ToString() };
                    data.Add( nU );
                    Assert.True( await connection.InsertAsync( nU ).ConfigureAwait( false ) );
                }

                var builder = new SqlBuilder();
                var justId = builder.AddTemplate( "SELECT /**select**/ FROM Users" );
                var all = builder.AddTemplate( "SELECT Name, /**select**/, Age FROM Users" );

                builder.Select( "Id" );

                var ids = await connection.QueryAsync<int>( justId.RawSql, justId.Parameters ).ConfigureAwait( false );
                var users = await connection.QueryAsync<User>( all.RawSql, all.Parameters ).ConfigureAwait( false );

                foreach ( var u in data )
                {
                    if ( !ids.Any( i => u.Id == i ) ) throw new Exception( "Missing ids in select" );
                    if ( !users.Any( a => a.Id == u.Id && a.Name == u.Name && a.Age == u.Age ) )
                        throw new Exception( "Missing users in select" );
                }
            }
        }

        [Fact]
        public async Task BuilderTemplateWithoutCompositionAsync ()
        {
            var builder = new SqlBuilder();
            var template = builder.AddTemplate( "SELECT COUNT(*) FROM Users WHERE Age = @age", new { age = 5 } );

            if ( template.RawSql == null ) throw new Exception( "RawSql null" );
            if ( template.Parameters == null ) throw new Exception( "Parameters null" );

            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<User>().ConfigureAwait( false );

                await connection.InsertAsync( new User { Age = 5, Name = "Testy McTestington" } ).ConfigureAwait( false );

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

        private async Task InsertHelperAsync<T> ( Func<IEnumerable<User>, T> helper )
            where T : class
        {
            const int numberOfEntities = 10;

            var users = new List<User>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new User { Name = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<User>().ConfigureAwait( false );

                Assert.True( await connection.InsertAsync( helper( users ) ).ConfigureAwait( false ) );
                users = connection.Query<User>( "select * from Users" ).ToList();
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

        private async Task UpdateHelperAsync<T> ( Func<IEnumerable<User>, T> helper )
            where T : class
        {
            const int numberOfEntities = 10;

            var users = new List<User>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new User { Name = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<User>().ConfigureAwait( false );

                Assert.True( await connection.InsertAsync( helper( users ) ).ConfigureAwait( false ) );
                users = connection.Query<User>( "select * from Users" ).ToList();
                Assert.Equal( users.Count, numberOfEntities );
                foreach ( var user in users )
                {
                    user.Name += " updated";
                }
                await connection.UpdateAsync( helper( users ) ).ConfigureAwait( false );
                var name = connection.Query<User>( "select * from Users" ).First().Name;
                Assert.Contains( "updated", name );
            }
        }

        [Fact]
        public async Task DeleteEnumerableAsync ()
        {
            await DeleteHelperAsync( src => src.AsEnumerable() ).ConfigureAwait( false );
        }

        [Fact]
        public async Task DeleteArrayAsync ()
        {
            await DeleteHelperAsync( src => src.ToArray() ).ConfigureAwait( false );
        }

        [Fact]
        public async Task DeleteListAsync ()
        {
            await DeleteHelperAsync( src => src.ToList() ).ConfigureAwait( false );
        }

        private async Task DeleteHelperAsync<T> ( Func<IEnumerable<User>, T> helper )
            where T : class
        {
            const int numberOfEntities = 10;

            var users = new List<User>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new User { Name = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<User>().ConfigureAwait( false );

                Assert.True( await connection.InsertAsync( helper( users ) ).ConfigureAwait( false ) );

                users = connection.Query<User>( "select * from Users" ).ToList();
                Assert.Equal( users.Count, numberOfEntities );

                var usersToDelete = users.Take( 10 ).ToList();
                await connection.DeleteAsync( helper( usersToDelete ) ).ConfigureAwait( false );
                users = connection.Query<User>( "select * from Users" ).ToList();
                Assert.Equal( users.Count, numberOfEntities - 10 );
            }
        }

        [Fact]
        public async Task GetAllAsync ()
        {
            const int numberOfEntities = 10;

            var users = new List<User>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new User { Name = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<User>().ConfigureAwait( false );

                Assert.True( await connection.InsertAsync( users ).ConfigureAwait( false ) );

                users = ( List<User> ) await connection.GetAllAsync<User>().ConfigureAwait( false );
                Assert.Equal( users.Count, numberOfEntities );
                var iusers = await connection.GetAllAsync<IUser>().ConfigureAwait( false );
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
                var d1 = new NullableDate { DateValue = new DateTime( 2011, 07, 14 ) };
                var d2 = new NullableDate { DateValue = null };

                Assert.True( connection.Insert( d1 ) );
                Assert.True( connection.Insert( d2 ) );

                var value1 = await connection.GetAsync<INullableDate>( d1.Id ).ConfigureAwait( false );
                Assert.Equal( new DateTime( 2011, 07, 14 ), value1.DateValue.Value );

                var value2 = await connection.GetAsync<INullableDate>( d2.Id ).ConfigureAwait( false );
                Assert.True( value2.DateValue == null );

                var value3 = await connection.GetAllAsync<INullableDate>().ConfigureAwait( false );
                var valuesList = value3.ToList();
                Assert.Equal( new DateTime( 2011, 07, 14 ), valuesList[ 0 ].DateValue.Value );
                Assert.True( valuesList[ 1 ].DateValue == null );
            }
        }

        [Fact]
        public async Task InsertFieldWithReservedNameAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<User>().ConfigureAwait( false );
                var id = await connection.InsertAsync( new Result { Name = "Adam", Order = 1 } ).ConfigureAwait( false );

                var result = await connection.GetAsync<Result>( id ).ConfigureAwait( false );
                Assert.Equal( 1, result.Order );
            }
        }

        [Fact]
        public async Task DeleteAllAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                await connection.DeleteAllAsync<User>().ConfigureAwait( false );

                var id1 = await connection.InsertAsync( new User { Name = "Alice", Age = 32 } ).ConfigureAwait( false );
                var id2 = await connection.InsertAsync( new User { Name = "Bob", Age = 33 } ).ConfigureAwait( false );
                await connection.DeleteAllAsync<User>().ConfigureAwait( false );
                Assert.Null( await connection.GetAsync<User>( id1 ).ConfigureAwait( false ) );
                Assert.Null( await connection.GetAsync<User>( id2 ).ConfigureAwait( false ) );
            }
        }

        [Fact]
        public async Task IgnoreInsertAttributeAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { IgnoreInsert = "This should be ignored" };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                var obj = connection.Get<ObjectQ>( u1.Id );
                Assert.Null( obj.IgnoreInsert );

                u1.IgnoreInsert = "This should now be changed";
                Assert.True( await connection.UpdateAsync( u1 ).ConfigureAwait( false ) );

                obj = connection.Get<ObjectQ>( u1.Id );
                Assert.Equal( "This should now be changed", obj.IgnoreInsert );

            }
        }

        [Fact]
        public async Task IgnoreUpdateAttributeAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { IgnoreUpdate = "Set On Insert" };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                var obj = await connection.GetAsync<ObjectQ>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Set On Insert", obj.IgnoreUpdate );

                u1.IgnoreUpdate = "This should not be changed";
                Assert.True( await connection.UpdateAsync( u1 ).ConfigureAwait( false ) );

                obj = await connection.GetAsync<ObjectQ>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Set On Insert", obj.IgnoreUpdate );
            }
        }


        [Fact]
        public async Task IgnoreSelectAttributeAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { IgnoreSelect = "Set On Insert" };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                var obj = await connection.GetAsync<ObjectQ>( u1.Id ).ConfigureAwait( false );
                Assert.Null( obj.IgnoreSelect );
            }
        }

        [Fact]
        public async Task ComputedAttributeAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { Computed = "Ignored On Insert or Update" };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                var obj = await connection.GetAsync<ObjectQ>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Computed", obj.Computed );

                u1.IgnoreUpdate = "This should not be changed";
                Assert.True( await connection.UpdateAsync( u1 ).ConfigureAwait( false ) );

                obj = await connection.GetAsync<ObjectQ>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Computed", obj.Computed );
            }
        }

        [Fact]
        public async Task ReadOnlyAttributeAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { Readonly = "Ignored On Insert or Update" };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                var obj = await connection.GetAsync<ObjectQ>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Readonly", obj.Readonly );

                u1.IgnoreUpdate = "This should not be changed";
                Assert.True( await connection.UpdateAsync( u1 ).ConfigureAwait( false ) );

                obj = await connection.GetAsync<ObjectQ>( u1.Id ).ConfigureAwait( false );
                Assert.Equal( "Readonly", obj.Readonly );
            }
        }

        [Fact]
        public async Task ExistsQueryAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { Readonly = "Ignored On Insert or Update" };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                Assert.True( await connection.ExistsAsync<ObjectQ>( u1.Id ).ConfigureAwait( false ) );

                Assert.False( await connection.ExistsAsync<ObjectQ>( -100 ).ConfigureAwait( false ) );

            }
        }

        [Fact]
        public async Task ExistsClauseQueryAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { IgnoreUpdate = "FetchMe" };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                Assert.True( await connection.ExistsAsync<ObjectQ>( "[IgnoreUpdate] = @FetchMe", new { FetchMe = "FetchMe" } ).ConfigureAwait( false ) );

                Assert.False( await connection.ExistsAsync<ObjectQ>( "[IgnoreUpdate] = @FetchMe", new { FetchMe = "junk" } ).ConfigureAwait( false ) );

            }

        }


        [Fact]
        public async Task UpdateColumnsSpecifiedAsync ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new User { Name = "ValueA", Age = 33 };
                Assert.True( await connection.InsertAsync( u1 ).ConfigureAwait( false ) );

                u1.Name = "ValueB";
                u1.Age = 43; // should not be updated.

                await connection.UpdateAsync( u1, new string[] { "Name" } ).ConfigureAwait( false );

                var uf = await connection.GetAsync<User>( u1.Id ).ConfigureAwait( false );

                Assert.Equal( "ValueB", uf.Name );
                Assert.Equal( 33, uf.Age );

            }
        }

    }
}

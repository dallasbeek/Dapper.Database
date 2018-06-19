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
        protected static readonly bool IsAppVeyor = Environment.GetEnvironmentVariable( "Appveyor" )?.ToUpperInvariant() == "TRUE";

        public abstract IDbConnection GetConnection ();

        private IDbConnection GetOpenConnection ()
        {
            var connection = GetConnection();
            connection.Open();
            return connection;
        }

        [Fact]
        public void aaa ()
        {
            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<GenericType<string>>();
                var objectToInsert = new GenericType<string>
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "something"
                };
                connection.Insert( objectToInsert );

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

                connection.Insert( objectsToInsert );
                var list = connection.GetAll<GenericType<string>>();

                Assert.Equal( 3, list.Count() );
            }
        }


        [Fact]
        public void TypeWithGenericParameterCanBeInserted ()
        {
            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<GenericType<string>>();
                var objectToInsert = new GenericType<string>
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "something"
                };
                connection.Insert( objectToInsert );

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

                connection.Insert( objectsToInsert );
                var list = connection.GetAll<GenericType<string>>();

                Assert.Equal( 3, list.Count() );
            }
        }

        [Fact]
        public void TypeWithGenericParameterCanBeUpdated ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var objectToInsert = new GenericType<string>
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "something"
                };
                connection.Insert( objectToInsert );

                objectToInsert.Name = "somethingelse";
                connection.Update( objectToInsert );

                var updatedObject = connection.Get<GenericType<string>>( objectToInsert.Id );
                Assert.Equal( objectToInsert.Name, updatedObject.Name );
            }
        }

        [Fact]
        public void TypeWithGenericParameterCanBeDeleted ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var objectToInsert = new GenericType<string>
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "something"
                };
                connection.Insert( objectToInsert );

                bool deleted = connection.Delete( objectToInsert );
                Assert.True( deleted );
            }
        }

        [Fact]
        public void Issue418 ()
        {
            using ( var connection = GetOpenConnection() )
            {
                //update first (will fail) then insert
                //added for bug #418
                var updateObject = new ObjectX
                {
                    ObjectXId = Guid.NewGuid().ToString(),
                    Name = "Someone"
                };
                var updates = connection.Update( updateObject );
                Assert.False( updates );

                connection.DeleteAll<ObjectX>();

                var objectXId = Guid.NewGuid().ToString();
                var insertObject = new ObjectX
                {
                    ObjectXId = objectXId,
                    Name = "Someone else"
                };
                connection.Insert( insertObject );
                var list = connection.GetAll<ObjectX>();
                Assert.Single( list );
            }
        }

        /// <summary>
        /// Tests for issue #351 
        /// </summary>
        [Fact]
        public void InsertGetUpdateDeleteWithExplicitKey ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var guid = Guid.NewGuid().ToString();
                var o1 = new ObjectX { ObjectXId = guid, Name = "Foo" };
                var originalxCount = connection.Query<int>( "Select Count(*) From ObjectX" ).First();
                connection.Insert( o1 );
                var list1 = connection.Query<ObjectX>( "select * from ObjectX" ).ToList();
                Assert.Equal( list1.Count, originalxCount + 1 );
                o1 = connection.Get<ObjectX>( guid );
                Assert.Equal( o1.ObjectXId, guid );
                o1.Name = "Bar";
                connection.Update( o1 );
                o1 = connection.Get<ObjectX>( guid );
                Assert.Equal( "Bar", o1.Name );
                connection.Delete( o1 );
                o1 = connection.Get<ObjectX>( guid );
                Assert.Null( o1 );

                const int id = 42;
                var o2 = new ObjectY { ObjectYId = id, Name = "Foo" };
                var originalyCount = connection.Query<int>( "Select Count(*) From ObjectY" ).First();
                connection.Insert( o2 );
                var list2 = connection.Query<ObjectY>( "select * from ObjectY" ).ToList();
                Assert.Equal( list2.Count, originalyCount + 1 );
                o2 = connection.Get<ObjectY>( id );
                Assert.Equal( o2.ObjectYId, id );
                o2.Name = "Bar";
                connection.Update( o2 );
                o2 = connection.Get<ObjectY>( id );
                Assert.Equal( "Bar", o2.Name );
                connection.Delete( o2 );
                o2 = connection.Get<ObjectY>( id );
                Assert.Null( o2 );
            }
        }

        [Fact]
        public void GetAllWithExplicitKey ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var guid = Guid.NewGuid().ToString();
                var o1 = new ObjectX { ObjectXId = guid, Name = "Foo" };
                connection.Insert( o1 );

                var objectXs = connection.GetAll<ObjectX>().ToList();
                Assert.True( objectXs.Count > 0 );
                Assert.Equal( 1, objectXs.Count( x => x.ObjectXId == guid ) );
            }
        }

        [Fact]
        public void InsertGetUpdateDeleteWithExplicitKeyNamedId ()
        {
            using ( var connection = GetOpenConnection() )
            {
                const int id = 42;
                var o2 = new ObjectZ { Id = id, Name = "Foo" };
                connection.Insert( o2 );
                var list2 = connection.Query<ObjectZ>( "select * from ObjectZ" ).ToList();
                Assert.Single( list2 );
                o2 = connection.Get<ObjectZ>( id );
                Assert.Equal( o2.Id, id );
            }
        }

        [Fact]
        public void ShortIdentity ()
        {
            using ( var connection = GetOpenConnection() )
            {
                const string name = "First item";

                var s1 = new Stuff { Name = name };
                Assert.True( connection.Insert( s1 ) );
                var id = s1.TheId;
                Assert.True( id > 0 ); // 1-n are valid here, due to parallel tests
                var item = connection.Get<Stuff>( id );
                Assert.Equal( item.TheId, ( short ) id );
                Assert.Equal( item.Name, name );
            }
        }

        [Fact]
        public void NullDateTime ()
        {
            using ( var connection = GetOpenConnection() )
            {
                connection.Insert( new Stuff { Name = "First item" } );
                connection.Insert( new Stuff { Name = "Second item", Created = DateTime.Now } );
                var stuff = connection.Query<Stuff>( "select * from Stuff" ).ToList();
                Assert.Null( stuff[ 0 ].Created );
                Assert.NotNull( stuff.Last().Created );
            }
        }

        [Fact]
        public void TableName ()
        {
            using ( var connection = GetOpenConnection() )
            {
                // tests against "Automobiles" table (Table attribute)

                var c1 = new Car { Name = "Volvo" };
                Assert.True( connection.Insert( c1 ) );
                var id = c1.Id;
                var car = connection.Get<Car>( id );
                Assert.NotNull( car );
                Assert.Equal( "Volvo", car.Name );
                Assert.Equal( "Volvo", connection.Get<Car>( id ).Name );
                Assert.True( connection.Update( new Car { Id = ( int ) id, Name = "Saab" } ) );
                Assert.Equal( "Saab", connection.Get<Car>( id ).Name );
                Assert.True( connection.Delete( new Car { Id = ( int ) id } ) );
                Assert.Null( connection.Get<Car>( id ) );
            }
        }

        [Fact]
        public void TestSimpleGet ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new User { Name = "Adama", Age = 10 };
                Assert.True( connection.Insert( u1 ) );
                var id = u1.Id;
                var user = connection.Get<User>( id );
                Assert.Equal( user.Id, ( int ) id );
                Assert.Equal( "Adama", user.Name );
                connection.Delete( user );
            }
        }

        [Fact]
        public void TestClosedConnection ()
        {
            using ( var connection = GetConnection() )
            {
                Assert.True( connection.Insert( new User { Name = "Adama", Age = 10 } ) );
                var users = connection.GetAll<User>();
                Assert.NotEmpty( users );
            }
        }

        [Fact]
        public void InsertEnumerable ()
        {
            InsertHelper( src => src.AsEnumerable() );
        }

        [Fact]
        public void InsertArray ()
        {
            InsertHelper( src => src.ToArray() );
        }

        [Fact]
        public void InsertList ()
        {
            InsertHelper( src => src.ToList() );
        }

        private void InsertHelper<T> ( Func<IEnumerable<User>, T> helper )
            where T : class
        {
            const int numberOfEntities = 10;

            var users = new List<User>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new User { Name = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<User>();

                var retEnt = connection.Insert( helper( users ) );
                users = connection.Query<User>( "select * from Users" ).ToList();
                Assert.Equal( users.Count, numberOfEntities );
            }
        }

        [Fact]
        public void UpdateEnumerable ()
        {
            UpdateHelper( src => src.AsEnumerable() );
        }

        [Fact]
        public void UpdateArray ()
        {
            UpdateHelper( src => src.ToArray() );
        }

        [Fact]
        public void UpdateList ()
        {
            UpdateHelper( src => src.ToList() );
        }

        private void UpdateHelper<T> ( Func<IEnumerable<User>, T> helper )
            where T : class
        {
            const int numberOfEntities = 10;

            var users = new List<User>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new User { Name = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<User>();

                var total = connection.Insert( helper( users ) );
                //Assert.Equal(total, numberOfEntities);
                users = connection.Query<User>( "select * from Users" ).ToList();
                Assert.Equal( users.Count, numberOfEntities );
                foreach ( var user in users )
                {
                    user.Name += " updated";
                }
                connection.Update( helper( users ) );
                var name = connection.Query<User>( "select * from Users" ).First().Name;
                Assert.Contains( "updated", name );
            }
        }

        [Fact]
        public void DeleteEnumerable ()
        {
            DeleteHelper( src => src.AsEnumerable() );
        }

        [Fact]
        public void DeleteArray ()
        {
            DeleteHelper( src => src.ToArray() );
        }

        [Fact]
        public void DeleteList ()
        {
            DeleteHelper( src => src.ToList() );
        }

        private void DeleteHelper<T> ( Func<IEnumerable<User>, T> helper )
            where T : class
        {
            const int numberOfEntities = 10;

            var users = new List<User>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new User { Name = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<User>();

                var total = connection.Insert( helper( users ) );
                //Assert.Equal(total, numberOfEntities);
                users = connection.Query<User>( "select * from Users" ).ToList();
                Assert.Equal( users.Count, numberOfEntities );

                var usersToDelete = users.Take( 10 ).ToList();
                connection.Delete( helper( usersToDelete ) );
                users = connection.Query<User>( "select * from Users" ).ToList();
                Assert.Equal( users.Count, numberOfEntities - 10 );
            }
        }

        [Fact]
        public void InsertGetUpdate ()
        {
            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<User>();
                Assert.Null( connection.Get<User>( 3 ) );

                //insert with computed attribute that should be ignored
                var c1 = new Car { Name = "Volvo", Computed = "this property should be ignored" };
                Assert.True( connection.Insert( c1 ) );
                var car = connection.Get<Car>( c1.Id );
                Assert.Null( car.Computed );

                var u1 = new User { Name = "Adam", Age = 10 };
                Assert.True( connection.Insert( u1 ) );

                //get a user with "isdirty" tracking
                var user = connection.Get<IUser>( u1.Id );
                Assert.Equal( "Adam", user.Name );
                Assert.False( connection.Update( user ) );    //returns false if not updated, based on tracking
                user.Name = "Bob";
                Assert.True( connection.Update( user ) );    //returns true if updated, based on tracking
                user = connection.Get<IUser>( u1.Id );
                Assert.Equal( "Bob", user.Name );

                //get a user with no tracking
                var notrackedUser = connection.Get<User>( u1.Id );
                Assert.Equal( "Bob", notrackedUser.Name );
                Assert.True( connection.Update( notrackedUser ) );   //returns true, even though user was not changed
                notrackedUser.Name = "Cecil";
                Assert.True( connection.Update( notrackedUser ) );
                Assert.Equal( "Cecil", connection.Get<User>( u1.Id ).Name );

                Assert.Single( connection.Query<User>( "select * from Users" ) );
                Assert.True( connection.Delete( user ) );
                Assert.Empty( connection.Query<User>( "select * from Users" ) );

                Assert.False( connection.Update( notrackedUser ) );   //returns false, user not found
            }
        }

#if NET451
                [Fact(Skip = "Not parallel friendly - thinking about how to test this")]
                public void InsertWithCustomDbType()
                {
                    SqlMapperExtensions.GetDatabaseType = conn => "SQLiteConnection";

                    bool sqliteCodeCalled = false;
                    using (var connection = GetOpenConnection())
                    {
                        connection.DeleteAll<User>();
                        Assert.Null(connection.Get<User>(3));
                        try
                        {
                            connection.Insert(new User { Name = "Adam", Age = 10 });
                        }
                        catch (SqlCeException ex)
                        {
                            sqliteCodeCalled = ex.Message.IndexOf("There was an error parsing the query", StringComparison.OrdinalIgnoreCase) >= 0;
                        }
                        // ReSharper disable once EmptyGeneralCatchClause
                        catch (Exception)
                        {
                        }
                    }
                    SqlMapperExtensions.GetDatabaseType = null;

                    if (!sqliteCodeCalled)
                    {
                        throw new Exception("Was expecting sqlite code to be called");
                    }
                }
#endif

        [Fact]
        public void InsertWithCustomTableNameMapper ()
        {
            SqlMapperExtensions.TableNameMapper = type =>
            {
                switch ( type.Name() )
                {
                    case "Person":
                        return "People";
                    default:
                        var tableattr = type.GetCustomAttributes( false ).SingleOrDefault( attr => attr.GetType().Name == "TableAttribute" ) as dynamic;
                        if ( tableattr != null )
                            return tableattr.Name;

                        var name = type.Name + "s";
                        if ( type.IsInterface() && name.StartsWith( "I" ) )
                            return name.Substring( 1 );
                        return name;
                }
            };

            using ( var connection = GetOpenConnection() )
            {
                var p1 = new Person { Name = "Mr Mapper" };
                Assert.True( connection.Insert( p1 ) );
                Assert.Equal( 1, p1.Id );
                connection.GetAll<Person>();
            }
        }

        [Fact]
        public void GetAll ()
        {
            const int numberOfEntities = 10;

            var users = new List<User>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new User { Name = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<User>();

                var total = connection.Insert( users );
                //Assert.Equal(total, numberOfEntities);
                users = connection.GetAll<User>().ToList();
                Assert.Equal( users.Count, numberOfEntities );
                var iusers = connection.GetAll<IUser>().ToList();
                Assert.Equal( iusers.Count, numberOfEntities );
                for ( var i = 0; i < numberOfEntities; i++ )
                    Assert.Equal( iusers[ i ].Age, i );
            }
        }

        /// <summary>
        /// Test for issue #933
        /// </summary>
        [Fact]
        public void GetAndGetAllWithNullableValues ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var nd1 = new NullableDate { DateValue = new DateTime( 2011, 07, 14 ) };
                var nd2 = new NullableDate { DateValue = null };
                Assert.True( connection.Insert( nd1 ) );
                Assert.True( connection.Insert( nd2 ) );

                var value1 = connection.Get<INullableDate>( nd1.Id );
                Assert.Equal( new DateTime( 2011, 07, 14 ), value1.DateValue.Value );

                var value2 = connection.Get<INullableDate>( nd2.Id );
                Assert.True( value2.DateValue == null );

                var value3 = connection.GetAll<INullableDate>().ToList();
                Assert.Equal( new DateTime( 2011, 07, 14 ), value3[ 0 ].DateValue.Value );
                Assert.True( value3[ 1 ].DateValue == null );
            }
        }

        [Fact]
        public void Transactions ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var c1 = new Car { Name = "one car" };
                Assert.True( connection.Insert( c1 ) );   //insert outside transaction

                var tran = connection.BeginTransaction();
                var car = connection.Get<Car>( c1.Id, tran );
                var orgName = car.Name;
                car.Name = "Another car";
                connection.Update( car, tran );
                tran.Rollback();

                car = connection.Get<Car>( c1.Id );  //updates should have been rolled back
                Assert.Equal( car.Name, orgName );
            }
        }

        [Fact]
        public void InsertCheckKey ()
        {
            using ( var connection = GetOpenConnection() )
            {
                Assert.Null( connection.Get<IUser>( 3 ) );
                User user = new User { Name = "Adamb", Age = 10 };
                Assert.True( connection.Insert( user ) );
                Assert.True( user.Id > 0 );
            }
        }

        [Fact]
        public void BuilderSelectClause ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var rand = new Random( 8675309 );
                var data = new List<User>();
                for ( int i = 0; i < 100; i++ )
                {
                    var nU = new User { Age = rand.Next( 70 ), Id = i, Name = Guid.NewGuid().ToString() };
                    data.Add( nU );
                    connection.Insert( nU );
                }

                var builder = new SqlBuilder();
                var justId = builder.AddTemplate( "SELECT /**select**/ FROM Users" );
                var all = builder.AddTemplate( "SELECT Name, /**select**/, Age FROM Users" );

                builder.Select( "Id" );

                var ids = connection.Query<int>( justId.RawSql, justId.Parameters );
                var users = connection.Query<User>( all.RawSql, all.Parameters );

                foreach ( var u in data )
                {
                    if ( !ids.Any( i => u.Id == i ) ) throw new Exception( "Missing ids in select" );
                    if ( !users.Any( a => a.Id == u.Id && a.Name == u.Name && a.Age == u.Age ) ) throw new Exception( "Missing users in select" );
                }
            }
        }

        [Fact]
        public void BuilderTemplateWithoutComposition ()
        {
            var builder = new SqlBuilder();
            var template = builder.AddTemplate( "SELECT COUNT(*) FROM Users WHERE Age = @age", new { age = 5 } );

            if ( template.RawSql == null ) throw new Exception( "RawSql null" );
            if ( template.Parameters == null ) throw new Exception( "Parameters null" );

            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<User>();
                connection.Insert( new User { Age = 5, Name = "Testy McTestington" } );

                if ( connection.Query<int>( template.RawSql, template.Parameters ).Single() != 1 )
                    throw new Exception( "Query failed" );
            }
        }

        [Fact]
        public void InsertFieldWithReservedName ()
        {
            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<User>();
                var r1 = new Result() { Name = "Adam", Order = 1 };
                Assert.True( connection.Insert( r1 ) );

                var result = connection.Get<Result>( r1.Id );
                Assert.Equal( 1, result.Order );
            }
        }

        [Fact]
        public void DeleteAll ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new User { Name = "Alice", Age = 32 };
                var u2 = new User { Name = "Bob", Age = 33 };

                Assert.True( connection.Insert( u1 ) );
                Assert.True( connection.Insert( u2 ) );
                Assert.True( connection.DeleteAll<User>() );
                Assert.Null( connection.Get<User>( u1.Id ) );
                Assert.Null( connection.Get<User>( u2.Id ) );
            }
        }

        [Fact]
        public void IgnoreInsertAttribute ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { IgnoreInsert = "This should be ignored" };
                Assert.True( connection.Insert( u1 ) );

                var obj = connection.Get<ObjectQ>( u1.Id );
                Assert.Null( obj.IgnoreInsert );

                u1.IgnoreInsert = "This should now be changed";
                Assert.True( connection.Update( u1 ) );

                obj = connection.Get<ObjectQ>( u1.Id );
                Assert.Equal( "This should now be changed", obj.IgnoreInsert );

            }
        }

        [Fact]
        public void IgnoreUpdateAttribute ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { IgnoreUpdate = "Set On Insert" };
                Assert.True( connection.Insert( u1 ) );

                var obj = connection.Get<ObjectQ>( u1.Id );
                Assert.Equal( "Set On Insert", obj.IgnoreUpdate );

                u1.IgnoreUpdate = "This should not be changed";
                Assert.True( connection.Update( u1 ) );

                obj = connection.Get<ObjectQ>( u1.Id );
                Assert.Equal( "Set On Insert", obj.IgnoreUpdate );
            }
        }


        [Fact]
        public void IgnoreSelectAttribute ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { IgnoreSelect = "Set On Insert" };
                Assert.True( connection.Insert( u1 ) );

                var obj = connection.Get<ObjectQ>( u1.Id );
                Assert.Null( obj.IgnoreSelect );
            }
        }

        [Fact]
        public void ComputedAttribute ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { Computed = "Ignored On Insert or Update" };
                Assert.True( connection.Insert( u1 ) );

                var obj = connection.Get<ObjectQ>( u1.Id );
                Assert.Equal( "Computed", obj.Computed );

                u1.IgnoreUpdate = "This should not be changed";
                Assert.True( connection.Update( u1 ) );

                obj = connection.Get<ObjectQ>( u1.Id );
                Assert.Equal( "Computed", obj.Computed );
            }
        }

        [Fact]
        public void ReadOnlyAttribute ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { Readonly = "Ignored On Insert or Update" };
                Assert.True( connection.Insert( u1 ) );

                var obj = connection.Get<ObjectQ>( u1.Id );
                Assert.Equal( "Readonly", obj.Readonly );

                u1.IgnoreUpdate = "This should not be changed";
                Assert.True( connection.Update( u1 ) );

                obj = connection.Get<ObjectQ>( u1.Id );
                Assert.Equal( "Readonly", obj.Readonly );
            }
        }

        [Fact]
        public void ExistsQuery ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { };
                Assert.True( connection.Insert( u1 ) );

                Assert.True( connection.Exists<ObjectQ>( u1.Id ) );

                Assert.False( connection.Exists<ObjectQ>( -100 ) );

            }
        }

        [Fact]
        public void ExistsClauseQuery ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new ObjectQ { IgnoreUpdate = "FetchMe" };
                Assert.True( connection.Insert( u1 ) );

                Assert.True( connection.Exists<ObjectQ>( "[IgnoreUpdate] = @FetchMe", new { FetchMe = "FetchMe" } ) );

                Assert.False( connection.Exists<ObjectQ>( "[IgnoreUpdate] = @FetchMe", new { FetchMe = "junk" } ) );

            }
        }

        [Fact]
        public void UpdateColumnsSpecified ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new User { Name = "ValueA", Age = 33 };
                Assert.True( connection.Insert( u1 ) );

                u1.Name = "ValueB";
                u1.Age = 43; // should not be updated.

                connection.Update( u1, new string[] { "Name" } );

                var uf = connection.Get<User>( u1.Id );

                Assert.Equal( "ValueB", uf.Name );
                Assert.Equal( 33, uf.Age );

            }
        }

        [Fact]
        public void Upsert ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new User { Name = "ValueA", Age = 33 };
                Assert.True( connection.Upsert( u1, new string[] { "Name" }, i => i.Name = "InsertName", u => u.Name = "UpdateName" ) );

                var uf1 = connection.Get<User>( u1.Id );

                Assert.Equal( "InsertName", uf1.Name );
                Assert.Equal( 33, uf1.Age );

                u1.Age = 43; // should not be updated.

                Assert.True( connection.Upsert( u1, new string[] { "Name" }, i => i.Name = "InsertName", u => u.Name = "UpdateName" ) );

                var uf2 = connection.Get<User>( u1.Id );

                Assert.Equal( "UpdateName", uf2.Name );
                Assert.Equal( 33, uf2.Age );

            }
        }

    }
}

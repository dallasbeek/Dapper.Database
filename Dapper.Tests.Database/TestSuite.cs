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
        public void TypeWithGenericParameterCanBeInserted ()
        {
            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<GenericType<string>>();
                var objectToInsert = new GenericType<string>
                {
                    SId = Guid.NewGuid().ToString(),
                    FirstName = "something"
                };
                connection.Insert( objectToInsert );

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
                    SId = Guid.NewGuid().ToString(),
                    FirstName = "something"
                };
                connection.Insert( objectToInsert );

                objectToInsert.FirstName = "somethingelse";
                connection.Update( objectToInsert );

                var updatedObject = connection.Get<GenericType<string>>( objectToInsert.SId );
                Assert.Equal( objectToInsert.FirstName, updatedObject.FirstName );
            }
        }

        [Fact]
        public void TypeWithGenericParameterCanBeDeleted ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var objectToInsert = new GenericType<string>
                {
                    SId = Guid.NewGuid().ToString(),
                    FirstName = "something"
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
                var updateObject = new CustomerStringId
                {
                    SId = Guid.NewGuid().ToString(),
                    FirstName = "Someone"
                };
                var updates = connection.Update( updateObject );
                Assert.False( updates );

                connection.DeleteAll<CustomerStringId>();

                var objectXId = Guid.NewGuid().ToString();
                var insertObject = new CustomerStringId
                {
                    SId = objectXId,
                    FirstName = "Someone else"
                };
                connection.Insert( insertObject );
                var list = connection.GetAll<CustomerStringId>();
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
                var o1 = new CustomerStringId { SId = guid, FirstName = "Foo" };
                var originalxCount = connection.Query<int>( "Select Count(*) From Customers" ).First();
                connection.Insert( o1 );
                var list1 = connection.Query<CustomerStringId>( "select * from Customers" ).ToList();
                Assert.Equal( list1.Count, originalxCount + 1 );
                o1 = connection.Get<CustomerStringId>( guid );
                Assert.Equal( o1.SId, guid );
                o1.FirstName = "Bar";
                connection.Update( o1 );
                o1 = connection.Get<CustomerStringId>( guid );
                Assert.Equal( "Bar", o1.FirstName );
                connection.Delete( o1 );
                o1 = connection.Get<CustomerStringId>( guid );
                Assert.Null( o1 );

                const int id = 42;
                var o2 = new CustomerIntegerId { IId = id, FirstName = "Foo" };
                var originalyCount = connection.Query<int>( "Select Count(*) From Customers" ).First();
                connection.Insert( o2 );
                var list2 = connection.Query<CustomerIntegerId>( "select * from Customers" ).ToList();
                Assert.Equal( list2.Count, originalyCount + 1 );
                o2 = connection.Get<CustomerIntegerId>( id );
                Assert.Equal( o2.IId, id );
                o2.FirstName = "Bar";
                connection.Update( o2 );
                o2 = connection.Get<CustomerIntegerId>( id );
                Assert.Equal( "Bar", o2.FirstName );
                connection.Delete( o2 );
                o2 = connection.Get<CustomerIntegerId>( id );
                Assert.Null( o2 );
            }
        }

        [Fact]
        public void GetAllWithExplicitKey ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var guid = Guid.NewGuid().ToString();
                var o1 = new CustomerStringId { SId = guid, FirstName = "Foo" };
                connection.Insert( o1 );

                var objectXs = connection.GetAll<CustomerStringId>().ToList();
                Assert.True( objectXs.Count > 0 );
                Assert.Equal( 1, objectXs.Count( x => x.SId == guid ) );
            }
        }

        [Fact]
        public void ShortIdentity ()
        {
            using ( var connection = GetOpenConnection() )
            {
                const string name = "First item";

                var s1 = new CustomerShortId { FirstName = name };
                Assert.True( connection.Insert( s1 ) );
                var id = s1.Id;
                Assert.True( id > 0 ); // 1-n are valid here, due to parallel tests
                var item = connection.Get<CustomerShortId>( id );
                Assert.Equal( item.Id, ( short ) id );
                Assert.Equal( item.FirstName, name );
            }
        }

        [Fact]
        public void NullDateTime ()
        {
            using ( var connection = GetOpenConnection() )
            {
                connection.Insert( new CustomerShortId { FirstName = "First item" } );
                connection.Insert( new CustomerShortId { FirstName = "Second item", CreatedOn = DateTime.Now } );
                var stuff = connection.Query<CustomerShortId>( "select * from Customers" ).ToList();
                Assert.Null( stuff[ 0 ].CreatedOn );
                Assert.NotNull( stuff.Last().CreatedOn );
            }
        }

        [Fact]
        public void TableName ()
        {
            using ( var connection = GetOpenConnection() )
            {
                // tests against "Automobiles" table (Table attribute)

                var c1 = new Car { FirstName = "Volvo" };
                Assert.True( connection.Insert( c1 ) );
                var id = c1.Id;
                var car = connection.Get<Car>( id );
                Assert.NotNull( car );
                Assert.Equal( "Volvo", car.FirstName );
                Assert.Equal( "Volvo", connection.Get<Car>( id ).FirstName );
                Assert.True( connection.Update( new Car { Id = ( int ) id, FirstName = "Saab" } ) );
                Assert.Equal( "Saab", connection.Get<Car>( id ).FirstName );
                Assert.True( connection.Delete( new Car { Id = ( int ) id } ) );
                Assert.Null( connection.Get<Car>( id ) );
            }
        }

        [Fact]
        public void TestSimpleGet ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerProxy { FirstName = "Adama", Age = 10 };
                Assert.True( connection.Insert( u1 ) );
                var id = u1.Id;
                var user = connection.Get<CustomerProxy>( id );
                Assert.Equal( user.Id, ( int ) id );
                Assert.Equal( "Adama", user.FirstName );
                connection.Delete( user );
            }
        }

        [Fact]
        public void TestClosedConnection ()
        {
            using ( var connection = GetConnection() )
            {
                Assert.True( connection.Insert( new CustomerProxy { FirstName = "Adama", Age = 10 } ) );
                var users = connection.GetAll<CustomerProxy>();
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

        private void InsertHelper<T> ( Func<IEnumerable<CustomerProxy>, T> helper )
            where T : class
        {
            const int numberOfEntities = 10;

            var users = new List<CustomerProxy>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new CustomerProxy { FirstName = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<CustomerProxy>();

                var retEnt = connection.Insert( helper( users ) );
                users = connection.Query<CustomerProxy>( "select * from Customers" ).ToList();
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

        private void UpdateHelper<T> ( Func<IEnumerable<CustomerProxy>, T> helper )
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
                foreach ( var user in users )
                {
                    user.FirstName += " updated";
                }
                connection.Update( helper( users ) );
                var name = connection.Query<CustomerProxy>( "select * from Customers" ).First().FirstName;
                Assert.Contains( "updated", name );
            }
        }


        [Fact]
        public void InsertGetUpdate ()
        {
            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<CustomerProxy>();
                Assert.Null( connection.Get<CustomerProxy>( 3 ) );

                //insert with computed attribute that should be ignored
                var c1 = new Car { FirstName = "Volvo", Computed = "this property should be ignored" };
                Assert.True( connection.Insert( c1 ) );
                var car = connection.Get<Car>( c1.Id );
                Assert.Null( car.Computed );

                var u1 = new CustomerProxy { FirstName = "Adam", Age = 10 };
                Assert.True( connection.Insert( u1 ) );

                //get a user with "isdirty" tracking
                var user = connection.Get<ICustomer>( u1.Id );
                Assert.Equal( "Adam", user.FirstName );
                Assert.False( connection.Update( user ) );    //returns false if not updated, based on tracking
                user.FirstName = "Bob";
                Assert.True( connection.Update( user ) );    //returns true if updated, based on tracking
                user = connection.Get<ICustomer>( u1.Id );
                Assert.Equal( "Bob", user.FirstName );

                //get a user with no tracking
                var notrackedUser = connection.Get<CustomerProxy>( u1.Id );
                Assert.Equal( "Bob", notrackedUser.FirstName );
                Assert.True( connection.Update( notrackedUser ) );   //returns true, even though user was not changed
                notrackedUser.FirstName = "Cecil";
                Assert.True( connection.Update( notrackedUser ) );
                Assert.Equal( "Cecil", connection.Get<CustomerProxy>( u1.Id ).FirstName );

                Assert.True( connection.Delete( user ) );
                Assert.Empty( connection.Query<CustomerProxy>( "select * from Customers where Id = @Id", new { Id = u1.Id } ) );

                Assert.False( connection.Update( notrackedUser ) );   //returns false, user not found
            }
        }

        [Fact]
        public void InsertWithCustomTableNameMapper ()
        {
            SqlMapperExtensions.TableNameMapper = type =>
            {
                switch ( type.Name() )
                {
                    case "CustomerMapped":
                        return "Customers";
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
                var p1 = new CustomerMapped { FirstName = "Mr Mapper" };
                Assert.True( connection.Insert( p1 ) );
            }
        }

        [Fact]
        public void GetAll ()
        {
            const int numberOfEntities = 10;

            var users = new List<CustomerProxy>();
            for ( var i = 0; i < numberOfEntities; i++ )
                users.Add( new CustomerProxy { FirstName = "User " + i, Age = i } );

            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<CustomerProxy>();

                var total = connection.Insert( users );
                //Assert.Equal(total, numberOfEntities);
                users = connection.GetAll<CustomerProxy>().ToList();
                Assert.Equal( users.Count, numberOfEntities );
                var iusers = connection.GetAll<ICustomer>().ToList();
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
                var nd1 = new CustomerProxy { UpdatedOn = new DateTime( 2011, 07, 14 ) };
                var nd2 = new CustomerProxy { UpdatedOn = null };
                Assert.True( connection.Insert( nd1 ) );
                Assert.True( connection.Insert( nd2 ) );

                var value1 = connection.Get<ICustomer>( nd1.Id );
                Assert.Equal( new DateTime( 2011, 07, 14 ), value1.UpdatedOn.Value );

                var value2 = connection.Get<ICustomer>( nd2.Id );
                Assert.True( value2.UpdatedOn == null );

                var value3 = connection.GetAll<ICustomer>().ToList();
                Assert.Equal( new DateTime( 2011, 07, 14 ), value3[ 0 ].UpdatedOn.Value );
                Assert.True( value3[ 1 ].UpdatedOn == null );
            }
        }

        [Fact]
        public void Transactions ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var c1 = new Car { FirstName = "one car" };
                Assert.True( connection.Insert( c1 ) );   //insert outside transaction

                var tran = connection.BeginTransaction();
                var car = connection.Get<Car>( c1.Id, tran );
                var orgName = car.FirstName;
                car.FirstName = "Another car";
                connection.Update( car, tran );
                tran.Rollback();

                car = connection.Get<Car>( c1.Id );  //updates should have been rolled back
                Assert.Equal( car.FirstName, orgName );
            }
        }

        [Fact]
        public void InsertCheckKey ()
        {
            using ( var connection = GetOpenConnection() )
            {
                Assert.Null( connection.Get<ICustomer>( 3 ) );
                CustomerProxy user = new CustomerProxy { FirstName = "Adamb", Age = 10 };
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
                var data = new List<CustomerProxy>();
                for ( int i = 0; i < 100; i++ )
                {
                    var nU = new CustomerProxy { Age = rand.Next( 70 ), Id = i, FirstName = Guid.NewGuid().ToString() };
                    data.Add( nU );
                    connection.Insert( nU );
                }

                var builder = new SqlBuilder();
                var justId = builder.AddTemplate( "SELECT /**select**/ FROM Customers" );
                var all = builder.AddTemplate( "SELECT FirstName, /**select**/, Age FROM Customers" );

                builder.Select( "Id" );

                var ids = connection.Query<int>( justId.RawSql, justId.Parameters );
                var users = connection.Query<CustomerProxy>( all.RawSql, all.Parameters );

                foreach ( var u in data )
                {
                    if ( !ids.Any( i => u.Id == i ) ) throw new Exception( "Missing ids in select" );
                    if ( !users.Any( a => a.Id == u.Id && a.FirstName == u.FirstName && a.Age == u.Age ) ) throw new Exception( "Missing users in select" );
                }
            }
        }

        [Fact]
        public void BuilderTemplateWithoutComposition ()
        {
            var builder = new SqlBuilder();
            var template = builder.AddTemplate( "SELECT COUNT(*) FROM Customers WHERE Age = @age", new { age = 5 } );

            if ( template.RawSql == null ) throw new Exception( "RawSql null" );
            if ( template.Parameters == null ) throw new Exception( "Parameters null" );

            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<CustomerProxy>();
                connection.Insert( new CustomerProxy { Age = 5, FirstName = "Testy McTestington" } );

                if ( connection.Query<int>( template.RawSql, template.Parameters ).Single() != 1 )
                    throw new Exception( "Query failed" );
            }
        }

        [Fact]
        public void InsertFieldWithReservedName ()
        {
            using ( var connection = GetOpenConnection() )
            {
                connection.DeleteAll<CustomerProxy>();
                var r1 = new Result() { FirstName = "Adam", Age = 1 };
                Assert.True( connection.Insert( r1 ) );

                var result = connection.Get<Result>( r1.Id );
                Assert.Equal( 1, result.Age );
            }
        }

        [Fact]
        public void IgnoreInsertAttribute ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerAttribute { FirstName = "This should be ignored" };
                Assert.True( connection.Insert( u1 ) );

                var obj = connection.Get<CustomerAttribute>( u1.Id );
                Assert.Null( obj.FirstName );

                u1.FirstName = "This should now be changed";
                Assert.True( connection.Update( u1 ) );

                obj = connection.Get<CustomerAttribute>( u1.Id );
                Assert.Equal( "This should now be changed", obj.FirstName );

            }
        }

        [Fact]
        public void IgnoreUpdateAttribute ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerAttribute { LastName = "Set On Insert" };
                Assert.True( connection.Insert( u1 ) );

                var obj = connection.Get<CustomerAttribute>( u1.Id );
                Assert.Equal( "Set On Insert", obj.LastName );

                u1.LastName = "This should not be changed";
                Assert.True( connection.Update( u1 ) );

                obj = connection.Get<CustomerAttribute>( u1.Id );
                Assert.Equal( "Set On Insert", obj.LastName );
            }
        }


        [Fact]
        public void IgnoreSelectAttribute ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerAttribute { Age = 100 };
                Assert.True( connection.Insert( u1 ) );

                var obj = connection.Get<CustomerAttribute>( u1.Id );
                Assert.Null( obj.Age );
            }
        }

        [Fact]
        public void ComputedAttribute ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerAttribute { FirstName = "Jim", LastName = "Bob", FullName = "Ignored On Insert or Update" };
                Assert.True( connection.Insert( u1 ) );

                var obj = connection.Get<CustomerAttribute>( u1.Id );
                Assert.Equal( " Bob", obj.FullName );

                u1.LastName = "This should not be changed";
                Assert.True( connection.Update( u1 ) );

                obj = connection.Get<CustomerAttribute>( u1.Id );
                Assert.Equal( "Jim Bob", obj.FullName );
            }
        }

        [Fact]
        public void ReadOnlyAttribute ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerAttribute { GId = Guid.NewGuid() };
                Assert.True( connection.Insert( u1 ) );

                var obj = connection.Get<CustomerAttribute>( u1.Id );
                Assert.Equal(Guid.Empty, obj.GId );

                u1.LastName = "This should not be changed";
                Assert.True( connection.Update( u1 ) );

                obj = connection.Get<CustomerAttribute>( u1.Id );
                Assert.Equal( Guid.Empty, obj.GId );
            }
        }

        [Fact]
        public void UpdateColumnsSpecified ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerProxy { FirstName = "ValueA", Age = 33 };
                Assert.True( connection.Insert( u1 ) );

                u1.FirstName = "ValueB";
                u1.Age = 43; // should not be updated.

                connection.Update( u1, new string[] { "FirstName" } );

                var uf = connection.Get<CustomerProxy>( u1.Id );

                Assert.Equal( "ValueB", uf.FirstName );
                Assert.Equal( 33, uf.Age );

            }
        }

        [Fact]
        public void Upsert ()
        {
            using ( var connection = GetOpenConnection() )
            {
                var u1 = new CustomerProxy { FirstName = "ValueA", Age = 33 };
                Assert.True( connection.Upsert( u1, new string[] { "FirstName" }, i => i.FirstName = "InsertName", u => u.FirstName = "UpdateName" ) );

                var uf1 = connection.Get<CustomerProxy>( u1.Id );

                Assert.Equal( "InsertName", uf1.FirstName );
                Assert.Equal( 33, uf1.Age );

                u1.Age = 43; // should not be updated.

                Assert.True( connection.Upsert( u1, new string[] { "FirstName" }, i => i.FirstName = "InsertName", u => u.FirstName = "UpdateName" ) );

                var uf2 = connection.Get<CustomerProxy>( u1.Id );

                Assert.Equal( "UpdateName", uf2.FirstName );
                Assert.Equal( 33, uf2.Age );

            }
        }

    }
}

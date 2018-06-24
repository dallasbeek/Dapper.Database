using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Xunit;
using Xunit.Sdk;
using Dapper.Database.Extensions;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Collections.Generic;

#if NET452
using System.Data.SqlServerCe;
using System.Transactions;
#endif

namespace Dapper.Tests.Database
{
    // The test suites here implement TestSuiteBase so that each provider runs
    // the entire set of tests without declarations per method
    // If we want to support a new provider, they need only be added here - not in multiple places

#if !NET452
    [XunitTestCaseDiscoverer( "Dapper.Tests.SkippableFactDiscoverer", "Dapper.Tests.Contrib" )]
    [AttributeUsage( AttributeTargets.Method, AllowMultiple = false )]
    public class SkippableFactAttribute : FactAttribute
    {
    }
#endif

    public class SqlServerTestSuite : TestSuite
    {
        private const string DbName = "tempdb";
        public static string ConnectionString =>
            IsAppVeyor
                ? @"Server=(local)\SQL2017;Database=tempdb;User ID=sa;Password=Password12!"
                : $"Data Source=(local)\\Dallas;Initial Catalog={DbName};Integrated Security=True";
        public override IDbConnection GetConnection () => new SqlConnection( ConnectionString );

        static SqlServerTestSuite ()
        {
            using ( var connection = new SqlConnection( ConnectionString ) )
            {
                // ReSharper disable once AccessToDisposedClosure
                Action<string> dropTable = name => connection.Execute( $"IF OBJECT_ID('{name}', 'U') IS NOT NULL DROP TABLE [{name}]; " );
                connection.Open();
                dropTable( "Customers" );
                connection.Execute( @"CREATE TABLE [dbo].[Customers](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [IId] [int] NULL,
	                [GId] [uniqueidentifier] NULL,
	                [SId] [varchar](100) NULL,
	                [FirstName] [nvarchar](100) NULL,
	                [LastName] [nvarchar](100) NULL,
	                [FullName]  AS ((isnull([FirstName],'')+' ')+isnull([LastName],'')),
	                [Age] [int] NULL,
	                [UpdatedOn] [datetime] NULL,
	                [CreatedOn] [datetime] NULL
                );" );
                dropTable( "Stuff" );
                dropTable( "People" );
                dropTable( "Users" );
                dropTable( "Automobiles" );
                dropTable( "Results" );
                dropTable( "ObjectX" );
                dropTable( "ObjectY" );
                dropTable( "ObjectZ" );
                dropTable( "GenericType" );
                dropTable( "NullableDates" );
                dropTable( "PkGuid" );
                dropTable( "ObjectQ" );
            }
        }

#if NET452
        [Fact]
        public void TransactionScope ()
        {
            using ( var txscope = new TransactionScope() )
            {
                using ( var connection = GetConnection() )
                {
                    var c1 = new Car { FirstName = "one car" };

                    Assert.True( connection.Insert( c1 ) );   //inser car within transaction

                    txscope.Dispose();  //rollback

                    Assert.Null( connection.Get<Car>( c1.Id ) );   //returns null - car with that id should not exist
                }
            }
        }
#endif

        [Fact]
        [Trait("Category", "Exists")]
        public void GetComposite()
        {
            using (var connection = GetConnection())
            {
                var u1 = new CustomerComposite { IId = 8, GId = Guid.NewGuid(), Age = 55, FirstName = "Emily" };
                Assert.True(connection.Insert(u1));
                var ur1 = connection.Get<CustomerComposite>(u1);
                Assert.Equal(u1.IId, ur1.IId);
                Assert.Equal(u1.GId, ur1.GId);
                Assert.Equal(u1.FirstName, ur1.FirstName);
                Assert.Equal(u1.Age, ur1.Age);
                connection.Delete(u1);

            }
        }


        [Fact]
        public void ComputedAttribute()
        {
            using (var connection = GetConnection())
            {
                var u1 = new CustomerAttribute { FirstName = "Jim", LastName = "Bob", FullName = "Ignored On Insert or Update" };
                Assert.True(connection.Insert(u1));
                Assert.Equal(" Bob", u1.FullName);

                var obj = connection.Get<CustomerAttribute>(u1.Id);
                Assert.Equal(" Bob", obj.FullName);

                u1.LastName = "This should not be changed";
                Assert.True(connection.Update(u1));
                Assert.Equal("Jim Bob", u1.FullName);

                obj = connection.Get<CustomerAttribute>(u1.Id);
                Assert.Equal("Jim Bob", obj.FullName);
            }
        }


        /// <summary>
        /// Tests for issue #351 
        /// </summary>
        [Fact]
        public void InsertGetUpdateDeleteWithExplicitKey()
        {
            using (var connection = GetConnection())
            {
                var guid = Guid.NewGuid().ToString();
                var o1 = new CustomerStringId { SId = guid, FirstName = "Foo" };
                var originalxCount = connection.Query<long>("Select Count(*) From Customers").First();
                connection.Insert(o1);
                var list1 = connection.Query<CustomerStringId>("select * from Customers").ToList();
                Assert.Equal(list1.Count, originalxCount + 1);
                o1 = connection.Get<CustomerStringId>(guid);
                Assert.Equal(o1.SId, guid);
                o1.FirstName = "Bar";
                connection.Update(o1);
                o1 = connection.Get<CustomerStringId>(guid);
                Assert.Equal("Bar", o1.FirstName);
                connection.Delete(o1);
                o1 = connection.Get<CustomerStringId>(guid);
                Assert.Null(o1);

                const int id = 42;
                var o2 = new CustomerIntegerId { IId = id, FirstName = "Foo" };
                var originalyCount = connection.Query<int>("Select Count(*) From Customers").First();
                connection.Insert(o2);
                var list2 = connection.Query<CustomerIntegerId>("select * from Customers").ToList();
                Assert.Equal(list2.Count, originalyCount + 1);
                o2 = connection.Get<CustomerIntegerId>(id);
                Assert.Equal(o2.IId, id);
                o2.FirstName = "Bar";
                connection.Update(o2);
                o2 = connection.Get<CustomerIntegerId>(id);
                Assert.Equal("Bar", o2.FirstName);
                connection.Delete(o2);
                o2 = connection.Get<CustomerIntegerId>(id);
                Assert.Null(o2);
            }
        }


        [Fact]
        public void BuilderSelectClause()
        {
            using (var connection = GetConnection())
            {
                var rand = new Random(8675309);
                var data = new List<CustomerProxy>();
                for (int i = 0; i < 100; i++)
                {
                    var nU = new CustomerProxy { Age = rand.Next(70), Id = i, FirstName = Guid.NewGuid().ToString() };
                    data.Add(nU);
                    connection.Insert(nU);
                }

                var builder = new SqlBuilder();
                var justId = builder.AddTemplate("SELECT /**select**/ FROM Customers");
                var all = builder.AddTemplate("SELECT FirstName, /**select**/, Age FROM Customers");

                builder.Select("Id");

                var ids = connection.Query<long>(justId.RawSql, justId.Parameters);
                var users = connection.Query<CustomerProxy>(all.RawSql, all.Parameters);

                foreach (var u in data)
                {
                    if (!ids.Any(i => u.Id == i)) throw new Exception("Missing ids in select");
                    if (!users.Any(a => a.Id == u.Id && a.FirstName == u.FirstName && a.Age == u.Age)) throw new Exception("Missing users in select");
                }
            }
        }
    }

    //public class MySqlServerTestSuite : TestSuite
    //{
    //    private const string DbName = "DapperContribTests";

    //    public static string ConnectionString { get; } =
    //        IsAppVeyor
    //            ? "Server=localhost;Uid=root;Pwd=Password12!;SslMode=none"
    //            : "Server=localhost;Uid=test;Pwd=pass;";

    //    public override IDbConnection GetConnection()
    //    {
    //        if (_skip) throw new SkipTestException("Skipping MySQL Tests - no server.");
    //        return new MySqlConnection(ConnectionString);
    //    }

    //    private static readonly bool _skip;

    //    static MySqlServerTestSuite()
    //    {
    //        try
    //        {
    //            using (var connection = new MySqlConnection(ConnectionString))
    //            {
    //                // ReSharper disable once AccessToDisposedClosure
    //                Action<string> dropTable = name => connection.Execute($"DROP TABLE IF EXISTS `{name}`;");
    //                connection.Open();
    //                connection.Execute($"DROP DATABASE IF EXISTS {DbName}; CREATE DATABASE {DbName}; USE {DbName};");
    //                dropTable("Stuff");
    //                connection.Execute("CREATE TABLE Stuff (TheId int not null AUTO_INCREMENT PRIMARY KEY, Name nvarchar(100) not null, Created DateTime null);");
    //                dropTable("People");
    //                connection.Execute("CREATE TABLE People (Id int not null AUTO_INCREMENT PRIMARY KEY, Name nvarchar(100) not null);");
    //                dropTable("Users");
    //                connection.Execute("CREATE TABLE Users (Id int not null AUTO_INCREMENT PRIMARY KEY, Name nvarchar(100) not null, Age int not null);");
    //                dropTable("Automobiles");
    //                connection.Execute("CREATE TABLE Automobiles (Id int not null AUTO_INCREMENT PRIMARY KEY, Name nvarchar(100) not null);");
    //                dropTable("Results");
    //                connection.Execute("CREATE TABLE Results (Id int not null AUTO_INCREMENT PRIMARY KEY, Name nvarchar(100) not null, `Order` int not null);");
    //                dropTable("ObjectX");
    //                connection.Execute("CREATE TABLE ObjectX (ObjectXId nvarchar(100) not null, Name nvarchar(100) not null);");
    //                dropTable("ObjectY");
    //                connection.Execute("CREATE TABLE ObjectY (ObjectYId int not null, Name nvarchar(100) not null);");
    //                dropTable("ObjectZ");
    //                connection.Execute("CREATE TABLE ObjectZ (Id int not null, Name nvarchar(100) not null);");
    //                dropTable("GenericType");
    //                connection.Execute("CREATE TABLE GenericType (Id nvarchar(100) not null, Name nvarchar(100) not null);");
    //                dropTable("NullableDates");
    //                connection.Execute("CREATE TABLE NullableDates (Id int not null AUTO_INCREMENT PRIMARY KEY, DateValue DateTime);");
    //            }
    //        }
    //        catch (MySqlException e)
    //        {
    //            if (e.Message.Contains("Unable to connect"))
    //                _skip = true;
    //            else
    //                throw;
    //        }
    //    }
    //}

    public class SQLiteTestSuite : TestSuite
    {
        private const string FileName = "Test.DB.sqlite";
        public static string ConnectionString => $"Filename=./{FileName};Mode=ReadWriteCreate;";
        public override IDbConnection GetConnection() => new SqliteConnection(ConnectionString);

        static SQLiteTestSuite()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute(@"CREATE TABLE Customers(
	                Id integer primary key autoincrement not null,
	                IId integer null,
	                GId nvarchar(100) null,
	                SId nvarchar(100) null,
	                FirstName nvarchar(100) null,
	                LastName nvarchar(100) null,
	                FullName nvarchar(100) null,
	                Age integer null,
	                UpdatedOn datetime null,
	                CreatedOn datetime null
                );");
            }
        }
    }

#if NET452
    public class SqlCETestSuite : TestSuite
    {
        const string FileName = "Test.DB.sdf";
        public static string ConnectionString => $"Data Source={FileName};";
        public override IDbConnection GetConnection() => new SqlCeConnection(ConnectionString);

        static SqlCETestSuite()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            var engine = new SqlCeEngine(ConnectionString);
            engine.CreateDatabase();
            using (var connection = new SqlCeConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute(@"CREATE TABLE Customers(
	                Id int IDENTITY(1,1) not null,
	                IId int null,
	                GId nvarchar(100) null,
	                SId nvarchar(100) null,
	                FirstName nvarchar(100) null,
	                LastName nvarchar(100) null,
	                FullName nvarchar(100) null,
	                Age int null,
	                UpdatedOn datetime null,
	                CreatedOn datetime null
                );");
            }
            Console.WriteLine("Created database");
        }
    }
#endif
}

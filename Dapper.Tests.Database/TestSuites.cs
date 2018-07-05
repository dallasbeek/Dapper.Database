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
using System.Text.RegularExpressions;
using Dapper;

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
    [XunitTestCaseDiscoverer("Dapper.Database.SkippableFactDiscoverer", "Dapper.Tests.Database")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SkippableFactAttribute : FactAttribute
    {
    }
#endif

    [Trait("Provider", "SqlServer")]
    [Provider(Provider.SqlServer)]
    public partial class SqlServerTestSuite : TestSuite
    {
        private const string DbName = "tempdb";
        public static string ConnectionString =>
            IsAppVeyor
                ? @"Server=(local)\SQL2017;Database=tempdb;User ID=sa;Password=Password12!"
                : $"Data Source=(local)\\Dallas;Initial Catalog={DbName};Integrated Security=True";

        public override IDbConnection GetConnection()
        {
            if (_skip) throw new SkipTestException("Skipping Sql Server Tests - no server.");
            return new SqlConnection(ConnectionString);
        }

        public Provider GetProvider() => Provider.SqlServer;

        private static readonly bool _skip;

        static SqlServerTestSuite()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    // ReSharper disable once AccessToDisposedClosure
                    //Action<string> dropTable = name => connection.Execute($"IF OBJECT_ID('{name}', 'U') IS NOT NULL DROP TABLE [{name}]; ");
                    connection.Open();
                    //    dropTable("Customers");
                    //    connection.Execute(@"CREATE TABLE [dbo].[Customers](
                    // [Id] [int] IDENTITY(1,1) NOT NULL,
                    // [IId] [int] NULL,
                    // [GId] [uniqueidentifier] NULL,
                    // [SId] [varchar](100) NULL,
                    // [FirstName] [nvarchar](100) NULL,
                    // [LastName] [nvarchar](100) NULL,
                    // [FullName]  AS ((isnull([FirstName],'')+' ')+isnull([LastName],'')),
                    // [Age] [int] NULL,
                    // [UpdatedOn] [datetime] NULL,
                    // [CreatedOn] [datetime] NULL
                    //);");

                    var awfile = File.ReadAllText("sqlserverawlite.sql");
                    connection.Execute(awfile);

                }
            }
            catch (SqlException e)
            {
                if (e.Message.Contains("The server was not found "))
                    _skip = true;
                else
                    throw;
            }
        }
    }

    //[Provider(Provider.SqlServer)]
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

    //    [Trait("Provider", "SQLite")]
    //    [Provider(Provider.SQLite)]
    //    public class SQLiteTestSuite : TestSuite
    //    {
    //        private const string FileName = "Test.DB.sqlite";
    //        public static string ConnectionString => $"Filename=./{FileName};Mode=ReadWriteCreate;";
    //        public override IDbConnection GetConnection() => new SqliteConnection(ConnectionString);

    //        public Provider GetProvider() => Provider.SqlCE;

    //        static SQLiteTestSuite()
    //        {
    //            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());
    //            SqlMapper.AddTypeHandler<decimal>(new NumericTypeHandler());

    //            //if (File.Exists(FileName))
    //            //{
    //            //    File.Delete(FileName);
    //            //}

    //            using (var connection = new SqliteConnection(ConnectionString))
    //            {
    //                if (!File.Exists(FileName))
    //                {
    //                    connection.Open();

    //                    var awfile = File.ReadAllText("sqliteawlite.sql");
    //                    connection.Execute(awfile);

    //                }
    //                connection.Execute("delete from [Customers]");
    //            }


    //        }
    //    }

    //#if NET452
    //    [Trait("Provider", "SqlCE")]
    //    [Provider(Provider.SqlCE)]
    //    public class SqlCETestSuite : TestSuite
    //    {
    //        const string FileName = "Test.DB.sdf";
    //        public static string ConnectionString => $"Data Source={FileName};";
    //        public override IDbConnection GetConnection() => new SqlCeConnection(ConnectionString);


    //        static SqlCETestSuite()
    //        {
    //            if (!File.Exists(FileName))
    //            {
    //                var engine = new SqlCeEngine(ConnectionString);
    //                engine.CreateDatabase();
    //                using (var connection = new SqlCeConnection(ConnectionString))
    //                {
    //                    connection.Open();
    //                    connection.Execute(@"CREATE TABLE Customers(
    //	                Id int IDENTITY(1,1) not null,
    //	                IId int null,
    //	                GId nvarchar(100) null,
    //	                SId nvarchar(100) null,
    //	                FirstName nvarchar(100) null,
    //	                LastName nvarchar(100) null,
    //	                FullName nvarchar(100) null,
    //	                Age int null,
    //	                UpdatedOn datetime null,
    //	                CreatedOn datetime null
    //                );");

    //                    var line = string.Empty;
    //                    var commandText = string.Empty;
    //                    var file = new StreamReader("sqlceawlite.sql");

    //                    while ((line = file.ReadLine()) != null)
    //                    {
    //                        if (line.Equals("GO", StringComparison.OrdinalIgnoreCase))
    //                        {
    //                            connection.Execute(commandText);
    //                            commandText = string.Empty;
    //                        }
    //                        else
    //                        {
    //                            commandText += "\r\n" + line;
    //                        }
    //                    }
    //                }
    //                Console.WriteLine("Created database");
    //            }
    //            else
    //            {
    //                using (var connection = new SqlCeConnection(ConnectionString))
    //                {
    //                    connection.Execute("delete from [Customers]");
    //                }
    //            }
    //        }
    //    }
    //#endif
}

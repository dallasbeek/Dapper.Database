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
using Dapper.Database;
using Npgsql;

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
    public partial class SqlServerTestSuite : TestSuite
    {
        private const string DbName = "tempdb";
        public static string ConnectionString =>
            IsAppVeyor
                ? @"Server=(local)\SQL2017;Database=tempdb;User ID=sa;Password=Password12!"
                : $"Data Source=(local)\\Dallas;Initial Catalog={DbName};Integrated Security=True";

        public override ISqlDatabase GetSqlDatabase()
        {
            return new SqlDatabase(new StringConnectionService<SqlConnection>(ConnectionString));
        }


        public Provider GetProvider() => Provider.SqlServer;

        private static readonly bool _skip;

        static SqlServerTestSuite()
        {
            Provider = Provider.SqlServer;
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    var awfile = File.ReadAllText("sqlserverawlite.sql");
                    connection.Execute(awfile);
                    connection.Execute("delete from [Person]");

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

    //[Trait("Provider", "MySql")]
    //[Provider(Provider.SQLite)]
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
    //                connection.Open();
    //                var awfile = File.ReadAllText("mysqlawlite.sql");
    //                connection.Execute(awfile);
    //                connection.Execute("delete from [Person]");
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

    [Trait("Provider", "SQLite")]
    public class SQLiteTestSuite : TestSuite
    {
        private const string FileName = "Test.DB.sqlite";
        public static string ConnectionString => $"Filename=./{FileName};Mode=ReadWriteCreate;";

        public override ISqlDatabase GetSqlDatabase()
        {
            return new SqlDatabase(new StringConnectionService<SqliteConnection>(ConnectionString));
        }

        public Provider GetProvider() => Provider.SqlCE;

        static SQLiteTestSuite()
        {
            Provider = Provider.SQLite;
            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());
            SqlMapper.AddTypeHandler<decimal>(new NumericTypeHandler());

            //if (File.Exists(FileName))
            //{
            //    File.Delete(FileName);
            //}

            using (var connection = new SqliteConnection(ConnectionString))
            {
                if (!File.Exists(FileName))
                {
                    connection.Open();

                    var awfile = File.ReadAllText("sqliteawlite.sql");
                    connection.Execute(awfile);

                }
                connection.Execute("delete from [Person]");
            }


        }
    }

#if NET452
    [Trait("Provider", "SqlCE")]
    public class SqlCETestSuite : TestSuite
    {
        const string FileName = "Test.DB.sdf";
        public static string ConnectionString => $"Data Source={FileName};";

        public override ISqlDatabase GetSqlDatabase()
        {
            return new SqlDatabase(new StringConnectionService<SqlCeConnection>(ConnectionString));
        }

        static SqlCETestSuite()
        {
            Provider = Provider.SqlCE;
            if (!File.Exists(FileName))
            {
                var engine = new SqlCeEngine(ConnectionString);
                engine.CreateDatabase();
                using (var connection = new SqlCeConnection(ConnectionString))
                {
                    connection.Open();
                    var line = string.Empty;
                    var commandText = string.Empty;
                    var file = new StreamReader("sqlceawlite.sql");

                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Equals("GO", StringComparison.OrdinalIgnoreCase))
                        {
                            connection.Execute(commandText);
                            commandText = string.Empty;
                        }
                        else
                        {
                            commandText += "\r\n" + line;
                        }
                    }
                }
                Console.WriteLine("Created database");
            }
            else
            {
                using (var connection = new SqlCeConnection(ConnectionString))
                {
                    connection.Execute("delete from [Person]");
                }
            }
        }
    }
#endif

    [Trait("Provider", "Postgres")]
    public partial class PostgresTestSuite : TestSuite
    {
        private const string DbName = "test";
        public static string ConnectionString =>
            IsAppVeyor
                ? @"Server=localhost;Port=5432;User Id=postgres;Password=Password12!;Database={DbName}"
                : $"Server=localhost;Port=5432;User Id=postgres;Password=Password12!;Database={DbName}";

        public override ISqlDatabase GetSqlDatabase()
        {
            return new SqlDatabase(new StringConnectionService<NpgsqlConnection>(ConnectionString));
        }


        public Provider GetProvider() => Provider.Postgres;

        private static readonly bool _skip;

        static PostgresTestSuite()
        {
            Provider = Provider.Postgres;
            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    var awfile = File.ReadAllText("postgresawlite.sql");
                    connection.Execute(awfile);
                    connection.Execute("delete from Person;");

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
}

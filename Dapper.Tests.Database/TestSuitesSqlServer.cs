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
    

}

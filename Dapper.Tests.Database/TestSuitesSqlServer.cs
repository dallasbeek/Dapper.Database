using System.Data.SqlClient;
using System.IO;
using Xunit;
using Dapper.Database;
using System;

namespace Dapper.Tests.Database
{

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
            if (_skip) throw new SkipTestException("Skipping Sql Server Tests - no server.");
            return new SqlDatabase(new StringConnectionService<SqlConnection>(ConnectionString));
        }


        public override Provider GetProvider() => Provider.SqlServer;

        private static readonly bool _skip;

        static SqlServerTestSuite()
        {
            Environment.SetEnvironmentVariable("NoCache", "True");

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    var awfile = File.ReadAllText(".\\Scripts\\sqlserverawlite.sql");
                    connection.Execute(awfile);
                    connection.Execute("delete from [Person]");

                }
            }
            catch (SqlException e)
            {
                if (e.Message.Contains("The server was not found ") || e.Message.Contains("Cannot open database"))
                    _skip = true;
                else
                    throw;
            }
        }
    }
    

}

using System;
using System.Data.SqlClient;
using System.IO;
using Dapper.Database;
using Xunit;


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

        protected override void CheckSkip()
        {
            if (_skip) throw new SkipTestException("Skipping Sql Server Tests - no server.");
        }

        public override ISqlDatabase GetSqlDatabase()
        {
            CheckSkip();
            return new SqlDatabase(new StringConnectionService<SqlConnection>(ConnectionString));
        }


        public override Provider GetProvider() => Provider.SqlServer;

        private static readonly bool _skip;

        static SqlServerTestSuite()
        {
            Environment.SetEnvironmentVariable("NoCache", "True");
            ResetDapperTypes();

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

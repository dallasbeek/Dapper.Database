using System.Data.SqlClient;
using System.IO;
using Dapper.Database;
using Dapper.Database.Adapters;
using Dapper.Database.Extensions;
using Xunit;

namespace Dapper.Tests.Database
{
    [Trait("Provider", "SqlServer")]
    public partial class SqlServerTestSuite : TestSuite
    {
        private const string DbName = "tempdb";
        public static string ConnectionString =>
            IsAppVeyor
                ? $"Server=(local)\\SQL2017;Database={DbName};User ID=sa;Password=Password12!"
                : $"Data Source=(localdb)\\mssqllocaldb;Initial Catalog={DbName};Integrated Security=True";

        protected override void CheckSkip()
        {
            Skip.If(_skip, "Skipping Sql Server Tests - no server.");
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
            SqlDatabase.CacheQueries = false;
            ResetDapperTypes();

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // For paginated queries prior to 2012 sql server uses row_number over
                    var sqlVersion = connection.ServerVersion;
                    if (!string.IsNullOrEmpty(sqlVersion) && sqlVersion.Length > 2)
                    {
                        var mv = int.Parse(sqlVersion.Substring(0, 2));
                        if (mv < 11)
                        {
                            SqlMapperExtensions.AddSqlAdapter<SqlConnection>(new SqlServerPre2012Adapter());
                        }
                    }

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

#if SQLCE
using System;
using System.Data.SqlServerCe;
using System.IO;
using Dapper.Database;
using Xunit;


namespace Dapper.Tests.Database
{

    [Trait("Provider", "SqlCE")]
    public class SqlCETestSuite : TestSuite
    {
        const string FileName = "Test.DB.sdf";
        public static string ConnectionString => $"Data Source={FileName};";

        protected override void CheckSkip()
        {
            if (_skip) throw new SkipTestException("Skipping SqlCE Tests - no server.");
        }

        public override ISqlDatabase GetSqlDatabase()
        {
            CheckSkip();
            return new SqlDatabase(new StringConnectionService<SqlCeConnection>(ConnectionString));
        }

        public override Provider GetProvider() => Provider.SqlCE;

        private static readonly bool _skip;

        static SqlCETestSuite()
        {
            Environment.SetEnvironmentVariable("NoCache", "True");

            ResetDapperTypes();
            if (!File.Exists(FileName))
            {
                try
                {

                    var engine = new SqlCeEngine(ConnectionString);
                    engine.CreateDatabase();
                    using (var connection = new SqlCeConnection(ConnectionString))
                    {
                        connection.Open();
                        var line = string.Empty;
                        var commandText = string.Empty;
                        var file = new StreamReader(".\\Scripts\\sqlceawlite.sql");

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
                }
                catch (Exception)
                {

                    throw;
                }
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

}
#endif

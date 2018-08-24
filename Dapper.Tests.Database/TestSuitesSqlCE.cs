#if NET452
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

using System.Data.SqlServerCe;
using System.Transactions;

namespace Dapper.Tests.Database
{

    [Trait("Provider", "SqlCE")]
    public class SqlCETestSuite : TestSuite
    {
        const string FileName = "Test.DB.sdf";
        public static string ConnectionString => $"Data Source={FileName};";

        public override ISqlDatabase GetSqlDatabase()
        {
            if (_skip) throw new SkipTestException("Skipping MySql Tests - no server.");
            return new SqlDatabase(new StringConnectionService<SqlCeConnection>(ConnectionString));
        }
        public override Provider GetProvider() => Provider.SqlCE;

        private static readonly bool _skip;

        static SqlCETestSuite()
        {
            Environment.SetEnvironmentVariable("NoCache", "True");

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

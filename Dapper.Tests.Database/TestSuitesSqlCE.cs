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
            Environment.SetEnvironmentVariable("NoCache", "True");
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

}

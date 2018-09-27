﻿#if SQLCE
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
        private const string FileName = "DBFiles\\Test.DB.sdf";
        public static string ConnectionString => $"Data Source={FileName};";

        protected override void CheckSkip()
        {
            Skip.If(_skip, "Skipping SqlCE Tests - no server or file.");
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

            SqlDatabase.CacheQueries = false;

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
            try
            {
                using (var connection = new SqlCeConnection(ConnectionString))
                {
                    connection.Execute("delete from [Person]");
                }
            }
            catch (SqlCeException)
            {
                _skip = true;
            }
        }
    }
}
#endif

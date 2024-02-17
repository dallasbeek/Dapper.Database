#if NET48

using System;
using System.Data.SqlServerCe;
using System.IO;
using Xunit;

namespace Dapper.Database.Tests
{
    [Trait("Provider", "SqlCE")]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Global
    public class SqlCETestSuite : TestSuite
    {
        private const string FileName = "DBFiles\\Test.DB.sdf";

        private static readonly bool Skip;

        static SqlCETestSuite()
        {
            SqlDatabase.CacheQueries = false;

            ResetDapperTypes();
            if (!File.Exists(FileName))
            {
                var engine = new SqlCeEngine(ConnectionString);
                engine.CreateDatabase();
                using var connection = new SqlCeConnection(ConnectionString);
                connection.Open();
                var commandText = string.Empty;
                var file = new StreamReader(@".\Scripts\sqlceawlite.sql");

                while (file.ReadLine() is { } line)
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

            try
            {
                using var connection = new SqlCeConnection(ConnectionString);
                connection.Execute("delete from [Person]");
            }
            catch (SqlCeException)
            {
                Skip = true;
            }
        }

        public static string ConnectionString => $"Data Source={FileName};";

        protected virtual void CheckSkip() => Xunit.Skip.If(Skip, "Skipping SqlCE Tests - no server or file.");

        public override ISqlDatabase GetSqlDatabase()
        {
            CheckSkip();
            return new SqlDatabase(new StringConnectionService<SqlCeConnection>(ConnectionString));
        }

        public override Provider GetProvider() => Provider.SqlCE;
    }
}
#endif

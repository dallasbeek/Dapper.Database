using System;
using System.IO;
using Dapper.Database;
using Microsoft.Data.Sqlite;
using Xunit;


namespace Dapper.Tests.Database
{

    [Trait("Provider", "SQLite")]
    public class SQLiteTestSuite : TestSuite
    {
        private const string FileName = "Test.DB.sqlite";
        public static string ConnectionString => $"Filename=./{FileName};Mode=ReadWriteCreate;";

        protected override void CheckSkip()
        {
            if (_skip) throw new SkipTestException("Skipping SQLite Tests - no server.");
        }

        public override ISqlDatabase GetSqlDatabase()
        {
            CheckSkip();
            return new SqlDatabase(new StringConnectionService<SqliteConnection>(ConnectionString));
        }

        public override Provider GetProvider() => Provider.SQLite;

        private static readonly bool _skip;

        static SQLiteTestSuite()
        {
            Environment.SetEnvironmentVariable("NoCache", "True");

            ResetDapperTypes();
            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());
            SqlMapper.AddTypeHandler<decimal>(new NumericTypeHandler());

            //if (File.Exists(FileName))
            //{
            //    File.Delete(FileName);
            //}
            try
            {
                using (var connection = new SqliteConnection(ConnectionString))
                {
                    if (!File.Exists(FileName))
                    {
                        connection.Open();

                        var awfile = File.ReadAllText(".\\Scripts\\sqliteawlite.sql");
                        connection.Execute(awfile);

                    }
                    connection.Execute("delete from [Person]");
                }

            }
            catch (SqliteException ex) when (ex.Message.Contains("SQLite Error 1:"))
            {
                _skip = true;
            }

        }
    }

}

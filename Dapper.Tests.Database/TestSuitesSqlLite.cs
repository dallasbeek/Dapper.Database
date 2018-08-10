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
 
    [Trait("Provider", "SQLite")]
    public class SQLiteTestSuite : TestSuite
    {
        private const string FileName = "Test.DB.sqlite";
        public static string ConnectionString => $"Filename=./{FileName};Mode=ReadWriteCreate;";

        public override ISqlDatabase GetSqlDatabase()
        {
            return new SqlDatabase(new StringConnectionService<SqliteConnection>(ConnectionString));
        }

        public Provider GetProvider() => Provider.SqlCE;

        static SQLiteTestSuite()
        {
            Environment.SetEnvironmentVariable("NoCache", "True");
            Provider = Provider.SQLite;

            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());
            SqlMapper.AddTypeHandler<decimal>(new NumericTypeHandler());

            //if (File.Exists(FileName))
            //{
            //    File.Delete(FileName);
            //}

            using (var connection = new SqliteConnection(ConnectionString))
            {
                if (!File.Exists(FileName))
                {
                    connection.Open();

                    var awfile = File.ReadAllText("sqliteawlite.sql");
                    connection.Execute(awfile);

                }
                connection.Execute("delete from [Person]");
            }


        }
    }
    
}

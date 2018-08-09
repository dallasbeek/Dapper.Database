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
    [Trait("Provider", "Postgres")]
    public partial class PostgresTestSuite : TestSuite
    {
        private const string DbName = "test";
        public static string ConnectionString =>
            IsAppVeyor
                ? $"Server=localhost;Port=5432;User Id=postgres;Password=Password12!;Database={DbName}"
                : $"Server=localhost;Port=5432;User Id=postgres;Password=Password12!;Database={DbName}";

        public override ISqlDatabase GetSqlDatabase()
        {
            return new SqlDatabase(new StringConnectionService<NpgsqlConnection>(ConnectionString));
        }


        public Provider GetProvider() => Provider.Postgres;

        private static readonly bool _skip;

        static PostgresTestSuite()
        {
            Provider = Provider.Postgres;
            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    var awfile = File.ReadAllText("postgresawlite.sql");
                    connection.Execute(awfile);
                    connection.Execute("delete from Person;");

                }
            }
            catch (SqlException e)
            {
                if (e.Message.Contains("The server was not found "))
                    _skip = true;
                else
                    throw;
            }
        }
    }
}

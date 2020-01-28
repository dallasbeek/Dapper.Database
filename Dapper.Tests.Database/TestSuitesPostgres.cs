using System;
using System.IO;
using System.Net.Sockets;
using Dapper.Database;
using Npgsql;
using Xunit;

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

        protected override void CheckSkip()
        {
            Skip.If(_skip, "Skipping Postgres Tests - no server.");
        }

        public override ISqlDatabase GetSqlDatabase()
        {
            CheckSkip();
            return new SqlDatabase(new StringConnectionService<NpgsqlConnection>(ConnectionString));
        }


        public override Provider GetProvider() => Provider.Postgres;

        private static readonly bool _skip;

        static PostgresTestSuite()
        {

            SqlDatabase.CacheQueries = false;

            ResetDapperTypes();
            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());
            try
            {
                PopulateDatabase();
            }
            catch (PostgresException e) when (e.Message.Contains($"database \"{DbName}\" does not exist"))
            {
                // PostgreSQL doesn't have a good way to detect if the "Test" database already exists:
                //  - Your connection string has to include the database you want
                //  - Their version of CREATE DATABASE does not have IF NOT EXISTS support
                CreateTestDatabase();
                PopulateDatabase();
            }
            catch (SocketException e) when (e.Message.Contains("No connection could be made because the target machine actively refused it"))
            {
                _skip = true;
            }
        }

        /// <summary>
        /// Connects to the default database and creates the test database.
        /// </summary>
        private static void CreateTestDatabase()
        {
            var cs = new NpgsqlConnectionStringBuilder(ConnectionString);
            var database = cs.Database;
            cs.Database = null;
            using (var connection = new NpgsqlConnection(cs.ToString()))
            {
                connection.Open();
                connection.Execute($"create database \"{database}\"");
            }

        }

        private static void PopulateDatabase()
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                var awfile = File.ReadAllText(".\\Scripts\\postgresawlite.sql");
                connection.Execute(awfile);
                connection.Execute("delete from Person;");
            }
        }
    }
}

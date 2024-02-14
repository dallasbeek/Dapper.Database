using System.IO;
using System.Net.Sockets;
using Npgsql;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "Postgres")]
// ReSharper disable once UnusedMember.Global
public class PostgresTestSuite : TestSuite
{
    private const string DbName = "test";

    private static readonly bool Skip;

    static PostgresTestSuite()
    {
        SqlDatabase.CacheQueries = false;

        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
        try
        {
            PopulateDatabase();
        }
        catch (PostgresException e) when (e.Message.Contains($"database \"{DbName}\" does not exist"))
        {
            // ReSharper disable once CommentTypo
            // PostgreSQL doesn't have a good way to detect if the "Test" database already exists:
            //  - Your connection string has to include the database you want
            //  - Their version of CREATE DATABASE does not have IF NOT EXISTS support
            CreateTestDatabase();
            PopulateDatabase();
        }
        catch (SocketException e) when (e.Message.Contains(
                                            "No connection could be made because the target machine actively refused it"))
        {
            Skip = true;
        }
    }

    public static string ConnectionString =>
        $"Server=localhost;Port=5432;User Id=postgres;Password=Password12!;Database={DbName}";

    protected virtual void CheckSkip() => Xunit.Skip.If(Skip, "Skipping Postgres Tests - no server.");

    public override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<NpgsqlConnection>(ConnectionString));
    }


    public override Provider GetProvider() => Provider.Postgres;

    /// <summary>
    ///     Connects to the default database and creates the test database.
    /// </summary>
    private static void CreateTestDatabase()
    {
        var cs = new NpgsqlConnectionStringBuilder(ConnectionString);
        var database = cs.Database;
        cs.Database = null;
        using var connection = new NpgsqlConnection(cs.ToString());
        connection.Open();
        connection.Execute($"create database \"{database}\"");
    }

    private static void PopulateDatabase()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var scriptSql = File.ReadAllText(@".\Scripts\postgresawlite.sql");
        connection.Execute(scriptSql);
        connection.Execute("delete from Person;");
    }
}

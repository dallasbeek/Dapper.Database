using System;
using System.IO;
using System.Net.Sockets;
using Npgsql;
using Testcontainers.PostgreSql;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "Postgres")]
// ReSharper disable once UnusedMember.Global
public class PostgresTestSuite : TestSuite, IClassFixture<PostgresDatabaseFixture>
{
    private readonly PostgresDatabaseFixture _fixture;

    public PostgresTestSuite(PostgresDatabaseFixture fixture)
    {
        _fixture = fixture;

        SqlDatabase.CacheQueries = false;

        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }

    protected virtual void CheckSkip() => Skip.If(_fixture.Skip, "Skipping Postgres Tests - no server.");

    public override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<NpgsqlConnection>(_fixture.ConnectionString));
    }

    public override Provider GetProvider() => Provider.Postgres;
}

public class PostgresDatabaseFixture : IDisposable
{
    private const string DbName = "test";

#if !(AV_Build || GH_Build)
    private readonly PostgreSqlContainer _sqlContainer;
#endif

    public PostgresDatabaseFixture()
    {
        try
        {
#if !(AV_Build || GH_Build)
            _sqlContainer = new PostgreSqlBuilder("postgres:18-alpine")
                .Build();

            _sqlContainer.StartAsync().GetAwaiter().GetResult();
            ConnectionString = _sqlContainer.GetConnectionString();
#endif

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
        catch (PostgresException)
        {
            Skip = true;
        }
    }

    public bool Skip { get; }

    public string ConnectionString { get; } =
        Environment.GetEnvironmentVariable("PostgresConnectionString")
        ?? $"Server=localhost;Port=5432;User Id=postgres;Password=Password12!;Database={DbName}";

    public void Dispose()
    {
#if !(AV_Build || GH_Build)
        _sqlContainer.DisposeAsync().GetAwaiter().GetResult();
#endif
    }

    private void CreateTestDatabase()
    {
        var cs = new NpgsqlConnectionStringBuilder(ConnectionString);
        var database = cs.Database;
        cs.Database = null;
        using var connection = new NpgsqlConnection(cs.ToString());
        connection.Open();
        connection.Execute($"create database \"{database}\"");
    }

    private void PopulateDatabase()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "postgresawlite.sql");
        var scriptSql = File.ReadAllText(scriptPath);
       
        connection.Execute(scriptSql);
        connection.Execute("delete from Person;");
    }
}

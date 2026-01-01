#if !AV_Build
using System;
using System.IO;
using System.Net.Sockets;
using Npgsql;
using Testcontainers.CockroachDb;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "CockroachDb")]
// ReSharper disable once UnusedMember.Global
public class CockroachDbTestSuite : TestSuite, IClassFixture<CockroachDbDatabaseFixture>
{
    private readonly CockroachDbDatabaseFixture _fixture;

    public CockroachDbTestSuite(CockroachDbDatabaseFixture fixture)
    {
        _fixture = fixture;

        SqlDatabase.CacheQueries = false;

        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }

    protected virtual void CheckSkip() => Skip.If(_fixture.Skip, "Skipping CockroachDb Tests - no server.");

    public override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<NpgsqlConnection>(_fixture.ConnectionString));
    }

    public override Provider GetProvider() => Provider.CockroachDb;
}

public class CockroachDbDatabaseFixture : IDisposable
{
    private const string DbName = "test";

#if !GH_Build
    private readonly CockroachDbContainer _sqlContainer;
#endif

    public CockroachDbDatabaseFixture()
    {
        try
        {
#if !GH_Build
            _sqlContainer = new CockroachDbBuilder("cockroachdb/cockroach:latest")
                .Build();

            _sqlContainer.StartAsync().GetAwaiter().GetResult();
            ConnectionString = _sqlContainer.GetConnectionString();
#endif

            PopulateDatabase();
        }
        catch (PostgresException e) when (e.Message.Contains($"database \"{DbName}\" does not exist"))
        {
            CreateTestDatabase();
            PopulateDatabase();
        }
        catch (SocketException e) when (e.Message.Contains(
                                            "No connection could be made because the target machine actively refused it"))
        {
            Skip = true;
        }
    }

    public bool Skip { get; }

    public string ConnectionString { get; } =
        Environment.GetEnvironmentVariable("CockroachDbConnectionString")
        ?? $"Server=localhost;Port=5432;User Id=postgres;Password=Password12!;Database={DbName}";

    public void Dispose()
    {
#if !GH_Build
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

        var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "cockroachdbawlite.sql");
        var scriptSql = File.ReadAllText(scriptPath);
       
        connection.Execute(scriptSql);
        connection.Execute("delete from Person;");
    }
}
#endif

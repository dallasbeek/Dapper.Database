using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Npgsql;
using Testcontainers.PostgreSql;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "Postgres")]
// ReSharper disable once UnusedMember.Global
public sealed class PostgresTestSuite : TestSuite, IClassFixture<PostgresDatabaseFixture>
{
    private readonly PostgresDatabaseFixture _fixture;

    public PostgresTestSuite(PostgresDatabaseFixture fixture)
    {
        _fixture = fixture;

        SqlDatabase.CacheQueries = false;

        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }

    private void CheckSkip() => Skip.If(_fixture.Skip, "Skipping Postgres Tests - no server.");

    [SkippableFact]
    [Trait("Category", "Fixture")]
    public async Task PostgresFixtureCanConnectAndPopulateDatabaseAsync()
    {
        CheckSkip();

        await using var connection = new NpgsqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        var addressCount = await connection.ExecuteScalarAsync<long>("select count(*) from Address");
        Assert.True(addressCount > 0);
    }

    protected override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<NpgsqlConnection>(_fixture.ConnectionString));
    }

    protected override Provider GetProvider() => Provider.Postgres;
}

public class PostgresDatabaseFixture : IAsyncLifetime
{
#if !GH_Build
    private PostgreSqlContainer _sqlContainer;
#endif

    public bool Skip { get; private set; }

    public string ConnectionString { get; private set; } =
        Environment.GetEnvironmentVariable("PostgresConnectionString");

    public async ValueTask InitializeAsync()
    {
#if GH_Build
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PostgresConnectionString")))
        {
            Skip = true;
            return;
        }
#endif

        try
        {
#if !GH_Build
            _sqlContainer = new PostgreSqlBuilder("postgres:latest")
                .Build();

            await _sqlContainer.StartAsync();
            ConnectionString = _sqlContainer.GetConnectionString();
#endif

            await PopulateDatabaseAsync();
        }
        catch (PostgresException e) when (e.SqlState == PostgresErrorCodes.InvalidCatalogName)
        {
            // ReSharper disable once CommentTypo
            // PostgreSQL doesn't have a good way to detect if the "Test" database already exists:
            //  - Your connection string has to include the database you want
            //  - Their version of CREATE DATABASE does not have IF NOT EXISTS support
            try
            {
                await CreateDatabaseAsync();
                await PopulateDatabaseAsync();
            }
            catch (Exception retryException) when (IsConnectivityFailure(retryException))
            {
                Skip = true;
            }
        }
        catch (Exception e) when (IsConnectivityFailure(e))
        {
            Skip = true;
        }
    }

    public async ValueTask DisposeAsync()
    {
#if !GH_Build
        if (_sqlContainer != null) await _sqlContainer.DisposeAsync();
#endif
        await Task.CompletedTask;
    }

    // Connection failures: socket/timeout errors anywhere in the chain, SQLSTATE class 08, or server starting up (57P03).
    private static bool IsConnectivityFailure(Exception e)
    {
        for (var ex = e; ex != null; ex = ex.InnerException)
        {
            if (ex is SocketException or TimeoutException)
                return true;

            if (ex is PostgresException pe && (pe.SqlState.StartsWith("08", StringComparison.Ordinal) ||
                                               pe.SqlState == PostgresErrorCodes.CannotConnectNow))
                return true;
        }

        return false;
    }

    private async Task CreateDatabaseAsync()
    {
        var cs = new NpgsqlConnectionStringBuilder(ConnectionString);
        var database = cs.Database;
        cs.Database = null;
        await using var connection = new NpgsqlConnection(cs.ToString());
        await connection.OpenAsync();
        await connection.ExecuteAsync($"create database \"{database}\"");
    }

    private async Task PopulateDatabaseAsync()
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync();

        var scriptSql = await Task.Run(() =>
            File.ReadAllText(ScriptConstants.GetScriptFile(ScriptConstants.PostgresScript)));

        await connection.ExecuteAsync(scriptSql);
        await connection.ExecuteAsync("delete from Person;");
    }
}

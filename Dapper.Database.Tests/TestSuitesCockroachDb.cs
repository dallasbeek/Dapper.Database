using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Npgsql;
using Testcontainers.CockroachDb;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "CockroachDb")]
// ReSharper disable once UnusedMember.Global
public sealed class CockroachDbTestSuite : TestSuite, IClassFixture<CockroachDbDatabaseFixture>
{
    private readonly CockroachDbDatabaseFixture _fixture;

    public CockroachDbTestSuite(CockroachDbDatabaseFixture fixture)
    {
        _fixture = fixture;
        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }

    private void CheckSkip() => Skip.If(_fixture.Skip, "Skipping CockroachDb Tests - no server.");

    [SkippableFact]
    [Trait("Category", "Fixture")]
    public async Task CockroachDbFixtureCanConnectAndPopulateDatabaseAsync()
    {
        CheckSkip();

        await using var connection = new NpgsqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        var addressCount = await connection.ExecuteScalarAsync<long>("select count(*) from Address;");
        Assert.True(addressCount > 0);
    }

    protected override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<NpgsqlConnection>(_fixture.ConnectionString));
    }

    protected override Provider GetProvider() => Provider.CockroachDb;
}

public class CockroachDbDatabaseFixture : IAsyncLifetime
{
    private const string DbName = "test";

#if !GH_Build
    private CockroachDbContainer _sqlContainer;
#endif

    public bool Skip { get; private set; }

    public string ConnectionString { get; private set; } =
        Environment.GetEnvironmentVariable("CockroachDbConnectionString");

    public async ValueTask InitializeAsync()
    {
        try
        {
#if !GH_Build
            _sqlContainer = new CockroachDbBuilder("cockroachdb/cockroach:latest")
                .Build();

            await _sqlContainer.StartAsync();
            ConnectionString = _sqlContainer.GetConnectionString();
#endif

            await PopulateDatabaseAsync();
        }
        catch (PostgresException e) when (e.Message.Contains($"database \"{DbName}\" does not exist"))
        {
            await CreateDatabaseAsync();
            await PopulateDatabaseAsync();
        }
        catch (SocketException)
        {
            Skip = true;
        }
        catch (PostgresException)
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
            File.ReadAllText(ScriptConstants.GetScriptFile(ScriptConstants.CockroachScript)));

        await connection.ExecuteAsync(scriptSql);
        await connection.ExecuteAsync("delete from Person;");
    }
}

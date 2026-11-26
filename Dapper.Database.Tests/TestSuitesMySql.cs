using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Testcontainers.MySql;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "MySql")]
// ReSharper disable once UnusedMember.Global
public sealed class MySqlTestSuite : TestSuite, IClassFixture<MySqlDatabaseFixture>
{
    private readonly MySqlDatabaseFixture _fixture;

    public MySqlTestSuite(MySqlDatabaseFixture fixture)
    {
        _fixture = fixture;

        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }


    private void CheckSkip() => Skip.If(_fixture.Skip, "Skipping MySql Tests - no server.");

    [SkippableFact]
    [Trait("Category", "Fixture")]
    public async Task MySqlFixtureCanConnectAndPopulateDatabaseAsync()
    {
        CheckSkip();

        using var connection = new MySqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        var addressCount = await connection.ExecuteScalarAsync<long>("select count(*) from Address;");
        Assert.True(addressCount > 0);
    }

    protected override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<MySqlConnection>(_fixture.ConnectionString));
    }

    protected override Provider GetProvider() => Provider.MySql;
}

public class MySqlDatabaseFixture : IAsyncLifetime
{
    private const string DbName = "test";

#if !GH_Build
    private MySqlContainer _sqlContainer;
#endif

    public bool Skip { get; private set; }

    public string ConnectionString { get; private set; } = Environment.GetEnvironmentVariable("MySqlConnectionString");

    public async ValueTask InitializeAsync()
    {
        try
        {
#if !GH_Build
            _sqlContainer = new MySqlBuilder("mysql:latest")
                .WithDatabase(DbName)
                .Build();

            await _sqlContainer.StartAsync();
            ConnectionString = _sqlContainer.GetConnectionString();
#endif
            await PopulateDatabaseAsync();
        }
        catch (SocketException)
        {
            Skip = true;
        }
        catch (MySqlException)
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

    private async Task PopulateDatabaseAsync()
    {
        using var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();

        var scriptSql = await Task.Run(() =>
            File.ReadAllText(ScriptConstants.GetScriptFile(ScriptConstants.MySqlScript)));

        await connection.ExecuteAsync(scriptSql, commandTimeout: 1200);
        await connection.ExecuteAsync("delete from Person;");
    }
}

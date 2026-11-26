#if !GH_Build
using System;
using System.Data;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using Testcontainers.FirebirdSql;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "Firebird")]
// ReSharper disable once UnusedMember.Global
public sealed class FirebirdTestSuite : TestSuite, IClassFixture<FirebirdDatabaseFixture>
{
    private readonly FirebirdDatabaseFixture _fixture;

    public FirebirdTestSuite(FirebirdDatabaseFixture fixture)
    {
        _fixture = fixture;
        SqlDatabase.CacheQueries = false;

        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }

    private void CheckSkip() => Skip.If(_fixture.Skip, "Skipping Firebird Tests - no server.");

    [SkippableFact]
    [Trait("Category", "Fixture")]
    public async Task FirebirdFixtureCanConnectAndPopulateDatabaseAsync()
    {
        CheckSkip();

        using var connection = new FbConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        var addressCount = await connection.ExecuteScalarAsync<long>("select count(*) from Address");
        Assert.True(addressCount > 0);
    }

    protected override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<FbConnection>(_fixture.ConnectionString));
    }

    protected override Provider GetProvider() => Provider.Firebird;
}

public class FirebirdDatabaseFixture : IAsyncLifetime
{
    // isc_network_error: "Unable to complete network request to host".
    private const int NetworkErrorCode = 335544721;

    private FirebirdSqlContainer _sqlContainer;

    public bool Skip { get; private set; }

    public string ConnectionString { get; private set; }

    public async ValueTask InitializeAsync()
    {
        try
        {
            _sqlContainer = new FirebirdSqlBuilder("jacobalberty/firebird:v4.0")
                .Build();

            await _sqlContainer.StartAsync();
            ConnectionString = _sqlContainer.GetConnectionString();

            await PopulateDatabaseAsync();
        }
        catch (SocketException)
        {
            Skip = true;
        }
        catch (FbException e) when (IsConnectivityFailure(e))
        {
            Skip = true;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_sqlContainer != null) await _sqlContainer.DisposeAsync();
    }

    private static bool IsConnectivityFailure(FbException e) =>
        e.ErrorCode == NetworkErrorCode
        || e.InnerException is SocketException or IOException
        || e.Message.Contains("Unable to complete network request");

    private async Task PopulateDatabaseAsync()
    {
        using var connection = new FbConnection(ConnectionString);
        await connection.OpenAsync();

        var initialize = false;

        try
        {
            await connection.ExecuteAsync("delete from Person");
        }
        catch (FbException e) when (!IsConnectivityFailure(e))
        {
            initialize = true;
        }

        if (!initialize) return;

        var scriptSql = await Task.Run(() =>
            File.ReadAllText(ScriptConstants.GetScriptFile(ScriptConstants.FirebirdScript)));
        var commandText = new StringBuilder();

        using var reader = new StringReader(scriptSql);
        while (await reader.ReadLineAsync() is { } line)
            if (line.Equals("GO", StringComparison.OrdinalIgnoreCase))
            {
                if (commandText.Length <= 0) continue;
                await connection.ExecuteAsync(commandText.ToString(), commandType: CommandType.Text);
                commandText.Clear();
            }
            else
            {
                commandText.Append("\r\n").Append(line);
            }

        if (commandText.Length > 0)
            await connection.ExecuteAsync(commandText.ToString(), commandType: CommandType.Text);

        await connection.ExecuteAsync("delete from Person");
    }
}

#endif

using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Dapper.Database.Adapters;
using Dapper.Database.Extensions;
using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "SqlServer")]
// ReSharper disable once UnusedMember.Global
public sealed partial class MsSqlServerTestSuite : TestSuite, IClassFixture<MsSqlDatabaseFixture>
{
    private readonly MsSqlDatabaseFixture _fixture;

    public MsSqlServerTestSuite(MsSqlDatabaseFixture fixture)
    {
        _fixture = fixture;
        ResetDapperTypes();
    }

    private void CheckSkip() => Skip.If(_fixture.Skip, "Skipping Sql Server Tests - no server.");

    [SkippableFact]
    [Trait("Category", "Fixture")]
    public async Task MsSqlFixtureCanConnectAndPopulateDatabaseAsync()
    {
        CheckSkip();

        using var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        var addressCount = await connection.ExecuteScalarAsync<long>("select count(*) from Address");
        Assert.True(addressCount > 0);
    }

    protected override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<SqlConnection>(_fixture.ConnectionString));
    }


    protected override Provider GetProvider() => Provider.SqlServer;
}

public class MsSqlDatabaseFixture : IAsyncLifetime
{
    // Connection failures: timeouts, server not found, refused/reset connections, and unavailable database.
    private static readonly int[] ConnectivityErrorNumbers =
    {
        -2, 2, 40, 53, 258, 1225, 4060, 10053, 10054, 10060, 10061, 11001
    };

#if !GH_Build
    private MsSqlContainer _sqlContainer;
#endif

    public bool Skip { get; private set; }

    public string ConnectionString { get; private set; } =
        Environment.GetEnvironmentVariable("MsSqlServerConnectionString");

    public async ValueTask InitializeAsync()
    {
#if GH_Build
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MsSqlServerConnectionString")))
        {
            Skip = true;
            return;
        }
#endif

        try
        {
#if !GH_Build
            _sqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2025-latest")
                .Build();

            await _sqlContainer.StartAsync();
            ConnectionString = _sqlContainer.GetConnectionString();
#endif

            await PopulateDatabaseAsync();
        }
        catch (SqlException e) when (IsConnectivityFailure(e))
        {
            Skip = true;
        }
        catch (SocketException)
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

    private static bool IsConnectivityFailure(SqlException e) =>
        Array.IndexOf(ConnectivityErrorNumbers, e.Number) >= 0 || e.InnerException is SocketException;

    private static void RegisterAdapterForServerVersion(string serverVersion)
    {
        var dot = serverVersion?.IndexOf('.') ?? -1;
        if (dot > 0 && serverVersion != null && int.TryParse(serverVersion.Substring(0, dot), out var majorVersion) &&
            majorVersion < 11)
            SqlMapperExtensions.AddSqlAdapter<SqlConnection>(new SqlServerPre2012Adapter());
    }

    private async Task PopulateDatabaseAsync()
    {
        using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        RegisterAdapterForServerVersion(connection.ServerVersion);

        var scriptSql = await Task.Run(() =>
            File.ReadAllText(ScriptConstants.GetScriptFile(ScriptConstants.SqlServerScript)));

        await connection.ExecuteAsync(scriptSql, commandTimeout: 120);
        await connection.ExecuteAsync("delete from [Person]", commandTimeout: 120);
    }
}

using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Xunit;
#if !GH_Build
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
#endif

namespace Dapper.Database.Tests;

[Trait("Provider", "SQLite")]
// ReSharper disable once InconsistentNaming
// ReSharper disable once UnusedMember.Global
public sealed class SQLiteTestSuite : TestSuite, IClassFixture<SqlLiteDatabaseFixture>
{
    private readonly SqlLiteDatabaseFixture _fixture;

    public SQLiteTestSuite(SqlLiteDatabaseFixture fixture)
    {
        _fixture = fixture;

        SqlDatabase.CacheQueries = false;

        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
        SqlMapper.AddTypeHandler(new NumericTypeHandler());
    }

    private void CheckSkip() => Skip.If(_fixture.Skip, "Skipping SQLite Tests - no server.");

    [SkippableFact]
    [Trait("Category", "Fixture")]
    public async Task SqliteFixtureCanConnectAndPopulateDatabaseAsync()
    {
        CheckSkip();

        using var connection = new SqliteConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        var addressCount = await connection.ExecuteScalarAsync<long>("select count(*) from Address");
        Assert.True(addressCount > 0);
    }

    protected override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<SqliteConnection>(_fixture.ConnectionString),
            IsolationLevel.Serializable);
    }

    protected override Provider GetProvider() => Provider.SQLite;
}

public class SqlLiteDatabaseFixture : IAsyncLifetime
{
    // Connection failures: busy, locked, read-only, I/O error and can't-open.
    private static readonly int[] ConnectivityErrorCodes = { 5, 6, 8, 10, 14 };

#if !GH_Build
    // Absolute path to the DB file on the host. This is the same file that gets
    // bind-mounted into the container, so both the host test process (via
    // Microsoft.Data.Sqlite) and the container (via the sqlite3 CLI, if needed)
    // operate on the exact same bytes on disk.
    private static readonly string FileName =
        Path.Combine(Directory.GetCurrentDirectory(), "DBFiles", "Test.DB.sqlite");

    private IContainer _sqlContainer;
#else
    // No Docker/server needed in CI - SQLite is just a local file.
    private const string FileName = "DBFiles/Test.DB.sqlite";
#endif

    public async ValueTask InitializeAsync()
    {
        try
        {
#if !GH_Build
            Directory.CreateDirectory(Path.GetDirectoryName(FileName)!);

            _sqlContainer = new ContainerBuilder("alpine/sqlite:latest")
                .WithBindMount(FileName, "/app/Test.DB.sqlite", AccessMode.ReadWrite)
                // sqlite3 has no long-running server process, so give the
                // container something to keep running while tests execute.
                .WithEntrypoint("sleep", "infinity")
                .Build();

            await _sqlContainer.StartAsync();
#endif

            var populate = !File.Exists(FileName);

            using var connection = new SqliteConnection(ConnectionString);
            await connection.OpenAsync();

            if (populate)
            {
                var scriptSql = await Task.Run(() =>
                    File.ReadAllText(ScriptConstants.GetScriptFile(ScriptConstants.SqlLiteScript)));

                await connection.ExecuteAsync(scriptSql);
            }

            await connection.ExecuteAsync("delete from [Person]");
        }
        catch (SqliteException ex) when (Array.IndexOf(ConnectivityErrorCodes, ex.SqliteErrorCode) >= 0)
        {
            Skip = true;
        }
    }

    public bool Skip { get; private set; }

    public string ConnectionString
    {
        get => $"Filename={FileName};Mode=ReadWriteCreate;";
    }

    public async ValueTask DisposeAsync()
    {
#if !GH_Build
        if (_sqlContainer != null) await _sqlContainer.DisposeAsync();
#endif
        await Task.CompletedTask;
    }
}

#if NET48 && !GH_Build
using System;
using System.Data.SqlServerCe;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Dapper.Database.Tests;
using Xunit;

namespace Dapper.Database.Tests
{
    [Trait("Provider", "SqlCE")]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Global
    public sealed class SqlCETestSuite : TestSuite, IClassFixture<SqlCEDatabaseFixture>
    {
        private readonly SqlCEDatabaseFixture _fixture;

        public SqlCETestSuite(SqlCEDatabaseFixture fixture)
        {
            _fixture = fixture;
            SqlDatabase.CacheQueries = false;
            ResetDapperTypes();
        }

        private void CheckSkip() => Skip.If(_fixture.Skip, "Skipping SqlCE Tests - no server or file.");

        [SkippableFact]
        [Trait("Category", "Fixture")]
        public async Task SqlCeFixtureCanConnectAndPopulateDatabaseAsync()
        {
            CheckSkip();

            using var connection = new SqlCeConnection(_fixture.ConnectionString);
            await connection.OpenAsync();

            var addressCount = await connection.ExecuteScalarAsync<int>("select count(*) from Address");
            Assert.True(addressCount > 0);
        }

        protected override ISqlDatabase GetSqlDatabase()
        {
            CheckSkip();
            return new SqlDatabase(new StringConnectionService<SqlCeConnection>(_fixture.ConnectionString));
        }

        protected override Provider GetProvider() => Provider.SqlCE;
    }
}


// ReSharper disable once InconsistentNaming
public class SqlCEDatabaseFixture : IAsyncLifetime
{
    private const string FileName = "DBFiles\\Test.DB.sdf";

    public bool Skip { get; private set; }

    public string ConnectionString { get; } =
        $"Data Source={FileName};";

    public async ValueTask InitializeAsync()
    {
        var populate = false;

        try
        {
            if (!File.Exists(FileName))
            {
                await Task.Run(() =>
                {
                    using var engine = new SqlCeEngine(ConnectionString);
                    engine.CreateDatabase();
                });
                populate = true;
            }

            using var probe = new SqlCeConnection(ConnectionString);
            await probe.OpenAsync();
        }
        catch (Exception e) when (IsUnavailable(e))
        {
            Skip = true;
            return;
        }

        using var connection = new SqlCeConnection(ConnectionString);
        await connection.OpenAsync();

        if (populate)
        {
            var commandText = string.Empty;
            using var file = new StreamReader(ScriptConstants.GetScriptFile(ScriptConstants.SqlCEScript));

            while (await file.ReadLineAsync() is { } line)
                if (line.Equals("GO", StringComparison.OrdinalIgnoreCase))
                {
                    await connection.ExecuteAsync(commandText);
                    commandText = string.Empty;
                }
                else
                {
                    commandText += "\r\n" + line;
                }

            if (!string.IsNullOrWhiteSpace(commandText))
                await connection.ExecuteAsync(commandText);
        }

        await connection.ExecuteAsync("delete from [Person]");
    }

    public ValueTask DisposeAsync() => default;

    // The engine is unavailable when the native runtime is missing or the database file can't be created or opened.
    private static bool IsUnavailable(Exception e) =>
        e is SqlCeException or DllNotFoundException or BadImageFormatException or IOException;
}

#endif

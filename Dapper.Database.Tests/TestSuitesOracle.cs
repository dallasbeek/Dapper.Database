using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper.Database.Adapters;
using Dapper.Database.Extensions;
using Oracle.ManagedDataAccess.Client;
using Testcontainers.Oracle;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;
using OracleConnection = Dapper.Database.Tests.OracleClient.OracleConnection;

namespace Dapper.Database.Tests;

[Trait("Provider", "Oracle")]
public sealed class OracleTestSuite : TestSuite, IClassFixture<OracleDatabaseFixture>
{
    private readonly OracleDatabaseFixture _fixture;


    public OracleTestSuite(OracleDatabaseFixture fixture)
    {
        _fixture = fixture;

        SqlDatabase.CacheQueries = false;
        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }

    protected override string P
    {
        get => ":";
    }

    private void CheckSkip() => Skip.If(_fixture.Skip, "Skipping Oracle Tests - no server.");

    protected override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<OracleConnection>(_fixture.ConnectionString));
    }

    protected override Provider GetProvider() => Provider.Oracle;

    #region Oracle Specific Tests

    [Fact]
    [Trait("Category", "Fixture")]
    public async Task OracleFixtureCanConnectAndPopulateDatabaseAsync()
    {
        CheckSkip();

        using var connection = new OracleConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        var addressCount = await connection.ExecuteScalarAsync<long>("select count(*) from Address");
        Assert.True(addressCount > 0);
    }

    [Fact]
    [Trait("Category", "Insert")]
    public void InsertSequenceComputed()
    {
        using var db = GetSqlDatabase();
        var p = new PersonIdentitySequence { FirstName = "Person", LastName = "Identity" };
        Assert.True(db.Insert(p));

        Assert.True(p.IdentityId > 0);
        if (p.FullName != null) Assert.Equal("Person Identity", p.FullName);

        var gp = db.Get<PersonIdentitySequence>(p.IdentityId);

        Assert.Equal(p.IdentityId, gp.IdentityId);
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.LastName, gp.LastName);
        Assert.Equal("Person Identity", gp.FullName);
    }

    #endregion
}

public class OracleDatabaseFixture : IAsyncLifetime
{
    private static readonly Regex CommandSeparator = new("^/\r?\n", RegexOptions.Multiline);

#if !GH_Build
    private OracleContainer _sqlContainer;
#endif

    public bool Skip { get; private set; }

    public string ConnectionString { get; private set; } = Environment.GetEnvironmentVariable("OracleConnectionString");

    public async ValueTask InitializeAsync()
    {
#if GH_Build
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("OracleConnectionString")))
        {
            Skip = true;
            return;
        }
#endif

        try
        {
#if !GH_Build
            _sqlContainer = new OracleBuilder("gvenzl/oracle-xe:21.3.0-slim-faststart")
                .Build();

            await _sqlContainer.StartAsync();
            ConnectionString = _sqlContainer.GetConnectionString();
#endif

            await PopulateDatabaseAsync();
        }
        catch (OracleException e) when (IsConnectivityFailure(e))
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

    // ReSharper disable once GrammarMistakeInComment
    // All ORA- errors (12500-12599) are TNS errors indicating connectivity.
    private static bool IsConnectivityFailure(OracleException e) =>
        e.Message.StartsWith("ORA-125", StringComparison.OrdinalIgnoreCase)
        || e.Message.Contains("No connection could be made because the target machine actively refused it")
        || e.Message.Contains("Unable to resolve connect hostname")
        || e.Message.Contains("Connection request timed out");

    private async Task PopulateDatabaseAsync()
    {
        using var connection = new OracleConnection(ConnectionString);
        await connection.OpenAsync();

        if (connection.ServerVersion != null &&
            connection.ServerVersion.StartsWith("11.", StringComparison.OrdinalIgnoreCase))
            // We have to override the Oracle adapter with the 11g adapter because:
            //  - The managed Oracle drivers (which are 12.1 and later) have some bugs when run against 11.2, which the 11g adapter works around
            //  - Oracle's "free" edition (XE) never had a 12.x release (latest is still 11.2)
            SqlMapperExtensions.AddSqlAdapter<OracleConnection>(new Oracle11gAdapter());

        var scriptSql = await Task.Run(() =>
            File.ReadAllText(ScriptConstants.GetScriptFile(ScriptConstants.OracleScript)));

        // Because the Oracle driver does not support multiple statements in a single IDbCommand, we have to manually split the file.
        // The file is marked with lines with just forward slashes ("/"), which is the way SQL*Plus and other tools recognize the end of a command in such situations, so just use that.
        // (It also helps the ability to debug the script in SQL*Plus or another tool.)
        foreach (var command in CommandSeparator.Split(scriptSql))
        {
            // don't execute blank commands (e.g. last line)
            if (string.IsNullOrWhiteSpace(command))
                continue;
            // don't execute anything starting with a comment indicating use of SQL*Plus
            // ReSharper disable once StringLiteralTypo
            if (command.StartsWith("/*SQLPLUS*/", StringComparison.OrdinalIgnoreCase))
                continue;

            try
            {
                await connection.ExecuteAsync(command);
            }
            catch (OracleException e)
            {
                var sb = new StringBuilder();
                sb.AppendLine(e.Message);
                sb.AppendLine("For command:");
                sb.Append(command);

                // can't throw new OracleException or DbException...
                throw new InvalidOperationException(sb.ToString(), e);
            }
        }

        await connection.ExecuteAsync("delete from Person");
    }
}

using System;
using System.IO;
using Dapper.Database.Adapters;
using Dapper.Database.Extensions;
using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "SqlServer")]
// ReSharper disable once UnusedMember.Global
public partial class MsSqlServerTestSuite : TestSuite, IClassFixture<MsSqlDatabaseFixture>
{
    private readonly MsSqlDatabaseFixture _fixture;

    public MsSqlServerTestSuite(MsSqlDatabaseFixture fixture)
    {
        _fixture = fixture;

        SqlDatabase.CacheQueries = false;
        SqlDatabase.SqlServerSelectComputed = false;

        ResetDapperTypes();
    }

    protected virtual void CheckSkip() => Skip.If(_fixture.Skip, "Skipping Sql Server Tests - no server.");

    public override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<SqlConnection>(_fixture.ConnectionString));
    }


    public override Provider GetProvider() => Provider.SqlServer;
}

public class MsSqlDatabaseFixture : IDisposable
{
    private const string DbName = "tempdb";

#if !(AV_Build || GH_Build)
    private readonly MsSqlContainer _sqlContainer;
#endif

    public MsSqlDatabaseFixture()
    {
        try
        {
#if !(AV_Build || GH_Build)
            _sqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2025-latest")
                .Build();

            _sqlContainer.StartAsync().GetAwaiter().GetResult();
            ConnectionString = _sqlContainer.GetConnectionString();
#endif

            PopulateDatabase();
        }
        catch (SqlException e)
        {
            if (e.Message.Contains("The server was not found ") || e.Message.Contains("Cannot open database"))
                Skip = true;
            else
                throw;
        }
    }

    public bool Skip { get; }

    public string ConnectionString { get; } =
        Environment.GetEnvironmentVariable("MsSqlServerConnectionString")
        ?? $"Server=(local)\\SQL2019;Database={DbName};User ID=sa;Password=Password12!;TrustServerCertificate=True";

    public void Dispose()
    {
#if !(AV_Build || GH_Build)
        _sqlContainer.DisposeAsync().GetAwaiter().GetResult();
#endif
    }

    private void PopulateDatabase()
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        var sqlVersion = connection.ServerVersion;
        if (!string.IsNullOrEmpty(sqlVersion) && sqlVersion.Length > 2)
        {
            var mv = int.Parse(sqlVersion.Substring(0, 2));
            if (mv < 11) SqlMapperExtensions.AddSqlAdapter<SqlConnection>(new SqlServerPre2012Adapter());
        }

        var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "sqlserverawlite.sql");

        var scriptSql = File.ReadAllText(scriptPath);
        connection.Execute(scriptSql);
        connection.Execute("delete from [Person]");
    }
}

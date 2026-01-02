using System;
using System.IO;
using System.Net.Sockets;
using MySqlConnector;
using Testcontainers.MariaDb;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "MariaDb")]
// ReSharper disable once UnusedMember.Global
public class MariaDbTestSuite : TestSuite, IClassFixture<MariaDbDatabaseFixture>
{
    private readonly MariaDbDatabaseFixture _fixture;

    public MariaDbTestSuite(MariaDbDatabaseFixture fixture)
    {
        _fixture = fixture;

        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }


    protected virtual void CheckSkip() => Skip.If(_fixture.Skip, "Skipping MariaDb Tests - no server.");

    public override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<MySqlConnection>(_fixture.ConnectionString));
    }

    public override Provider GetProvider() => Provider.MariaDb;
}

public class MariaDbDatabaseFixture : IDisposable
{
    private const string DbName = "test";

#if !(AV_Build || GH_Build)
    private readonly MariaDbContainer _sqlContainer;
#endif

    public MariaDbDatabaseFixture()
    {
        try
        {
#if !(AV_Build || GH_Build)
            _sqlContainer = new MariaDbBuilder("mariadb:latest")
                .WithDatabase(DbName)
                .Build();

            _sqlContainer.StartAsync().GetAwaiter().GetResult();
            ConnectionString = _sqlContainer.GetConnectionString();
#endif
            PopulateDatabase();
        }
        catch (SocketException e) when (
            e.Message.Contains("No connection could be made because the target machine actively refused it"))
        {
            Skip = true;
        }
        //catch (MariaDbException e) when (e.Message == "Unable to connect to any of the specified MariaDb hosts.")
        //{
        //    Skip = true;
        //}
    }

    public bool Skip { get; }

    public string ConnectionString { get; } = Environment.GetEnvironmentVariable("MariaDbConnectionString") 
                                              ?? $"Server=localhost;Port=3306;User Id=root;Password=Password12!;Database={DbName};SSL Mode=None;";

    public void Dispose()
    {
#if !(AV_Build || GH_Build)
        _sqlContainer.DisposeAsync().GetAwaiter().GetResult();
#endif
    }

    private void PopulateDatabase()
    {
        using var connection =
            new MySqlConnection(ConnectionString);
        connection.Open();

        var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "mysqlawlite.sql");
        var scriptSql = File.ReadAllText(scriptPath);

        connection.Execute(scriptSql, commandTimeout: 1200);
        connection.Execute("delete from Person;");
    }

    //public static string ConnectionString =>
    //    IsAppVeyor
    //        ? $"Server=localhost;Port=3306;User Id=root;Password=Password12!;Database={DbName};SSL Mode=None;"
    //        : $"Server=localhost;Port=3306;User Id=root;Password=Password12!;Database={DbName};";
}

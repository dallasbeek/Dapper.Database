using System;
using System.IO;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using Testcontainers.MySql;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "MySql")]
// ReSharper disable once UnusedMember.Global
public class MySqlTestSuite : TestSuite, IClassFixture<MySqlDatabaseFixture>
{
    private readonly MySqlDatabaseFixture _fixture;

    public MySqlTestSuite(MySqlDatabaseFixture fixture)
    {
        _fixture = fixture;

        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }


    protected virtual void CheckSkip() => Skip.If(_fixture.Skip, "Skipping MySql Tests - no server.");

    public override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<MySqlConnection>(_fixture.ConnectionString));
    }

    public override Provider GetProvider() => Provider.MySql;
}

public class MySqlDatabaseFixture : IDisposable
{
    private const string DbName = "test";

#if !CI_Build
    private readonly MySqlContainer _sqlContainer;
#endif

    public MySqlDatabaseFixture()
    {
        try
        {
#if !CI_Build
            _sqlContainer = new MySqlBuilder()
                .WithDatabase(DbName)
                .WithImage("mysql:latest")
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
        catch (MySqlException e) when (e.Message == "Unable to connect to any of the specified MySQL hosts.")
        {
            Skip = true;
        }
    }

    public bool Skip { get; }

    public string ConnectionString { get; } = Environment.GetEnvironmentVariable("MySqlConnectionString") 
                                              ?? $"Server=localhost;Port=3306;User Id=root;Password=Password12!;Database={DbName};SSL Mode=None;";

    public void Dispose()
    {
#if !CI_Build
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

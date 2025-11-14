#if !CI_Build
using System;
using System.IO;
using FirebirdSql.Data.FirebirdClient;
using Testcontainers.FirebirdSql;
using Xunit;
using File = System.IO.File;

namespace Dapper.Database.Tests;

[Trait("Provider", "Firebird")]
// ReSharper disable once UnusedMember.Global
public class FirebirdTestSuite : TestSuite, IClassFixture<FirebirdDatabaseFixture>
{
    private readonly FirebirdDatabaseFixture _fixture;

    public FirebirdTestSuite(FirebirdDatabaseFixture fixture)
    {
        _fixture = fixture;
        SqlDatabase.CacheQueries = false;

        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }

    protected virtual void CheckSkip() => Skip.If(_fixture.Skip, "Skipping Firebird Tests - no server.");

    public override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<FbConnection>(_fixture.ConnectionString));
    }

    public override Provider GetProvider() => Provider.Firebird;
}

public class FirebirdDatabaseFixture : IDisposable
{
    private const string DbFile = "Test.DB.fdb";

    private readonly FirebirdSqlContainer _sqlContainer;

    public FirebirdDatabaseFixture()
    {
        // $"DataSource=localhost;User=SYSDBA;Password=Password12!;Database={_dbFile};";
        // DataSource=127.0.0.1;Port=51295;Database=test;User=test;Password=test

        //var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "DBFiles", DbFile);
        //var containerDbPath = $"/firebird/data/{DbFile}";

        _sqlContainer = new FirebirdSqlBuilder()
            //.WithBindMount(dbPath, containerDbPath, AccessMode.ReadWrite)
            //.WithUsername("SYSDBA")
            //.WithPassword("Password12!")
            //.WithDatabase(DbFile)
            .WithImage("jacobalberty/firebird:v4.0")
            .Build();

        _sqlContainer.StartAsync().GetAwaiter().GetResult();

        ConnectionString = _sqlContainer.GetConnectionString();

        var init = false;

        //if (File.Exists(dbPath))
        //{
        //    File.Delete(dbPath);
        //}

        var commandText = string.Empty;

        try
        {
            using var connection = new FbConnection(ConnectionString);
            connection.Open();

            try
            {
                connection.Execute("delete from Person");
            }
            catch (FbException)
            {
                init = true;
            }

            if (!init) return;

            var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "firebirdawlite.sql");

            using (var file = File.OpenText(scriptPath))
            {
                while (file.ReadLine() is { } line)
                    if (line.Equals("GO", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!string.IsNullOrEmpty(commandText))
                            connection.Execute(commandText);
                        commandText = string.Empty;
                    }
                    else
                    {
                        commandText += "\r\n" + line;
                    }
            }

            connection.Execute("delete from Person");
        }
        catch (FbException ex) when (ex.Message.Contains("Unable to complete network request"))
        {
            Skip = true;
        }
    }

    public bool Skip { get; }

    public string ConnectionString { get; }

    public void Dispose() => _sqlContainer.DisposeAsync().GetAwaiter().GetResult();
}

#endif

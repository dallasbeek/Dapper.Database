#if !GH_Build
using System;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "SQLite")]
// ReSharper disable once InconsistentNaming
// ReSharper disable once UnusedMember.Global
public class SQLiteTestSuite : TestSuite, IClassFixture<SqlLiteDatabaseFixture>
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

    protected virtual void CheckSkip() => Skip.If(_fixture.Skip, "Skipping SQLite Tests - no server.");

    public override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<SqliteConnection>(_fixture.ConnectionString),
            IsolationLevel.Serializable);
    }

    public override Provider GetProvider() => Provider.SQLite;
}

public class SqlLiteDatabaseFixture : IDisposable
{
    private const string FileName = "DBFiles\\Test.DB.sqlite";

    public SqlLiteDatabaseFixture()
    {
        // ... initialize data in the test database ...
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            if (!File.Exists(FileName))
            {
                connection.Open();

                var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "sqliteawlite.sql");

                connection.Execute(scriptPath);
            }

            connection.Execute("delete from [Person]");
        }
        catch (SqliteException ex) when (ex.Message.Contains("SQLite Error 1:"))
        {
            Skip = true;
        }
    }

    public bool Skip { get; }

    public string ConnectionString
    {
        get => $"Filename=./{FileName};Mode=ReadWriteCreate;";
    }

    public void Dispose()
    {
    }
}
#endif

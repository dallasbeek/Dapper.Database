using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "SQLite")]
// ReSharper disable once InconsistentNaming
// ReSharper disable once UnusedMember.Global
public class SQLiteTestSuite : TestSuite
{
    private const string FileName = "DBFiles\\Test.DB.sqlite";

    private static readonly bool Skip;

    static SQLiteTestSuite()
    {
        SqlDatabase.CacheQueries = false;

        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
        SqlMapper.AddTypeHandler(new NumericTypeHandler());

        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            if (!File.Exists(FileName))
            {
                connection.Open();

                var scriptSql = File.ReadAllText(@".\Scripts\sqliteawlite.sql");
                connection.Execute(scriptSql);
            }

            connection.Execute("delete from [Person]");
        }
        catch (SqliteException ex) when (ex.Message.Contains("SQLite Error 1:"))
        {
            Skip = true;
        }
    }

    public static string ConnectionString => $"Filename=./{FileName};Mode=ReadWriteCreate;";

    protected virtual void CheckSkip() => Xunit.Skip.If(Skip, "Skipping SQLite Tests - no server.");

    public override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<SqliteConnection>(ConnectionString),
            IsolationLevel.Serializable);
    }

    public override Provider GetProvider() => Provider.SQLite;
}

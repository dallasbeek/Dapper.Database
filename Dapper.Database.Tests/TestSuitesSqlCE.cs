#if NET48

using System;
using System.Data.SqlServerCe;
using System.IO;
using Dapper;
using Xunit;

namespace Dapper.Database.Tests
{
    [Trait("Provider", "SqlCE")]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Global
    public class SqlCETestSuite : TestSuite, IClassFixture<SqlCEDatabaseFixture>
    {
        private readonly SqlCEDatabaseFixture _fixture;

        public SqlCETestSuite(SqlCEDatabaseFixture fixture)
        {
            _fixture = fixture;
            SqlDatabase.CacheQueries = false;
            ResetDapperTypes();
        }

        protected virtual void CheckSkip() => Skip.If(_fixture.Skip, "Skipping SqlCE Tests - no server or file.");

        public override ISqlDatabase GetSqlDatabase()
        {
            CheckSkip();
            return new SqlDatabase(new StringConnectionService<SqlCeConnection>(_fixture.ConnectionString));
        }

        public override Provider GetProvider() => Provider.SqlCE;
    }
}


// ReSharper disable once InconsistentNaming
public class SqlCEDatabaseFixture : IDisposable
{
    private const string DbName = "test";
    private const string FileName = "DBFiles\\Test.DB.sdf";

    public SqlCEDatabaseFixture()
    {
        if (!File.Exists(FileName))
        {
            var engine = new SqlCeEngine(ConnectionString);
            engine.CreateDatabase();
            using var connection = new SqlCeConnection(ConnectionString);
            connection.Open();
            var commandText = string.Empty;
            var file = new StreamReader(@".\Scripts\sqlceawlite.sql");

            while (file.ReadLine() is { } line)
                if (line.Equals("GO", StringComparison.OrdinalIgnoreCase))
                {
                    connection.Execute(commandText);
                    commandText = string.Empty;
                }
                else
                {
                    commandText += "\r\n" + line;
                }
        }

        try
        {
            using var connection = new SqlCeConnection(ConnectionString);
            connection.Execute("delete from [Person]");
        }
        catch (SqlCeException)
        {
            Skip = true;
        }
    }

    public bool Skip { get; }

    public string ConnectionString { get; } =
        $"Data Source={FileName};";

    public void Dispose()
    {
    }
}

#endif

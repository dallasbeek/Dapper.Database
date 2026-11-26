using System;
using System.IO;

namespace Dapper.Database.Tests;

public static class ScriptConstants
{
    public const string CockroachScript = "cockroach.sql";
    public const string FirebirdScript = "firebird.sql";
    public const string MySqlScript = "mysql.sql";
    public const string MariaDbScript = "mysql.sql";
    public const string OracleScript = "oracle.sql";
    public const string PostgresScript = "postgres.sql";
    public const string SqlCEScript = "sqlce.sql";
    public const string SqlLiteScript = "sqlite.sql";
    public const string SqlServerScript = "sqlserver.sql";

    public static string GetScriptFile(string scriptFile)
    {
        var scriptPath = Path.Combine(AppContext.BaseDirectory, "Scripts", scriptFile);
        return !File.Exists(scriptPath)
            ? throw new FileNotFoundException($"Script file not found: {scriptPath}")
            : scriptPath;
    }
}

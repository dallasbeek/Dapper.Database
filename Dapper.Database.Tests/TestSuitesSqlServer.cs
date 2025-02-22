﻿using System.IO;
using Microsoft.Data.SqlClient;
using Dapper.Database.Adapters;
using Dapper.Database.Extensions;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "SqlServer")]
// ReSharper disable once UnusedMember.Global
public partial class SqlServerTestSuite : TestSuite
{
    private const string DbName = "tempdb";

    private static readonly bool Skip;

    static SqlServerTestSuite()
    {
        SqlDatabase.CacheQueries = false;
        SqlDatabase.SqlServerSelectComputed = false;

        ResetDapperTypes();

        try
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            // For paginated queries prior to 2012 sql server uses row_number over
            var sqlVersion = connection.ServerVersion;
            if (!string.IsNullOrEmpty(sqlVersion) && sqlVersion.Length > 2)
            {
                var mv = int.Parse(sqlVersion.Substring(0, 2));
                if (mv < 11) SqlMapperExtensions.AddSqlAdapter<SqlConnection>(new SqlServerPre2012Adapter());
            }

            var scriptSql = File.ReadAllText(@".\Scripts\sqlserverawlite.sql");
            connection.Execute(scriptSql);
            connection.Execute("delete from [Person]");
        }
        catch (SqlException e)
        {
            if (e.Message.Contains("The server was not found ") || e.Message.Contains("Cannot open database"))
                Skip = true;
            else
                throw;
        }
    }

    public static string ConnectionString =>
        // ReSharper disable once StringLiteralTypo
        IsAppVeyor
            ? $"Server=(local)\\SQL2019;Database={DbName};User ID=sa;Password=Password12!;TrustServerCertificate=True"
            : $"Data Source=(localdb)\\mssqllocaldb;Initial Catalog={DbName};Integrated Security=True;TrustServerCertificate=True";

    protected virtual void CheckSkip() => Xunit.Skip.If(Skip, "Skipping Sql Server Tests - no server.");

    public override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<SqlConnection>(ConnectionString));
    }


    public override Provider GetProvider() => Provider.SqlServer;
}

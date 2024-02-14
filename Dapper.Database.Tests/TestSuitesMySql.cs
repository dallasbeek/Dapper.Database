using System.IO;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using Xunit;

namespace Dapper.Database.Tests;

[Trait("Provider", "MySql")]
// ReSharper disable once UnusedMember.Global
public class MySqlTestSuite : TestSuite
{
    private const string DbName = "test";

    private static readonly bool Skip;

    static MySqlTestSuite()
    {
        ResetDapperTypes();
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
        try
        {
            using var connection =
                new MySqlConnection("Server=localhost;Port=3306;User Id=root;Password=Password12!;SSL Mode=None;");
            connection.Open();

            var scriptSql = File.ReadAllText(@".\Scripts\mysqlawlite.sql");
            connection.Execute(scriptSql, commandTimeout: 600);
            connection.Execute("delete from Person;");
        }
        catch (SocketException e) when (e.Message.Contains(
                                            "No connection could be made because the target machine actively refused it"))
        {
            Skip = true;
        }
        catch (MySqlException e) when (e.Message == "Unable to connect to any of the specified MySQL hosts.")
        {
            Skip = true;
        }
    }

    public static string ConnectionString =>
        IsAppVeyor
            ? $"Server=localhost;Port=3306;User Id=root;Password=Password12!;Database={DbName};SSL Mode=None;"
            : $"Server=localhost;Port=3306;User Id=root;Password=Password12!;Database={DbName};";

    protected virtual void CheckSkip() => Xunit.Skip.If(Skip, "Skipping MySql Tests - no server.");

    public override ISqlDatabase GetSqlDatabase()
    {
        CheckSkip();
        return new SqlDatabase(new StringConnectionService<MySqlConnection>(ConnectionString));
    }

    public override Provider GetProvider() => Provider.MySql;
}

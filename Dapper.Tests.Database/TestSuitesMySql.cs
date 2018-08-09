using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Xunit;
using Xunit.Sdk;
using Dapper.Database.Extensions;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Dapper;
using Dapper.Database;
using Npgsql;

#if NET452
using System.Data.SqlServerCe;
using System.Transactions;
#endif

namespace Dapper.Tests.Database
{

    //[Trait("Provider", "MySql")]
    //[Provider(Provider.SQLite)]
    //public class MySqlServerTestSuite : TestSuite
    //{
    //    private const string DbName = "DapperContribTests";

    //    public static string ConnectionString { get; } =
    //        IsAppVeyor
    //            ? "Server=localhost;Uid=root;Pwd=Password12!;SslMode=none"
    //            : "Server=localhost;Uid=test;Pwd=pass;";

    //    public override IDbConnection GetConnection()
    //    {
    //        if (_skip) throw new SkipTestException("Skipping MySQL Tests - no server.");
    //        return new MySqlConnection(ConnectionString);
    //    }

    //    private static readonly bool _skip;

    //    static MySqlServerTestSuite()
    //    {
    //        try
    //        {
    //            using (var connection = new MySqlConnection(ConnectionString))
    //            {
    //                connection.Open();
    //                var awfile = File.ReadAllText("mysqlawlite.sql");
    //                connection.Execute(awfile);
    //                connection.Execute("delete from [Person]");
    //            }
    //        }
    //        catch (MySqlException e)
    //        {
    //            if (e.Message.Contains("Unable to connect"))
    //                _skip = true;
    //            else
    //                throw;
    //        }
    //    }
    //}
    

}

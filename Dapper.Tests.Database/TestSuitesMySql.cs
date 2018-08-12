using System;
using System.IO;
using Xunit;
using Dapper.Database;
using Npgsql;
using System.Net.Sockets;
using MySql.Data.MySqlClient;

namespace Dapper.Tests.Database
{
    [Trait("Provider", "MySql")]
    public partial class MySqlTestSuite : TestSuite
    {
        private const string DbName = "test";
        public static string ConnectionString =>
            IsAppVeyor
                ? $"Server=localhost;Port=3306;User Id=root;Password=Password12!;Database={DbName};"
                : $"Server=localhost;Port=3306;User Id=root;Password=Password12!;Database={DbName};";

        public override ISqlDatabase GetSqlDatabase()
        {
            if ( _skip ) throw new SkipTestException("Skipping MySql Tests - no server.");
            return new SqlDatabase(new StringConnectionService<MySqlConnection>(ConnectionString));
        }


        public override Provider GetProvider() => Provider.MySql;

        private static readonly bool _skip;

        static MySqlTestSuite()
        {
            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());
            try
            {
                using ( var connection = new MySqlConnection($"Server=localhost;Port=3306;User Id=root;Password=Password12!;") )
                {
                    connection.Open();

                    var awfile = File.ReadAllText(".\\Scripts\\mysqlawlite.sql");
                    connection.Execute(awfile, commandTimeout: 600);
                    connection.Execute("delete from Person;");

                }
            }
            catch ( SocketException e )
            {
                if ( e.Message.Contains("No connection could be made because the target machine actively refused it") )
                    _skip = true;
                else
                    throw;
            }
        }
    }
}

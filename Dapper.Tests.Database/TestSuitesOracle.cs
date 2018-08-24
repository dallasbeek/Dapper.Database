#if NET452
using System;
using System.IO;
using Xunit;
using Dapper.Database;
using System.Net.Sockets;
using Oracle.ManagedDataAccess.Client;

namespace Dapper.Tests.Database
{
    [Trait("Provider", "Oracle")]
    public partial class OracleTestSuite : TestSuite
    {
        public static string ConnectionString => $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=Denver)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=testuser;Password=Password12!;";

        protected override string P => ":";

        //Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = MyHost)(PORT = MyPort))(CONNECT_DATA = (SERVICE_NAME = MyOracleSID)));
        //User Id = myUsername; Password=myPassword;

        //public static string ConnectionString => "Data Source=MyOracleDB;User Id=testuser;Password=Password12!;Integrated Security = no;";



        //SERVER=(DESCRIPTION =
        //    (ADDRESS = (PROTOCOL = TCP)(HOST = Denver)(PORT = 1521))
        //    (CONNECT_DATA =
        //      (SERVER = DEDICATED)
        //      (SERVICE_NAME = XE)
        //    )
        //  )


        public override ISqlDatabase GetSqlDatabase()
        {
            if ( _skip ) throw new SkipTestException("Skipping Oracle Tests - no server.");
            return new SqlDatabase(new StringConnectionService<OracleConnection>(ConnectionString));
        }


        public override Provider GetProvider() => Provider.Oracle;

        private static readonly bool _skip;

        static OracleTestSuite()
        {
            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());
            try
            {
                using ( var connection = new OracleConnection(ConnectionString) )
                {
                    connection.Open();

                    var awfile = File.ReadAllText(".\\Scripts\\oracleawlite.sql");
                    //connection.Execute(awfile, commandTimeout: 600);
                    connection.Execute("delete from Person");

                }
            }
            catch ( Exception e )
            {
                if ( e.Message.Contains("No connection could be made because the target machine actively refused it") )
                    _skip = true;
                else
                    throw;
            }
        }
    }
}
#endif

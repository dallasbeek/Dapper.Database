#if NET451
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

        public override ISqlDatabase GetSqlDatabase()
        {
            if ( _skip ) throw new SkipTestException("Skipping Oracle Tests - no server.");
            return new SqlDatabase(new StringConnectionService<OracleConnection>(ConnectionString));
        }


        public override Provider GetProvider() => Provider.Oracle;

        private static readonly bool _skip;

        static OracleTestSuite()
        {
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));
            SqlMapper.AddTypeHandler<Guid>(new OracleGuidTypeHandler());

            var commandText = string.Empty;
            try
            {
                using ( var connection = new OracleConnection(ConnectionString) )
                {
                    connection.Open();

                    var file = File.OpenText(".\\Scripts\\oracleawlite.sql");
                    var line = string.Empty;
                    var prevline = string.Empty;

                    while ( (line = file.ReadLine()) != null )
                    {
                        if ( line.Equals(string.Empty, StringComparison.OrdinalIgnoreCase) && prevline.EndsWith(";") )
                        {
                            //if ( !string.IsNullOrEmpty(commandText) )
                            //    connection.Execute(commandText.Remove(commandText.Length -1));
                            //commandText = string.Empty;
                        }
                        else
                        {
                            commandText += "\r\n" + line;
                            prevline = line;
                        }
                    }
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

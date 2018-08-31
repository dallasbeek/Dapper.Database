#if ORACLE
using System;
using System.Data.Common;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Dapper.Database;
using Oracle.ManagedDataAccess.Client;
using Xunit;


namespace Dapper.Tests.Database
{
    [Trait("Provider", "Oracle")]
    public partial class OracleTestSuite : TestSuite
    {
        public static string ConnectionString => $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=Denver)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=testuser;Password=Password12!;";

        protected override string P => ":";

        protected override void CheckSkip()
        {
            if (_skip) throw new SkipTestException("Skipping Oracle Tests - no server.");
        }

        public override ISqlDatabase GetSqlDatabase()
        {
            CheckSkip();
            return new SqlDatabase(new StringConnectionService<OracleConnection>(ConnectionString));
        }


        public override Provider GetProvider() => Provider.Oracle;

        private static readonly bool _skip;

        static OracleTestSuite()
        {
            ResetDapperTypes();
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
            catch (OracleException e)
            {
                // All ORA- errors (12500-12599) are TNS errors indicating connectivity.
                _skip = e.Message.StartsWith("ORA-125", StringComparison.OrdinalIgnoreCase)
                    || e.Message.Contains("No connection could be made because the target machine actively refused it")
                    || e.Message.Contains("Unable to resolve connect hostname")
                    ;
            }
            catch (SocketException e) when ( e.Message.Contains("No connection could be made because the target machine actively refused it") )
            {
                _skip = true;
            }
        }
    }
}
#endif

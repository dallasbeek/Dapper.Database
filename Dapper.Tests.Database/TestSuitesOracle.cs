#if ORACLE
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Dapper.Database;
using Oracle.ManagedDataAccess.Client;
using Xunit;

using OracleConnection = Dapper.Tests.Database.OracleClient.OracleConnection;

namespace Dapper.Tests.Database
{
    [Trait("Provider", "Oracle")]
    public partial class OracleTestSuite : TestSuite
    {
        public static string ConnectionString => $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=Denver)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=testuser;Password=Password12!;";
        //public static string ConnectionString => "User Id=testuser;Password=Password12!;Data Source=localhost:1521/ORCLPDB1.localdomain";

        protected override string P => ":";

        protected override void CheckSkip()
        {
            Skip.If(_skip, "Skipping Oracle Tests - no server.");
        }

        public override ISqlDatabase GetSqlDatabase()
        {
            CheckSkip();
            return new SqlDatabase(new StringConnectionService<OracleConnection>(ConnectionString));
        }


        public override Provider GetProvider() => Provider.Oracle;

        private static readonly bool _skip;

        private static readonly Regex CommandSeparator = new Regex("^/\r?\n", RegexOptions.Multiline);

        static OracleTestSuite()
        {
            Environment.SetEnvironmentVariable("NoCache", "True");
            ResetDapperTypes();
            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());
            try
            {
                using (var connection = new OracleConnection(ConnectionString))
                {
                    connection.Open();

                    var awfile = File.ReadAllText(".\\Scripts\\oracleawlite.sql");

                    // Because the Oracle driver does not support multiple statements in a single IDbCommand, we have to manually split the file.
                    // The file is marked with lines with just forward slashes ("/"), which is the way SQL*Plus and other tools recognize the end of a command in such situations, so just use that.
                    // (It also helps the ability to debug the script in SQL*Plus or another tool.)
                    foreach (var command in CommandSeparator.Split(awfile))
                    {
                        // don't execute blank commands (e.g. last line)
                        if (string.IsNullOrWhiteSpace(command))
                            continue;
                        // don't execute anything starting with a comment indicating use of SQL*Plus
                        if (command.StartsWith("/*SQLPLUS*/", StringComparison.OrdinalIgnoreCase))
                            continue;

                        try
                        {
                            connection.Execute(command);
                        }
                        catch (OracleException e)
                        {
                            var sb = new StringBuilder();
                            sb.AppendLine(e.Message);
                            sb.AppendLine("For command:");
                            sb.Append(command);

                            // can't throw new OracleException or DbException...
                            throw new InvalidOperationException(sb.ToString(), e);
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
            catch (SocketException e) when (e.Message.Contains("No connection could be made because the target machine actively refused it"))
            {
                _skip = true;
            }
        }
    }
}
#endif

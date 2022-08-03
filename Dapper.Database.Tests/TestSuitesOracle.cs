#if !CI_Build
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Dapper.Database.Adapters;
using Dapper.Database.Extensions;
using Oracle.ManagedDataAccess.Client;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;
using OracleConnection = Dapper.Database.Tests.OracleClient.OracleConnection;

namespace Dapper.Database.Tests
{
    [Trait("Provider", "Oracle")]
    public class OracleTestSuite : TestSuite
    {
        private static readonly bool _skip;

        private static readonly Regex CommandSeparator = new("^/\r?\n", RegexOptions.Multiline);

        static OracleTestSuite()
        {
            SqlDatabase.CacheQueries = false;
            ResetDapperTypes();
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            try
            {
                using (var connection = new OracleConnection(ConnectionString))
                {
                    connection.Open();

                    if (connection.ServerVersion != null &&
                        connection.ServerVersion.StartsWith("11.", StringComparison.OrdinalIgnoreCase))
                        // We have to override the Oracle adapter with the 11g adapter because:
                        //  - The managed Oracle drivers (which are 12.1 and later) have some bugs when run against 11.2, which the 11g adapter works around
                        //  - Oracle's "free" edition (XE) never had a 12.x release (latest is still 11.2)
                        SqlMapperExtensions.AddSqlAdapter<OracleConnection>(new Oracle11gAdapter());

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
                        || e.Message.Contains(
                            "No connection could be made because the target machine actively refused it")
                        || e.Message.Contains("Unable to resolve connect hostname")
                        || e.Message.Contains("Connection request timed out");
            }
            catch (SocketException e) when (e.Message.Contains(
                                                "No connection could be made because the target machine actively refused it"))
            {
                _skip = true;
            }
        }

        public static string ConnectionString =>
            "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.0.2.15)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=testuser;Password=Password12!;";

        protected override string P => ":";

        protected override void CheckSkip() => Skip.If(_skip, "Skipping Oracle Tests - no server.");

        public override ISqlDatabase GetSqlDatabase()
        {
            CheckSkip();
            return new SqlDatabase(new StringConnectionService<OracleConnection>(ConnectionString));
        }

        public override Provider GetProvider() => Provider.Oracle;

        #region Oracle Specific Tests

        [Fact]
        [Trait("Category", "Insert")]
        public void InsertSequenceComputed()
        {
            using var db = GetSqlDatabase();
            var p = new PersonIdentitySequence { FirstName = "Person", LastName = "Identity" };
            Assert.True(db.Insert(p));

            Assert.True(p.IdentityId > 0);
            if (p.FullName != null) Assert.Equal("Person Identity", p.FullName);

            var gp = db.Get<PersonIdentitySequence>(p.IdentityId);

            Assert.Equal(p.IdentityId, gp.IdentityId);
            Assert.Equal(p.FirstName, gp.FirstName);
            Assert.Equal(p.LastName, gp.LastName);
            Assert.Equal("Person Identity", gp.FullName);
        }

        #endregion
    }
}
#endif

#if ORACLE
using System;
using System.Data;
using System.Data.Common;
using RealOracleConnection = Oracle.ManagedDataAccess.Client.OracleConnection;

namespace Dapper.Tests.Database.OracleClient
{
    /// <summary>
    /// Wrapper for <see cref="RealOracleConnection"/> that creates safe <see cref="OracleCommand"/> objects.
    /// </summary>
    public class OracleConnection : DbConnection
    {
        /// <summary>
        /// The wrapped connection.
        /// </summary>
        internal RealOracleConnection RealConnection { get; }

        internal OracleConnection(RealOracleConnection connection) => RealConnection = connection ?? throw new ArgumentNullException(nameof(connection));

        public OracleConnection() : this(new RealOracleConnection())
        {
        }

        public OracleConnection(string connectionString) : this(new RealOracleConnection(connectionString))
        {
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                RealConnection?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel) => RealConnection.BeginTransaction(isolationLevel);


        public override void ChangeDatabase(string databaseName) => RealConnection.ChangeDatabase(databaseName);

        public override void Close() => RealConnection.Close();

        public new OracleCommand CreateCommand() => new OracleCommand(this);

        protected override DbCommand CreateDbCommand() => new OracleCommand(this);

        public override void Open() => RealConnection.Open();

        public override string ConnectionString
        {
            get => RealConnection.ConnectionString;
            set => RealConnection.ConnectionString = value;
        }

        public override int ConnectionTimeout => RealConnection.ConnectionTimeout;
        public override string Database => RealConnection.Database;
        public override ConnectionState State => RealConnection.State;
        public override string DataSource => RealConnection.DataSource;
        public override string ServerVersion => RealConnection.ServerVersion;
    }
}
#endif

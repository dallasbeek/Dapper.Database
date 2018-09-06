#if ORACLE
using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using OracleTransaction = Oracle.ManagedDataAccess.Client.OracleTransaction; // not wrapping this one
using RealOracleCommand = Oracle.ManagedDataAccess.Client.OracleCommand;
using RealOracleConnection = Oracle.ManagedDataAccess.Client.OracleConnection;

namespace Dapper.Tests.Database.OracleClient
{
    /// <summary>
    /// Wrapper of <see cref="RealOracleCommand"/> whose sole purpose is to massage standard Dapper SQL into Oracle SQL.
    /// </summary>
    /// <remarks>
    /// Of all the ADO.NET drivers, ODP.NET is the only one that doesn't parse MSSQL-style bind variables (e.g. <c>@foo</c>).
    /// This wraps ODP.NET so that tests can use ODP.NET without fear.
    /// </remarks>
    public class OracleCommand : DbCommand
    {
        internal RealOracleCommand RealCommand { get; }
        private OracleConnection _connection;

        /// <summary>
        /// Called from <see cref="OracleConnection.CreateCommand"/>.
        /// </summary>
        /// <param name="connection"></param>
        internal OracleCommand(OracleConnection connection)
        {
            _connection = connection;
            RealCommand = connection.RealConnection.CreateCommand();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                RealCommand?.Dispose();
            }

            base.Dispose(disposing);
        }

        public override void Cancel() => RealCommand.Cancel();

        protected override DbParameter CreateDbParameter() => new OracleParameter(RealCommand.CreateParameter());

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior) => RealCommand.ExecuteReader(behavior);

        public override int ExecuteNonQuery() => RealCommand.ExecuteNonQuery();

        public override object ExecuteScalar() => RealCommand.ExecuteScalar();

        public override void Prepare() => RealCommand.Prepare();

        private string _rawCommandText;

        public override string CommandText
        {
            get => _rawCommandText;
            set
            {
                if (_rawCommandText == value) return;

                _rawCommandText = value;
                RealCommand.CommandText = value?.Replace('@', ':'); // FIXME more granular
            }
        }

        public bool BindByName
        {
            get => RealCommand.BindByName;
            set => RealCommand.BindByName = value;
        }

        public int InitialLOBFetchSize
        {
            get => RealCommand.InitialLOBFetchSize;
            set => RealCommand.InitialLOBFetchSize = value;
        }

        public int InitialLONGFetchSize
        {
            get => RealCommand.InitialLONGFetchSize;
            set => RealCommand.InitialLONGFetchSize = value;
        }

        public override int CommandTimeout
        {
            get => RealCommand.CommandTimeout;
            set => RealCommand.CommandTimeout = value;
        }

        public override CommandType CommandType
        {
            get => RealCommand.CommandType;
            set => RealCommand.CommandType = value;
        }

        public new OracleConnection Connection
        {
            get => _connection;
            set
            {
                if (_connection == value) return;

                _connection = value;
                RealCommand.Connection = _connection?.RealConnection;
            }
        }

        protected override DbConnection DbConnection
        {
            get => Connection;
            set
            {
                switch (value)
                {
                    case null:
                        Connection = null;
                        break;
                    case OracleConnection connection:
                        Connection = connection;
                        break;
                    case RealOracleConnection connection:
                        if (Connection?.RealConnection != connection)
                        {
                            Connection = new OracleConnection(connection);
                        }
                        break;
                    default:
                        throw new InvalidCastException($"Cannot cast connection of type {value.GetType()} to {typeof(RealOracleConnection)}.");
                }
            }
        }

        protected override DbParameterCollection DbParameterCollection => Parameters;

        private OracleParameterCollection _parameters;

        public new OracleParameterCollection Parameters
        {
            get
            {
                if (_parameters == null)
                    _parameters = new OracleParameterCollection(RealCommand.Parameters);

                return _parameters; 
            }
        }

        protected override DbTransaction DbTransaction
        {
            get => RealCommand.Transaction;
            set => RealCommand.Transaction = (OracleTransaction)value;
        }

        public new OracleTransaction Transaction
        {
            get => RealCommand.Transaction;
            set => RealCommand.Transaction = value;
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get => RealCommand.UpdatedRowSource;
            set => RealCommand.UpdatedRowSource = value;
        }

        public override bool DesignTimeVisible
        {
            get => RealCommand.DesignTimeVisible;
            set => RealCommand.DesignTimeVisible = value;
        }

        public override Task<object> ExecuteScalarAsync(CancellationToken cancellationToken) => RealCommand.ExecuteScalarAsync(cancellationToken);
        public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken) => RealCommand.ExecuteNonQueryAsync(cancellationToken);
        protected override Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken) => RealCommand.ExecuteReaderAsync(behavior, cancellationToken);
    }
}
#endif

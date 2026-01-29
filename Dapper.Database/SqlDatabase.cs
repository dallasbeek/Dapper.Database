using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper.Database
{
    /// <summary>
    ///     Provides database access and transaction management.
    /// </summary>
    public partial class SqlDatabase : ISqlDatabase
    {
        private readonly IConnectionService _connectionService;
        private readonly AsyncLocal<IDbConnection> _currentConnection = new AsyncLocal<IDbConnection>();
        private readonly IsolationLevel _isolationLevel;

        private IDbTransaction _transaction;
        private bool TransactionIsAborted { get; set; }
        private int TransactionCount { get; set; }

        /// <summary>
        ///     Globally turns off query caching
        /// </summary>
        public static bool CacheQueries = true;

        /// <summary>
        ///     When true computed columns are selected instead of returned in output clause
        /// </summary>
        public static bool SqlServerSelectComputed = false;

        /// <summary>
        ///     Sets the timeout value for all SQL statements.
        /// </summary>
        public int? CommandTimeout { get; set; }

        /// <summary>
        ///     Sets the timeout value for the next (and only next) SQL statement
        /// </summary>
        public int? OneTimeCommandTimeout { get; set; }

        /// <summary>
        ///     Gets the current shared connection.
        /// </summary>
        internal IDbConnection SharedConnection => _currentConnection.Value;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlDatabase"/> class.
        /// </summary>
        /// <param name="connectionService"></param>
        /// <param name="defaultIsolationLevel">Default Isolation level to use for this database</param>
        public SqlDatabase(IConnectionService connectionService,
            IsolationLevel defaultIsolationLevel = IsolationLevel.ReadCommitted)
        {
            _connectionService = connectionService;
            _isolationLevel = defaultIsolationLevel;
            TransactionCount = 0;
        }

        #region Connection Management

        private void OpenSharedConnectionInternal() 
        {
            if (SharedConnection != null && SharedConnection.State != ConnectionState.Broken &&
                SharedConnection.State != ConnectionState.Closed) return;

            _currentConnection.Value = _connectionService.GetConnection();

            if (SharedConnection == null)
                throw new Exception("SQL Connection failed to configure.");

            if (SharedConnection.State == ConnectionState.Broken) SharedConnection.Close();

            if (SharedConnection.State == ConnectionState.Closed) SharedConnection.Open();
        }

        private void CloseSharedConnection()
        {
            if (SharedConnection == null)
                return;

            SharedConnection.Close();
            _currentConnection.Value = null;
        }

        private void CloseSharedConnectionInternal()
        {
            if (_transaction == null) CloseSharedConnection();
        }

        #endregion

        #region Transaction Management

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isolationLevel"></param>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (_transaction == null)
            {
                TransactionCount = 0;
                OpenSharedConnectionInternal();
                _transaction = SharedConnection.BeginTransaction(isolationLevel);
            }

            if (_transaction != null) TransactionCount++;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AbortTransaction()
        {
            TransactionIsAborted = true;
            AbortTransactionInternal(false);
        }

        private void AbortTransactionInternal(bool fromComplete)
        {
            if (_transaction == null)
                return;

            if (!fromComplete)
            {
                TransactionCount--;
                if (TransactionCount >= 1)
                {
                    TransactionIsAborted = true;
                    return;
                }
            }

            if (TransactionIsOk() && !TransactionIsAborted)
                _transaction.Rollback();

            _transaction?.Dispose();

            _transaction = null;
            TransactionIsAborted = false;

            // You cannot continue to use a connection after a transaction has been rolled back
            if (SharedConnection != null)
            {
                SharedConnection.Close();
                SharedConnection.Open();
            }

            CloseSharedConnectionInternal();
        }

        /// <summary>
        /// 
        /// </summary>
        public void CompleteTransaction()
        {
            if (_transaction == null)
                return;

            TransactionCount--;
            if (TransactionCount >= 1)
                return;

            if (TransactionIsAborted)
            {
                AbortTransactionInternal(true);
                return;
            }

            if (TransactionIsOk())
                _transaction.Commit();

            _transaction?.Dispose();

            _transaction = null;

            CloseSharedConnectionInternal();
        }

        private bool TransactionIsOk() =>
            SharedConnection != null &&
            _transaction?.Connection != null &&
            _transaction.Connection.State == ConnectionState.Open;

        #endregion

        #region Command Execution

        private T ExecuteInternal<T>(Func<T> command, bool forceTransaction = false)
        {
            var openInternalTransaction = false;
            ITransaction internalTransaction = null;
            try
            {
                if (forceTransaction && _transaction == null)
                    openInternalTransaction = true;

                if (openInternalTransaction)
                    internalTransaction = GetTransaction(_isolationLevel);

                OpenSharedConnectionInternal();

                var data = command();

                if (OneTimeCommandTimeout != null)
                    OneTimeCommandTimeout = null;

                if (openInternalTransaction)
                    internalTransaction.Complete();

                return data;
            }
            catch (Exception)
            {
                if (openInternalTransaction)
                    internalTransaction?.Dispose();

                throw;
            }
            finally
            {
                CloseSharedConnectionInternal();
            }
        }

        private async Task<T> ExecuteInternalAsync<T>(Func<Task<T>> command, bool forceTransaction = false)
        {
            var openInternalTransaction = false;
            ITransaction internalTransaction = null;
            try
            {
                if (forceTransaction && _transaction == null)
                    openInternalTransaction = true;

                if (openInternalTransaction)
                    internalTransaction = GetTransaction(_isolationLevel);

                OpenSharedConnectionInternal();

                var data = await command();

                if (OneTimeCommandTimeout != null)
                    OneTimeCommandTimeout = null;

                if (openInternalTransaction)
                    internalTransaction.Complete();

                return data;
            }
            catch (Exception)
            {
                if (openInternalTransaction)
                    internalTransaction?.Dispose();

                throw;
            }
            finally
            {
                CloseSharedConnectionInternal();
            }
        }

        #endregion

        #region Transaction Factory

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ITransaction GetTransaction() => GetTransaction(_isolationLevel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public ITransaction GetTransaction(IsolationLevel isolationLevel) => new Transaction(this, isolationLevel);

        #endregion

        #region IDisposable
        /// <summary>
        /// 
        /// </summary>
        public void Dispose() => CloseSharedConnection();

        #endregion
    }

    /// <summary>
    ///     Represents a database transaction.
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        ///     Completes the transaction.
        /// </summary>
        void Complete();
    }

    /// <summary>
    ///     Transaction object helps maintain transaction depth counts.
    /// </summary>
    public class Transaction : ITransaction
    {
        private SqlDatabase _db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="isolationLevel"></param>
        public Transaction(SqlDatabase db, IsolationLevel isolationLevel)
        {
            _db = db;
            _db.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Complete()
        {
            _db.CompleteTransaction();
            _db = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose() => _db?.AbortTransaction();
    }
}

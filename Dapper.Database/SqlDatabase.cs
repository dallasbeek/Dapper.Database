using System;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.Database
{
    /// <summary>
    /// </summary>
    public partial class SqlDatabase : ISqlDatabase
    {
        /// <summary>
        ///     Globally turns off query caching
        /// </summary>
        public static bool CacheQueries = true;

        /// <summary>
        ///     When true computed columns are selected instead of returned in output clause
        /// </summary>
        public static bool SqlServerSelectComputed = false;

        private readonly IsolationLevel _isolationLevel;

        /// <summary>
        /// </summary>
        protected readonly IConnectionService ConnectionService;

        private IDbTransaction _transaction;

        /// <summary>
        /// </summary>
        protected IDbConnection SharedConnection;

        /// <summary>
        /// </summary>
        /// <param name="connectionService"></param>
        /// <param name="defaultIsolationLevel">Default Isolation level to use for this database</param>
        public SqlDatabase(IConnectionService connectionService,
            IsolationLevel defaultIsolationLevel = IsolationLevel.ReadCommitted)
        {
            ConnectionService = connectionService;
            _isolationLevel = defaultIsolationLevel;
            TransactionCount = 0;
        }


        /// <summary>
        /// </summary>
        internal bool TransactionIsAborted { get; set; }

        /// <summary>
        /// </summary>
        internal int TransactionCount { get; set; }

        /// <summary>
        ///     Sets the timeout value for all SQL statements.
        /// </summary>
        public int? CommandTimeout { get; set; }

        /// <summary>
        ///     Sets the timeout value for the next (and only next) SQL statement
        /// </summary>
        public int? OneTimeCommandTimeout { get; set; }

        /// <summary>
        /// </summary>
        public void Dispose() => CloseSharedConnection();

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ITransaction GetTransaction() => GetTransaction(_isolationLevel);

        /// <summary>
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public ITransaction GetTransaction(IsolationLevel isolationLevel) => new Transaction(this, isolationLevel);

        /// <summary>
        /// </summary>
        protected void OpenSharedConnectionInternal() => OpenSharedConnectionImp();

        /// <summary>
        /// </summary>
        private void OpenSharedConnectionImp()
        {
            if (SharedConnection != null && SharedConnection.State != ConnectionState.Broken &&
                SharedConnection.State != ConnectionState.Closed) return;

            SharedConnection = ConnectionService.GetConnection();

            if (SharedConnection == null)
                throw new Exception("SQL Connection failed to configure.");


            if (SharedConnection.State == ConnectionState.Broken) SharedConnection.Close();

            if (SharedConnection.State == ConnectionState.Closed) SharedConnection.Open();
        }

        /// <summary>
        /// </summary>
        public void CloseSharedConnection()
        {
            if (SharedConnection == null)
                return;

            SharedConnection.Close();
            //SharedConnection.Dispose();
            SharedConnection = null;
        }

        /// <summary>
        /// </summary>
        protected void CloseSharedConnectionInternal()
        {
            if (_transaction == null) CloseSharedConnection();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="forceTransaction">Open a transaction if one isn't already open</param>
        /// <returns></returns>
        protected T ExecuteInternal<T>(Func<T> command, bool forceTransaction = false)
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
                //internalTransaction?.Dispose();
                CloseSharedConnectionInternal();
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="forceTransaction">Open a transaction if one isn't already open</param>
        /// <returns></returns>
        protected async Task<T> ExecuteInternalAsync<T>(Func<Task<T>> command, bool forceTransaction = false)
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
                //internalTransaction?.Dispose();
                CloseSharedConnectionInternal();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="tran"></param>
        public void SetTransaction(IDbTransaction tran) => _transaction = tran;

        /// <summary>
        /// </summary>
        public void BeginTransaction() => BeginTransaction(_isolationLevel);

        /// <summary>
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
        /// </summary>
        public void AbortTransaction()
        {
            TransactionIsAborted = true;
            AbortTransaction(false);
        }

        /// <summary>
        /// </summary>
        /// <param name="fromComplete"></param>
        public void AbortTransaction(bool fromComplete)
        {
            if (_transaction == null)
                return;

            if (fromComplete == false)
            {
                TransactionCount--;
                if (TransactionCount >= 1)
                {
                    TransactionIsAborted = true;
                    return;
                }
            }

            if (TransactionIsOk())
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
                AbortTransaction(true);
                return;
            }

            if (TransactionIsOk())
                _transaction.Commit();

            _transaction?.Dispose();

            _transaction = null;

            CloseSharedConnectionInternal();
        }

        private bool TransactionIsOk() => SharedConnection != null && _transaction?.Connection != null &&
                                          _transaction.Connection.State == ConnectionState.Open;
    }

    /// <summary>
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// </summary>
        void Complete();
    }


    /// <summary>
    ///     Transaction object helps maintain transaction depth counts
    /// </summary>
    public class Transaction : ITransaction
    {
        private SqlDatabase _db;

        /// <summary>
        /// </summary>
        /// <param name="db"></param>
        /// <param name="isolationLevel"></param>
        public Transaction(SqlDatabase db, IsolationLevel isolationLevel)
        {
            _db = db;
            _db.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// </summary>
        public void Complete()
        {
            _db.CompleteTransaction();
            _db = null;
        }

        /// <summary>
        /// </summary>
        public void Dispose() => _db?.AbortTransaction();
    }
}

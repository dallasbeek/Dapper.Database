using System;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.Database
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SqlDatabase : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IConnectionService _connectionService;

        /// <summary>
        /// 
        /// </summary>
        protected IDbConnection _sharedConnection;

        private IDbTransaction _transaction;
        private readonly IsolationLevel _isolationLevel = IsolationLevel.ReadCommitted;

        /// <summary>
        /// Sets the timeout value for all SQL statements.
        /// </summary>
        public int? CommandTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the timeout value for the next (and only next) SQL statement
        /// </summary>
        public int? OneTimeCommandTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionService"></param>
        public SqlDatabase(IConnectionService connectionService)
        {
            _connectionService = connectionService;

            TransactionCount = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void OpenSharedConnectionInternal()
        {
            OpenSharedConnectionImp(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isInternal"></param>
        private void OpenSharedConnectionImp(bool isInternal)
        {
            if (_sharedConnection != null && _sharedConnection.State != ConnectionState.Broken && _sharedConnection.State != ConnectionState.Closed)
            {
                return;
            }

            _sharedConnection = _connectionService.GetConnection();

            if (_sharedConnection == null)
                throw new Exception("SQL Connection failed to configure.");


            if (_sharedConnection.State == ConnectionState.Broken)
            {
                _sharedConnection.Close();
            }

            if (_sharedConnection.State == ConnectionState.Closed)
            {
                _sharedConnection.Open();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void CloseSharedConnection()
        {
            if (_sharedConnection == null)
                return;

            _sharedConnection.Close();
            _sharedConnection.Dispose();
            _sharedConnection = null;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void CloseSharedConnectionInternal()
        {
            if (_transaction == null)
            {
                CloseSharedConnection();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        protected T ExecuteInternal<T>(Func<T> command)
        {
            try
            {
                OpenSharedConnectionInternal();

                var data = command();

                if (OneTimeCommandTimeout != null)
                {
                    OneTimeCommandTimeout = null;
                }
                return data;
            }
            finally
            {
                CloseSharedConnectionInternal();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        protected async Task<T> ExecuteInternalAsync<T>(Func<Task<T>> command)
        {
            try
            {
                OpenSharedConnectionInternal();

                var data = await command();

                if (OneTimeCommandTimeout != null)
                {
                    OneTimeCommandTimeout = null;
                }
                return data;
            }
            finally
            {
                CloseSharedConnectionInternal();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            CloseSharedConnection();
        }


        /// <summary>
        /// 
        /// </summary>
        internal bool TransactionIsAborted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal int TransactionCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ITransaction GetTransaction()
        {
            return GetTransaction(_isolationLevel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public ITransaction GetTransaction(IsolationLevel isolationLevel)
        {
            return new Transaction(this, isolationLevel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tran"></param>
        public void SetTransaction(IDbTransaction tran)
        {
            _transaction = tran;
        }

        /// <summary>
        /// 
        /// </summary>
        public void BeginTransaction()
        {
            BeginTransaction(_isolationLevel);
        }

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
                _transaction = _sharedConnection.BeginTransaction(isolationLevel);
            }

            if (_transaction != null)
            {
                TransactionCount++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void AbortTransaction()
        {
            TransactionIsAborted = true;
            AbortTransaction(false);
        }

        /// <summary>
        /// 
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

            if (_transaction != null)
                _transaction.Dispose();

            _transaction = null;
            TransactionIsAborted = false;

            // You cannot continue to use a connection after a transaction has been rolled back
            if (_sharedConnection != null)
            {
                _sharedConnection.Close();
                _sharedConnection.Open();
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
                AbortTransaction(true);
                return;
            }

            if (TransactionIsOk())
                _transaction.Commit();

            if (_transaction != null)
                _transaction.Dispose();

            _transaction = null;

            CloseSharedConnectionInternal();
        }

        private bool TransactionIsOk()
        {
            return _sharedConnection != null
                && _transaction != null
                && _transaction.Connection != null
                && _transaction.Connection.State == ConnectionState.Open;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        void Complete();
    }


    /// <summary>
    /// Transaction object helps maintain transaction depth counts
    /// </summary>
    public class Transaction : ITransaction
    {
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
        public void Dispose()
        {
            if (_db != null)
            {
                _db.AbortTransaction();
            }
        }

        SqlDatabase _db;
    }

}

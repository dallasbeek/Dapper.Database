using System;
using System.Data;

namespace Dapper.Database
{
    /// <summary>
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public partial interface ISqlDatabase : IDisposable
    {
        #region Transaction Methods

        /// <summary>
        ///     Get a transaction
        /// </summary>
        /// <returns></returns>
        ITransaction GetTransaction();

        /// <summary>
        ///     Get a transaction
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        ITransaction GetTransaction(IsolationLevel isolationLevel);

        #endregion

        #region Timeout Settings

        /// <summary>
        ///     Sets the Database timeout for all transactions
        /// </summary>
        int? CommandTimeout { get; set; }

        /// <summary>
        ///     Sets the timeout value for the next (and only next) SQL statement
        /// </summary>
        int? OneTimeCommandTimeout { get; set; }

        #endregion
    }
}

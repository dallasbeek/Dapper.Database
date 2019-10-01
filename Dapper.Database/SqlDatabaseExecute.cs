using System.Threading.Tasks;

namespace Dapper.Database
{
    public partial interface ISqlDatabase
    {
        #region Execute Methods

        /// <summary>
        ///     Execute SQL.
        /// </summary>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <returns>
        ///     The number of rows affected.
        /// </returns>
        int Execute(string fullSql);

        /// <summary>
        ///     Execute parameterized SQL.
        /// </summary>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns></returns>
        int Execute(string fullSql, object parameters);

        #endregion

        #region ExecuteAsync Methods

        /// <summary>
        ///     Execute SQL.
        /// </summary>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <returns>
        ///     The number of rows affected.
        /// </returns>
        Task<int> ExecuteAsync(string fullSql);

        /// <summary>
        ///     Execute parameterized SQL.
        /// </summary>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string fullSql, object parameters);

        #endregion
    }

    public partial class SqlDatabase
    {
        #region Execute Methods

        /// <summary>
        ///     Execute SQL.
        /// </summary>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <returns>
        ///     The number of rows affected.
        /// </returns>
        public int Execute(string fullSql) => ExecuteInternal(() =>
            SharedConnection.Execute(fullSql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute parameterized SQL.
        /// </summary>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns></returns>
        public int Execute(string fullSql, object parameters) => ExecuteInternal(() =>
            SharedConnection.Execute(fullSql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region ExecuteAsync Methods

        /// <summary>
        ///     Execute SQL.
        /// </summary>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <returns>
        ///     The number of rows affected.
        /// </returns>
        public async Task<int> ExecuteAsync(string fullSql) => await ExecuteInternalAsync(() =>
            SharedConnection.ExecuteAsync(fullSql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute parameterized SQL.
        /// </summary>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string fullSql, object parameters) => await ExecuteInternalAsync(() =>
            SharedConnection.ExecuteAsync(fullSql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        #endregion
    }
}

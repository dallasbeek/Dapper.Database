using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database
{
    public partial interface ISqlDatabase
    {
        #region Count Methods

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        int Count(string fullSql);

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        int Count(string fullSql, object parameters);

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        int Count<T>(string sql = null) where T : class;

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        int Count<T>(string sql, object parameters) where T : class;

        #endregion

        #region CountAsync Methods

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        Task<int> CountAsync(string fullSql);

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        Task<int> CountAsync(string fullSql, object parameters);

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        Task<int> CountAsync<T>(string sql = null) where T : class;

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        Task<int> CountAsync<T>(string sql, object parameters) where T : class;

        #endregion
    }

    public partial class SqlDatabase
    {
        #region Count Methods

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        public int Count(string fullSql) => ExecuteInternal(() =>
            SharedConnection.Count(fullSql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        public int Count(string fullSql, object parameters) => ExecuteInternal(() =>
            SharedConnection.Count(fullSql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        public int Count<T>(string sql = null) where T : class => ExecuteInternal(() =>
            SharedConnection.Count<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        public int Count<T>(string sql, object parameters) where T : class => ExecuteInternal(() =>
            SharedConnection.Count<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region CountAsync Methods

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        public async Task<int> CountAsync(string fullSql) => await ExecuteInternalAsync(() =>
            SharedConnection.CountAsync(fullSql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        public async Task<int> CountAsync(string fullSql, object parameters) => await ExecuteInternalAsync(() =>
            SharedConnection.CountAsync(fullSql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        public async Task<int> CountAsync<T>(string sql = null) where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.CountAsync<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the number of matching records.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     Total Count of matching records.
        /// </returns>
        public async Task<int> CountAsync<T>(string sql, object parameters) where T : class =>
            await ExecuteInternalAsync(() =>
                SharedConnection.CountAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        #endregion
    }
}

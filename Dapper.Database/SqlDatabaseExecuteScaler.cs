using System.Threading.Tasks;

namespace Dapper.Database
{
    public partial interface ISqlDatabase
    {
        #region ExecuteScalar Methods

        /// <summary>
        ///     Execute SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <returns>
        ///     The first cell selected as <see cref="object" />.
        /// </returns>
        T ExecuteScalar<T>(string fullSql);

        /// <summary>
        ///     Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     The first cell selected as <typeparamref name="T" />.
        /// </returns>
        T ExecuteScalar<T>(string fullSql, object parameters);

        #endregion

        #region ExecuteScalarAsync Methods

        /// <summary>
        ///     Execute SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <returns>
        ///     The first cell selected as <see cref="object" />.
        /// </returns>
        Task<T> ExecuteScalarAsync<T>(string fullSql);

        /// <summary>
        ///     Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     The first cell selected as <typeparamref name="T" />.
        /// </returns>
        Task<T> ExecuteScalarAsync<T>(string fullSql, object parameters);

        #endregion
    }

    public partial class SqlDatabase
    {
        #region ExecuteScalar Methods

        /// <summary>
        ///     Execute SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <returns>
        ///     The first cell selected as <see cref="object" />.
        /// </returns>
        public T ExecuteScalar<T>(string fullSql) => ExecuteInternal(() =>
            SharedConnection.ExecuteScalar<T>(fullSql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     The first cell selected as <typeparamref name="T" />.
        /// </returns>
        public T ExecuteScalar<T>(string fullSql, object parameters) => ExecuteInternal(() =>
            SharedConnection.ExecuteScalar<T>(fullSql, parameters, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region ExecuteScalarAsync Methods

        /// <summary>
        ///     Execute SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <returns>
        ///     The first cell selected as <see cref="object" />.
        /// </returns>
        public async Task<T> ExecuteScalarAsync<T>(string fullSql) => await ExecuteInternalAsync(() =>
            SharedConnection.ExecuteScalarAsync<T>(fullSql, null, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     The first cell selected as <typeparamref name="T" />.
        /// </returns>
        public async Task<T> ExecuteScalarAsync<T>(string fullSql, object parameters) => await ExecuteInternalAsync(
            () => SharedConnection.ExecuteScalarAsync<T>(fullSql, parameters, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        #endregion
    }
}

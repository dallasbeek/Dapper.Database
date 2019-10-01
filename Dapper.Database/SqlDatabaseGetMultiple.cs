using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Dapper.Database
{
    public partial interface ISqlDatabase
    {
        #region GetMultiple Methods

        /// <summary>
        ///     Execute SQL that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     A GridReader
        /// </returns>
        GridReader GetMultiple(string fullSql);

        /// <summary>
        ///     Execute SQL that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     A GridReader
        /// </returns>
        GridReader GetMultiple(string fullSql, object parameters);

        #endregion

        #region GetMultipleAsync Methods

        /// <summary>
        ///     Execute SQL that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     A GridReader
        /// </returns>
        Task<GridReader> GetMultipleAsync(string fullSql);

        /// <summary>
        ///     Execute SQL that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     A GridReader
        /// </returns>
        Task<GridReader> GetMultipleAsync(string fullSql, object parameters);

        #endregion
    }

    public partial class SqlDatabase
    {
        #region GetMultiple Methods

        /// <summary>
        ///     Execute SQL that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     A GridReader
        /// </returns>
        public GridReader GetMultiple(string fullSql) => ExecuteInternal(() =>
            SharedConnection.QueryMultiple(fullSql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     A GridReader
        /// </returns>
        public GridReader GetMultiple(string fullSql, object parameters) => ExecuteInternal(() =>
            SharedConnection.QueryMultiple(fullSql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region GetMultipleAsync Methods

        /// <summary>
        ///     Execute SQL that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     A GridReader
        /// </returns>
        public async Task<GridReader> GetMultipleAsync(string fullSql) => await ExecuteInternalAsync(() =>
            SharedConnection.QueryMultipleAsync(fullSql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     A GridReader
        /// </returns>
        public async Task<GridReader> GetMultipleAsync(string fullSql, object parameters) => await ExecuteInternalAsync(
            () => SharedConnection.QueryMultipleAsync(fullSql, parameters, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        #endregion
    }
}

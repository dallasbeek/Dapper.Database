using System.Data;

namespace Dapper.Database
{
    public partial interface ISqlDatabase
    {
        #region GetDataTable Methods

        /// <summary>
        ///     Execute SQL that returns a DataTable.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     A DataTable
        /// </returns>
        DataTable GetDataTable(string fullSql);

        /// <summary>
        ///     Execute SQL that returns a DataTable.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     A DataTable
        /// </returns>
        DataTable GetDataTable(string fullSql, object parameters);

        #endregion
    }

    public partial class SqlDatabase
    {
        #region GetDataTable Methods

        /// <summary>
        ///     Execute SQL that returns a DataTable.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     A DataTable
        /// </returns>
        public DataTable GetDataTable(string fullSql) =>
            ExecuteInternal(() =>
            {
                var dt = new DataTable();
                dt.Load(SharedConnection.ExecuteReader(fullSql, null, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout));
                return dt;
            });

        /// <summary>
        ///     Execute SQL that returns a DataTable.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     A DataTable
        /// </returns>
        public DataTable GetDataTable(string fullSql, object parameters) =>
            ExecuteInternal(() =>
            {
                var dt = new DataTable();
                dt.Load(SharedConnection.ExecuteReader(fullSql, parameters, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout));
                return dt;
            });

        #endregion
    }
}

using System.Data;
using System.Threading.Tasks;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Contrib extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region CountAsync Extensions
        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Return Total Count of matching records</returns>
        public static async Task<int> CountAsync(this IDbConnection connection, string sql = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await connection.CountAsync(sql, null, transaction, commandTimeout);
        }

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the where clause to count</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Return Total Count of matching records</returns>
        public static async Task<int> CountAsync(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var adapter = GetFormatter(connection);
            return await connection.ExecuteScalarAsync<int>(adapter.CountQuery(null, sql), parameters, transaction, commandTimeout);
        }

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Return Total Count of matching records</returns>
        public static async Task<int> CountAsync<T>(this IDbConnection connection, string sql = null, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return await connection.CountAsync<T>(sql, null, transaction, commandTimeout);
        }

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the where clause to count</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Return Total Count of matching records</returns>
        public static async Task<int> CountAsync<T>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return await connection.ExecuteScalarAsync<int>(sqlHelper.Adapter.CountQuery(sqlHelper.TableInfo, sql), parameters, transaction, commandTimeout);
        }
        #endregion

    }
}

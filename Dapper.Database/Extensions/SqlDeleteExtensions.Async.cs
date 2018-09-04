using System;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Contrib extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region DeleteAsync Extensions
        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToDelete">Entity to delete. If Keys are specified, they will be used as the WHERE condition to delete.</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToDelete == null)
            {
                throw new ArgumentNullException(nameof(entityToDelete), "Cannot Delete null Object");
            }

            var queryWithParameters = BuildDeleteByEntityQueryWithParams(connection, entityToDelete);

            return await connection.ExecuteAsync(queryWithParameters.SqlQuery, queryWithParameters.DynamicParameters, transaction, commandTimeout) > 0;
        }

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKeyValue">a Single primary key to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> DeleteByPrimaryKeyAsync<T>(this IDbConnection connection, object primaryKeyValue, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var queryWithParameters = BuildDeleteByPkQueryWithParams<T>(connection, primaryKeyValue);

            return await connection.ExecuteAsync(queryWithParameters.SqlQuery, queryWithParameters.DynamicParameters, transaction, commandTimeout) > 0;
        }

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            return await connection.ExecuteAsync(BuildBasicDeleteQueryFromSql<T>(connection, sql), null, transaction, commandTimeout) > 0;
        }

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            return await connection.ExecuteAsync(BuildBasicDeleteQueryFromSql<T>(connection, sql), parameters, transaction, commandTimeout) > 0;
        }

        /// <summary>
        /// Delete ALL entities in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> DeleteAllAsync<T>(this IDbConnection connection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return await connection.ExecuteAsync(BuildBasicDeleteQueryFromSql<T>(connection, null), null, transaction, commandTimeout) > 0;
        }

        #endregion
    }
}

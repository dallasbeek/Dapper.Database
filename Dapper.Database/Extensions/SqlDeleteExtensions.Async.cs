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
        /// Delete entity in table "Ts" that match the key values of the entity (T) passed in
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
        /// Delete entity in table "Ts" by a primary key value specified on (T)
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKeyValue">a Single primary key to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, object primaryKeyValue, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var queryWithParameters = BuildDeleteByPkQueryWithParams<T>(connection, primaryKeyValue);

            return await connection.ExecuteAsync(queryWithParameters.SqlQuery, queryWithParameters.DynamicParameters, transaction, commandTimeout) > 0;
        }

        /// <summary>
        /// Delete entity in table "Ts" by an unparameterized WHERE clause.
        /// If you want to Delete All of the data, call the DeleteAll() command
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="whereClause">The where clause to use to bound a delete, cannot be null, empty, or whitespace</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, string whereClause, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
            {
                throw new ArgumentNullException(nameof(whereClause));
            }

            return await connection.ExecuteAsync(BuildBasicDeleteQueryFromSql<T>(connection, whereClause), null, transaction, commandTimeout) > 0;
        }

        /// <summary>
        /// Delete entity in table "Ts" by a parameterized WHERE clause, with Parameters passed in.
        /// If you want to Delete All of the data, call the DeleteAll() command
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="whereClause">The where clause to use to bound a delete, cannot be null, empty, or whitespace</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
            {
                throw new ArgumentNullException(nameof(whereClause));
            }

            return await connection.ExecuteAsync(BuildBasicDeleteQueryFromSql<T>(connection, whereClause), parameters, transaction, commandTimeout) > 0;
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

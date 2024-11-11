using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.Database.Extensions
{
    /// <summary>
    ///     The Dapper.Contrib extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {
        #region Update Queries

        /// <summary>
        ///     Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static bool Update<T>(this IDbConnection connection, T entityToUpdate, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class =>
            connection.Update(entityToUpdate, null, transaction, commandTimeout);

        /// <summary>
        ///     Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static bool Update<T>(this IDbConnection connection, T entityToUpdate,
            IEnumerable<string> columnsToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return sqlHelper.Adapter.Update(connection, transaction, commandTimeout, sqlHelper.TableInfo,
                entityToUpdate, columnsToUpdate);
        }

        #endregion

        #region UpdateAsync Queries

        /// <summary>
        ///     Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static async Task<bool> UpdateAsync<T>(this IDbConnection connection, T entityToUpdate,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class =>
            await connection.UpdateAsync(entityToUpdate, null, transaction, commandTimeout);

        /// <summary>
        ///     Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static async Task<bool> UpdateAsync<T>(this IDbConnection connection, T entityToUpdate,
            IEnumerable<string> columnsToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return await sqlHelper.Adapter.UpdateAsync(connection, transaction, commandTimeout, sqlHelper.TableInfo,
                entityToUpdate, columnsToUpdate);
        }

        #endregion

        #region UpdateList Queries

        /// <summary>
        ///     Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToUpdate">List of Entities to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static bool UpdateList<T>(this IDbConnection connection, IEnumerable<T> entitiesToUpdate,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class =>
            connection.UpdateList(entitiesToUpdate, null, transaction, commandTimeout);

        /// <summary>
        ///     Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToUpdate">List of Entities to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static bool UpdateList<T>(this IDbConnection connection, IEnumerable<T> entitiesToUpdate,
            IEnumerable<string> columnsToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return sqlHelper.Adapter.UpdateList(connection, transaction, commandTimeout, sqlHelper.TableInfo,
                entitiesToUpdate, columnsToUpdate);
        }

        #endregion

        #region UpdateListAsync Queries

        /// <summary>
        ///     Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToUpdate">List of Entities to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static async Task<bool> UpdateListAsync<T>(this IDbConnection connection,
            IEnumerable<T> entitiesToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class => await connection.UpdateListAsync(entitiesToUpdate, null, transaction, commandTimeout);

        /// <summary>
        ///     Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToUpdate">List of Entities to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static async Task<bool> UpdateListAsync<T>(this IDbConnection connection,
            IEnumerable<T> entitiesToUpdate, IEnumerable<string> columnsToUpdate, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return await sqlHelper.Adapter.UpdateListAsync(connection, transaction, commandTimeout, sqlHelper.TableInfo,
                entitiesToUpdate, columnsToUpdate);
        }

        #endregion

        #region UpdateMany Queries
        /// <summary>
        /// Bulk updates many records based on the where clause
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="whereClause">The where clause to use to bind update, pass null or whitespace to update all records</param>
        /// <param name="columnsToUpdate">The list of columns to update.</param>
        /// <param name="parameters">The parameters to use for this update.</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Count of records updated</returns>
        public static int UpdateMany<T>(this IDbConnection connection, string whereClause, IEnumerable<string> columnsToUpdate, object parameters, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return sqlHelper.Adapter.UpdateMany<T>(connection, transaction, commandTimeout, sqlHelper.TableInfo,
                whereClause, columnsToUpdate, parameters);
        }

        #endregion

        #region UpdateManyAsync Queries
        /// <summary>
        /// Bulk updates many records based on the where clause
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="whereClause">The where clause to use to bind update, pass null or whitespace to update all records</param>
        /// <param name="columnsToUpdate">The list of columns to update.</param>
        /// <param name="parameters">The parameters to use for this update.</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Count of records updated</returns>
        public static async Task<int> UpdateManyAsync<T>(this IDbConnection connection, string whereClause, IEnumerable<string> columnsToUpdate, object parameters, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return await sqlHelper.Adapter.UpdateManyAsync<T>(connection, transaction, commandTimeout, sqlHelper.TableInfo,
                whereClause, columnsToUpdate, parameters);
        }

        #endregion
    }
}

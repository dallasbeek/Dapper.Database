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
    }
}

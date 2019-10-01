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
        #region Upsert Queries

        /// <summary>
        ///     Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpsert">Entity to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static bool Upsert<T>(this IDbConnection connection, T entityToUpsert, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class =>
            connection.Upsert(entityToUpsert, null, null, null, transaction, commandTimeout);

        /// <summary>
        ///     Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpsert">Entity to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static bool Upsert<T>(this IDbConnection connection, T entityToUpsert,
            IEnumerable<string> columnsToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class =>
            connection.Upsert(entityToUpsert, columnsToUpdate, null, null, transaction, commandTimeout);

        /// <summary>
        ///     Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpsert">Entity to be inserted or updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updating</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static bool Upsert<T>(this IDbConnection connection, T entityToUpsert, Action<T> insertAction,
            Action<T> updateAction, IDbTransaction transaction = null, int? commandTimeout = null) where T : class =>
            connection.Upsert(entityToUpsert, null, insertAction, updateAction, transaction, commandTimeout);

        /// <summary>
        ///     Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpsert">Entity to be inserted or updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updating</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static bool Upsert<T>(this IDbConnection connection, T entityToUpsert,
            IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return sqlHelper.Adapter.Upsert(connection, transaction, commandTimeout, sqlHelper.TableInfo,
                entityToUpsert, columnsToUpdate, insertAction, updateAction);
        }

        #endregion

        #region UpsertAsync Queries

        /// <summary>
        ///     Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpsert">Entity to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static async Task<bool> UpsertAsync<T>(this IDbConnection connection, T entityToUpsert,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class =>
            await connection.UpsertAsync(entityToUpsert, null, null, null, transaction, commandTimeout);

        /// <summary>
        ///     Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpsert">Entity to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static async Task<bool> UpsertAsync<T>(this IDbConnection connection, T entityToUpsert,
            IEnumerable<string> columnsToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class => await connection.UpsertAsync(entityToUpsert, columnsToUpdate, null, null, transaction,
            commandTimeout);

        /// <summary>
        ///     Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpsert">Entity to be inserted or updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updating</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static async Task<bool> UpsertAsync<T>(this IDbConnection connection, T entityToUpsert,
            Action<T> insertAction, Action<T> updateAction, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class => await connection.UpsertAsync(entityToUpsert, null,
            insertAction, updateAction, transaction, commandTimeout);

        /// <summary>
        ///     Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpsert">Entity to be inserted or updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updating</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static async Task<bool> UpsertAsync<T>(this IDbConnection connection, T entityToUpsert,
            IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return await sqlHelper.Adapter.UpsertAsync(connection, transaction, commandTimeout, sqlHelper.TableInfo,
                entityToUpsert, columnsToUpdate, insertAction, updateAction);
        }

        #endregion

        #region UpsertList Queries

        /// <summary>
        ///     Updates or inserts a list of entities in table
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToUpsert">List of Entities to be updated or inserted</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated or inserted, false if not</returns>
        public static bool UpsertList<T>(this IDbConnection connection, IEnumerable<T> entitiesToUpsert,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class =>
            connection.UpsertList(entitiesToUpsert, null, null, null, transaction, commandTimeout);

        /// <summary>
        ///     Updates or inserts a list of entities in table
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToUpsert">List of Entities to be updated or inserted</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated or inserted, false if not</returns>
        public static bool UpsertList<T>(this IDbConnection connection, IEnumerable<T> entitiesToUpsert,
            IEnumerable<string> columnsToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class => connection.UpsertList(entitiesToUpsert, columnsToUpdate, null, null, transaction,
            commandTimeout);

        /// <summary>
        ///     Updates or inserts a list of entities in table
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToUpsert">List of Entities to be updated or inserted</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updating</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated or inserted, false if not</returns>
        public static bool UpsertList<T>(this IDbConnection connection, IEnumerable<T> entitiesToUpsert,
            Action<T> insertAction, Action<T> updateAction, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class => connection.UpsertList(entitiesToUpsert, null, insertAction,
            updateAction, transaction, commandTimeout);

        /// <summary>
        ///     Updates or inserts a list of entities in table
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToUpsert">List of Entities to be updated or inserted</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updating</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated or inserted, false if not</returns>
        public static bool UpsertList<T>(this IDbConnection connection, IEnumerable<T> entitiesToUpsert,
            IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return sqlHelper.Adapter.UpsertList(connection, transaction, commandTimeout, sqlHelper.TableInfo,
                entitiesToUpsert, columnsToUpdate, insertAction, updateAction);
        }

        #endregion

        #region UpsertListAsync Queries

        /// <summary>
        ///     Updates or inserts a list of entities in table
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToUpsert">List of Entities to be updated or inserted</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated or inserted, false if not</returns>
        public static async Task<bool> UpsertListAsync<T>(this IDbConnection connection,
            IEnumerable<T> entitiesToUpsert, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class =>
            await connection.UpsertListAsync(entitiesToUpsert, null, null, null, transaction, commandTimeout);

        /// <summary>
        ///     Updates or inserts a list of entities in table
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToUpsert">List of Entities to be updated or inserted</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated or inserted, false if not</returns>
        public static async Task<bool> UpsertListAsync<T>(this IDbConnection connection,
            IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class => await connection.UpsertListAsync(entitiesToUpsert,
            columnsToUpdate, null, null, transaction, commandTimeout);

        /// <summary>
        ///     Updates or inserts a list of entities in table
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToUpsert">List of Entities to be updated or inserted</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updating</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated or inserted, false if not</returns>
        public static async Task<bool> UpsertListAsync<T>(this IDbConnection connection,
            IEnumerable<T> entitiesToUpsert, Action<T> insertAction, Action<T> updateAction,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class =>
            await connection.UpsertListAsync(entitiesToUpsert, null, insertAction, updateAction, transaction,
                commandTimeout);

        /// <summary>
        ///     Updates or inserts a list of entities in table
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToUpsert">List of Entities to be updated or inserted</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updating</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated or inserted, false if not</returns>
        public static async Task<bool> UpsertListAsync<T>(this IDbConnection connection,
            IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction,
            Action<T> updateAction, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return await sqlHelper.Adapter.UpsertListAsync(connection, transaction, commandTimeout, sqlHelper.TableInfo,
                entitiesToUpsert, columnsToUpdate, insertAction, updateAction);
        }

        #endregion
    }
}

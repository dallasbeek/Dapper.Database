using System;
using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database
{
    public partial interface ISqlDatabase
    {
        #region Delete Methods

        /// <summary>
        ///     Delete entity in table "Ts" that match the key values of the entity (T) passed in
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entityToDelete">
        ///     Entity to delete. If Keys are specified, they will be used as the WHERE condition to
        ///     delete.
        /// </param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        bool Delete<T>(T entityToDelete) where T : class;

        /// <summary>
        ///     Delete entity in table "Ts" by a primary key value specified on (T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKeyValue">a Single primary key to delete</param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        bool Delete<T>(object primaryKeyValue) where T : class;

        /// <summary>
        ///     Delete entity in table "Ts" by an un-parameterized WHERE clause.
        ///     If you want to Delete All of the data, call the DeleteAll() command
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereClause">The where clause to use to bound a delete, cannot be null, empty, or whitespace</param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        bool Delete<T>(string whereClause) where T : class;

        /// <summary>
        ///     Delete entity(s).
        /// </summary>
        /// <typeparam name="T">The type of entity to delete.</typeparam>
        /// <param name="whereClause">The where clause.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        bool Delete<T>(string whereClause, object parameters) where T : class;

        /// <summary>
        ///     Delete ALL entities.
        /// </summary>
        /// <typeparam name="T">The type of entity to delete.</typeparam>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        bool DeleteAll<T>() where T : class;

        #endregion

        #region DeleteAsync Methods

        /// <summary>
        ///     Delete entity in table "Ts" that match the key values of the entity (T) passed in
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entityToDelete">
        ///     Entity to delete. If Keys are specified, they will be used as the WHERE condition to
        ///     delete.
        /// </param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        Task<bool> DeleteAsync<T>(T entityToDelete) where T : class;

        /// <summary>
        ///     Delete entity in table "Ts" by a primary key value specified on (T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKeyValue">a Single primary key to delete</param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        Task<bool> DeleteAsync<T>(object primaryKeyValue) where T : class;

        /// <summary>
        ///     Delete entity in table "Ts" by an un-parameterized WHERE clause.
        ///     If you want to Delete All of the data, call the DeleteAll() command
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereClause">The where clause to use to bound a delete, cannot be null, empty, or whitespace</param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        Task<bool> DeleteAsync<T>(string whereClause) where T : class;

        /// <summary>
        ///     Delete entity(s).
        /// </summary>
        /// <typeparam name="T">The type of entity to delete.</typeparam>
        /// <param name="whereClause">The where clause.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        Task<bool> DeleteAsync<T>(string whereClause, object parameters) where T : class;

        /// <summary>
        ///     Delete ALL entities.
        /// </summary>
        /// <typeparam name="T">The type of entity to delete.</typeparam>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        Task<bool> DeleteAllAsync<T>() where T : class;

        #endregion
    }

    public partial class SqlDatabase
    {
        #region Delete Methods

        /// <summary>
        ///     Delete entity in table "Ts" that match the key values of the entity (T) passed in
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entityToDelete">
        ///     Entity to delete. If Keys are specified, they will be used as the WHERE condition to
        ///     delete.
        /// </param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        public bool Delete<T>(T entityToDelete) where T : class => ExecuteInternal(() =>
            SharedConnection.Delete(entityToDelete, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Delete entity in table "Ts" by a primary key value specified on (T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKeyValue">a Single primary key to delete</param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        public bool Delete<T>(object primaryKeyValue) where T : class => ExecuteInternal(() =>
            SharedConnection.Delete<T>(primaryKeyValue, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Delete entity in table "Ts" by an un-parameterized WHERE clause.
        ///     If you want to Delete All of the data, call the DeleteAll() command
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereClause">The where clause to use to bound a delete, cannot be null, empty, or whitespace</param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        public bool Delete<T>(string whereClause) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
                throw new ArgumentNullException(nameof(whereClause), "Must specify a where clause for deletion.");
            return ExecuteInternal(() =>
                SharedConnection.Delete<T>(whereClause, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        ///     Delete entity(s).
        /// </summary>
        /// <typeparam name="T">The type of entity to delete.</typeparam>
        /// <param name="whereClause">The where clause.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        public bool Delete<T>(string whereClause, object parameters) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
                throw new ArgumentNullException(nameof(whereClause), "Must specify a where clause for deletion.");
            return ExecuteInternal(() =>
                SharedConnection.Delete<T>(whereClause, parameters, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        ///     Delete ALL entities.
        /// </summary>
        /// <typeparam name="T">The type of entity to delete.</typeparam>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        public bool DeleteAll<T>() where T : class => ExecuteInternal(() =>
            SharedConnection.DeleteAll<T>(_transaction, OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region DeleteAsync Methods

        /// <summary>
        ///     Delete entity in table "Ts" that match the key values of the entity (T) passed in
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entityToDelete">
        ///     Entity to delete. If Keys are specified, they will be used as the WHERE condition to
        ///     delete.
        /// </param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        public async Task<bool> DeleteAsync<T>(T entityToDelete) where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.DeleteAsync(entityToDelete, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Delete entity in table "Ts" by a primary key value specified on (T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKeyValue">a Single primary key to delete</param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        public async Task<bool> DeleteAsync<T>(object primaryKeyValue) where T : class => await ExecuteInternalAsync(
            () => SharedConnection.DeleteAsync<T>(primaryKeyValue, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Delete entity in table "Ts" by an un-parameterized WHERE clause.
        ///     If you want to Delete All of the data, call the DeleteAll() command
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereClause">The where clause to use to bound a delete, cannot be null, empty, or whitespace</param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        public async Task<bool> DeleteAsync<T>(string whereClause) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
                throw new ArgumentNullException(nameof(whereClause), "Must specify a where clause for deletion.");
            return await ExecuteInternalAsync(() =>
                SharedConnection.DeleteAsync<T>(whereClause, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        ///     Delete entity(s).
        /// </summary>
        /// <typeparam name="T">The type of entity to delete.</typeparam>
        /// <param name="whereClause">The where clause.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        public async Task<bool> DeleteAsync<T>(string whereClause, object parameters) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
                throw new ArgumentNullException(nameof(whereClause), "Must specify a where clause for deletion.");
            return await ExecuteInternalAsync(() =>
                SharedConnection.DeleteAsync<T>(whereClause, parameters, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        ///     Delete ALL entities.
        /// </summary>
        /// <typeparam name="T">The type of entity to delete.</typeparam>
        /// <returns>
        ///     True if deleted, false if not found.
        /// </returns>
        public async Task<bool> DeleteAllAsync<T>() where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.DeleteAllAsync<T>(_transaction, OneTimeCommandTimeout ?? CommandTimeout));

        #endregion
    }
}

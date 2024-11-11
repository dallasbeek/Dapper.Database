using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database
{
    public partial interface ISqlDatabase
    {
        #region Update Methods

        /// <summary>
        ///     Updates an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entityToUpdate">The Entity to update.</param>
        /// <returns>
        ///     True if the record is updated.
        /// </returns>
        bool Update<T>(T entityToUpdate) where T : class;

        /// <summary>
        ///     Updates an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entityToUpdate">The Entity to update.</param>
        /// <param name="columnsToUpdate">The list of columns to updates.</param>
        /// <returns>
        ///     True if the record is updated.
        /// </returns>
        bool Update<T>(T entityToUpdate, IEnumerable<string> columnsToUpdate) where T : class;

        #endregion

        #region UpdateAsync Methods

        /// <summary>
        ///     Updates an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entityToUpdate">The Entity to update.</param>
        /// <returns>
        ///     True if the record is updated.
        /// </returns>
        Task<bool> UpdateAsync<T>(T entityToUpdate) where T : class;

        /// <summary>
        ///     Updates an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entityToUpdate">The Entity to update.</param>
        /// <param name="columnsToUpdate">The list of columns to updates.</param>
        /// <returns>
        ///     True if the record is updated.
        /// </returns>
        Task<bool> UpdateAsync<T>(T entityToUpdate, IEnumerable<string> columnsToUpdate) where T : class;

        #endregion

        #region UpdateList Methods

        /// <summary>
        ///     Updates a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entitiesToUpdate">The IEnumerable list of Entity to update.</param>
        /// <returns>
        ///     True if records are updated.
        /// </returns>
        bool UpdateList<T>(IEnumerable<T> entitiesToUpdate) where T : class;

        /// <summary>
        ///     Inserts a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entitiesToUpdate">The IEnumerable list of Entity to update.</param>
        /// <param name="columnsToUpdate">The list of columns to updates.</param>
        /// <returns>
        ///     True if records are updated.
        /// </returns>
        bool UpdateList<T>(IEnumerable<T> entitiesToUpdate, IEnumerable<string> columnsToUpdate) where T : class;

        #endregion

        #region UpdateListAsync Methods

        /// <summary>
        ///     Updates a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entitiesToUpdate">The IEnumerable list of Entity to update.</param>
        /// <returns>
        ///     True if records are updated.
        /// </returns>
        Task<bool> UpdateListAsync<T>(IEnumerable<T> entitiesToUpdate) where T : class;

        /// <summary>
        ///     Inserts a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entitiesToUpdate">The IEnumerable list of Entity to update.</param>
        /// <param name="columnsToUpdate">The list of columns to update.</param>
        /// <returns>
        ///     True if records are updated.
        /// </returns>
        Task<bool> UpdateListAsync<T>(IEnumerable<T> entitiesToUpdate, IEnumerable<string> columnsToUpdate)
            where T : class;

        #endregion

        #region UpdateMany Methods

        /// <summary>
        /// Bulk updates many records based on the where clause
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="whereClause">The where clause to use to bind update, pass null or whitespace to update all records</param>
        /// <param name="columnsToUpdate">The list of columns to update.</param>
        /// <param name="parameters">The parameters to use for this update.</param>
        /// <returns>Count of records updated</returns>
        int UpdateMany<T>(string whereClause, IEnumerable<string> columnsToUpdate, object parameters) where T : class;

        #endregion

        #region UpdateManyAsync Methods

        /// <summary>
        /// Bulk updates many records based on the where clause
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="whereClause">The where clause to use to bind update, pass null or whitespace to update all records</param>
        /// <param name="columnsToUpdate">The list of columns to update.</param>
        /// <param name="parameters">The parameters to use for this update.</param>
        /// <returns>Count of records updated</returns>
        Task<int> UpdateManyAsync<T>(string whereClause, IEnumerable<string> columnsToUpdate, object parameters) where T : class;

        #endregion
    }

    public partial class SqlDatabase
    {
        #region Update Methods

        /// <summary>
        ///     Updates an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entityToUpdate">The Entity to update.</param>
        /// <returns>
        ///     True if the record is updated.
        /// </returns>
        public bool Update<T>(T entityToUpdate) where T : class => ExecuteInternal(() =>
            SharedConnection.Update(entityToUpdate, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Updates an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entityToUpdate">The Entity to update.</param>
        /// <param name="columnsToUpdate">The list of columns to updates.</param>
        /// <returns>
        ///     True if the record is updated.
        /// </returns>
        public bool Update<T>(T entityToUpdate, IEnumerable<string> columnsToUpdate) where T : class => ExecuteInternal(
            () => SharedConnection.Update(entityToUpdate, columnsToUpdate, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region UpdateAsync Methods

        /// <summary>
        ///     Updates an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entityToUpdate">The Entity to update.</param>
        /// <returns>
        ///     True if the record is updated.
        /// </returns>
        public async Task<bool> UpdateAsync<T>(T entityToUpdate) where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.UpdateAsync(entityToUpdate, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Updates an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entityToUpdate">The Entity to update.</param>
        /// <param name="columnsToUpdate">The list of columns to updates.</param>
        /// <returns>
        ///     True if the record is updated.
        /// </returns>
        public async Task<bool> UpdateAsync<T>(T entityToUpdate, IEnumerable<string> columnsToUpdate) where T : class =>
            await ExecuteInternalAsync(() => SharedConnection.UpdateAsync(entityToUpdate, columnsToUpdate, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region UpdateList Methods

        /// <summary>
        ///     Updates a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entitiesToUpdate">The IEnumerable list of Entity to update.</param>
        /// <returns>
        ///     True if records are updated.
        /// </returns>
        public bool UpdateList<T>(IEnumerable<T> entitiesToUpdate) where T : class => ExecuteInternal(
            () => SharedConnection.UpdateList(entitiesToUpdate, _transaction, OneTimeCommandTimeout ?? CommandTimeout),
            true);

        /// <summary>
        ///     Inserts a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entitiesToUpdate">The IEnumerable list of Entity to update.</param>
        /// <param name="columnsToUpdate">The list of columns to updates.</param>
        /// <returns>
        ///     True if records are updated.
        /// </returns>
        public bool UpdateList<T>(IEnumerable<T> entitiesToUpdate, IEnumerable<string> columnsToUpdate)
            where T : class =>
            ExecuteInternal(
                () => SharedConnection.UpdateList(entitiesToUpdate, columnsToUpdate, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout), true);

        #endregion

        #region UpdateListAsync Methods

        /// <summary>
        ///     Updates a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entitiesToUpdate">The IEnumerable list of Entity to update.</param>
        /// <returns>
        ///     True if records are updated.
        /// </returns>
        public async Task<bool> UpdateListAsync<T>(IEnumerable<T> entitiesToUpdate) where T : class =>
            await ExecuteInternalAsync(
                () => SharedConnection.UpdateListAsync(entitiesToUpdate, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout), true);

        /// <summary>
        ///     Inserts a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entitiesToUpdate">The IEnumerable list of Entity to update.</param>
        /// <param name="columnsToUpdate">The list of columns to updates.</param>
        /// <returns>
        ///     True if records are updated.
        /// </returns>
        public async Task<bool> UpdateListAsync<T>(IEnumerable<T> entitiesToUpdate, IEnumerable<string> columnsToUpdate)
            where T : class =>
            await ExecuteInternalAsync(
                () => SharedConnection.UpdateListAsync(entitiesToUpdate, columnsToUpdate, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout), true);

        #endregion

        #region UpdateMany
        /// <summary>
        /// Bulk updates many records based on the where clause
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="whereClause">The where clause to use to bind update, pass null or whitespace to update all records</param>
        /// <param name="columnsToUpdate">The list of columns to update.</param>
        /// <param name="parameters">The parameters to use for this update.</param>
        /// <returns>Count of records updated</returns>
        public int UpdateMany<T>(string whereClause, IEnumerable<string> columnsToUpdate, object parameters) where T : class
            => ExecuteInternal(
            () => SharedConnection.UpdateMany<T>(whereClause, columnsToUpdate, parameters, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));
        #endregion

        #region UpdateManyAsync
        /// <summary>
        /// Bulk updates many records based on the where clause
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="whereClause">The where clause to use to bind update, pass null or whitespace to update all records</param>
        /// <param name="columnsToUpdate">The list of columns to update.</param>
        /// <param name="parameters">The parameters to use for this update.</param>
        /// <returns>Count of records updated</returns>
        public async Task<int> UpdateManyAsync<T>(string whereClause, IEnumerable<string> columnsToUpdate, object parameters) where T : class =>
            await ExecuteInternalAsync(() => SharedConnection.UpdateManyAsync<T>(whereClause, columnsToUpdate, parameters, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));
        #endregion
    }
}

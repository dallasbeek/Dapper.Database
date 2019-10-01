using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database
{
    public partial interface ISqlDatabase
    {
        #region Upsert Methods

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        bool Upsert<T>(T entityToUpsert) where T : class;

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        bool Upsert<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate) where T : class;

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        bool Upsert<T>(T entityToUpsert, Action<T> insertAction, Action<T> updateAction) where T : class;

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        bool Upsert<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction,
            Action<T> updateAction) where T : class;

        #endregion

        #region UpsertAsync Methods

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        Task<bool> UpsertAsync<T>(T entityToUpsert) where T : class;

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        Task<bool> UpsertAsync<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate) where T : class;

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        Task<bool> UpsertAsync<T>(T entityToUpsert, Action<T> insertAction, Action<T> updateAction) where T : class;

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        Task<bool> UpsertAsync<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction,
            Action<T> updateAction) where T : class;

        #endregion

        #region UpsertList Methods

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        bool UpsertList<T>(IEnumerable<T> entitiesToUpsert) where T : class;

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        bool UpsertList<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate) where T : class;

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        bool UpsertList<T>(IEnumerable<T> entitiesToUpsert, Action<T> insertAction, Action<T> updateAction)
            where T : class;

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        bool UpsertList<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction,
            Action<T> updateAction) where T : class;

        #endregion

        #region UpsertListAsync Methods

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert) where T : class;

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate)
            where T : class;

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert, Action<T> insertAction, Action<T> updateAction)
            where T : class;

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate,
            Action<T> insertAction, Action<T> updateAction) where T : class;

        #endregion
    }

    public partial class SqlDatabase
    {
        #region Upsert Methods

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        public bool Upsert<T>(T entityToUpsert) where T : class => ExecuteInternal(() =>
            SharedConnection.Upsert(entityToUpsert, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        public bool Upsert<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate) where T : class => ExecuteInternal(
            () => SharedConnection.Upsert(entityToUpsert, columnsToUpdate, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        public bool Upsert<T>(T entityToUpsert, Action<T> insertAction, Action<T> updateAction) where T : class =>
            ExecuteInternal(() => SharedConnection.Upsert(entityToUpsert, insertAction, updateAction, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        public bool Upsert<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction,
            Action<T> updateAction) where T : class => ExecuteInternal(() => SharedConnection.Upsert(entityToUpsert,
            columnsToUpdate, insertAction, updateAction, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region UpsertAsync Methods

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        public async Task<bool> UpsertAsync<T>(T entityToUpsert) where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.UpsertAsync(entityToUpsert, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        public async Task<bool> UpsertAsync<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate) where T : class =>
            await ExecuteInternalAsync(() => SharedConnection.UpsertAsync(entityToUpsert, columnsToUpdate, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        public async Task<bool> UpsertAsync<T>(T entityToUpsert, Action<T> insertAction, Action<T> updateAction)
            where T : class => await ExecuteInternalAsync(() => SharedConnection.UpsertAsync(entityToUpsert,
            insertAction, updateAction, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the record is updated or inserted.
        /// </returns>
        public async Task<bool> UpsertAsync<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate,
            Action<T> insertAction, Action<T> updateAction) where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.UpsertAsync(entityToUpsert, columnsToUpdate, insertAction, updateAction, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region UpsertList Methods

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        public bool UpsertList<T>(IEnumerable<T> entitiesToUpsert) where T : class => ExecuteInternal(
            () => SharedConnection.UpsertList(entitiesToUpsert, _transaction, OneTimeCommandTimeout ?? CommandTimeout),
            true);

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        public bool UpsertList<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate)
            where T : class =>
            ExecuteInternal(
                () => SharedConnection.UpsertList(entitiesToUpsert, columnsToUpdate, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout), true);

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        public bool UpsertList<T>(IEnumerable<T> entitiesToUpsert, Action<T> insertAction, Action<T> updateAction)
            where T : class =>
            ExecuteInternal(
                () => SharedConnection.UpsertList(entitiesToUpsert, insertAction, updateAction, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout), true);

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        public bool UpsertList<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate,
            Action<T> insertAction, Action<T> updateAction) where T : class => ExecuteInternal(
            () => SharedConnection.UpsertList(entitiesToUpsert, columnsToUpdate, insertAction, updateAction,
                _transaction, OneTimeCommandTimeout ?? CommandTimeout), true);

        #endregion

        #region UpsertListAsync Methods

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        public async Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert) where T : class =>
            await ExecuteInternalAsync(
                () => SharedConnection.UpsertListAsync(entitiesToUpsert, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout), true);

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        public async Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate)
            where T : class =>
            await ExecuteInternalAsync(
                () => SharedConnection.UpsertListAsync(entitiesToUpsert, columnsToUpdate, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout), true);

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        public async Task<bool>
            UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert, Action<T> insertAction, Action<T> updateAction)
            where T : class =>
            await ExecuteInternalAsync(
                () => SharedConnection.UpsertListAsync(entitiesToUpsert, insertAction, updateAction, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout), true);

        /// <summary>
        ///     Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        ///     True if the records are updated or inserted.
        /// </returns>
        public async Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate,
            Action<T> insertAction, Action<T> updateAction) where T : class => await ExecuteInternalAsync(
            () => SharedConnection.UpsertListAsync(entitiesToUpsert, columnsToUpdate, insertAction, updateAction,
                _transaction, OneTimeCommandTimeout ?? CommandTimeout), true);

        #endregion
    }
}

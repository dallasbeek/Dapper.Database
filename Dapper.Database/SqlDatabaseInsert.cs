using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database
{
    public partial interface ISqlDatabase
    {
        #region Insert Methods

        /// <summary>
        ///     Inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to insert.</typeparam>
        /// <param name="entityToInsert">The Entity to insert.</param>
        /// <returns>
        ///     True if the record is inserted.
        /// </returns>
        bool Insert<T>(T entityToInsert) where T : class;

        #endregion

        #region InsertAsync Methods

        /// <summary>
        ///     Inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to insert.</typeparam>
        /// <param name="entityToInsert">The Entity to insert.</param>
        /// <returns>
        ///     True if the record is inserted.
        /// </returns>
        Task<bool> InsertAsync<T>(T entityToInsert) where T : class;

        #endregion

        #region InsertList Methods

        /// <summary>
        ///     Inserts a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to insert.</typeparam>
        /// <param name="entitiesToInsert">The IEnumerable list of Entity to insert.</param>
        /// <returns>
        ///     True if records are inserted.
        /// </returns>
        bool InsertList<T>(IEnumerable<T> entitiesToInsert) where T : class;

        #endregion

        #region InsertListAsync Methods

        /// <summary>
        ///     Inserts a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to insert.</typeparam>
        /// <param name="entitiesToInsert">The IEnumerable list of Entity to insert.</param>
        /// <returns>
        ///     True if records are inserted.
        /// </returns>
        Task<bool> InsertListAsync<T>(IEnumerable<T> entitiesToInsert) where T : class;

        #endregion
    }

    public partial class SqlDatabase
    {
        #region Insert Methods

        /// <summary>
        ///     Inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to insert.</typeparam>
        /// <param name="entityToInsert">The Entity to insert.</param>
        /// <returns>
        ///     True if the record is inserted.
        /// </returns>
        public bool Insert<T>(T entityToInsert) where T : class => ExecuteInternal(() =>
            SharedConnection.Insert(entityToInsert, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region InsertAsync Methods

        /// <summary>
        ///     Inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to insert.</typeparam>
        /// <param name="entityToInsert">The Entity to insert.</param>
        /// <returns>
        ///     True if the record is inserted.
        /// </returns>
        public async Task<bool> InsertAsync<T>(T entityToInsert) where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.InsertAsync(entityToInsert, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region InsertList Methods

        /// <summary>
        ///     Inserts a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to insert.</typeparam>
        /// <param name="entitiesToInsert">The IEnumerable list of Entity to insert.</param>
        /// <returns>
        ///     True if records are inserted.
        /// </returns>
        public bool InsertList<T>(IEnumerable<T> entitiesToInsert) where T : class => ExecuteInternal(
            () => SharedConnection.InsertList(entitiesToInsert, _transaction, OneTimeCommandTimeout ?? CommandTimeout),
            true);

        #endregion

        #region InsertListAsync Methods

        /// <summary>
        ///     Inserts a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to insert.</typeparam>
        /// <param name="entitiesToInsert">The IEnumerable list of Entity to insert.</param>
        /// <returns>
        ///     True if records are inserted.
        /// </returns>
        public async Task<bool> InsertListAsync<T>(IEnumerable<T> entitiesToInsert) where T : class =>
            await ExecuteInternalAsync(
                () => SharedConnection.InsertListAsync(entitiesToInsert, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout), true);

        #endregion
    }
}

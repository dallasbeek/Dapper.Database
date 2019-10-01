using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database
{
    public partial interface ISqlDatabase
    {
        #region Exists Methods

        /// <summary>
        ///     Execute SQL that checks if record(s) exist.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        bool Exists(string fullSql = null);

        /// <summary>
        ///     Execute SQL that checks if record(s) exist.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        bool Exists(string fullSql, object parameters);

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entityToCheck">Entity to check for existence.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        bool Exists<T>(T entityToCheck) where T : class;

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="primaryKey">A single primary key to check.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        bool Exists<T>(object primaryKey) where T : class;

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        bool Exists<T>(string sql = null) where T : class;

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The SQL clause to check for existence.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        bool Exists<T>(string sql, object parameters) where T : class;

        #endregion

        #region ExistsAsync Methods

        /// <summary>
        ///     Execute SQL that checks if record(s) exist.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        Task<bool> ExistsAsync(string fullSql = null);

        /// <summary>
        ///     Execute SQL that checks if record(s) exist.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        Task<bool> ExistsAsync(string fullSql, object parameters);

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entityToCheck">Entity to check for existence.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        Task<bool> ExistsAsync<T>(T entityToCheck) where T : class;

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="primaryKey">A single primary key to check.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        Task<bool> ExistsAsync<T>(object primaryKey) where T : class;

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        Task<bool> ExistsAsync<T>(string sql = null) where T : class;

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The SQL clause to check for existence.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        Task<bool> ExistsAsync<T>(string sql, object parameters) where T : class;

        #endregion
    }

    public partial class SqlDatabase
    {
        #region Exists Methods

        /// <summary>
        ///     Execute SQL that checks if record(s) exist.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        public bool Exists(string fullSql) => ExecuteInternal(() =>
            SharedConnection.ExecuteScalar<bool>(fullSql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that checks if record(s) exist.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        public bool Exists(string fullSql, object parameters) => ExecuteInternal(() =>
            SharedConnection.ExecuteScalar<bool>(fullSql, parameters, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entityToCheck">Entity to check for existence.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        public bool Exists<T>(T entityToCheck) where T : class => ExecuteInternal(() =>
            SharedConnection.Exists(entityToCheck, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="primaryKey">A single primary key to check.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        public bool Exists<T>(object primaryKey) where T : class => ExecuteInternal(() =>
            SharedConnection.Exists<T>(primaryKey, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        public bool Exists<T>(string sql = null) where T : class => ExecuteInternal(() =>
            SharedConnection.Exists<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The SQL clause to check for existence.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        public bool Exists<T>(string sql, object parameters) where T : class => ExecuteInternal(() =>
            SharedConnection.Exists<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region ExistAsync Methods

        /// <summary>
        ///     Execute SQL that checks if record(s) exist.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        public async Task<bool> ExistsAsync(string fullSql) => await ExecuteInternalAsync(() =>
            SharedConnection.ExecuteScalarAsync<bool>(fullSql, null, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that checks if record(s) exist.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        public async Task<bool> ExistsAsync(string fullSql, object parameters) => await ExecuteInternalAsync(() =>
            SharedConnection.ExecuteScalarAsync<bool>(fullSql, parameters, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entityToCheck">Entity to check for existence.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        public async Task<bool> ExistsAsync<T>(T entityToCheck) where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.ExistsAsync(entityToCheck, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="primaryKey">A single primary key to check.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        public async Task<bool> ExistsAsync<T>(object primaryKey) where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.ExistsAsync<T>(primaryKey, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        public async Task<bool> ExistsAsync<T>(string sql = null) where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.ExistsAsync<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The SQL clause to check for existence.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     True if record is found.
        /// </returns>
        public async Task<bool> ExistsAsync<T>(string sql, object parameters) where T : class =>
            await ExecuteInternalAsync(() =>
                SharedConnection.ExistsAsync<T>(sql, parameters, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout));

        #endregion
    }
}

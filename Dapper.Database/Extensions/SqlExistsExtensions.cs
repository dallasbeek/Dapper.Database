using System;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.Database.Extensions
{
    /// <summary>
    ///     The Dapper.Contrib extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {
        #region Exists Extensions

        /// <summary>
        ///     Check if a record exists in table "Ts"
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entity">Entity to check</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if record is found</returns>
        public static bool Exists<T>(this IDbConnection connection, T entity, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class
        {
            if (entity == null)
                throw new ArgumentException("Cannot check null object for existence", nameof(entity));

            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return sqlHelper.Adapter.Exists(connection, transaction, commandTimeout, sqlHelper.TableInfo,
                entity);
        }

        /// <summary>
        ///     Check if a record exists
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKey">a Single primary key to check</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if record is found</returns>
        public static bool Exists<T>(this IDbConnection connection, object primaryKey,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            var existsQuery =
                sqlHelper.GenerateSingleKeyQuery(primaryKey, (ti, sql) => sqlHelper.Adapter.ExistsQuery(ti, sql));
            return connection.ExecuteScalar<bool>(existsQuery.SqlStatement, existsQuery.Parameters, transaction,
                commandTimeout);
        }

        /// <summary>
        ///     Check if a record exists
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if record is found</returns>
        public static bool Exists<T>(this IDbConnection connection, string sql = null,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return connection.ExecuteScalar<bool>(sqlHelper.Adapter.ExistsQuery(sqlHelper.TableInfo, sql), null,
                transaction, commandTimeout);
        }

        /// <summary>
        ///     Check if a record exists
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if record is found</returns>
        public static bool Exists<T>(this IDbConnection connection, string sql, object parameters,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return connection.ExecuteScalar<bool>(sqlHelper.Adapter.ExistsQuery(sqlHelper.TableInfo, sql), parameters,
                transaction, commandTimeout);
        }

        #endregion

        #region ExistsAsync Extensions

        /// <summary>
        ///     Check if a record exists
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToExists">Entity to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if record is found</returns>
        public static async Task<bool> ExistsAsync<T>(this IDbConnection connection, T entityToExists,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToExists == null)
                throw new ArgumentException("Cannot Exists null Object", nameof(entityToExists));

            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            var existsQuery =
                sqlHelper.GenerateCompositeKeyQuery(entityToExists,
                    (ti, sql) => sqlHelper.Adapter.ExistsQuery(ti, sql));
            return await connection.ExecuteScalarAsync<bool>(existsQuery.SqlStatement, existsQuery.Parameters,
                transaction, commandTimeout);
        }

        /// <summary>
        ///     Check if a record exists
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKey">a Single primary key to check</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if record is found</returns>
        public static async Task<bool> ExistsAsync<T>(this IDbConnection connection, object primaryKey,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            var existsQuery =
                sqlHelper.GenerateSingleKeyQuery(primaryKey, (ti, sql) => sqlHelper.Adapter.ExistsQuery(ti, sql));
            return await connection.ExecuteScalarAsync<bool>(existsQuery.SqlStatement, existsQuery.Parameters,
                transaction, commandTimeout);
        }

        /// <summary>
        ///     Check if a record exists
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if record is found</returns>
        public static async Task<bool> ExistsAsync<T>(this IDbConnection connection, string sql = null,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return await connection.ExecuteScalarAsync<bool>(sqlHelper.Adapter.ExistsQuery(sqlHelper.TableInfo, sql),
                null, transaction, commandTimeout);
        }

        /// <summary>
        ///     Check if a record exists
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if record is found</returns>
        public static async Task<bool> ExistsAsync<T>(this IDbConnection connection, string sql, object parameters,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return await connection.ExecuteScalarAsync<bool>(sqlHelper.Adapter.ExistsQuery(sqlHelper.TableInfo, sql),
                parameters, transaction, commandTimeout);
        }

        #endregion
    }
}

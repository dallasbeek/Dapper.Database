using System;
using System.Data;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Contrib extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region Exists Extensions
        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToExists">Entity to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if record is found</returns>
        public static bool Exists<T>(this IDbConnection connection, T entityToExists, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToExists == null)
                throw new ArgumentException("Cannot Exists null Object", nameof(entityToExists));

            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            return connection.ExecuteScalar<bool>(adapter.ExistsQuery(tinfo, null), entityToExists, transaction, commandTimeout);
        }

        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKey">a Single primary key to check</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if record is found</returns>
        public static bool Exists<T>(this IDbConnection connection, object primaryKey, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            var key = tinfo.GetSingleKey();
            var dynParms = new DynamicParameters();
            dynParms.Add(key.PropertyName, primaryKey);
            return connection.ExecuteScalar<bool>(adapter.ExistsQuery(tinfo, null), dynParms, transaction, commandTimeout);
        }

        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if record is found</returns>
        public static bool Exists<T>(this IDbConnection connection, string sql = null, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            return connection.ExecuteScalar<bool>(adapter.ExistsQuery(tinfo, sql ?? "where 1 = 1"), null, transaction, commandTimeout);
        }

        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if record is found</returns>
        public static bool Exists<T>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            return connection.ExecuteScalar<bool>(adapter.ExistsQuery(tinfo, sql), parameters, transaction, commandTimeout);
        }
        #endregion

    }
}

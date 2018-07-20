using System;
using System.Data;
using System.Linq;
using Dapper.Mapper;

using Dapper;
using System.Threading.Tasks;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Database extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region GetAsync Queries
        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToGet">Entity to Retrieve with keys populated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity, else null</returns>
        public static async Task<T> GetAsync<T>(this IDbConnection connection, T entityToGet, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToGet == null)
                throw new ArgumentException("Cannot Get null Object", nameof(entityToGet));

            var adapter = GetFormatter(connection);

            return await connection.GetAsync<T>(adapter, null, entityToGet, transaction, commandTimeout, true);
        }

        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity, else null</returns>
        public static async Task<T> GetAsync<T>(this IDbConnection connection, object primaryKey, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            var key = tinfo.GetSingleKey("Get");
            var dynParms = new DynamicParameters();
            dynParms.Add(key.ColumnName, primaryKey);

            return await connection.GetAsync<T>(adapter, null, dynParms, transaction, commandTimeout, true);
        }

        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity, else null</returns>
        public static async Task<T> GetAsync<T>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            return await connection.GetAsync<T>(adapter, sql, parameters, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetAsync<T1, T2>(this IDbConnection connection, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return (await connection.QueryAsync<T1, T2>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetAsync<T1, T2, T3>(this IDbConnection connection, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class
        {
            return (await connection.QueryAsync<T1, T2, T3>(sql, new { }, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetAsync<T1, T2, T3>(this IDbConnection connection, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class
        {
            return (await connection.QueryAsync<T1, T2, T3>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetAsync<T1, T2, T3, T4>(this IDbConnection connection, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return (await connection.QueryAsync<T1, T2, T3, T4>(sql, new { }, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetAsync<T1, T2, T3, T4>(this IDbConnection connection, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return (await (connection.QueryAsync<T1, T2, T3, T4>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })))).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetAsync<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where TRet : class
        {
            return (await connection.QueryAsync<T1, T2, TRet>(sql, mapper, null, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetAsync<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where TRet : class
        {
            return (await connection.QueryAsync(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetAsync<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return (await connection.QueryAsync(sql, mapper, new { }, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetAsync<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return (await connection.QueryAsync(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).SingleOrDefault();
        }


        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return (await connection.QueryAsync(sql, mapper, new { }, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return (await connection.QueryAsync(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type T.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="adapter">ISqlAdapter for getting the sql statement</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="fromCache">Cache the query.</param>
        /// <returns>the entity, else null</returns>
        private static async Task<T> GetAsync<T>(this IDbConnection connection, ISqlAdapter adapter, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null, bool fromCache = false) where T : class
        {
            var type = typeof(T);
            var tinfo = TableInfoCache(type);
            var selectSql = adapter.GetQuery(tinfo, sql, fromCache);

            return (await connection.QueryAsync<T>(selectSql, parameters, transaction, commandTimeout: commandTimeout)).SingleOrDefault();
        }
        #endregion

    }
}

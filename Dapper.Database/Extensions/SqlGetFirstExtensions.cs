using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Mapper;

namespace Dapper.Database.Extensions
{
    /// <summary>
    ///     The Dapper.Database extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {
        #region GetFirst Queries

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>enumerable list of entities</returns>
        public static T GetFirst<T>(this IDbConnection connection, string sql = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return connection.QueryFirstOrDefault<T>(sqlHelper.Adapter.GetListQuery(sqlHelper.TableInfo, sql), null,
                transaction, commandTimeout);
        }

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T GetFirst<T>(this IDbConnection connection, string sql, object parameters,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return connection.QueryFirstOrDefault<T>(sqlHelper.Adapter.GetListQuery(sqlHelper.TableInfo, sql),
                parameters, transaction, commandTimeout);
        }


        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 GetFirst<T1, T2>(this IDbConnection connection, string sql, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class =>
            connection.Query<T1, T2>(sql, null, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) })).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 GetFirst<T1, T2>(this IDbConnection connection, string sql, object parameters,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class => connection.Query<T1, T2>(sql, parameters, transaction,
            commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) })).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 GetFirst<T1, T2, T3>(this IDbConnection connection, string sql, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class => connection.Query<T1, T2, T3>(sql, new { },
            transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 GetFirst<T1, T2, T3>(this IDbConnection connection, string sql, object parameters,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class => connection.Query<T1, T2, T3>(sql, parameters,
            transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })).FirstOrDefault();


        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 GetFirst<T1, T2, T3, T4>(this IDbConnection connection, string sql, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => connection.Query<T1, T2, T3, T4>(sql,
            new { }, transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 GetFirst<T1, T2, T3, T4>(this IDbConnection connection, string sql, object parameters,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => connection.Query<T1, T2, T3, T4>(sql,
            parameters, transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet GetFirst<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where TRet : class => connection.Query(sql, mapper, null, transaction,
            commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) })).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet GetFirst<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql,
            object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where TRet : class => connection.Query(sql, mapper, parameters,
                transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))
            .FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet GetFirst<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper,
            string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => connection.Query(sql, mapper,
            new { }, transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet GetFirst<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper,
            string sql, object parameters, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where TRet : class =>
            connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })).FirstOrDefault();


        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet GetFirst<T1, T2, T3, T4, TRet>(this IDbConnection connection,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class => connection
            .Query(sql, mapper, new { }, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet GetFirst<T1, T2, T3, T4, TRet>(this IDbConnection connection,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class => connection
            .Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })).FirstOrDefault();

        #endregion

        #region GetFirstAsync Queries

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>enumerable list of entities</returns>
        public static async Task<T> GetFirstAsync<T>(this IDbConnection connection, string sql = null,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return await connection.QueryFirstOrDefaultAsync<T>(
                sqlHelper.Adapter.GetListQuery(sqlHelper.TableInfo, sql), null, transaction, commandTimeout);
        }

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T> GetFirstAsync<T>(this IDbConnection connection, string sql, object parameters,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return await connection.QueryFirstOrDefaultAsync<T>(
                sqlHelper.Adapter.GetListQuery(sqlHelper.TableInfo, sql), parameters, transaction, commandTimeout);
        }

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetFirstAsync<T1, T2>(this IDbConnection connection, string sql,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class => (await connection.QueryAsync<T1, T2>(sql, null, transaction,
                commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) })))
            .FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetFirstAsync<T1, T2>(this IDbConnection connection, string sql, object parameters,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class => (await connection.QueryAsync<T1, T2>(sql, parameters, transaction,
                commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) })))
            .FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetFirstAsync<T1, T2, T3>(this IDbConnection connection, string sql,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class => (await connection.QueryAsync<T1, T2, T3>(sql, new { },
            transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetFirstAsync<T1, T2, T3>(this IDbConnection connection, string sql,
            object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class => (await connection.QueryAsync<T1, T2, T3>(sql,
            parameters, transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).FirstOrDefault();


        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetFirstAsync<T1, T2, T3, T4>(this IDbConnection connection, string sql,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class =>
            (await connection.QueryAsync<T1, T2, T3, T4>(sql, new { }, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetFirstAsync<T1, T2, T3, T4>(this IDbConnection connection, string sql,
            object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class =>
            (await connection.QueryAsync<T1, T2, T3, T4>(sql, parameters, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetFirstAsync<T1, T2, TRet>(this IDbConnection connection,
            Func<T1, T2, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class where TRet : class =>
            (await connection.QueryAsync(sql, mapper, null, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetFirstAsync<T1, T2, TRet>(this IDbConnection connection,
            Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where TRet : class => (await connection.QueryAsync(sql, mapper,
            parameters, transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(this IDbConnection connection,
            Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class where T3 : class =>
            (await connection.QueryAsync(sql, mapper, new { }, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(this IDbConnection connection,
            Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => (await connection.QueryAsync(sql,
            mapper, parameters, transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).FirstOrDefault();


        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(this IDbConnection connection,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class =>
            (await connection.QueryAsync(sql, mapper, new { }, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).FirstOrDefault();

        /// <summary>
        ///     Returns the first matching T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(this IDbConnection connection,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class =>
            (await connection.QueryAsync(sql, mapper, parameters, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).FirstOrDefault();

        #endregion
    }
}

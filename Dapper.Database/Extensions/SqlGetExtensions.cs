using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Database.Mapper;

namespace Dapper.Database.Extensions
{
    /// <summary>
    ///     The Dapper.Database extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {
        #region Get Queries

        /// <summary>
        ///     Returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToGet">Entity to Retrieve with keys populated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity, else null</returns>
        public static T Get<T>(this IDbConnection connection, T entityToGet, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class
        {
            if (entityToGet == null)
                throw new ArgumentException("Cannot Get null Object", nameof(entityToGet));

            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            var getQuery =
                sqlHelper.GenerateCompositeKeyQuery(entityToGet, (ti, sql) => sqlHelper.Adapter.GetQuery(ti, sql));
            return connection
                .Query<T>(getQuery.SqlStatement, getQuery.Parameters, transaction, commandTimeout: commandTimeout)
                .SingleOrDefault();
        }

        /// <summary>
        ///     Returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity, else null</returns>
        public static T Get<T>(this IDbConnection connection, object primaryKey, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            var getQuery =
                sqlHelper.GenerateSingleKeyQuery(primaryKey, (ti, sql) => sqlHelper.Adapter.GetQuery(ti, sql));
            return connection
                .Query<T>(getQuery.SqlStatement, getQuery.Parameters, transaction, commandTimeout: commandTimeout)
                .SingleOrDefault();
        }

        /// <summary>
        ///     Returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity, else null</returns>
        public static T Get<T>(this IDbConnection connection, string sql, object parameters,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return connection.Query<T>(sqlHelper.Adapter.GetQuery(sqlHelper.TableInfo, sql), parameters, transaction,
                commandTimeout: commandTimeout).SingleOrDefault();
        }

        /// <summary>
        ///     Returns a single entity of type 'T1'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 Get<T1, T2>(this IDbConnection connection, string sql, object parameters,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class => connection.Query<T1, T2>(sql, parameters, transaction,
                commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))
            .SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'T1'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 Get<T1, T2, T3>(this IDbConnection connection, string sql, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class => connection.Query<T1, T2, T3>(sql, new { },
            transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })).SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'T1'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 Get<T1, T2, T3>(this IDbConnection connection, string sql, object parameters,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class => connection.Query<T1, T2, T3>(sql, parameters,
            transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })).SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'T1'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 Get<T1, T2, T3, T4>(this IDbConnection connection, string sql, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => connection
            .Query<T1, T2, T3, T4>(sql, new { }, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })).SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'T1'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 Get<T1, T2, T3, T4>(this IDbConnection connection, string sql, object parameters,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => connection
            .Query<T1, T2, T3, T4>(sql, parameters, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })).SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'TRet'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet Get<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where TRet : class => connection.Query(sql, mapper, null, transaction,
                commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))
            .SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'TRet'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet Get<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql,
            object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where TRet : class => connection.Query(sql, mapper, parameters,
                transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))
            .SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'TRet'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet Get<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper,
            string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => connection.Query(sql, mapper,
            new { }, transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })).SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'TRet'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet Get<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper,
            string sql, object parameters, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where TRet : class =>
            connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })).SingleOrDefault();


        /// <summary>
        ///     Returns a single entity of type 'TRet'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet Get<T1, T2, T3, T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper,
            string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class => connection
            .Query(sql, mapper, new { }, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })).SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'TRet'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet Get<T1, T2, T3, T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper,
            string sql, object parameters, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class => connection
            .Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })).SingleOrDefault();

        #endregion

        #region GetAsync Queries

        /// <summary>
        ///     Returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToGet">Entity to Retrieve with keys populated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity, else null</returns>
        public static async Task<T> GetAsync<T>(this IDbConnection connection, T entityToGet,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToGet == null)
                throw new ArgumentException("Cannot Get null Object", nameof(entityToGet));

            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            var getQuery =
                sqlHelper.GenerateCompositeKeyQuery(entityToGet, (ti, sql) => sqlHelper.Adapter.GetQuery(ti, sql));
            return (await connection.QueryAsync<T>(getQuery.SqlStatement, getQuery.Parameters, transaction,
                commandTimeout)).SingleOrDefault();
        }

        /// <summary>
        ///     Returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity, else null</returns>
        public static async Task<T> GetAsync<T>(this IDbConnection connection, object primaryKey,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            var getQuery =
                sqlHelper.GenerateSingleKeyQuery(primaryKey, (ti, sql) => sqlHelper.Adapter.GetQuery(ti, sql));
            return (await connection.QueryAsync<T>(getQuery.SqlStatement, getQuery.Parameters, transaction,
                commandTimeout)).SingleOrDefault();
        }

        /// <summary>
        ///     Returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity, else null</returns>
        public static async Task<T> GetAsync<T>(this IDbConnection connection, string sql, object parameters,
            IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return (await connection.QueryAsync<T>(sqlHelper.Adapter.GetQuery(sqlHelper.TableInfo, sql), parameters,
                transaction, commandTimeout)).SingleOrDefault();
        }

        /// <summary>
        ///     Returns a single entity of type 'T1'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetAsync<T1, T2>(this IDbConnection connection, string sql, object parameters,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class => (await connection.QueryAsync<T1, T2>(sql, parameters, transaction,
                commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) })))
            .SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'T1'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetAsync<T1, T2, T3>(this IDbConnection connection, string sql,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class => (await connection.QueryAsync<T1, T2, T3>(sql, new { },
            transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'T1'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetAsync<T1, T2, T3>(this IDbConnection connection, string sql, object parameters,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class => (await connection.QueryAsync<T1, T2, T3>(sql,
            parameters, transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'T1'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetAsync<T1, T2, T3, T4>(this IDbConnection connection, string sql,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class =>
            (await connection.QueryAsync<T1, T2, T3, T4>(sql, new { }, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'T1'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetAsync<T1, T2, T3, T4>(this IDbConnection connection, string sql,
            object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class =>
            (await connection.QueryAsync<T1, T2, T3, T4>(sql, parameters, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'TRet'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetAsync<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper,
            string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where TRet : class => (await connection.QueryAsync(sql, mapper, null,
                transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) })))
            .SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'TRet'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetAsync<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper,
            string sql, object parameters, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class where TRet : class =>
            (await connection.QueryAsync(sql, mapper, parameters, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))).SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'TRet'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetAsync<T1, T2, T3, TRet>(this IDbConnection connection,
            Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where TRet : class =>
            (await connection.QueryAsync(sql, mapper, new { }, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'TRet'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetAsync<T1, T2, T3, TRet>(this IDbConnection connection,
            Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => (await connection.QueryAsync(sql,
            mapper, parameters, transaction, commandTimeout: commandTimeout,
            splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).SingleOrDefault();


        /// <summary>
        ///     Returns a single entity of type 'TRet'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(this IDbConnection connection,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class =>
            (await connection.QueryAsync(sql, mapper, new { }, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).SingleOrDefault();

        /// <summary>
        ///     Returns a single entity of type 'TRet'.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(this IDbConnection connection,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class =>
            (await connection.QueryAsync(sql, mapper, parameters, transaction, commandTimeout: commandTimeout,
                splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).SingleOrDefault();

        #endregion
    }
}

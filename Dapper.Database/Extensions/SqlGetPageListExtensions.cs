using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.Database.Mapper;

namespace Dapper.Database.Extensions
{
    /// <summary>
    ///     The Dapper.Database extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {
        #region GetPagedList Queries

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>enumerable list of entities</returns>
        public static IPagedEnumerable<T> GetPageList<T>(this IDbConnection connection, int page, int pageSize,
            string sql = null, IDbTransaction transaction = null, int? commandTimeout = null) where T : class =>
            connection.GetPageList<T>(page, pageSize, sql, null, transaction, commandTimeout);

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T> GetPageList<T>(this IDbConnection connection, int page, int pageSize,
            string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);

            return new PagedList<T>(
                connection.Query<T>(selectSql, selectParameters, transaction, commandTimeout: commandTimeout),
                page,
                pageSize,
                connection.Count<T>(sql, parameters, transaction, commandTimeout)
            );
        }


        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2>(this IDbConnection connection, int page, int pageSize,
            string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class => GetPageList<T1, T2>(connection, page, pageSize, sql, null, splitOn,
            transaction, commandTimeout);

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2>(this IDbConnection connection, int page, int pageSize,
            string sql, object parameters, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T1), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);

            return new PagedList<T1>(
                connection.Query<T1, T2>(selectSql, selectParameters, transaction, commandTimeout: commandTimeout,
                    splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) })),
                page,
                pageSize,
                connection.Count<T1>(sql, parameters, transaction, commandTimeout)
            );
        }

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2, T3>(this IDbConnection connection, int page,
            int pageSize, string sql, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class where T3 : class =>
            GetPageList<T1, T2, T3>(connection, page, pageSize, sql, null, splitOn, transaction, commandTimeout);

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2, T3>(this IDbConnection connection, int page,
            int pageSize, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class where T3 : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T1), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);

            return new PagedList<T1>(
                connection.Query<T1, T2, T3>(selectSql, selectParameters, transaction, commandTimeout: commandTimeout,
                    splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })),
                page,
                pageSize,
                connection.Count<T1>(sql, parameters, transaction, commandTimeout)
            );
        }


        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(this IDbConnection connection, int page,
            int pageSize, string sql, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class =>
            GetPageList<T1, T2, T3, T4>(connection, page, pageSize, sql, null, splitOn, transaction, commandTimeout);

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(this IDbConnection connection, int page,
            int pageSize, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T1), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);

            return new PagedList<T1>(
                connection.Query<T1, T2, T3, T4>(selectSql, selectParameters, transaction,
                    commandTimeout: commandTimeout,
                    splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })),
                page,
                pageSize,
                connection.Count<T1>(sql, parameters, transaction, commandTimeout)
            );
        }

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Data mapping function</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(this IDbConnection connection, int page,
            int pageSize, Func<T1, T2, TRet> mapper, string sql, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where TRet : class => GetPageList(connection, page, pageSize, mapper, sql,
            null, splitOn, transaction, commandTimeout);

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Data mapping function</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(this IDbConnection connection, int page,
            int pageSize, Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where TRet : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T1), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);

            return new PagedList<TRet>(
                connection.Query(selectSql, mapper, selectParameters, transaction, commandTimeout: commandTimeout,
                    splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) })),
                page,
                pageSize,
                connection.Count<T1>(sql, parameters, transaction, commandTimeout)
            );
        }

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(this IDbConnection connection, int page,
            int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => GetPageList(connection, page,
            pageSize, mapper, sql, null, splitOn, transaction, commandTimeout);

        /// <summary>
        ///     Returns a list entities of type TRet.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(this IDbConnection connection, int page,
            int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class
            where T2 : class
            where T3 : class
            where TRet : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T1), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);

            return new PagedList<TRet>(
                connection.Query(selectSql, mapper, selectParameters, transaction, commandTimeout: commandTimeout,
                    splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })),
                page,
                pageSize,
                connection.Count<T1>(sql, parameters, transaction, commandTimeout)
            );
        }


        /// <summary>
        ///     Returns a list entities of type TRet.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(this IDbConnection connection, int page,
            int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class =>
            GetPageList(connection, page, pageSize, mapper, sql, null, splitOn, transaction, commandTimeout);

        /// <summary>
        ///     Returns a list entities of type TRet.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(this IDbConnection connection, int page,
            int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where TRet : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T1), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);

            return new PagedList<TRet>(
                connection.Query(selectSql, mapper, selectParameters, transaction, commandTimeout: commandTimeout,
                    splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })),
                page,
                pageSize,
                connection.Count<T1>(sql, parameters, transaction, commandTimeout)
            );
        }

        #endregion

        #region GetPageListAsync Queries

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>enumerable list of entities</returns>
        public static async Task<IPagedEnumerable<T>> GetPageListAsync<T>(this IDbConnection connection, int page,
            int pageSize, string sql = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class =>
            await connection.GetPageListAsync<T>(page, pageSize, sql, null, transaction, commandTimeout);

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<IPagedEnumerable<T>> GetPageListAsync<T>(this IDbConnection connection, int page,
            int pageSize, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class =>
            await connection.GetPageListImplAsync<T>(page, pageSize, sql, parameters, transaction, commandTimeout);


        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2>(this IDbConnection connection, int page,
            int pageSize, string sql, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class => await GetPageListAsync<T1, T2>(connection,
            page, pageSize, sql, null, splitOn, transaction, commandTimeout);

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2>(this IDbConnection connection, int page,
            int pageSize, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T1), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);

            return new PagedList<T1>(
                await connection.QueryAsync<T1, T2>(selectSql, selectParameters, transaction,
                    commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) })),
                page,
                pageSize,
                await connection.CountAsync<T1>(sql, parameters, transaction, commandTimeout)
            );
        }

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3>(this IDbConnection connection,
            int page, int pageSize, string sql, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class where T3 : class =>
            await GetPageListAsync<T1, T2, T3>(connection, page, pageSize, sql, null, splitOn, transaction,
                commandTimeout);

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3>(this IDbConnection connection,
            int page, int pageSize, string sql, object parameters, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T1), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);

            return new PagedList<T1>(
                await connection.QueryAsync<T1, T2, T3>(selectSql, selectParameters, transaction,
                    commandTimeout: commandTimeout,
                    splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })),
                page,
                pageSize,
                await connection.CountAsync(sql, parameters, transaction, commandTimeout)
            );
        }


        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3, T4>(this IDbConnection connection,
            int page, int pageSize, string sql, string splitOn = null, IDbTransaction transaction = null,
            int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class =>
            await GetPageListAsync<T1, T2, T3, T4>(connection, page, pageSize, sql, null, splitOn, transaction,
                commandTimeout);

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3, T4>(this IDbConnection connection,
            int page, int pageSize, string sql, object parameters, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T1), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);

            return new PagedList<T1>(
                await connection.QueryAsync<T1, T2, T3, T4>(selectSql, selectParameters, transaction,
                    commandTimeout: commandTimeout,
                    splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })),
                page,
                pageSize,
                await connection.CountAsync(sql, parameters, transaction, commandTimeout)
            );
        }

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Data mapping function</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(this IDbConnection connection,
            int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where TRet : class => await GetPageListAsync(connection, page, pageSize,
            mapper, sql, null, splitOn, transaction, commandTimeout);

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Data mapping function</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(this IDbConnection connection,
            int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where TRet : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T1), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);

            return new PagedList<TRet>(
                await connection.QueryAsync(selectSql, mapper, selectParameters, transaction,
                    commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) })),
                page,
                pageSize,
                await connection.CountAsync<T1>(sql, parameters, transaction, commandTimeout)
            );
        }

        /// <summary>
        ///     Returns a paged list entities of type T.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(
            this IDbConnection connection, int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => await GetPageListAsync(connection,
            page, pageSize, mapper, sql, null, splitOn, transaction, commandTimeout);

        /// <summary>
        ///     Returns a list entities of type TRet.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(
            this IDbConnection connection, int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql,
            object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where TRet : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T1), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);


            return new PagedList<TRet>(
                await connection.QueryAsync(selectSql, mapper, selectParameters, transaction,
                    commandTimeout: commandTimeout,
                    splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })),
                page,
                pageSize,
                await connection.CountAsync<T1>(sql, parameters, transaction, commandTimeout)
            );
        }


        /// <summary>
        ///     Returns a list entities of type TRet.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(
            this IDbConnection connection, int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql,
            string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class =>
            await GetPageListAsync(connection, page, pageSize, mapper, sql, null, splitOn, transaction, commandTimeout);

        /// <summary>
        ///     Returns a list entities of type TRet.
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(
            this IDbConnection connection, int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql,
            object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T1), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);


            return new PagedList<TRet>(
                await connection.QueryAsync(selectSql, mapper, selectParameters, transaction,
                    commandTimeout: commandTimeout,
                    splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })),
                page,
                pageSize,
                await connection.CountAsync<T1>(sql, parameters, transaction, commandTimeout)
            );
        }

        /// <summary>
        ///     Returns a list entities of type T.
        /// </summary>
        /// <param name="connection">Sql Connection</param>
        /// <param name="page">The page to return</param>
        /// <param name="pageSize">The number of records to return per page</param>
        /// <param name="sql">The where clause</param>
        /// <param name="parameters">Parameters of the where clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>True if records are deleted</returns>
        private static async Task<IPagedEnumerable<T>> GetPageListImplAsync<T>(this IDbConnection connection, int page,
            int pageSize, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql =
                sqlHelper.Adapter.GetPageListQuery(sqlHelper.TableInfo, page, pageSize, sql, selectParameters);

            return new PagedList<T>(
                await connection.QueryAsync<T>(selectSql, selectParameters, transaction, commandTimeout),
                page,
                pageSize,
                await connection.CountAsync<T>(sql, parameters, transaction, commandTimeout)
            );
        }

        #endregion
    }
}

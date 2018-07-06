using System;
using System.Data;
using Dapper.Mapper;

using Dapper;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Database extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region GetList Queries
        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>enumerable list of entities</returns>
        public static IPagedEnumerable<T> GetPageList<T>(this IDbConnection connection, int page, int pageSize, string sql = null, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var adapter = GetFormatter(connection);
            return connection.GetPageList<T>(page, pageSize, adapter, sql ?? "where 1 = 1", null, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T> GetPageList<T>(this IDbConnection connection, int page, int pageSize, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var adapter = GetFormatter(connection);
            return connection.GetPageList<T>(page, pageSize, adapter, sql, parameters, transaction, commandTimeout);
        }


        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2>(this IDbConnection connection, int page, int pageSize, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return GetPageList<T1, T2>(connection, page, pageSize, sql, null, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2>(this IDbConnection connection, int page, int pageSize, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            var type = typeof(T1);
            var tinfo = TableInfoCache(type);
            var adapter = GetFormatter(connection);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql);
            return new PagedList<T1>(
                connection.Query<T1, T2>(selectSql, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2) })),
                page,
                    pageSize,
                    connection.Count<T1>(sql, parameters, transaction, commandTimeout: commandTimeout)
                );
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2, T3>(this IDbConnection connection, int page, int pageSize, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return GetPageList<T1, T2, T3>(connection, page, pageSize, sql, null, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2, T3>(this IDbConnection connection, int page, int pageSize, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            var type = typeof(T1);
            var tinfo = TableInfoCache(type);
            var adapter = GetFormatter(connection);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql);

            return new PagedList<T1>(
                connection.Query<T1, T2, T3>(selectSql, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) })),
                page,
                    pageSize,
                    connection.Count<T1>(sql, parameters, transaction, commandTimeout: commandTimeout)
                );
        }


        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(this IDbConnection connection, int page, int pageSize, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return GetPageList<T1, T2, T3, T4>(connection, page, pageSize, sql, null, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(this IDbConnection connection, int page, int pageSize, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            var type = typeof(T1);
            var tinfo = TableInfoCache(type);
            var adapter = GetFormatter(connection);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql);

            return new PagedList<T1>(
                connection.Query<T1, T2, T3, T4>(selectSql, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })),
                page,
                    pageSize,
                    connection.Count<T1>(sql, parameters, transaction, commandTimeout: commandTimeout)
                );
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Data mapping function</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static  IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(this IDbConnection connection, int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return  GetPageList<T1, T2, TRet>(connection, page, pageSize, mapper, sql, null, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Data mapping function</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static  IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(this IDbConnection connection, int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            var type = typeof(T1);
            var tinfo = TableInfoCache(type);
            var adapter = GetFormatter(connection);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql);

            return new PagedList<TRet>(
                connection.Query(selectSql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2) })),
                page,
                pageSize,
                connection.Count<T1>(sql, parameters, transaction, commandTimeout: commandTimeout)
            );
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static  IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(this IDbConnection connection, int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return  GetPageList<T1, T2, T3, TRet>(connection, page, pageSize, mapper, sql, null, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static  IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(this IDbConnection connection, int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            var type = typeof(T1);
            var tinfo = TableInfoCache(type);
            var adapter = GetFormatter(connection);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql);

            return new PagedList<TRet>(
                connection.Query(selectSql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) })),
                page,
                pageSize,
                connection.Count<T1>(sql, parameters, transaction, commandTimeout: commandTimeout)
            );
        }


        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static  IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(this IDbConnection connection, int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return  GetPageList<T1, T2, T3, T4, TRet>(connection, page, pageSize, mapper, sql, null, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static  IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(this IDbConnection connection, int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            var type = typeof(T1);
            var tinfo = TableInfoCache(type);
            var adapter = GetFormatter(connection);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql);

            return new PagedList<TRet>(
                connection.Query(selectSql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })),
                page,
                pageSize,
                connection.Count<T1>(sql, parameters, transaction, commandTimeout: commandTimeout)
            );
        }

        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="connection">Sql Connection</param>
        /// <param name="page">The page to return</param>
        /// <param name="pageSize">The number of records to return per page</param>
        /// <param name="adapter">ISqlAdapter for getting the sql statement</param>
        /// <param name="sql">The where clause</param>
        /// <param name="parameters">Parameters of the where clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="fromCache">Cache the query.</param>
        /// <returns>True if records are deleted</returns>
        private static IPagedEnumerable<T> GetPageList<T>(this IDbConnection connection, int page, int pageSize, ISqlAdapter adapter, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null, bool fromCache = false) where T : class
        {
            var type = typeof(T);
            var tinfo = TableInfoCache(type);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql);

            return new PagedList<T>(
                connection.Query<T>(selectSql, parameters, transaction, commandTimeout: commandTimeout),
                page,
                pageSize,
                connection.Count<T>(sql, parameters, transaction, commandTimeout: commandTimeout)
            );
        }

        #endregion


    }
}








//#region GetPage Queries
///// <summary>
///// Returns many entities of type T.  
///// </summary>
///// <param name="connection">Open SqlConnection</param>
///// <param name="page">The page requested</param>
///// <param name="pageSize">Items per page to return</param>
///// <param name="sql">The where clause to delete</param>
///// <param name="transaction">The transaction to run under, null (the default) if none</param>
///// <param name="commandTimeout">Number of seconds before command execution timeout</param>
///// <returns>enumerable list of entities</returns>
//public static IPagedEnumerable<T> GetPage<T>(this IDbConnection connection, long page, long pageSize, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
//{
//    var adapter = GetFormatter(connection);
//    return connection.GetPage<T>(adapter, page, pageSize, sql, null, transaction, commandTimeout);
//}

/////// <summary>
/////// Returns a single entity by a single id from table "Ts".  
/////// </summary>
/////// <param name="connection">Open SqlConnection</param>
/////// <param name="sql">The where clause to delete</param>
/////// <param name="parameters">The parameters of the where clause to delete</param>
/////// <param name="transaction">The transaction to run under, null (the default) if none</param>
/////// <param name="commandTimeout">Number of seconds before command execution timeout</param>
/////// <returns>true if deleted, false if not found</returns>
////public static IEnumerable<T> GetPage<T>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
////{
////    var adapter = GetFormatter(connection);
////    return connection.GetPage<T>(adapter, sql, parameters, transaction, commandTimeout);
////}


/////// <summary>
/////// Returns a single entity by a single id from table "Ts".  
/////// </summary>
/////// <param name="connection">Open SqlConnection</param>
/////// <param name="sql">The where clause to delete</param>
/////// <param name="transaction">The transaction to run under, null (the default) if none</param>
/////// <param name="commandTimeout">Number of seconds before command execution timeout</param>
/////// <returns>true if deleted, false if not found</returns>
////public static IEnumerable<T1> GetPage<T1, T2>(this IDbConnection connection, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
////{
////    return connection.Query<T1, T2>(sql, null, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2) }));
////}

/////// <summary>
/////// Returns a single entity by a single id from table "Ts".  
/////// </summary>
/////// <param name="connection">Open SqlConnection</param>
/////// <param name="sql">The where clause to delete</param>
/////// <param name="parameters">Parameters of the clause</param>
/////// <param name="transaction">The transaction to run under, null (the default) if none</param>
/////// <param name="commandTimeout">Number of seconds before command execution timeout</param>
/////// <returns>true if deleted, false if not found</returns>
////public static IEnumerable<T1> GetPage<T1, T2>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
////{
////    return connection.Query<T1, T2>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2) }));
////}

/////// <summary>
/////// Returns a single entity by a single id from table "Ts".  
/////// </summary>
/////// <param name="connection">Open SqlConnection</param>
/////// <param name="sql">The where clause to delete</param>
/////// <param name="transaction">The transaction to run under, null (the default) if none</param>
/////// <param name="commandTimeout">Number of seconds before command execution timeout</param>
/////// <returns>true if deleted, false if not found</returns>
////public static IEnumerable<T1> GetPage<T1, T2, T3>(this IDbConnection connection, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
////{
////    return connection.Query<T1, T2, T3>(sql, new { }, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) }));
////}

/////// <summary>
/////// Returns a single entity by a single id from table "Ts".  
/////// </summary>
/////// <param name="connection">Open SqlConnection</param>
/////// <param name="sql">The where clause to delete</param>
/////// <param name="parameters">Parameters of the clause</param>
/////// <param name="transaction">The transaction to run under, null (the default) if none</param>
/////// <param name="commandTimeout">Number of seconds before command execution timeout</param>
/////// <returns>true if deleted, false if not found</returns>
////public static IEnumerable<T1> GetPage<T1, T2, T3>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
////{
////    return connection.Query<T1, T2, T3>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) }));
////}


/////// <summary>
/////// Returns a single entity by a single id from table "Ts".  
/////// </summary>
/////// <param name="connection">Open SqlConnection</param>
/////// <param name="sql">The where clause to delete</param>
/////// <param name="transaction">The transaction to run under, null (the default) if none</param>
/////// <param name="commandTimeout">Number of seconds before command execution timeout</param>
/////// <returns>true if deleted, false if not found</returns>
////public static IEnumerable<T1> GetPage<T1, T2, T3, T4>(this IDbConnection connection, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
////{
////    return connection.Query<T1, T2, T3, T4>(sql, new { }, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }));
////}

/////// <summary>
/////// Returns a single entity by a single id from table "Ts".  
/////// </summary>
/////// <param name="connection">Open SqlConnection</param>
/////// <param name="sql">The where clause to delete</param>
/////// <param name="parameters">Parameters of the clause</param>
/////// <param name="transaction">The transaction to run under, null (the default) if none</param>
/////// <param name="commandTimeout">Number of seconds before command execution timeout</param>
/////// <returns>true if deleted, false if not found</returns>
////public static IEnumerable<T1> GetPage<T1, T2, T3, T4>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
////{
////    return connection.Query<T1, T2, T3, T4>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }));
////}

/////// <summary>
/////// Returns a single entity by a single id from table "Ts".  
/////// </summary>
/////// <param name="connection">Open SqlConnection</param>
/////// <param name="mapper">Open SqlConnection</param>
/////// <param name="sql">The where clause to delete</param>
/////// <param name="transaction">The transaction to run under, null (the default) if none</param>
/////// <param name="commandTimeout">Number of seconds before command execution timeout</param>
/////// <returns>true if deleted, false if not found</returns>
////public static IEnumerable<TRet> GetPage<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
////{
////    return connection.Query<T1, T2, TRet>(sql, mapper, null, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2) }));
////}

/////// <summary>
/////// Returns a single entity by a single id from table "Ts".  
/////// </summary>
/////// <param name="connection">Open SqlConnection</param>
/////// <param name="mapper">Open SqlConnection</param>
/////// <param name="sql">The where clause to delete</param>
/////// <param name="parameters">Parameters of the clause</param>
/////// <param name="transaction">The transaction to run under, null (the default) if none</param>
/////// <param name="commandTimeout">Number of seconds before command execution timeout</param>
/////// <returns>true if deleted, false if not found</returns>
////public static IEnumerable<TRet> GetPage<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
////{
////    return connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2) }));
////}

/////// <summary>
/////// Returns a single entity by a single id from table "Ts".  
/////// </summary>
/////// <param name="connection">Open SqlConnection</param>
/////// <param name="mapper">Open SqlConnection</param>
/////// <param name="sql">The where clause to delete</param>
/////// <param name="transaction">The transaction to run under, null (the default) if none</param>
/////// <param name="commandTimeout">Number of seconds before command execution timeout</param>
/////// <returns>true if deleted, false if not found</returns>
////public static IEnumerable<TRet> GetPage<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
////{
////    return connection.Query(sql, mapper, new { }, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) }));
////}

/////// <summary>
/////// Returns a single entity by a single id from table "Ts".  
/////// </summary>
/////// <param name="connection">Open SqlConnection</param>
/////// <param name="mapper">Open SqlConnection</param>
/////// <param name="sql">The where clause to delete</param>
/////// <param name="parameters">Parameters of the clause</param>
/////// <param name="transaction">The transaction to run under, null (the default) if none</param>
/////// <param name="commandTimeout">Number of seconds before command execution timeout</param>
/////// <returns>true if deleted, false if not found</returns>
////public static IEnumerable<TRet> GetPage<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
////{
////    return connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) }));
////}


/////// <summary>
/////// Returns a single entity by a single id from table "Ts".  
/////// </summary>
/////// <param name="connection">Open SqlConnection</param>
/////// <param name="mapper">Open SqlConnection</param>
/////// <param name="sql">The where clause to delete</param>
/////// <param name="transaction">The transaction to run under, null (the default) if none</param>
/////// <param name="commandTimeout">Number of seconds before command execution timeout</param>
/////// <returns>true if deleted, false if not found</returns>
////public static IEnumerable<TRet> GetPage<T1, T2, T3, T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
////{
////    return connection.Query(sql, mapper, new { }, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }));
////}

/////// <summary>
/////// Returns a single entity by a single id from table "Ts".  
/////// </summary>
/////// <param name="connection">Open SqlConnection</param>
/////// <param name="mapper">Open SqlConnection</param>
/////// <param name="sql">The where clause to delete</param>
/////// <param name="parameters">Parameters of the clause</param>
/////// <param name="transaction">The transaction to run under, null (the default) if none</param>
/////// <param name="commandTimeout">Number of seconds before command execution timeout</param>
/////// <returns>true if deleted, false if not found</returns>
////public static IEnumerable<TRet> GetPage<T1, T2, T3, T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
////{
////    return connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }));
////}

////private static string SplitOnArgument(IList<Type> types)
////{
////    return string.Join(",", types.Select(t => TableInfoCache(t).GetSingleKey("SplitOnArgument").PropertyName));
////}

///// <summary>
///// Performs a SQL Paged Get
///// </summary>
///// <param name="connection">Sql Connection</param>
///// <param name="adapter">ISqlAdapter for getting the sql statement</param>
///// <param name="page">Requested Page</param>
///// <param name="pageSize">Page size Requested</param>
///// <param name="sql">The where clause</param>
///// <param name="parameters">Parameters of the where clause</param>
///// <param name="transaction">The transaction to run under, null (the default) if none</param>
///// <param name="commandTimeout">Number of seconds before command execution timeout</param>
///// <param name="fromCache">Cache the query.</param>
///// <returns>True if records are deleted</returns>
//private static IPagedEnumerable<T> GetPage<T>(this IDbConnection connection, ISqlAdapter adapter, long page, long pageSize, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null, bool fromCache = false) where T : class
//{
//    var type = typeof(T);
//    var tinfo = TableInfoCache(type);
//    var queries = adapter.GetPageQueries(tinfo, page, pageSize, sql);

//    T obj;

//    if (type.IsInterface())
//    {
//        var result = connection.Query(queries.PageQuery, parameters, transaction, commandTimeout: commandTimeout);

//        if (result == null)
//            return null;


//        var list = new List<T>();
//        foreach (IDictionary<string, object> res in result)
//        {
//            obj = ProxyGenerator.GetInterfaceProxy<T>();
//            foreach (var property in tinfo.PropertyList)
//            {
//                var val = res[property.Name];
//                if (val == null) continue;
//                if (property.PropertyType.IsGenericType() && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
//                {
//                    var genericType = Nullable.GetUnderlyingType(property.PropertyType);
//                    if (genericType != null) property.SetValue(obj, Convert.ChangeType(val, genericType), null);
//                }
//                else
//                {
//                    property.SetValue(obj, Convert.ChangeType(val, property.PropertyType), null);
//                }
//            }

//        ((IProxy)obj).IsDirty = false;   //reset change tracking and return
//            list.Add(obj);
//        }
//        return new PagedList<T>(
//            list,
//            1,
//            33,
//            222
//        );
//    }
//    else
//    {
//        return new PagedList<T>(
//            connection.Query<T>(queries.PageQuery, parameters, transaction, commandTimeout: commandTimeout),
//            1,
//            10,
//            100
//        );

//    }
//}

//#endregion

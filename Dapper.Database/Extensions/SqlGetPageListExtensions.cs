using System;
using System.Data;
using Dapper;
using Dapper.Database.Adapters;
using Dapper.Mapper;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Database extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region GetPagedList Queries
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
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2>(this IDbConnection connection, int page, int pageSize, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return GetPageList<T1, T2>(connection, page, pageSize, sql, null, splitOn, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
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
        public static IPagedEnumerable<T1> GetPageList<T1, T2>(this IDbConnection connection, int page, int pageSize, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            var type = typeof(T1);
            var tinfo = TableInfoCache(type);
            var adapter = GetFormatter(connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql, selectParameters);
            return new PagedList<T1>(
                connection.Query<T1, T2>(selectSql, selectParameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) })),
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
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2, T3>(this IDbConnection connection, int page, int pageSize, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class
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
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2, T3>(this IDbConnection connection, int page, int pageSize, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class
        {
            var type = typeof(T1);
            var tinfo = TableInfoCache(type);
            var adapter = GetFormatter(connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql, selectParameters);

            return new PagedList<T1>(
                connection.Query<T1, T2, T3>(selectSql, selectParameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })),
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
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(this IDbConnection connection, int page, int pageSize, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class
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
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(this IDbConnection connection, int page, int pageSize, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            var type = typeof(T1);
            var tinfo = TableInfoCache(type);
            var adapter = GetFormatter(connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql, selectParameters);

            return new PagedList<T1>(
                connection.Query<T1, T2, T3, T4>(selectSql, selectParameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })),
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
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(this IDbConnection connection, int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where TRet : class
        {
            return GetPageList<T1, T2, TRet>(connection, page, pageSize, mapper, sql, null, transaction, commandTimeout);
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
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(this IDbConnection connection, int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where TRet : class
        {
            var type = typeof(T1);
            var tinfo = TableInfoCache(type);
            var adapter = GetFormatter(connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql, selectParameters);

            return new PagedList<TRet>(
                connection.Query(selectSql, mapper, selectParameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) })),
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
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(this IDbConnection connection, int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return GetPageList<T1, T2, T3, TRet>(connection, page, pageSize, mapper, sql, null, transaction, commandTimeout);
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
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(this IDbConnection connection, int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            var type = typeof(T1);
            var tinfo = TableInfoCache(type);
            var adapter = GetFormatter(connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql, selectParameters);

            return new PagedList<TRet>(
                connection.Query(selectSql, mapper, selectParameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) })),
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
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(this IDbConnection connection, int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return GetPageList<T1, T2, T3, T4, TRet>(connection, page, pageSize, mapper, sql, null, transaction, commandTimeout);
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
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(this IDbConnection connection, int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            var type = typeof(T1);
            var tinfo = TableInfoCache(type);
            var adapter = GetFormatter(connection);
            var selectParameters = new DynamicParameters(parameters);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql, selectParameters);

            return new PagedList<TRet>(
                connection.Query(selectSql, mapper, selectParameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })),
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
            var selectParameters = new DynamicParameters(parameters);
            var selectSql = adapter.GetPageListQuery(tinfo, page, pageSize, sql, selectParameters);

            return new PagedList<T>(
                connection.Query<T>(selectSql, selectParameters, transaction, commandTimeout: commandTimeout),
                page,
                pageSize,
                connection.Count<T>(sql, parameters, transaction, commandTimeout: commandTimeout)
            );
        }

        #endregion


    }
}


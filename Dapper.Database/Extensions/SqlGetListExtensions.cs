using System;
using System.Collections.Generic;
using System.Data;
using Dapper.Mapper;
using Dapper;
using Dapper.Database.Adapters;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Database extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region GetList Queries
        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>enumerable list of entities</returns>
        public static IEnumerable<T> GetList<T>(this IDbConnection connection, string sql = null, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var adapter = GetFormatter(connection);
            return connection.GetList<T>(adapter, sql ?? "where 1 = 1", null, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T> GetList<T>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var adapter = GetFormatter(connection);
            return connection.GetList<T>(adapter, sql, parameters, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T1> GetList<T1, T2>(this IDbConnection connection, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query<T1, T2>(sql, null, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T1> GetList<T1, T2>(this IDbConnection connection, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query<T1, T2>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T1> GetList<T1, T2, T3>(this IDbConnection connection, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class
        {
            return connection.Query<T1, T2, T3>(sql, new { }, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T1> GetList<T1, T2, T3>(this IDbConnection connection, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class
        {
            return connection.Query<T1, T2, T3>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }));
        }


        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T1> GetList<T1, T2, T3, T4>(this IDbConnection connection, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return connection.Query<T1, T2, T3, T4>(sql, new { }, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T1> GetList<T1, T2, T3, T4>(this IDbConnection connection, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return connection.Query<T1, T2, T3, T4>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<TRet> GetList<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where TRet : class
        {
            return connection.Query<T1, T2, TRet>(sql, mapper, null, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<TRet> GetList<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where TRet : class
        {
            return connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<TRet> GetList<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return connection.Query(sql, mapper, new { }, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<TRet> GetList<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }));
        }


        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<TRet> GetList<T1, T2, T3, T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return connection.Query(sql, mapper, new { }, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<TRet> GetList<T1, T2, T3, T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }));
        }

        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="connection">Sql Connection</param>
        /// <param name="adapter">ISqlAdapter for getting the sql statement</param>
        /// <param name="sql">The where clause</param>
        /// <param name="parameters">Parameters of the where clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="fromCache">Cache the query.</param>
        /// <returns>True if records are deleted</returns>
        private static IEnumerable<T> GetList<T>(this IDbConnection connection, ISqlAdapter adapter, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null, bool fromCache = false) where T : class
        {
            var type = typeof(T);
            var tinfo = TableInfoCache(type);
            var selectSql = adapter.GetListQuery(tinfo, sql);
            return connection.Query<T>(selectSql, parameters, transaction, commandTimeout: commandTimeout);

            //T obj;

            //if (type.IsInterface())
            //{
            //    var result = connection.Query(selectSql, parameters, transaction, commandTimeout: commandTimeout);

            //    if (result == null)
            //        return null;


            //    var list = new List<T>();
            //    foreach (IDictionary<string, object> res in result)
            //    {
            //        obj = ProxyGenerator.GetInterfaceProxy<T>();
            //        foreach (var property in tinfo.PropertyList)
            //        {
            //            var val = res[property.Name];
            //            if (val == null) continue;
            //            if (property.PropertyType.IsGenericType() && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            //            {
            //                var genericType = Nullable.GetUnderlyingType(property.PropertyType);
            //                if (genericType != null) property.SetValue(obj, Convert.ChangeType(val, genericType), null);
            //            }
            //            else
            //            {
            //                property.SetValue(obj, Convert.ChangeType(val, property.PropertyType), null);
            //            }
            //        }

            //    ((IProxy)obj).IsDirty = false;   //reset change tracking and return
            //        list.Add(obj);
            //    }
            //    return list;
            //}
            //else
            //{
            //    return connection.Query<T>(selectSql, parameters, transaction, commandTimeout: commandTimeout);
            //}
        }

        #endregion


    }
}

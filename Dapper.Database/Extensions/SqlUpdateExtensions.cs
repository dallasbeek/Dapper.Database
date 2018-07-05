using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper.Mapper;
using System.Collections.Concurrent;
using System.Reflection.Emit;

using Dapper;
using System.ComponentModel.DataAnnotations;

#if NETSTANDARD1_3
using DataException = System.InvalidOperationException;
#else
using System.Threading;
#endif



namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Contrib extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region Update Queries
        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static bool Update<T>(this IDbConnection connection, T entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return connection.Update(entityToUpdate, null, transaction, commandTimeout);
        }

        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static bool Update<T>(this IDbConnection connection, T entityToUpdate, IEnumerable<string> columnsToUpdate, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);

            return adapter.Update(connection, transaction, commandTimeout, tinfo, entityToUpdate, columnsToUpdate);

            //    if (entityToUpdate is IProxy proxy && !proxy.IsDirty)
            //{
            //    return false;
            //}

            //var type = typeof(T);
            //var isList = false;

            //if (type.IsArray)
            //{
            //    isList = true;
            //    type = type.GetElementType();
            //}
            //else if (type.IsGenericType())
            //{
            //    var typeInfo = type.GetTypeInfo();
            //    bool implementsGenericIEnumerableOrIsGenericIEnumerable =
            //        typeInfo.ImplementedInterfaces.Any(ti => ti.IsGenericType() && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
            //        typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);

            //    if (implementsGenericIEnumerableOrIsGenericIEnumerable)
            //    {
            //        isList = true;
            //        type = type.GetGenericArguments()[0];
            //    }
            //}

            //var adapter = GetFormatter(connection);
            //var tinfo = TableInfoCache(type);

            //if (!isList)
            //{
            //    return adapter.Update(connection, transaction, commandTimeout, tinfo, entityToUpdate, columnsToUpdate);
            //}
            //else
            //{
            //    return connection.Execute(adapter.UpdateQuery(tinfo, columnsToUpdate), entityToUpdate, transaction, commandTimeout) > 0;
            //}
        }

        #endregion


    }
}

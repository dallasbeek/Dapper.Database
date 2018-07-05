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

        #region Insert Queries
        /// <summary>
        /// Inserts an entity into table "Ts" and returns identity id or number of inserted rows if inserting a list.
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToInsert">Entity to insert, can be list of entities</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity to insert or the list of entities</returns>
        public static bool Insert<T>(this IDbConnection connection, T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            return adapter.Insert(connection, transaction, commandTimeout, tinfo, entityToInsert);
            //var isList = false;

            //var type = typeof(T);

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

            //if (!isList)    //single entity
            //{
            //    return adapter.Insert(connection, transaction, commandTimeout, tinfo, entityToInsert);
            //}
            //else
            //{
            //    return connection.Execute(adapter.InsertQuery(tinfo), entityToInsert, transaction, commandTimeout) > 0;
            //}
        }

        #endregion


    }
}

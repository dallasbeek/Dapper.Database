using System;
using System.Data;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Contrib extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region Delete Extensions
        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToDelete">Entity to delete. If Keys are specified, they will be used as the WHERE condition to delete.</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool Delete<T>(this IDbConnection connection, T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToDelete == null)
            {
                throw new ArgumentNullException(nameof(entityToDelete), "Cannot Delete null Object");
            }

            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            var keys = tinfo.GetCompositeKeys("Delete");
            var dynParms = new DynamicParameters();
            foreach (var key in keys)
            {
                var value = key.GetValue(entityToDelete);
                dynParms.Add(key.ColumnName, value);
            }

            var qWhere = $" where {adapter.EscapeWhereList(tinfo.KeyColumns)}";

            return connection.Execute(adapter.DeleteQuery(tinfo, qWhere), dynParms, transaction, commandTimeout) > 0;

            //var type = typeof(T);

            //if (type.IsArray)
            //{
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
            //        type = type.GetGenericArguments()[0];
            //    }
            //}

            //var adapter = GetFormatter(connection);
            //var tinfo = TableInfoCache(type);

            //return connection.Execute(adapter.DeleteQuery(tinfo, null), entityToDelete, transaction, commandTimeout) > 0;
        }

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKeyValue">a Single primary key to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool DeleteByPrimaryKey<T>(this IDbConnection connection, object primaryKeyValue, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);

            var key = tinfo.GetSingleKey("Delete");
            var dynParms = new DynamicParameters();
            dynParms.Add(key.PropertyName, primaryKeyValue);
            var qWhere = $" where {adapter.EscapeWhereList(tinfo.KeyColumns)}";

            return connection.Execute(adapter.DeleteQuery(tinfo, qWhere), dynParms, transaction, commandTimeout) > 0;
        }

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete. Cannot be null</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool DeleteByWhereClause<T>(this IDbConnection connection, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            return connection.Execute(adapter.DeleteQuery(tinfo, sql), null, transaction, commandTimeout) > 0;
        }

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool DeleteByWhereClause<T>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            return connection.Execute(adapter.DeleteQuery(tinfo, sql), parameters, transaction, commandTimeout) > 0;
        }

        /// <summary>
        /// Delete ALL entities in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool DeleteAll<T>(this IDbConnection connection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            return connection.Execute(adapter.DeleteQuery(tinfo, null), null, transaction, commandTimeout) > 0;
        }

        #endregion


    }
}

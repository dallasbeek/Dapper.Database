using System;
using System.Data;
using Dapper.Database.Adapters;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Contrib extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {
        private struct QueryWithParametersComponent
        {
            public QueryWithParametersComponent(DynamicParameters dynamicParameters, string sqlQuery) : this()
            {
                DynamicParameters = dynamicParameters;
                SqlQuery = sqlQuery;
            }

            public DynamicParameters DynamicParameters { get; }
            public string SqlQuery { get; }
        }

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

            var queryWithParameters = BuildDeleteByEntityQueryWithParams(connection, entityToDelete);

            return connection.Execute(queryWithParameters.SqlQuery, queryWithParameters.DynamicParameters, transaction, commandTimeout) > 0;
        }

        private static QueryWithParametersComponent BuildDeleteByEntityQueryWithParams<T>(IDbConnection connection, T entityToDelete) where T : class
        {
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
            return new QueryWithParametersComponent(dynParms, adapter.DeleteQuery(tinfo, qWhere));
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
            var queryWithParameters = BuildDeleteByPkQueryWithParams<T>(connection, primaryKeyValue);

            return connection.Execute(queryWithParameters.SqlQuery, queryWithParameters.DynamicParameters, transaction, commandTimeout) > 0;
        }

        private static QueryWithParametersComponent BuildDeleteByPkQueryWithParams<T>(IDbConnection connection, object primaryKeyValue) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);

            var key = tinfo.GetSingleKey("Delete");
            var dynParms = new DynamicParameters();
            dynParms.Add(key.PropertyName, primaryKeyValue);
            var qWhere = $" where {adapter.EscapeWhereList(tinfo.KeyColumns)}";

            return new QueryWithParametersComponent(dynParms, adapter.DeleteQuery(tinfo, qWhere));
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

            return connection.Execute(BuildBasicDeleteQueryFromSql<T>(connection, sql), null, transaction, commandTimeout) > 0;
        }

        private static string BuildBasicDeleteQueryFromSql<T>(IDbConnection connection, string sql)
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);

            return adapter.DeleteQuery(tinfo, sql);
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

            return connection.Execute(BuildBasicDeleteQueryFromSql<T>(connection, sql), parameters, transaction, commandTimeout) > 0;
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
            return connection.Execute(BuildBasicDeleteQueryFromSql<T>(connection, null), null, transaction, commandTimeout) > 0;
        }

        #endregion
    }
}

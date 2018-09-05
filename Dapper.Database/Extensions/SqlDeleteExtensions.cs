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
        /// Delete entity in table "Ts" that match the key values of the entity (T) passed in
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
        /// Delete entity in table "Ts" by a primary key value specified on (T)
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKeyValue">a Single primary key to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool Delete<T>(this IDbConnection connection, object primaryKeyValue, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
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
        /// Delete entity in table "Ts" by an unparameterized WHERE clause.
        /// If you want to Delete All of the data, call the DeleteAll() command
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="whereClause">The where clause to use to bound a delete, cannot be null, empty, or whitespace</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool Delete<T>(this IDbConnection connection, string whereClause, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
            {
                throw new ArgumentNullException(nameof(whereClause));
            }

            return connection.Execute(BuildBasicDeleteQueryFromSql<T>(connection, whereClause), null, transaction, commandTimeout) > 0;
        }

        private static string BuildBasicDeleteQueryFromSql<T>(IDbConnection connection, string sql)
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);

            return adapter.DeleteQuery(tinfo, sql);
        }

        /// <summary>
        /// Delete entity in table "Ts" by a parameterized WHERE clause, with Parameters passed in.
        /// If you want to Delete All of the data, call the DeleteAll() command
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="whereClause">The where clause to use to bound a delete, cannot be null, empty, or whitespace</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool Delete<T>(this IDbConnection connection, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
            {
                throw new ArgumentNullException(nameof(whereClause));
            }

            return connection.Execute(BuildBasicDeleteQueryFromSql<T>(connection, whereClause), parameters, transaction, commandTimeout) > 0;
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

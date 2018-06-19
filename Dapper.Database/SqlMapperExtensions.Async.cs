using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Dapper.Database.Extensions
{
    public static partial class SqlMapperExtensions
    {
        /// <summary>
        /// Returns a single entity by a single id from table "Ts" asynchronously using .NET 4.5 Task. T must be of interface type. 
        /// Id must be marked with [Key] attribute.
        /// Created entity is tracked/intercepted for changes and used by the Update() extension. 
        /// </summary>
        /// <typeparam name="T">Interface type to create and populate</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="id">Id of the entity to get, must be marked with [Key] attribute</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Entity of T</returns>
        public static async Task<T> GetAsync<T>(this IDbConnection connection, dynamic id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            if (!GetQueries.TryGetValue(type.TypeHandle, out string sql))
            {
                var key = GetSingleKey<T>(nameof(GetAsync));
                var name = GetTableName(type, connection);

                var sbColumnList = GetSelectColumns(connection, type);
                sql = $"SELECT {sbColumnList} FROM {name} WHERE {key.Name} = @id";
                GetQueries[type.TypeHandle] = sql;
            }

            var dynParms = new DynamicParameters();
            dynParms.Add("@id", id);

            if (!type.IsInterface())
                return (await connection.QueryAsync<T>(sql, dynParms, transaction, commandTimeout).ConfigureAwait(false)).FirstOrDefault();

            var res = (await connection.QueryAsync<dynamic>(sql, dynParms).ConfigureAwait(false)).FirstOrDefault() as IDictionary<string, object>;

            if (res == null)
                return null;

            var obj = ProxyGenerator.GetInterfaceProxy<T>();

            foreach (var property in TypePropertiesCache(type))
            {
                var val = res[property.Name];
                if (val == null) continue;
                if (property.PropertyType.IsGenericType() && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var genericType = Nullable.GetUnderlyingType(property.PropertyType);
                    if (genericType != null) property.SetValue(obj, Convert.ChangeType(val, genericType), null);
                }
                else
                {
                    property.SetValue(obj, Convert.ChangeType(val, property.PropertyType), null);
                }
            }

            ((IProxy)obj).IsDirty = false;   //reset change tracking and return

            return obj;
        }

        /// <summary>
        /// Returns a list of entites from table "Ts".  
        /// Id of T must be marked with [Key] attribute.
        /// Entities created from interfaces are tracked/intercepted for changes and used by the Update() extension
        /// for optimal performance. 
        /// </summary>
        /// <typeparam name="T">Interface or type to create and populate</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Entity of T</returns>
        public static Task<IEnumerable<T>> GetAllAsync<T>(this IDbConnection connection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var cacheType = typeof(List<T>);

            if (!GetQueries.TryGetValue(cacheType.TypeHandle, out string sql))
            {
                GetSingleKey<T>(nameof(GetAll));
                var name = GetTableName(type, connection);
                var sbColumnList = GetSelectColumns(connection, type);

                sql = $"SELECT {sbColumnList} FROM {name}";

                GetQueries[cacheType.TypeHandle] = sql;
            }

            if (!type.IsInterface())
            {
                return connection.QueryAsync<T>(sql, null, transaction, commandTimeout);
            }
            return GetAllAsyncImpl<T>(connection, transaction, commandTimeout, sql, type);
        }

        private static async Task<IEnumerable<T>> GetAllAsyncImpl<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string sql, Type type) where T : class
        {
            var result = await connection.QueryAsync(sql).ConfigureAwait(false);
            var list = new List<T>();
            foreach (IDictionary<string, object> res in result)
            {
                var obj = ProxyGenerator.GetInterfaceProxy<T>();
                foreach (var property in TypePropertiesCache(type))
                {
                    var val = res[property.Name];
                    if (val == null) continue;
                    if (property.PropertyType.IsGenericType() && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var genericType = Nullable.GetUnderlyingType(property.PropertyType);
                        if (genericType != null) property.SetValue(obj, Convert.ChangeType(val, genericType), null);
                    }
                    else
                    {
                        property.SetValue(obj, Convert.ChangeType(val, property.PropertyType), null);
                    }
                }
                ((IProxy)obj).IsDirty = false;   //reset change tracking and return
                list.Add(obj);
            }
            return list;
        }

        #region Delete Async

        //public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        //{
        //    if (entityToDelete == null)
        //        throw new ArgumentException("Cannot Delete null Object", nameof(entityToDelete));

        //    var type = typeof(T);

        //    if (type.IsArray)
        //    {
        //        type = type.GetElementType();
        //    }
        //    else if (type.IsGenericType())
        //    {
        //        var typeInfo = type.GetTypeInfo();
        //        bool implementsGenericIEnumerableOrIsGenericIEnumerable =
        //            typeInfo.ImplementedInterfaces.Any(ti => ti.IsGenericType() && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
        //            typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);

        //        if (implementsGenericIEnumerableOrIsGenericIEnumerable)
        //        {
        //            type = type.GetGenericArguments()[0];
        //        }
        //    }

        //    var keyProperties = KeyPropertiesCache(type);
        //    var explicitKeyProperties = ExplicitKeyPropertiesCache(type);
        //    if (keyProperties.Count == 0 && explicitKeyProperties.Count == 0)
        //        throw new ArgumentException("Entity must have at least one [Key] or [ExplicitKey] property");

        //    var name = GetTableName(type, connection);
        //    keyProperties.AddRange(explicitKeyProperties);

        //    var sb = new StringBuilder();
        //    sb.AppendFormat("DELETE FROM {0} WHERE ", name);

        //    for (var i = 0; i < keyProperties.Count; i++)
        //    {
        //        var property = keyProperties[i];
        //        sb.AppendFormat("{0} = @{1}", property.Name, property.Name);
        //        if (i < keyProperties.Count - 1)
        //            sb.Append(" AND ");
        //    }
        //    var deleted = await connection.ExecuteAsync(sb.ToString(), entityToDelete, transaction, commandTimeout).ConfigureAwait(false);
        //    return deleted > 0;
        //}

        //public static async Task<bool> DeleteAllAsync<T>(this IDbConnection connection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        //{
        //    var type = typeof(T);
        //    var statement = "DELETE FROM " + GetTableName(type, connection);
        //    var deleted = await connection.ExecuteAsync(statement, null, transaction, commandTimeout).ConfigureAwait(false);
        //    return deleted > 0;
        //}


        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToDelete">Entity to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToDelete == null)
                throw new ArgumentException("Cannot Delete null Object", nameof(entityToDelete));

            var type = typeof(T);

            if (type.IsArray)
            {
                type = type.GetElementType();
            }
            else if (type.IsGenericType())
            {
                var typeInfo = type.GetTypeInfo();
                bool implementsGenericIEnumerableOrIsGenericIEnumerable =
                    typeInfo.ImplementedInterfaces.Any(ti => ti.IsGenericType() && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
                    typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                if (implementsGenericIEnumerableOrIsGenericIEnumerable)
                {
                    type = type.GetGenericArguments()[0];
                }
            }

            var adapter = GetFormatter(connection);

            var tinfo = TableInfoCache(type);

            var name = tinfo.GetTableName(adapter.EscapeTableName, adapter.SupportsSchemas);
            var sbWhereClause = tinfo.GetUpdateWhere(adapter.EscapeSqlAssignment);

            if (string.IsNullOrEmpty(sbWhereClause))
                throw new ArgumentException("Entity must have at least one [Key] property");

            return await connection.DeleteAsync(name, sbWhereClause, entityToDelete, transaction, commandTimeout);
        }

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, object primaryKey, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);

            var adapter = GetFormatter(connection);

            var tinfo = TableInfoCache(type);

            var name = tinfo.GetTableName(adapter.EscapeTableName, adapter.SupportsSchemas);
            var sbWhereClause = tinfo.GetUpdateWhere(adapter.EscapeSqlAssignment);

            if (string.IsNullOrEmpty(sbWhereClause))
                throw new ArgumentException("Entity must have at least one [Key] property");

            var key = tinfo.GetSingleKey("Delete");
            var dynParms = new DynamicParameters();

            dynParms.Add(key.ColumnName, primaryKey);


            return await connection.DeleteAsync(name, sbWhereClause, dynParms, transaction, commandTimeout);
        }

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="whereClause">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);

            var adapter = GetFormatter(connection);

            var tinfo = TableInfoCache(type);

            var name = tinfo.GetTableName(adapter.EscapeTableName, adapter.SupportsSchemas);

            return await connection.DeleteAsync(name, whereClause, parameters, transaction, commandTimeout);
        }

        /// <summary>
        /// Performs and SQL Delete
        /// </summary>
        /// <param name="connection">Sql Connection</param>
        /// <param name="tableName">The name of the table to delete from</param>
        /// <param name="whereClause">The where clause</param>
        /// <param name="parameters">Parameters of the where clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>True if records are deleted</returns>
        private static async Task<bool> DeleteAsync(this IDbConnection connection, string tableName, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var sb = $"delete from {tableName} where {whereClause}";
            return await connection.ExecuteAsync(sb, parameters, transaction, commandTimeout) > 0;
        }

        /// <summary>
        /// Delete all entities in the table related to the type T.
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if none found</returns>
        public static async Task<bool> DeleteAllAsync<T>(this IDbConnection connection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            var name = tinfo.GetTableName(adapter.EscapeTableName, adapter.SupportsSchemas);
            var statement = $"delete from {name}";
            return await connection.ExecuteAsync(statement, null, transaction, commandTimeout) > 0;
        }

        #endregion

        #region Count Async
        /// <summary>
        /// Count of entity in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="whereClause">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<int> CountAsync<T>(this IDbConnection connection, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);

            var adapter = GetFormatter(connection);

            var tinfo = TableInfoCache(type);

            var tableName = tinfo.GetTableName(adapter.EscapeTableName, adapter.SupportsSchemas);

            var sb = $"select count(*) from {tableName} where {whereClause}";
            return await connection.ExecuteScalarAsync<int>(sb, parameters, transaction, commandTimeout);
        }
        #endregion

        #region Exists Async
        /// <summary>
        /// Performs a SQL Exists
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToExists">Entity to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> ExistsAsync<T>(this IDbConnection connection, T entityToExists, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToExists == null)
                throw new ArgumentException("Cannot Exists null Object", nameof(entityToExists));

            var type = typeof(T);

            var adapter = GetFormatter(connection);

            var tinfo = TableInfoCache(type);

            var name = tinfo.GetTableName(adapter.EscapeTableName, adapter.SupportsSchemas);
            var sbWhereClause = tinfo.GetUpdateWhere(adapter.EscapeSqlAssignment);

            if (string.IsNullOrEmpty(sbWhereClause))
                throw new ArgumentException("Entity must have at least one [Key] property");

            return await connection.ExistsAsync(adapter, name, sbWhereClause, entityToExists, transaction, commandTimeout);
        }

        /// <summary>
        /// Performs a SQL Exists
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> ExistsAsync<T>(this IDbConnection connection, object primaryKey, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);

            var adapter = GetFormatter(connection);

            var tinfo = TableInfoCache(type);

            var name = tinfo.GetTableName(adapter.EscapeTableName, adapter.SupportsSchemas);
            var sbWhereClause = tinfo.GetUpdateWhere(adapter.EscapeSqlAssignment);

            if (string.IsNullOrEmpty(sbWhereClause))
                throw new ArgumentException("Entity must have at least one [Key] property");

            var key = tinfo.GetSingleKey("Exists");
            var dynParms = new DynamicParameters();

            dynParms.Add(key.ColumnName, primaryKey);

            return await connection.ExistsAsync(adapter, name, sbWhereClause, dynParms, transaction, commandTimeout);
        }

        /// <summary>
        /// Performs a SQL Exists
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="whereClause">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<bool> ExistsAsync<T>(this IDbConnection connection, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);

            var adapter = GetFormatter(connection);

            var tinfo = TableInfoCache(type);

            var name = tinfo.GetTableName(adapter.EscapeTableName, adapter.SupportsSchemas);

            return await connection.ExistsAsync(adapter, name, whereClause, parameters, transaction, commandTimeout);
        }

        /// <summary>
        /// Performs a SQL Exists
        /// </summary>
        /// <param name="connection">Sql Connection</param>
        /// <param name="adapter">ISqlAdapter for getting the sql statement</param>
        /// <param name="tableName">The name of the table to delete from</param>
        /// <param name="whereClause">The where clause</param>
        /// <param name="parameters">Parameters of the where clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>True if records are deleted</returns>
        private static async Task<bool> ExistsAsync(this IDbConnection connection, ISqlAdapter adapter, string tableName, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await connection.ExecuteScalarAsync<bool>(adapter.GetExistsSql(tableName, whereClause), parameters, transaction, commandTimeout);
        }

        #endregion


        /// <summary>
        /// Inserts an entity into table "Ts" asynchronously using .NET 4.5 Task and returns identity id.
        /// </summary>
        /// <typeparam name="T">The type being inserted.</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="sqlAdapter">The specific ISqlAdapter to use, auto-detected based on connection if null</param>
        /// <returns>true if inserted</returns>
        public static Task<bool> InsertAsync<T>(this IDbConnection connection, T entityToInsert, IDbTransaction transaction = null,
            int? commandTimeout = null, ISqlAdapter sqlAdapter = null) where T : class
        {
            var type = typeof(T);
            sqlAdapter = sqlAdapter ?? GetFormatter(connection);

            var isList = false;
            if (type.IsArray)
            {
                isList = true;
                type = type.GetElementType();
            }
            else if (type.IsGenericType())
            {
                var typeInfo = type.GetTypeInfo();
                bool implementsGenericIEnumerableOrIsGenericIEnumerable =
                    typeInfo.ImplementedInterfaces.Any(ti => ti.IsGenericType() && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
                    typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                if (implementsGenericIEnumerableOrIsGenericIEnumerable)
                {
                    isList = true;
                    type = type.GetGenericArguments()[0];
                }
            }

            var name = GetTableName(type, connection);
            var sbColumnList = new StringBuilder(null);
            var allProperties = TypePropertiesCache(type);
            var keyProperties = KeyPropertiesCache(type);
            var ignoreInsertProperties = IgnoreInsertPropertiesCache(type);
            var allPropertiesExceptKeyAndComputed = allProperties.Except(keyProperties.Union(ignoreInsertProperties)).ToList();

            for (var i = 0; i < allPropertiesExceptKeyAndComputed.Count; i++)
            {
                var property = allPropertiesExceptKeyAndComputed[i];
                sqlAdapter.AppendColumnName(sbColumnList, property.Name);
                if (i < allPropertiesExceptKeyAndComputed.Count - 1)
                    sbColumnList.Append(", ");
            }

            var sbParameterList = new StringBuilder(null);
            for (var i = 0; i < allPropertiesExceptKeyAndComputed.Count; i++)
            {
                var property = allPropertiesExceptKeyAndComputed[i];
                sbParameterList.AppendFormat("@{0}", property.Name);
                if (i < allPropertiesExceptKeyAndComputed.Count - 1)
                    sbParameterList.Append(", ");
            }

            if (!isList)    //single entity
            {
                return sqlAdapter.InsertAsync(connection, transaction, commandTimeout, name, sbColumnList.ToString(),
                    sbParameterList.ToString(), keyProperties, entityToInsert);
            }

            //insert list of entities
            var cmd = $"INSERT INTO {name} ({sbColumnList}) values ({sbParameterList})";
            return connection.ExecuteAsync(cmd, entityToInsert, transaction, commandTimeout)
                .ContinueWith(t => t.Result > 0);
        }

        /// <summary>
        /// Updates entity in table "Ts" asynchronously using .NET 4.5 Task, checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static async Task<bool> UpdateAsync<T>(this IDbConnection connection, T entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return await connection.UpdateAsync(entityToUpdate, null, transaction, commandTimeout);

        }

        /// <summary>
        /// Updates entity in table "Ts" asynchronously using .NET 4.5 Task, checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static async Task<bool> UpdateAsync<T>(this IDbConnection connection, T entityToUpdate, IEnumerable<string> columnsToUpdate, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if ((entityToUpdate is IProxy proxy) && !proxy.IsDirty)
            {
                return false;
            }

            var type = typeof(T);

            if (type.IsArray)
            {
                type = type.GetElementType();
            }
            else if (type.IsGenericType())
            {
                var typeInfo = type.GetTypeInfo();
                bool implementsGenericIEnumerableOrIsGenericIEnumerable =
                    typeInfo.ImplementedInterfaces.Any(ti => ti.IsGenericType() && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
                    typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                if (implementsGenericIEnumerableOrIsGenericIEnumerable)
                {
                    type = type.GetGenericArguments()[0];
                }
            }

            var keyProperties = KeyPropertiesCache(type);
            var explicitKeyProperties = ExplicitKeyPropertiesCache(type);
            if (keyProperties.Count == 0 && explicitKeyProperties.Count == 0)
                throw new ArgumentException("Entity must have at least one [Key] or [ExplicitKey] property");

            var name = GetTableName(type, connection);

            var sb = new StringBuilder();
            sb.AppendFormat("update {0} set ", name);

            var allProperties = TypePropertiesCache(type);
            keyProperties.AddRange(explicitKeyProperties);
            var ignoreUpdateProperties = IgnoreUpdatePropertiesCache(type);
            var nonIdProps = allProperties.Except(keyProperties.Union(ignoreUpdateProperties)).ToList();

            if (columnsToUpdate != null && columnsToUpdate.Any())
            {
                nonIdProps = nonIdProps.Where(np => columnsToUpdate.Contains(np.Name)).ToList();
            }

            var adapter = GetFormatter(connection);

            for (var i = 0; i < nonIdProps.Count; i++)
            {
                var property = nonIdProps[i];
                adapter.AppendColumnNameEqualsValue(sb, property.Name);
                if (i < nonIdProps.Count - 1)
                    sb.Append(", ");
            }
            sb.Append(" where ");
            for (var i = 0; i < keyProperties.Count; i++)
            {
                var property = keyProperties[i];
                adapter.AppendColumnNameEqualsValue(sb, property.Name);
                if (i < keyProperties.Count - 1)
                    sb.Append(" and ");
            }
            var updated = await connection.ExecuteAsync(sb.ToString(), entityToUpdate, commandTimeout: commandTimeout, transaction: transaction).ConfigureAwait(false);
            return updated > 0;
        }

    }
}

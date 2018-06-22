using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.Concurrent;
using System.Reflection.Emit;

using Dapper;
using Dapper.Database;

#if NETSTANDARD1_3
using DataException = System.InvalidOperationException;
#else
using System.Threading;
#endif

#if NET451
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endif


namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Contrib extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {
        /// <summary>
        /// Defined a proxy object with a possibly dirty state.
        /// </summary>
        public interface IProxy //must be kept public
        {
            /// <summary>
            /// Whether the object has been changed.
            /// </summary>
            bool IsDirty { get; set; }
        }

        /// <summary>
        /// Defines a table name mapper for getting table names from types.
        /// </summary>
        public interface ITableNameMapper
        {
            /// <summary>
            /// Gets a table name from a given <see cref="Type"/>.
            /// </summary>
            /// <param name="type">The <see cref="Type"/> to get a name from.</param>
            /// <returns>The table name for the given <paramref name="type"/>.</returns>
            string GetTableName(Type type);
        }

        /// <summary>
        /// The function to get a database type from the given <see cref="IDbConnection"/>.
        /// </summary>
        /// <param name="connection">The connection to get a database type name from.</param>
        public delegate string GetDatabaseTypeDelegate(IDbConnection connection);
        /// <summary>
        /// The function to get a a table name from a given <see cref="Type"/>
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to get a table name for.</param>
        public delegate string TableNameMapperDelegate(Type type);

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, TableInfo> TableInfos = new ConcurrentDictionary<RuntimeTypeHandle, TableInfo>();

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> KeyProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ExplicitKeyProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> IgnoreInsertProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> IgnoreUpdateProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> IgnoreSelectProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> GetQueries = new ConcurrentDictionary<RuntimeTypeHandle, string>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeTableName = new ConcurrentDictionary<RuntimeTypeHandle, string>();

        private static readonly ISqlAdapter DefaultAdapter = new SqlServerAdapter();
        private static readonly Dictionary<string, ISqlAdapter> AdapterDictionary
            = new Dictionary<string, ISqlAdapter>
            {
                ["sqlconnection"] = new SqlServerAdapter(),
                ["sqlceconnection"] = new SqlCeServerAdapter(),
                ["npgsqlconnection"] = new PostgresAdapter(),
                ["sqliteconnection"] = new SQLiteAdapter(),
                ["mysqlconnection"] = new MySqlAdapter(),
                ["fbconnection"] = new FbAdapter()
            };


        private static TableInfo TableInfoCache(Type type)
        {
            if (TableInfos.TryGetValue(type.TypeHandle, out TableInfo ti))
            {
                return ti;
            }

            var tInfo = new TableInfo(type, TableNameMapper);
            TableInfos[type.TypeHandle] = tInfo;
            return tInfo;
        }

        private static List<PropertyInfo> IgnoreInsertPropertiesCache(Type type)
        {
            if (IgnoreInsertProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pi))
            {
                return pi.ToList();
            }

            var ignoreInsertProperties = TypePropertiesCache(type).Where(p =>
                p.GetCustomAttributes(true).Any(a =>
                    a is IgnoreInsertAttribute
                    || a is ReadOnlyAttribute
                    || (a is DatabaseGeneratedAttribute && (a as DatabaseGeneratedAttribute).DatabaseGeneratedOption == DatabaseGeneratedOption.Computed)
                )
            ).ToList();

            TypePropertiesCache(type).Where(p => p.GetCustomAttributes(true).Any(a => a is DatabaseGeneratedAttribute && (a as DatabaseGeneratedAttribute).DatabaseGeneratedOption == DatabaseGeneratedOption.Computed)).ToList();

            IgnoreInsertProperties[type.TypeHandle] = ignoreInsertProperties;
            return ignoreInsertProperties;
        }

        private static List<PropertyInfo> IgnoreUpdatePropertiesCache(Type type)
        {
            if (IgnoreUpdateProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pi))
            {
                return pi.ToList();
            }

            var ignoreUpdateProperties = TypePropertiesCache(type).Where(p =>
                p.GetCustomAttributes(true).Any(a =>
                    a is IgnoreUpdateAttribute
                    || a is ReadOnlyAttribute
                    || (a is DatabaseGeneratedAttribute && (a as DatabaseGeneratedAttribute).DatabaseGeneratedOption == DatabaseGeneratedOption.Computed)
                )
            ).ToList();

            TypePropertiesCache(type).Where(p => p.GetCustomAttributes(true).Any(a => a is DatabaseGeneratedAttribute && (a as DatabaseGeneratedAttribute).DatabaseGeneratedOption == DatabaseGeneratedOption.Computed)).ToList();

            IgnoreUpdateProperties[type.TypeHandle] = ignoreUpdateProperties;
            return ignoreUpdateProperties;
        }

        private static List<PropertyInfo> IgnoreSelectPropertiesCache(Type type)
        {
            if (IgnoreSelectProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pi))
            {
                return pi.ToList();
            }

            var ignoreSelectProperties = TypePropertiesCache(type).Where(p =>
                p.GetCustomAttributes(true).Any(a =>
                    a is IgnoreSelectAttribute
               )
            ).ToList();

            TypePropertiesCache(type).Where(p => p.GetCustomAttributes(true).Any(a => a is DatabaseGeneratedAttribute && (a as DatabaseGeneratedAttribute).DatabaseGeneratedOption == DatabaseGeneratedOption.Computed)).ToList();

            IgnoreSelectProperties[type.TypeHandle] = ignoreSelectProperties;
            return ignoreSelectProperties;
        }

        private static List<PropertyInfo> ExplicitKeyPropertiesCache(Type type)
        {
            if (ExplicitKeyProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pi))
            {
                return pi.ToList();
            }

            var explicitKeyProperties = TypePropertiesCache(type).Where(p =>
                p.GetCustomAttributes(true).Any(a => a is KeyAttribute)
                    && !p.GetCustomAttributes(true).Any(a => a is DatabaseGeneratedAttribute
                    && (a as DatabaseGeneratedAttribute).DatabaseGeneratedOption == DatabaseGeneratedOption.Identity)
            ).ToList();

            ExplicitKeyProperties[type.TypeHandle] = explicitKeyProperties;
            return explicitKeyProperties;
        }

        private static List<PropertyInfo> KeyPropertiesCache(Type type)
        {
            if (KeyProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pi))
            {
                return pi.ToList();
            }

            var allProperties = TypePropertiesCache(type);

            var keyProperties = allProperties.Where(p =>
                p.GetCustomAttributes(true).Any(a => a is KeyAttribute)
                    && p.GetCustomAttributes(true).Any(a => a is DatabaseGeneratedAttribute
                    && (a as DatabaseGeneratedAttribute).DatabaseGeneratedOption == DatabaseGeneratedOption.Identity)
                ).ToList();

            if (keyProperties.Count == 0)
            {
                var idProp = allProperties.Find(p => string.Equals(p.Name, "id", StringComparison.CurrentCultureIgnoreCase));
                if (idProp != null && !idProp.GetCustomAttributes(true).Any(a => a is KeyAttribute))
                {
                    keyProperties.Add(idProp);
                }
            }

            KeyProperties[type.TypeHandle] = keyProperties;
            return keyProperties;
        }

        private static List<PropertyInfo> TypePropertiesCache(Type type)
        {
            if (TypeProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pis))
            {
                return pis.ToList();
            }

            var properties = type.GetProperties().Where(IsWriteable).ToArray();
            TypeProperties[type.TypeHandle] = properties;
            return properties.ToList();
        }

        private static bool IsWriteable(PropertyInfo pi)
        {
            var attributes = pi.GetCustomAttributes(typeof(IgnoreAttribute), false).AsList();
            if (attributes.Count != 1) return true;

            return false;
        }

        private static PropertyInfo GetSingleKey<T>(string method)
        {
            return GetSingleKey(typeof(T), method);
        }

        private static PropertyInfo GetSingleKey(Type type, string method)
        {
            var keys = KeyPropertiesCache(type);
            var explicitKeys = ExplicitKeyPropertiesCache(type);
            var keyCount = keys.Count + explicitKeys.Count;
            if (keyCount > 1)
                throw new DataException($"{method}<T> only supports an entity with a single [Key] or [ExplicitKey] property");
            if (keyCount == 0)
                throw new DataException($"{method}<T> only supports an entity with a [Key] or an [ExplicitKey] property");

            return keys.Count > 0 ? keys[0] : explicitKeys[0];
        }

        private static string GetSelectColumns(IDbConnection connection, Type type)
        {
            var sbColumnList = new StringBuilder(null);
            var allProperties = TypePropertiesCache(type);
            var ignoreSelectProperties = IgnoreSelectPropertiesCache(type);
            var allPropertiesExceptIgnored = allProperties.Except(ignoreSelectProperties).ToList();

            var adapter = GetFormatter(connection);

            for (var i = 0; i < allPropertiesExceptIgnored.Count; i++)
            {
                var property = allPropertiesExceptIgnored[i];
                adapter.AppendColumnName(sbColumnList, property.Name);
                if (i < allPropertiesExceptIgnored.Count - 1)
                    sbColumnList.Append(", ");
            }
            return sbColumnList.ToString();
        }

        /// <summary>
        /// Returns true if entity exists  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToCheck">Entity to check</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if found else false</returns>
        public static bool Exists(this IDbConnection connection, object entityToCheck, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var type = entityToCheck.GetType();

            var name = GetTableName(type, connection);

            var adapter = GetFormatter(connection);

            var keyProperties = KeyPropertiesCache(type);
            var explicitKeyProperties = ExplicitKeyPropertiesCache(type);
            if (keyProperties.Count == 0 && explicitKeyProperties.Count == 0)
                throw new ArgumentException("Entity must have at least one [Key] or [ExplicitKey] property");

            keyProperties.AddRange(explicitKeyProperties);

            var sb = new StringBuilder();
            for (var i = 0; i < keyProperties.Count; i++)
            {
                var property = keyProperties[i];
                adapter.AppendColumnNameEqualsValue(sb, property.Name);
                if (i < keyProperties.Count - 1)
                    sb.Append(" AND ");
            }

            var sql = string.Format(adapter.GetExistsSql(name, sb.ToString()));

            return connection.ExecuteScalar<int>(sql, entityToCheck, transaction, commandTimeout: commandTimeout) != 0;
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
        public static IEnumerable<T> GetAll<T>(this IDbConnection connection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var cacheType = typeof(List<T>);

            if (!GetQueries.TryGetValue(cacheType.TypeHandle, out string sql))
            {
                GetSingleKey<T>(nameof(GetAll));
                var name = GetTableName(type, connection);

                var sbColumnList = GetSelectColumns(connection, type);

                sql = $"select {sbColumnList} from {name}";
                GetQueries[cacheType.TypeHandle] = sql;
            }

            if (!type.IsInterface()) return connection.Query<T>(sql, null, transaction, commandTimeout: commandTimeout);

            var result = connection.Query(sql);
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

        /// <summary>
        /// Specify a custom table name mapper based on the POCO type name
        /// </summary>
        public static TableNameMapperDelegate TableNameMapper;

        private static string GetTableName(Type type, IDbConnection connection)
        {
            var adapter = GetFormatter(connection);

            if (TypeTableName.TryGetValue(type.TypeHandle, out string name)) return name;

            string schema = null;

            if (TableNameMapper != null)
            {
                name = TableNameMapper(type);
            }
            else
            {
                //NOTE: This as dynamic trick should be able to handle both our own Table-attribute as well as the one in EntityFramework 
                var tableAttr = type
#if NETSTANDARD1_3
                    .GetTypeInfo()
#endif
                    .GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic;
                if (tableAttr != null)
                {
                    name = tableAttr.Name;
                    if (tableAttr.Schema != null)
                    {
                        schema = tableAttr.Schema;
                    }
                }
                else
                {
                    name = type.Name + "s";
                    if (type.IsInterface() && name.StartsWith("I"))
                        name = name.Substring(1);
                }
            }

            var formatted = adapter.FormatSchemaTable(name, schema);
            TypeTableName[type.TypeHandle] = formatted;
            return formatted;
        }

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
            var isList = false;

            var type = typeof(T);

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

            var adapter = GetFormatter(connection);

            var tinfo = TableInfoCache(type);


            if (!isList)    //single entity
            {
                return adapter.Insert(connection, transaction, commandTimeout,tinfo, entityToInsert);
            }
            else
            {
                var name = tinfo.GetTableName(adapter.EscapeTableName, adapter.SupportsSchemas);
                var sbColumnList = tinfo.GetInsertColumns(adapter.EscapeSqlIdentifier);
                var sbParameterList = tinfo.GetInsertParameters();

                //insert list of entities
                var cmd = $"insert into {name} ({sbColumnList}) values ({sbParameterList})";
                return connection.Execute(cmd, entityToInsert, transaction, commandTimeout) > 0;
            }
        }

        /// <summary>
        /// Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
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
        /// Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
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
            if (entityToUpdate is IProxy proxy && !proxy.IsDirty)
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

            var adapter = GetFormatter(connection);

            var tinfo = TableInfoCache(type);

            var name = tinfo.GetTableName(adapter.EscapeTableName, adapter.SupportsSchemas);
            var sbColumnList = tinfo.GetUpdateValues(adapter.EscapeSqlAssignment, columnsToUpdate);
            var sbWhereClause = tinfo.GetUpdateWhere(adapter.EscapeSqlAssignment);

            if (string.IsNullOrEmpty(sbWhereClause))
                throw new ArgumentException("Entity must have at least one [Key] property");

            var sb = $"update {name} set {sbColumnList} where {sbWhereClause}";

            return connection.Execute(sb, entityToUpdate, commandTimeout: commandTimeout, transaction: transaction) > 0;
        }

        /// <summary>
        /// Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToUpsert">Entity to be inserted or updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updatinRg</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public static bool Upsert<T>(this IDbConnection connection, T entityToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (!connection.Exists(entityToUpsert, transaction, commandTimeout))
            {
                insertAction?.Invoke(entityToUpsert);
                return connection.Insert(entityToUpsert, transaction, commandTimeout);
            }
            else
            {
                updateAction?.Invoke(entityToUpsert);
                return connection.Update(entityToUpsert, columnsToUpdate, transaction, commandTimeout);
            }
        }

        #region Delete Extensions
        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToDelete">Entity to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool Delete<T>(this IDbConnection connection, T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
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

            return connection.Delete<T>(adapter, null, entityToDelete, transaction, commandTimeout);
        }

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool Delete<T>(this IDbConnection connection, object primaryKey, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);

            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);

            var key = tinfo.GetSingleKey("Delete");
            var dynParms = new DynamicParameters();
            dynParms.Add(key.ColumnName, primaryKey);

            return connection.Delete<T>(adapter, null, dynParms, transaction, commandTimeout);
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
        public static bool Delete<T>(this IDbConnection connection, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            return connection.Delete<T>(adapter, whereClause, parameters, transaction, commandTimeout);
        }

        /// <summary>
        /// Performs a SQL Get
        /// </summary>
        /// <param name="connection">Sql Connection</param>
        /// <param name="adapter">ISqlAdapter for getting the sql statement</param>
        /// <param name="whereClause">The where clause</param>
        /// <param name="parameters">Parameters of the where clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>True if records are deleted</returns>
        private static bool Delete<T>(this IDbConnection connection, ISqlAdapter adapter, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var tinfo = TableInfoCache(type);
            var sql = adapter.GetQuery(tinfo, whereClause);
            return connection.Execute(adapter.DeleteQuery(tinfo, whereClause), parameters, transaction, commandTimeout) > 0;
        }


        /// <summary>
        /// Delete all entities in the table related to the type T.
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if none found</returns>
        public static bool DeleteAll<T>(this IDbConnection connection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            var name = tinfo.GetTableName(adapter.EscapeTableName, adapter.SupportsSchemas);
            var statement = $"delete from {name}";
            return connection.Execute(statement, null, transaction, commandTimeout) > 0;
        }
        #endregion

        #region Count Extensions
        /// <summary>
        /// Count of entity in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="whereClause">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static int Count<T>(this IDbConnection connection, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            return connection.ExecuteScalar<int>(adapter.CountQuery(tinfo, whereClause), parameters, transaction, commandTimeout);
        }
        #endregion

        #region Exists Extensions
        /// <summary>
        /// Performs a SQL Exists
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToExists">Entity to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool Exists<T>(this IDbConnection connection, T entityToExists, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToExists == null)
                throw new ArgumentException("Cannot Exists null Object", nameof(entityToExists));

            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            return connection.ExecuteScalar<bool>(adapter.ExistsQuery(tinfo, null), entityToExists, transaction, commandTimeout);
        }

        /// <summary>
        /// Performs a SQL Exists
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static bool Exists<T>(this IDbConnection connection, object primaryKey, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            var key = tinfo.GetSingleKey("Exists");
            var dynParms = new DynamicParameters();
            dynParms.Add(key.ColumnName, primaryKey);
            return connection.ExecuteScalar<bool>(adapter.ExistsQuery(tinfo, null), dynParms, transaction, commandTimeout);
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
        public static bool Exists<T>(this IDbConnection connection, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            return connection.ExecuteScalar<bool>(adapter.ExistsQuery(tinfo, whereClause), parameters, transaction, commandTimeout);
        }

        #endregion

        #region Get Extensions
        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToGet">Entity to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T Get<T>(this IDbConnection connection, T entityToGet, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToGet == null)
                throw new ArgumentException("Cannot Get null Object", nameof(entityToGet));

            var adapter = GetFormatter(connection);

            return connection.Get<T>(adapter, null, entityToGet, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T Get<T>(this IDbConnection connection, object primaryKey, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            var name = tinfo.GetTableName(adapter.EscapeTableName, adapter.SupportsSchemas);

            var key = tinfo.GetSingleKey("Get");
            var dynParms = new DynamicParameters();
            dynParms.Add(key.ColumnName, primaryKey);

            return connection.Get<T>(adapter, null, dynParms, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="whereClause">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T Get<T>(this IDbConnection connection, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            return connection.Get<T>(adapter,  whereClause, parameters, transaction, commandTimeout);
        }

        /// <summary>
        /// Performs a SQL Get
        /// </summary>
        /// <param name="connection">Sql Connection</param>
        /// <param name="adapter">ISqlAdapter for getting the sql statement</param>
        /// <param name="whereClause">The where clause</param>
        /// <param name="parameters">Parameters of the where clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>True if records are deleted</returns>
        private static T Get<T>(this IDbConnection connection, ISqlAdapter adapter, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var tinfo = TableInfoCache(type);
            var sql = adapter.GetQuery(tinfo, whereClause);

            T obj;

            if (type.IsInterface())
            {
                var res = connection.Query(sql, parameters, transaction, commandTimeout: commandTimeout).SingleOrDefault() as IDictionary<string, object>;

                if (res == null)
                    return null;

                obj = ProxyGenerator.GetInterfaceProxy<T>();

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
            }
            else
            {
                obj = connection.Query<T>(sql, parameters, transaction, commandTimeout: commandTimeout).SingleOrDefault();
            }
            return obj;
        }

          //public static T Get<T>(this IDbConnection connection, dynamic id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        //{
        //    var type = typeof(T);

        //    if (!GetQueries.TryGetValue(type.TypeHandle, out string sql))
        //    {
        //        var key = GetSingleKey<T>(nameof(Get));
        //        var name = GetTableName(type, connection);

        //        var sbColumnList = GetSelectColumns(connection, type);

        //        sql = $"select {sbColumnList} from {name} where {key.Name} = @id";
        //        GetQueries[type.TypeHandle] = sql;
        //    }

        //    var dynParms = new DynamicParameters();
        //    dynParms.Add("@id", id);

        //    T obj;

        //    if (type.IsInterface())
        //    {
        //        var res = connection.Query(sql, dynParms).FirstOrDefault() as IDictionary<string, object>;

        //        if (res == null)
        //            return null;

        //        obj = ProxyGenerator.GetInterfaceProxy<T>();

        //        foreach (var property in TypePropertiesCache(type))
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

        //        ((IProxy)obj).IsDirty = false;   //reset change tracking and return
        //    }
        //    else
        //    {
        //        obj = connection.Query<T>(sql, dynParms, transaction, commandTimeout: commandTimeout).FirstOrDefault();
        //    }
        //    return obj;
        //}
        #endregion

        /// <summary>
        /// Specifies a custom callback that detects the database type instead of relying on the default strategy (the name of the connection type object).
        /// Please note that this callback is global and will be used by all the calls that require a database specific adapter.
        /// </summary>
        public static GetDatabaseTypeDelegate GetDatabaseType;

        private static ISqlAdapter GetFormatter(IDbConnection connection)
        {
            var name = GetDatabaseType?.Invoke(connection).ToLower()
                       ?? connection.GetType().Name.ToLower();

            return !AdapterDictionary.ContainsKey(name)
                ? DefaultAdapter
                : AdapterDictionary[name];
        }

        private static class ProxyGenerator
        {
            private static readonly Dictionary<Type, Type> TypeCache = new Dictionary<Type, Type>();

            private static AssemblyBuilder GetAsmBuilder(string name)
            {
#if !NET451
                return AssemblyBuilder.DefineDynamicAssembly(new AssemblyName { Name = name }, AssemblyBuilderAccess.Run);
#else
                return Thread.GetDomain().DefineDynamicAssembly(new AssemblyName { Name = name }, AssemblyBuilderAccess.Run);
#endif
            }

            public static T GetInterfaceProxy<T>()
            {
                Type typeOfT = typeof(T);

                if (TypeCache.TryGetValue(typeOfT, out Type k))
                {
                    return (T)Activator.CreateInstance(k);
                }
                var assemblyBuilder = GetAsmBuilder(typeOfT.Name);

                var moduleBuilder = assemblyBuilder.DefineDynamicModule("SqlMapperExtensions." + typeOfT.Name); //NOTE: to save, add "asdasd.dll" parameter

                var interfaceType = typeof(IProxy);
                var typeBuilder = moduleBuilder.DefineType(typeOfT.Name + "_" + Guid.NewGuid(),
                    TypeAttributes.Public | TypeAttributes.Class);
                typeBuilder.AddInterfaceImplementation(typeOfT);
                typeBuilder.AddInterfaceImplementation(interfaceType);

                //create our _isDirty field, which implements IProxy
                var setIsDirtyMethod = CreateIsDirtyProperty(typeBuilder);

                // Generate a field for each property, which implements the T
                foreach (var property in typeof(T).GetProperties())
                {
                    var isId = property.GetCustomAttributes(true).Any(a => a is KeyAttribute);
                    CreateProperty<T>(typeBuilder, property.Name, property.PropertyType, setIsDirtyMethod, isId);
                }

#if !NET451
                var generatedType = typeBuilder.CreateTypeInfo().AsType();
#else
                var generatedType = typeBuilder.CreateType();
#endif

                TypeCache.Add(typeOfT, generatedType);
                return (T)Activator.CreateInstance(generatedType);
            }

            private static MethodInfo CreateIsDirtyProperty(TypeBuilder typeBuilder)
            {
                var propType = typeof(bool);
                var field = typeBuilder.DefineField("_" + nameof(IProxy.IsDirty), propType, FieldAttributes.Private);
                var property = typeBuilder.DefineProperty(nameof(IProxy.IsDirty),
                                               System.Reflection.PropertyAttributes.None,
                                               propType,
                                               new[] { propType });

                const MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.NewSlot | MethodAttributes.SpecialName
                                                  | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig;

                // Define the "get" and "set" accessor methods
                var currGetPropMthdBldr = typeBuilder.DefineMethod("get_" + nameof(IProxy.IsDirty),
                                             getSetAttr,
                                             propType,
                                             Type.EmptyTypes);
                var currGetIl = currGetPropMthdBldr.GetILGenerator();
                currGetIl.Emit(OpCodes.Ldarg_0);
                currGetIl.Emit(OpCodes.Ldfld, field);
                currGetIl.Emit(OpCodes.Ret);
                var currSetPropMthdBldr = typeBuilder.DefineMethod("set_" + nameof(IProxy.IsDirty),
                                             getSetAttr,
                                             null,
                                             new[] { propType });
                var currSetIl = currSetPropMthdBldr.GetILGenerator();
                currSetIl.Emit(OpCodes.Ldarg_0);
                currSetIl.Emit(OpCodes.Ldarg_1);
                currSetIl.Emit(OpCodes.Stfld, field);
                currSetIl.Emit(OpCodes.Ret);

                property.SetGetMethod(currGetPropMthdBldr);
                property.SetSetMethod(currSetPropMthdBldr);
                var getMethod = typeof(IProxy).GetMethod("get_" + nameof(IProxy.IsDirty));
                var setMethod = typeof(IProxy).GetMethod("set_" + nameof(IProxy.IsDirty));
                typeBuilder.DefineMethodOverride(currGetPropMthdBldr, getMethod);
                typeBuilder.DefineMethodOverride(currSetPropMthdBldr, setMethod);

                return currSetPropMthdBldr;
            }

            private static void CreateProperty<T>(TypeBuilder typeBuilder, string propertyName, Type propType, MethodInfo setIsDirtyMethod, bool isIdentity)
            {
                //Define the field and the property 
                var field = typeBuilder.DefineField("_" + propertyName, propType, FieldAttributes.Private);
                var property = typeBuilder.DefineProperty(propertyName,
                                               System.Reflection.PropertyAttributes.None,
                                               propType,
                                               new[] { propType });

                const MethodAttributes getSetAttr = MethodAttributes.Public
                                                    | MethodAttributes.Virtual
                                                    | MethodAttributes.HideBySig;

                // Define the "get" and "set" accessor methods
                var currGetPropMthdBldr = typeBuilder.DefineMethod("get_" + propertyName,
                                             getSetAttr,
                                             propType,
                                             Type.EmptyTypes);

                var currGetIl = currGetPropMthdBldr.GetILGenerator();
                currGetIl.Emit(OpCodes.Ldarg_0);
                currGetIl.Emit(OpCodes.Ldfld, field);
                currGetIl.Emit(OpCodes.Ret);

                var currSetPropMthdBldr = typeBuilder.DefineMethod("set_" + propertyName,
                                             getSetAttr,
                                             null,
                                             new[] { propType });

                //store value in private field and set the isdirty flag
                var currSetIl = currSetPropMthdBldr.GetILGenerator();
                currSetIl.Emit(OpCodes.Ldarg_0);
                currSetIl.Emit(OpCodes.Ldarg_1);
                currSetIl.Emit(OpCodes.Stfld, field);
                currSetIl.Emit(OpCodes.Ldarg_0);
                currSetIl.Emit(OpCodes.Ldc_I4_1);
                currSetIl.Emit(OpCodes.Call, setIsDirtyMethod);
                currSetIl.Emit(OpCodes.Ret);

                //TODO: Should copy all attributes defined by the interface?
                if (isIdentity)
                {
                    var keyAttribute = typeof(KeyAttribute);
                    var myConstructorInfo = keyAttribute.GetConstructor(new Type[] { });
                    var attributeBuilder = new CustomAttributeBuilder(myConstructorInfo, new object[] { });
                    property.SetCustomAttribute(attributeBuilder);
                }

                property.SetGetMethod(currGetPropMthdBldr);
                property.SetSetMethod(currSetPropMthdBldr);
                var getMethod = typeof(T).GetMethod("get_" + propertyName);
                var setMethod = typeof(T).GetMethod("set_" + propertyName);
                typeBuilder.DefineMethodOverride(currGetPropMthdBldr, getMethod);
                typeBuilder.DefineMethodOverride(currSetPropMthdBldr, setMethod);
            }
        }
    }
}

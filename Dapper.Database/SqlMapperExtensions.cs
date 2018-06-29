using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper.Mapper;
using System.Text;
using System.Collections.Concurrent;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

using Dapper;
using Dapper.Database;
using Dapper.Database.Attributes;

#if NETSTANDARD1_3
using DataException = System.InvalidOperationException;
#else
using System.Threading;
#endif


#if NETSTANDARD1_3 || NETSTANDARD2_0
using Dapper.Database.Attributes.Schema;
#else
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

        /// <summary>
        /// Specify a custom table name mapper based on the POCO type name
        /// </summary>
        public static TableNameMapperDelegate TableNameMapper;

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
                return adapter.Insert(connection, transaction, commandTimeout, tinfo, entityToInsert);
            }
            else
            {
                return connection.Execute(adapter.InsertQuery(tinfo), entityToInsert, transaction, commandTimeout) > 0;
            }
        }

        #endregion

        #region Update Queries
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

            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);

            if (!isList)
            {
                return adapter.Update(connection, transaction, commandTimeout, tinfo, entityToUpdate, columnsToUpdate);
            }
            else
            {
                return connection.Execute(adapter.UpdateQuery(tinfo, columnsToUpdate), entityToUpdate, transaction, commandTimeout) > 0;
            }
        }

        #endregion

        #region Upsert Queries
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
        #endregion


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

            return connection.Execute(adapter.DeleteQuery(tinfo, null), entityToDelete, transaction, commandTimeout) > 0;
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

            return connection.Execute(adapter.DeleteQuery(tinfo, null), dynParms, transaction, commandTimeout) > 0;
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
            return connection.Delete<T>("1 = 1", null, transaction, commandTimeout);
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


        /// <summary>
        /// Count of entity in table "Ts".
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static int CountAll<T>(this IDbConnection connection, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return connection.Count<T>("1 = 1", null, transaction, commandTimeout);
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

        #region Get Queries
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

            return connection.Get<T>(adapter, null, entityToGet, transaction, commandTimeout, true);
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
            return connection.Get<T>(adapter, whereClause, parameters, transaction, commandTimeout);
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
        /// <param name="fromCache">Cache the query.</param>
        /// <returns>True if records are deleted</returns>
        private static T Get<T>(this IDbConnection connection, ISqlAdapter adapter, string whereClause, object parameters, IDbTransaction transaction = null, int? commandTimeout = null, bool fromCache = false) where T : class
        {
            var type = typeof(T);
            var tinfo = TableInfoCache(type);
            var sql = adapter.GetQuery(tinfo, whereClause, fromCache);

            T obj;

            if (type.IsInterface())
            {
                var res = connection.Query(sql, parameters, transaction, commandTimeout: commandTimeout).SingleOrDefault() as IDictionary<string, object>;

                if (res == null)
                    return null;

                obj = ProxyGenerator.GetInterfaceProxy<T>();

                foreach (var property in tinfo.PropertyList)
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
        #endregion

        #region GetMany Queries
        /// <summary>
        /// Returns many entities of type T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>enumerable list of entities</returns>
        public static IEnumerable<T> GetMany<T>(this IDbConnection connection, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var adapter = GetFormatter(connection);
            return connection.GetMany<T>(adapter, sql, null, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T> GetMany<T>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var adapter = GetFormatter(connection);
            return connection.GetMany<T>(adapter, sql, parameters, transaction, commandTimeout);
        }


        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T1> GetMany<T1, T2>(this IDbConnection connection, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query<T1, T2>(sql, null, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2) }));
        }

        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T1> GetMany<T1, T2>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query<T1, T2>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2) }));
        }

        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T1> GetMany<T1, T2, T3>(this IDbConnection connection, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query<T1, T2, T3>(sql, new { }, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) }));
        }

        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T1> GetMany<T1, T2, T3>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query<T1, T2, T3>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) }));
        }


        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T1> GetMany<T1, T2, T3, T4>(this IDbConnection connection, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query<T1, T2, T3, T4>(sql, new { }, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }));
        }

        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<T1> GetMany<T1, T2, T3, T4>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query<T1, T2, T3, T4>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }));
        }

        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<TRet> GetMany<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query<T1, T2, TRet>(sql, mapper, null, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2) }));
        }

        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<TRet> GetMany<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2) }));
        }

        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<TRet> GetMany<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query(sql, mapper, new { }, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) }));
        }

        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<TRet> GetMany<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2,T3, TRet> mapper, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) }));
        }


        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<TRet> GetMany<T1, T2, T3,T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query(sql, mapper, new { }, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }));
        }

        /// <summary>
        /// Returns a single entity by a single id from table "Ts".  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static IEnumerable<TRet> GetMany<T1, T2, T3,T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }));
        }

        private static string SplitOnArgument(IList<Type> types)
        {
            return string.Join(",", types.Select(t => TableInfoCache(t).GetSingleKey("SplitOnArgument").PropertyName));
        }

        /// <summary>
        /// Performs a SQL Get
        /// </summary>
        /// <param name="connection">Sql Connection</param>
        /// <param name="adapter">ISqlAdapter for getting the sql statement</param>
        /// <param name="sql">The where clause</param>
        /// <param name="parameters">Parameters of the where clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="fromCache">Cache the query.</param>
        /// <returns>True if records are deleted</returns>
        private static IEnumerable<T> GetMany<T>(this IDbConnection connection, ISqlAdapter adapter, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null, bool fromCache = false) where T : class
        {
            var type = typeof(T);
            var tinfo = TableInfoCache(type);
            var selectSql = adapter.GetManyQuery(tinfo, sql);

            T obj;

            var result = connection.Query(selectSql, parameters, transaction, commandTimeout: commandTimeout);
            if (type.IsInterface())
            {

                if (result == null)
                    return null;


                var list = new List<T>();
                foreach (IDictionary<string, object> res in result)
                {
                    obj = ProxyGenerator.GetInterfaceProxy<T>();
                    foreach (var property in tinfo.PropertyList)
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
            else
            {
                return connection.Query<T>(selectSql, parameters, transaction, commandTimeout: commandTimeout);
            }
        }

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

            /// <summary>
            /// Dummy type for excluding from multi-map
            /// </summary>
            private class DontMap { /* hiding constructor */ }

        }
    }
}

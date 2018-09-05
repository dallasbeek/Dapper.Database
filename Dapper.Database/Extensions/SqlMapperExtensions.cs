using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Collections.Concurrent;
using Dapper.Database.Adapters;

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
        private static readonly ConcurrentDictionary<string, ISqlAdapter> AdapterDictionary
            = new ConcurrentDictionary<string, ISqlAdapter>
            {
                ["sqlconnection"] = new SqlServerAdapter(),
                ["sqlceconnection"] = new SqlCeServerAdapter(),
                ["sqliteconnection"] = new SQLiteAdapter(),
                ["npgsqlconnection"] = new PostgresAdapter(),
                ["mysqlconnection"] = new MySqlAdapter(),
                ["fbconnection"] = new FirebirdAdapter(),
                ["oracleconnection"] = new OracleAdapter()

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


        private static string SplitOnArgument(IList<Type> types)
        {
            return string.Join(",", types.Select(t => TableInfoCache(t).GetSingleKey("SplitOnArgument").PropertyName));
        }



        /// <summary>
        /// Specifies a custom callback that detects the database type instead of relying on the default strategy (the name of the connection type object).
        /// Please note that this callback is global and will be used by all the calls that require a database specific adapter.
        /// </summary>
        public static GetDatabaseTypeDelegate GetDatabaseType;

        private static string CleanSqlAdapterName(string name) => name.ToLowerInvariant();

        private static string GetSqlAdapterName<TConnection>() where TConnection : IDbConnection, new()
            => CleanSqlAdapterName(GetDatabaseType?.Invoke(new TConnection()) ?? typeof(TConnection).Name);

        private static string GetSqlAdapterName(IDbConnection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            return CleanSqlAdapterName(GetDatabaseType?.Invoke(connection) ?? connection.GetType().Name);
        }

        private static ISqlAdapter GetFormatter(IDbConnection connection)
        {
            var name = GetSqlAdapterName(connection);

            return !AdapterDictionary.ContainsKey(name)
                ? DefaultAdapter
                : AdapterDictionary[name];
        }

        /// <summary>
        /// Configure the specified name to resolve to a custom SQL adapter.
        /// </summary>
        /// <param name="name">The name to assign the adapter.</param>
        /// <param name="adapter">An <see cref="ISqlAdapter"/> to register under <paramref name="name" />.</param>
        /// <exception cref="NullReferenceException">if <paramref name="name"/> or <paramref name="adapter"/> is null.</exception>
        public static void AddSqlAdapter(string name, ISqlAdapter adapter)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (adapter == null) throw new ArgumentNullException(nameof(adapter));

            AdapterDictionary.AddOrUpdate(CleanSqlAdapterName(name), adapter, (_name, oldAdapter) => adapter);
        }

        /// <summary>
        /// Configure the specified connection to resolve to a custom SQL adapter.
        /// </summary>
        /// <param name="connection">An <see cref="IDbConnection"/> for which the concrete type will be mapped to the specified adapter by name.</param>
        /// <param name="adapter">An <see cref="ISqlAdapter"/> to register under <paramref name="connection" />.</param>
        /// <exception cref="NullReferenceException">if <paramref name="connection"/> or <paramref name="adapter"/> is null.</exception>
        public static void AddSqlAdapter(IDbConnection connection, ISqlAdapter adapter)
            => AddSqlAdapter(GetSqlAdapterName(connection), adapter);

        /// <summary>
        /// Configure the specified connection to resolve to a custom SQL adapter.
        /// </summary>
        /// <typeparam name="TConnection">An <see cref="IDbConnection"/> type which will be mapped to the specified adapter by its <see cref="System.Reflection.MemberInfo.Name"/>.</typeparam>
        /// <param name="adapter">An <see cref="ISqlAdapter"/> to register under <typeparamref name="TConnection" />.</param>
        /// <exception cref="NullReferenceException">if <paramref name="adapter"/> is null.</exception>
        public static void AddSqlAdapter<TConnection>(ISqlAdapter adapter) where TConnection : IDbConnection, new()
            => AddSqlAdapter(GetSqlAdapterName<TConnection>(), adapter);

    }
}

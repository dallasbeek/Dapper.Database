using System;
using System.Data;

#if !NETSTANDARD1_3 && !NETCOREAPP1_0
using System.Configuration;
#endif

namespace Dapper.Database
{
    /// <summary>
    /// Represents a service that returns a database connection
    /// </summary>
    public interface IConnectionService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();
    }

    /// <summary>
    /// 
    /// </summary>
    public class StringConnectionService<T> : IConnectionService where T : IDbConnection
    {
        private readonly string _connectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public StringConnectionService(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            return (T)Activator.CreateInstance(typeof(T), _connectionString);
        }
    }

#if !NETSTANDARD1_3 && !NETCOREAPP1_0
    /// <summary>
    /// 
    /// </summary>
    public class ConfigConnectionService<T> : IConnectionService where T : IDbConnection
    {
        private readonly string _key;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">the key in app.config or web.config</param>
        public ConfigConnectionService(string key)
        {
            _key = key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            return (T)Activator.CreateInstance(typeof(T), ConfigurationManager.ConnectionStrings[_key].ConnectionString);
        }
    }
#endif
}

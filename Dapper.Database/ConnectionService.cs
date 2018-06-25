using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}

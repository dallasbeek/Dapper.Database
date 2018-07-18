using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database
{
    public partial class SqlDatabase : ISqlDatabase, IDisposable
    {

        #region Count Methods
        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        public async Task<int> CountAsync(string sql)
        {
            return await CountAsync(sql, null);
        }

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>Return Total Count of matching records</returns>
        public async Task<int> CountAsync(string sql, object parameters)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteScalarAsync<int>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        public async Task<int> CountAsync<T>(string sql = null) where T : class
        {
            return await CountAsync<T>(sql, null);
        }

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>Return Total Count of matching records</returns>
        public async Task<int> CountAsync<T>(string sql, object parameters) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.CountAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion


    }
}

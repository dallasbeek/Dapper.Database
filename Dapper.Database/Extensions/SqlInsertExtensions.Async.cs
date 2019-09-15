using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Database extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region InsertAsync Queries
        /// <summary>
        /// Inserts an entity into table "Ts"
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if the entity was inserted</returns>
        public static async Task<bool> InsertAsync<T>(this IDbConnection connection, T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return await sqlHelper.Adapter.InsertAsync(connection, transaction, commandTimeout, sqlHelper.TableInfo, entityToInsert);
        }

        #endregion

        #region InsertListAsync Queries
        /// <summary>
        /// Inserts an entity into table "Ts"
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToInsert">List of Entities to insert</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if the entity was inserted</returns>
        public static async Task<bool> InsertListAsync<T>(this IDbConnection connection, IEnumerable<T> entitiesToInsert, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return await sqlHelper.Adapter.InsertListAsync(connection, transaction, commandTimeout, sqlHelper.TableInfo, entitiesToInsert);
        }

        #endregion



    }
}

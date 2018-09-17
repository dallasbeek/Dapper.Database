using System.Collections.Generic;
using System.Data;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Database extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region Insert Queries
        /// <summary>
        /// Inserts an entity into table "Ts" and returns identity id or number of inserted rows if inserting a list.
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity to insert or the list of entities</returns>
        public static bool Insert<T>(this IDbConnection connection, T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return sqlHelper.Adapter.Insert(connection, transaction, commandTimeout, sqlHelper.TableInfo, entityToInsert);
        }

        #endregion

        #region InsertList Queries
        /// <summary>
        /// Inserts an entity into table "Ts" and returns identity id or number of inserted rows if inserting a list.
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entitiesToInsert">List of Entities to insert</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity to insert or the list of entities</returns>
        public static bool InsertList<T>(this IDbConnection connection, IEnumerable<T> entitiesToInsert, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var sqlHelper = new SqlQueryHelper(typeof(T), connection);
            return sqlHelper.Adapter.InsertList(connection, transaction, commandTimeout, sqlHelper.TableInfo, entitiesToInsert);
        }

        #endregion
    }
}

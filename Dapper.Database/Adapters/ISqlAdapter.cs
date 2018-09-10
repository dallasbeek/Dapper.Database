using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{

    /// <summary>
    /// The interface for all Dapper.Database database operations
    /// Implementing this is each provider's model.
    /// </summary>
    public partial interface ISqlAdapter
    {
        /// <summary>
        /// Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">Table information</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <returns>true if the entity was inserted</returns>
        bool Insert<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToInsert);

        /// <summary>
        /// updates an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">Table information</param>
        /// <param name="entityToUpdate">Entity to update</param>
        /// <param name="columnsToUpdate">A list of columns to update</param>
        /// <returns>true if the entity was updated</returns>
        bool Update<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate);

        /// <summary>
        /// constructs an insert query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <returns>An insert sql statement</returns>
        string InsertQuery(TableInfo tableInfo);

        /// <summary>
        /// Returns an update query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="columnsToUpdate">columns to be updated</param>
        /// <returns>An update sql statement</returns>
        string UpdateQuery(TableInfo tableInfo, IEnumerable<string> columnsToUpdate);

        /// <summary>
        /// Returns a count query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <returns>A count sql statement</returns>
        string CountQuery(TableInfo tableInfo, string sql);

        /// <summary>
        /// Returns a delete query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement. 
        /// If NULL is passed in, this will return a DELETE without a WHERE condition. This will typically delete all data from the database.</param>
        /// <returns>A delete sql statement</returns>
        string DeleteQuery(TableInfo tableInfo, string sql);

        /// <summary>
        /// Returns an exists query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <returns>An exists sql statement</returns>
        string ExistsQuery(TableInfo tableInfo, string sql);

        /// <summary>
        /// Returns a get query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <param name="fromCache"></param>
        /// <returns>A get sql statement</returns>
        string GetQuery(TableInfo tableInfo, string sql, bool fromCache = false);

        /// <summary>
        /// Returns a get list query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <returns>A get list statement</returns>
        string GetListQuery(TableInfo tableInfo, string sql);


        /// <summary>
        /// Returns a get paged list query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="page">the page requested</param>
        /// <param name="pageSize">number of records to return</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <param name="parameters">the dynamic parameters for the query</param>
        /// <returns>A paginated get sql statement</returns>
        /// <remarks>
        /// If supported, the parameters will be added to <paramref name="parameters"/>.
        /// </remarks>
        string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql, DynamicParameters parameters);

        /// <summary>
        /// Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">Table information</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <returns>true if the entity was inserted</returns>
        Task<bool> InsertAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToInsert);

        /// <summary>
        /// updates an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">Table information</param>
        /// <param name="entityToUpdate">Entity to update</param>
        /// <param name="columnsToUpdate">A list of columns to update</param>
        /// <returns>true if the entity was updated</returns>
        Task<bool> UpdateAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        string EscapeWhereList(IEnumerable<ColumnInfo> columns);
    }
}

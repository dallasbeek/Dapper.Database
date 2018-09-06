using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{
    /// <summary>
    /// Base class for SqlAdapter handlers - provides default/common handling for different database engines
    /// </summary>
    public abstract partial class SqlAdapter
    {
        /// <summary>
        /// Cache for Get Queries
        /// </summary>
        protected static readonly ConcurrentDictionary<RuntimeTypeHandle, string> GetQueries = new ConcurrentDictionary<RuntimeTypeHandle, string>();

        /// <summary>
        /// Cache for Insert Queries
        /// </summary>
        protected static readonly ConcurrentDictionary<RuntimeTypeHandle, string> InsertQueries = new ConcurrentDictionary<RuntimeTypeHandle, string>();

        /// <summary>
        /// Cache for Update Queries
        /// </summary>
        protected static readonly ConcurrentDictionary<RuntimeTypeHandle, string> UpdateQueries = new ConcurrentDictionary<RuntimeTypeHandle, string>();

        /// <summary>
        /// Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <returns>true if the entity was inserted</returns>
        public virtual bool Insert<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToInsert)
        {
            return false;
        }

        /// <summary>
        /// updates an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToUpdate">Entity to update</param>
        /// <param name="columnsToUpdate">A list of columns to update</param>
        /// <returns>true if the entity was updated</returns>
        public virtual bool Update<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            return false;
        }

        /// <summary>
        /// Default implementation of an insert query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <returns>An insert sql statement</returns>
        public virtual string InsertQuery(TableInfo tableInfo)
        {
            return InsertQueries.Acquire(
                tableInfo.ClassType.TypeHandle,
                () => true,
                () => $"insert into {EscapeTableName(tableInfo)} ({EscapeColumnList(tableInfo.InsertColumns)}) values ({EscapeParameters(tableInfo.InsertColumns)}) "
            );
        }


        /// <summary>
        /// Default implementation of an update query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="columnsToUpdate">columns to be updated</param>
        /// <returns>An update sql statement</returns>
        public virtual string UpdateQuery(TableInfo tableInfo, IEnumerable<string> columnsToUpdate)
        {
            return UpdateQueries.Acquire(
                tableInfo.ClassType.TypeHandle,
                () => columnsToUpdate == null || !columnsToUpdate.Any(),
                () =>
                {
                    var updates = tableInfo.UpdateColumns.Where(ci => (columnsToUpdate == null || !columnsToUpdate.Any() || columnsToUpdate.Contains(ci.PropertyName)));
                    return $"update {EscapeTableName(tableInfo)} set {EscapeAssignmentList(updates)} where {EscapeWhereList(tableInfo.KeyColumns)}";
                }
            );

        }

        /// <summary>
        /// Default implementation of a count query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <returns>A sql statement that selects the count of matching records</returns>
        public virtual string CountQuery(TableInfo tableInfo, string sql)
        {
            var q = new SqlParser(sql ?? "");

            if (q.Sql.StartsWith(";"))
                return q.Sql.Substring(1);

            if (!string.IsNullOrEmpty(q.OrderByClause))
            {
                q.Sql = q.Sql.Replace(q.OrderByClause, "");
            }

            if (!q.IsSelect)
            {
                if (string.IsNullOrEmpty(q.FromClause))
                {
                    return $"select count(*) from {EscapeTableName(tableInfo)} {q.Sql}";
                }
                else
                {
                    return $"select count(*) {q.Sql}";

                }
            }

            return $"select count(*) from ({q.Sql}) calc_inner";
        }

        /// <summary>
        /// Default implementation of a delete query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <returns>A sql statement that deletes</returns>
        public virtual string DeleteQuery(TableInfo tableInfo, string sql)
        {
            var q = new SqlParser(sql ?? string.Empty);

            if (q.Sql.StartsWith(";"))
                return q.Sql.Substring(1);

            //Remove any order by statements
            if (!string.IsNullOrEmpty(q.OrderByClause))
            {
                q.Sql = q.Sql.Replace(q.OrderByClause, string.Empty);
                q.OrderByClause = string.Empty;
            }

            //Partial statement passed in
            if (!q.IsDelete)
            {
                return string.IsNullOrEmpty(q.FromClause)
                    ? $"delete from { EscapeTableName(tableInfo)} {q.Sql}"
                    : $"delete {q.Sql}";
            }

            return $"delete from ({q.Sql}) calc_inner";
        }

        /// <summary>
        /// Default implementation of an Exists query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <returns>A sql statement that selects true if a record matches</returns>
        public virtual string ExistsQuery(TableInfo tableInfo, string sql)
        {
            var q = new SqlParser(sql ?? "");

            if (q.Sql.StartsWith(";"))
                return q.Sql.Substring(1);

            if (!q.IsSelect)
            {
                var wc = string.IsNullOrWhiteSpace(q.Sql) ? $"where {EscapeWhereList(tableInfo.KeyColumns)}" : q.Sql;

                if (string.IsNullOrEmpty(q.FromClause))
                    return $"select 1 where exists (select 1 from {EscapeTableName(tableInfo)} {wc})";
                else
                    return $"select 1 where exists (select 1 {wc})";

            }

            return $"select 1 where exists ({q.Sql})";

        }

        /// <summary>
        /// Default implementation of a Get Query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <param name="cache">true if this query should be cached</param>
        /// <returns>A sql statement that selects a single item</returns>
        public virtual string GetQuery(TableInfo tableInfo, string sql, bool cache = false)
        {
            var q = new SqlParser(sql ?? "");

            if (q.Sql.StartsWith(";"))
                return q.Sql.Substring(1);

            if (!q.IsSelect)
            {
                return GetQueries.Acquire(
                    tableInfo.ClassType.TypeHandle,
                    () => cache && string.IsNullOrEmpty(q.Sql),
                    () =>
                    {
                        var wc = string.IsNullOrWhiteSpace(q.Sql) ? $"where {EscapeWhereList(tableInfo.KeyColumns)}" : q.Sql;

                        if (string.IsNullOrEmpty(q.FromClause))
                            return $"select {EscapeColumnListWithAliases(tableInfo.SelectColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} {wc}";
                        else
                            return $"select {EscapeColumnListWithAliases(tableInfo.SelectColumns, tableInfo.TableName)} {wc}";
                    }
                );

            }
            return q.Sql;

        }


        /// <summary>
        /// Default implementation of a Get List query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <returns>A sql statement</returns>
        public virtual string GetListQuery(TableInfo tableInfo, string sql)
        {
            var q = new SqlParser(sql ?? "");

            if (q.Sql.StartsWith(";"))
                return q.Sql.Substring(1);

            if (!q.IsSelect)
            {
                var wc = string.IsNullOrWhiteSpace(q.Sql) ? $"where {EscapeWhereList(tableInfo.KeyColumns)}" : q.Sql;

                if (string.IsNullOrEmpty(q.FromClause))
                    return $"select {EscapeColumnListWithAliases(tableInfo.SelectColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} {wc}";
                else
                    return $"select {EscapeColumnListWithAliases(tableInfo.SelectColumns, tableInfo.TableName)} {wc}";
            }

            return q.Sql;
        }

        /// <summary>
        /// Default implementation of a a paged sql statement
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="page">the page to request</param>
        /// <param name="pageSize">the size of the page to request</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <param name="parameters">the dynamic parameters for the query</param>
        /// <returns>A paginated sql statement</returns>
        /// <remarks>
        /// Base implementation does not modify <paramref name="parameters"/>.
        /// </remarks>
        public virtual string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql, DynamicParameters parameters)
        {
            var q = new SqlParser(GetListQuery(tableInfo, sql));
            var pageSkip = (page - 1) * pageSize;

            var sqlOrderBy = string.Empty;

            if (string.IsNullOrEmpty(q.OrderByClause) && tableInfo.KeyColumns.Any())
            {
                sqlOrderBy = $"order by {EscapeColumnn(tableInfo.KeyColumns.First().PropertyName)}";
            }

            return $"{q.Sql} {sqlOrderBy} limit {pageSize} offset {pageSkip}";
        }

        /// <summary>
        /// Returns the format for table name
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <returns></returns>
        public virtual string EscapeTableName(TableInfo tableInfo) => EscapeTableName(tableInfo.TableName);

        /// <summary>
        /// Returns the format for table name
        /// </summary>
        public virtual string EscapeTableName(string value) => $"[{value}]";

        /// <summary>
        /// Returns the format for column
        /// </summary>
        public virtual string EscapeColumnn(string value) => $"[{value}]";

        /// <summary>
        /// Returns the format for parameter
        /// </summary>
        public virtual string EscapeParameter(string value) => $"@{value}";

        /// <summary>
        /// Returns the format for columns
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="tableName"></param> 
        /// <returns></returns>
        public virtual string EscapeColumnList(IEnumerable<ColumnInfo> columns, string tableName = null) => string.Join(", ", columns.Select(ci => (tableName != null ? EscapeTableName(tableName) + "." : "") + EscapeColumnn(ci.ColumnName)));

        /// <summary>
        /// Returns the format for columns
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="tableName"></param> 
        /// <returns></returns>
        public virtual string EscapeColumnListWithAliases(IEnumerable<ColumnInfo> columns, string tableName = null) => string.Join(", ", columns.Select(ci => (tableName != null ? EscapeTableName(tableName) + "." : "") + EscapeColumnn(ci.ColumnName) + (ci.ColumnName != ci.PropertyName ? " AS " + EscapeColumnn(ci.PropertyName) : "")));

        /// <summary>
        /// Returns the format for where clause
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public virtual string EscapeWhereList(IEnumerable<ColumnInfo> columns) => string.Join(" and ", columns.Select(ci => $"{EscapeColumnn(ci.ColumnName)} = {EscapeParameter(ci.PropertyName)}"));

        /// <summary>
        /// Returns the format for parameters
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public virtual string EscapeParameters(IEnumerable<ColumnInfo> columns) => string.Join(", ", columns.Select(ci => EscapeParameter(ci.PropertyName)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public virtual string EscapeAssignmentList(IEnumerable<ColumnInfo> columns) => string.Join(", ", columns.Select(ci => $"{EscapeColumnn(ci.ColumnName)} = {EscapeParameter(ci.PropertyName)}"));

        /// <summary>
        /// Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <returns>true if the entity was inserted</returns>
        public virtual async Task<bool> InsertAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToInsert)
        {
            return await new Task<bool>(() => false); ;
        }

        /// <summary>
        /// updates an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToUpdate">Entity to update</param>
        /// <param name="columnsToUpdate">A list of columns to update</param>
        /// <returns>true if the entity was updated</returns>
        public virtual async Task<bool> UpdateAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            return await new Task<bool>(() => false); ;
        }
    }

}

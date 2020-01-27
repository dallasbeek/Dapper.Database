using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{
    /// <summary>
    ///     Base class for SqlAdapter handlers - provides default/common handling for different database engines
    /// </summary>
    public abstract class SqlAdapter : ISqlAdapter
    {
        /// <summary>
        ///     Cache for Get Queries
        /// </summary>
        protected static readonly ConcurrentDictionary<RuntimeTypeHandle, string> GetQueries =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        /// <summary>
        ///     Cache for Insert Queries
        /// </summary>
        protected static readonly ConcurrentDictionary<RuntimeTypeHandle, string> InsertQueries =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        /// <summary>
        ///     Cache for Update Queries
        /// </summary>
        protected static readonly ConcurrentDictionary<RuntimeTypeHandle, string> UpdateQueries =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        /// <summary>
        ///     Parameter name for page size in <see cref="GetPageListQuery" />.
        /// </summary>
        protected virtual string PageSizeParamName { get; } = "__PageSize";

        /// <summary>
        ///     Parameter name for page skip in <see cref="GetPageListQuery" />.
        /// </summary>
        protected virtual string PageSkipParamName { get; } = "__PageSkip";


        /// <summary>
        ///     Default implementation of an insert query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <returns>An insert sql statement</returns>
        /// <remarks>
        ///     Statements are cached by type handle.
        /// </remarks>
        public virtual string InsertQuery(TableInfo tableInfo) =>
            InsertQueries.Acquire(
                tableInfo.ClassType.TypeHandle,
                () => true,
                () => BuildInsertQuery(tableInfo)
            );

        /// <summary>
        ///     Default implementation of an update query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="columnsToUpdate">columns to be updated</param>
        /// <returns>An update sql statement</returns>
        /// <remarks>
        ///     Statements are cached by type handle.
        /// </remarks>
        public virtual string UpdateQuery(TableInfo tableInfo, IEnumerable<string> columnsToUpdate) =>
            UpdateQueries.Acquire(
                tableInfo.ClassType.TypeHandle,
                () => columnsToUpdate == null || !columnsToUpdate.Any(),
                () => BuildUpdateQuery(tableInfo, columnsToUpdate));

        /// <summary>
        ///     Default implementation of a count query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <returns>A sql statement that selects the count of matching records</returns>
        public virtual string CountQuery(TableInfo tableInfo, string sql)
        {
            var q = new SqlParser(sql ?? "");

            if (q.Sql.StartsWith(";"))
                return q.Sql.Substring(1);

            if (!string.IsNullOrEmpty(q.OrderByClause)) q.Sql = q.Sql.Replace(q.OrderByClause, "");

            if (!q.IsSelect)
            {
                if (string.IsNullOrEmpty(q.FromClause))
                    return $"select count(*) from {EscapeTableName(tableInfo)} {q.Sql}";
                return $"select count(*) {q.Sql}";
            }

            return $"select count(*) from ({q.Sql}) count_inner";
        }

        /// <summary>
        ///     Default implementation of a delete query
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
                return string.IsNullOrEmpty(q.FromClause)
                    ? $"delete from {EscapeTableName(tableInfo)} {q.Sql}"
                    : $"delete {q.Sql}";

            return $"delete from ({q.Sql}) calc_inner";
        }

        /// <summary>
        ///     Default implementation of an Exists query
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
                return string.IsNullOrEmpty(q.FromClause)
                    ? $"select 1 where exists (select 1 from {EscapeTableName(tableInfo)} {q.Sql})"
                    : $"select 1 where exists (select 1 {q.Sql})";

            return $"select 1 from ({q.Sql}) calc_inner";
        }

        /// <summary>
        ///     Default implementation of a Get Query
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
                return GetQueries.Acquire(
                    tableInfo.ClassType.TypeHandle,
                    () => cache && string.IsNullOrEmpty(q.Sql),
                    () => string.IsNullOrEmpty(q.FromClause)
                        ? $"select {EscapeColumnListWithAliases(tableInfo.SelectColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} {q.Sql}"
                        : $"select {EscapeColumnListWithAliases(tableInfo.SelectColumns, tableInfo.TableName)} {q.Sql}");
            return q.Sql;
        }


        /// <summary>
        ///     Default implementation of a Get List query
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
                return string.IsNullOrEmpty(q.FromClause)
                    ? $"select {EscapeColumnListWithAliases(tableInfo.SelectColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} {q.Sql}"
                    : $"select {EscapeColumnListWithAliases(tableInfo.SelectColumns, tableInfo.TableName)} {q.Sql}";

            return q.Sql;
        }


        /// <summary>
        ///     Default implementation of a a paged sql statement
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="page">the page to request</param>
        /// <param name="pageSize">the size of the page to request</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <param name="parameters">the dynamic parameters for the query</param>
        /// <returns>A paginated sql statement</returns>
        /// <remarks>
        ///     Base implementation does not modify <paramref name="parameters" />.
        /// </remarks>
        public virtual string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql,
            DynamicParameters parameters)
        {
            var q = new SqlParser(GetListQuery(tableInfo, sql));
            var pageSkip = (page - 1) * pageSize;

            var sqlOrderBy = string.Empty;

            if (string.IsNullOrEmpty(q.OrderByClause) && tableInfo.KeyColumns.Any())
                sqlOrderBy = $"order by {EscapeColumn(tableInfo.KeyColumns.First().ColumnName)}";

            parameters.Add(PageSizeParamName, pageSize, DbType.Int64);
            parameters.Add(PageSkipParamName, pageSkip, DbType.Int64);

            return
                $"{q.Sql} {sqlOrderBy} limit {EscapeParameter(PageSizeParamName)} offset {EscapeParameter(PageSkipParamName)}";
        }

        /// <summary>
        ///     Returns the format for where clause
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public virtual string EscapeWhereList(IEnumerable<ColumnInfo> columns) => string.Join(" and ",
            columns.Select(ci => $"{EscapeColumn(ci.ColumnName)} = {EscapeParameter(ci.PropertyName)}"));

        /// <summary>
        ///     Default implementation of an insert query.
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <returns>An insert sql statement</returns>
        protected virtual string BuildInsertQuery(TableInfo tableInfo)
            =>
                $"insert into {EscapeTableName(tableInfo)} ({EscapeColumnList(tableInfo.InsertColumns)}) values ({EscapeParameters(tableInfo.InsertColumns)}) ";

        /// <summary>
        ///     Default implementation of an update query.
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="columnsToUpdate">columns to be updated</param>
        /// <returns>An update sql statement</returns>
        protected virtual string BuildUpdateQuery(TableInfo tableInfo, IEnumerable<string> columnsToUpdate)
        {
            var updateColumns = columnsToUpdate?.ToArray() ?? new string[0];
            var updates =
                tableInfo.UpdateColumns.Where(ci => !updateColumns.Any() || updateColumns.Contains(ci.PropertyName));
            return
                $"update {EscapeTableName(tableInfo)} set {EscapeAssignmentList(updates)} where {EscapeWhereList(tableInfo.KeyColumns.Union(tableInfo.ConcurrencyCheckColumns))}";
        }

        /// <summary>
        ///     Returns the format for table name
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <returns></returns>
        public virtual string EscapeTableName(TableInfo tableInfo) => EscapeTableName(tableInfo.TableName);

        /// <summary>
        ///     Returns the format for table name
        /// </summary>
        public virtual string EscapeTableName(string value) => $"[{value}]";

        /// <summary>
        ///     Returns the format for column
        /// </summary>
        public virtual string EscapeColumn(string value) => $"[{value}]";

        /// <summary>
        ///     Returns the format for parameter
        /// </summary>
        public virtual string EscapeParameter(string value) => $"@{value}";

        /// <summary>
        ///     Returns the format for columns
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public virtual string EscapeColumnList(IEnumerable<ColumnInfo> columns, string tableName = null) => string.Join(
            ", ",
            columns.Select(ci =>
                (tableName != null ? EscapeTableName(tableName) + "." : "") + EscapeColumn(ci.ColumnName)));

        /// <summary>
        ///     Returns the format for columns
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public virtual string EscapeColumnListWithAliases(IEnumerable<ColumnInfo> columns, string tableName = null) =>
            string.Join(", ",
                columns.Select(ci =>
                    (tableName != null ? EscapeTableName(tableName) + "." : "") + EscapeColumn(ci.ColumnName) +
                    (ci.ColumnName != ci.PropertyName ? " AS " + EscapeColumn(ci.PropertyName) : "")));

        /// <summary>
        ///     Returns the format for parameters
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public virtual string EscapeParameters(IEnumerable<ColumnInfo> columns) =>
            string.Join(", ", columns.Select(ci => EscapeParameter(ci.PropertyName)));

        /// <summary>
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public virtual string EscapeAssignmentList(IEnumerable<ColumnInfo> columns) => string.Join(", ",
            columns.Select(ci => $"{EscapeColumn(ci.ColumnName)} = {EscapeParameter(ci.PropertyName)}"));


        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableInfo"></param>
        /// <param name="entity"></param>
        /// <param name="returnValues"></param>
        protected virtual void ApplyGeneratedValues<T>(TableInfo tableInfo, T entity,
            IDictionary<string, object> returnValues)
        {
            foreach (var key in returnValues.Keys)
            {
                var returnValue = returnValues[key];

                var generatedProperty = tableInfo.GeneratedColumns.Single(generatedColumn =>
                    generatedColumn.PropertyName.Equals(key, StringComparison.OrdinalIgnoreCase)).Property;
                generatedProperty.SetValue(entity, Convert.ChangeType(returnValue, generatedProperty.PropertyType),
                    null);
            }
        }

        #region Insert Implementations

        /// <summary>
        ///     Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <returns>true if the entity was inserted</returns>
        public virtual bool Insert<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout,
            TableInfo tableInfo, T entityToInsert) => false;

        /// <summary>
        ///     Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entitiesToInsert">List of Entities to insert</param>
        /// <returns>true if the entity was inserted</returns>
        public virtual bool InsertList<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout,
            TableInfo tableInfo, IEnumerable<T> entitiesToInsert)
        {
            var toInsert = entitiesToInsert as T[] ?? entitiesToInsert.ToArray();
            var success = toInsert.Any();
            if (success && !tableInfo.GeneratedColumns.Any())
                return connection.Execute(InsertQuery(tableInfo), entitiesToInsert, transaction, commandTimeout) >=
                       toInsert.Length;

            return toInsert.Aggregate(success,
                (current, e) => current && Insert(connection, transaction, commandTimeout, tableInfo, e));
        }

        /// <summary>
        ///     Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <returns>true if the entity was inserted</returns>
        public virtual async Task<bool> InsertAsync<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, T entityToInsert) => await new Task<bool>(() => false);

        /// <summary>
        ///     Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entitiesToInsert">List of Entities to insert</param>
        /// <returns>true if the entity was inserted</returns>
        public virtual async Task<bool> InsertListAsync<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, IEnumerable<T> entitiesToInsert)
        {
            var toInsert = entitiesToInsert as T[] ?? entitiesToInsert.ToArray();
            var success = toInsert.Any();
            if (success && !tableInfo.GeneratedColumns.Any())
                return await connection.ExecuteAsync(InsertQuery(tableInfo), entitiesToInsert, transaction,
                           commandTimeout) >= toInsert.Length;

            foreach (var e in toInsert)
                success = success && await InsertAsync(connection, transaction, commandTimeout, tableInfo, e);
            return success;
        }

        #endregion

        #region Update Implementations

        /// <summary>
        ///     updates an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToUpdate">Entity to update</param>
        /// <param name="columnsToUpdate">A list of columns to update</param>
        /// <returns>true if the entity was updated</returns>
        public virtual bool Update<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout,
            TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate) => false;

        /// <summary>
        ///     updates an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entitiesToUpdate">List Entities to update</param>
        /// <param name="columnsToUpdate">A list of columns to update</param>
        /// <returns>true if the entity was updated</returns>
        public virtual bool UpdateList<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout,
            TableInfo tableInfo, IEnumerable<T> entitiesToUpdate, IEnumerable<string> columnsToUpdate)
        {
            var toUpdate = entitiesToUpdate as T[] ?? entitiesToUpdate.ToArray();
            var success = toUpdate.Any();

            return toUpdate.Aggregate(success,
                (current, e) =>
                    current && Update(connection, transaction, commandTimeout, tableInfo, e, columnsToUpdate));
        }

        /// <summary>
        ///     updates an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToUpdate">Entity to update</param>
        /// <param name="columnsToUpdate">A list of columns to update</param>
        /// <returns>true if the entity was updated</returns>
        public virtual async Task<bool> UpdateAsync<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate) =>
            await new Task<bool>(() => false);

        /// <summary>
        ///     updates an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entitiesToUpdate">List Entities to update</param>
        /// <param name="columnsToUpdate">A list of columns to update</param>
        /// <returns>true if the entity was updated</returns>
        public virtual async Task<bool> UpdateListAsync<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, IEnumerable<T> entitiesToUpdate,
            IEnumerable<string> columnsToUpdate)
        {
            var toUpdate = entitiesToUpdate as T[] ?? entitiesToUpdate.ToArray();
            var success = toUpdate.Any();
            foreach (var e in toUpdate)
                // ReSharper disable once PossibleMultipleEnumeration
                success = success && await UpdateAsync(connection, transaction, commandTimeout, tableInfo, e,
                              columnsToUpdate);

            return success;
        }

        #endregion

        #region Upsert Implementations

        /// <summary>
        ///     Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToUpsert">Entity to Update Or Insert to update</param>
        /// <param name="columnsToUpdate">A list of columns to update</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updating</param>
        /// <returns>true if inserted or updated, false if not</returns>
        public virtual bool Upsert<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout,
            TableInfo tableInfo, T entityToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction,
            Action<T> updateAction)
        {
            var qWhere = $" where {EscapeWhereList(tableInfo.KeyColumns)}";
            if (!connection.ExecuteScalar<bool>(ExistsQuery(tableInfo, qWhere), entityToUpsert, transaction,
                commandTimeout))
            {
                insertAction?.Invoke(entityToUpsert);
                return Insert(connection, transaction, commandTimeout, tableInfo, entityToUpsert);
            }

            updateAction?.Invoke(entityToUpsert);
            return Update(connection, transaction, commandTimeout, tableInfo, entityToUpsert, columnsToUpdate);
        }

        /// <summary>
        ///     Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entitiesToUpsert">Entity to Update Or Insert to update</param>
        /// <param name="columnsToUpdate">A list of columns to update</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updating</param>
        /// <returns>true if inserted or updated, false if not</returns>
        public virtual bool UpsertList<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout,
            TableInfo tableInfo, IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate,
            Action<T> insertAction, Action<T> updateAction)
        {
            var toUpsert = entitiesToUpsert as T[] ?? entitiesToUpsert.ToArray();
            var success = toUpsert.Any();
            var qWhere = $" where {EscapeWhereList(tableInfo.KeyColumns)}";

            foreach (var e in toUpsert)
                if (!connection.ExecuteScalar<bool>(ExistsQuery(tableInfo, qWhere), e, transaction, commandTimeout))
                {
                    insertAction?.Invoke(e);
                    success = success && Insert(connection, transaction, commandTimeout, tableInfo, e);
                }
                else
                {
                    updateAction?.Invoke(e);
                    // ReSharper disable once PossibleMultipleEnumeration
                    success = success && Update(connection, transaction, commandTimeout, tableInfo, e, columnsToUpdate);
                }

            return success;
        }

        /// <summary>
        ///     Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToUpsert">Entity to Update Or Insert to update</param>
        /// <param name="columnsToUpdate">A list of columns to update</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updating</param>
        /// <returns>true if inserted or updated, false if not</returns>
        public virtual async Task<bool> UpsertAsync<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, T entityToUpsert, IEnumerable<string> columnsToUpdate,
            Action<T> insertAction, Action<T> updateAction)
        {
            var qWhere = $" where {EscapeWhereList(tableInfo.KeyColumns)}";
            if (!await connection.ExecuteScalarAsync<bool>(ExistsQuery(tableInfo, qWhere), entityToUpsert, transaction,
                commandTimeout))
            {
                insertAction?.Invoke(entityToUpsert);
                return await InsertAsync(connection, transaction, commandTimeout, tableInfo, entityToUpsert);
            }

            updateAction?.Invoke(entityToUpsert);
            return await UpdateAsync(connection, transaction, commandTimeout, tableInfo, entityToUpsert,
                columnsToUpdate);
        }

        /// <summary>
        ///     Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entitiesToUpsert">Entity to Update Or Insert to update</param>
        /// <param name="columnsToUpdate">A list of columns to update</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updating</param>
        /// <returns>true if inserted or updated, false if not</returns>
        public virtual async Task<bool> UpsertListAsync<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, IEnumerable<T> entitiesToUpsert,
            IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction)
        {
            var toUpsert = entitiesToUpsert as T[] ?? entitiesToUpsert.ToArray();
            var success = toUpsert.Any();
            var qWhere = $" where {EscapeWhereList(tableInfo.KeyColumns)}";
            foreach (var e in toUpsert)
                if (!await connection.ExecuteScalarAsync<bool>(ExistsQuery(tableInfo, qWhere), e, transaction,
                    commandTimeout))
                {
                    insertAction?.Invoke(e);
                    success = success && await InsertAsync(connection, transaction, commandTimeout, tableInfo, e);
                }
                else
                {
                    updateAction?.Invoke(e);
                    // ReSharper disable once PossibleMultipleEnumeration
                    success = success && await UpdateAsync(connection, transaction, commandTimeout, tableInfo, e,
                                  columnsToUpdate);
                }

            return success;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database.Adapters
{
    /// <summary>
    /// The Oracle database adapter for modern Oracle databases.
    /// </summary>
    public partial class OracleAdapter : SqlAdapter, ISqlAdapter
    {
        /// <summary>
        /// Default implementation of an Oracle insert statement with outputs.
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <returns>An Oracle SQL statement like <c>INSERT..RETURNING</c> or equivalent.</returns>
        protected virtual string InsertReturningQuery(TableInfo tableInfo)
            => $"{InsertQuery(tableInfo)} RETURNING {EscapeColumnList(tableInfo.GeneratedColumns)} into {EscapeReturnParameters(tableInfo.GeneratedColumns)}";

        /// <summary>
        /// Default implementation of an Oracle update statement with outputs.
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="columnsToUpdate">A list of columns to update</param>
        /// <returns>An Oracle SQL statement like <c>UPDATE..RETURNING</c> or equivalent.</returns>
        protected virtual string UpdateReturningQuery(TableInfo tableInfo, IEnumerable<string> columnsToUpdate)
            => $"{UpdateQuery(tableInfo, columnsToUpdate)} RETURNING {EscapeColumnList(tableInfo.GeneratedColumns)} into {EscapeReturnParameters(tableInfo.GeneratedColumns)}";

        /// <summary>
        /// Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <returns>true if the entity was inserted</returns>
        public override bool Insert<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToInsert)
        {
            if (!tableInfo.GeneratedColumns.Any())
            {
                return connection.Execute(InsertQuery(tableInfo), entityToInsert, transaction, commandTimeout) > 0;
            }

            // Oracle does not return RETURNING values in a result set; rather, it returns them as InputOutput parameters.
            // We need to wrap the incoming object in a DynamicParameters collection to get at the values.
            // While it does InputOutput binding, it does _not_ set size for strings by default; we have to call parameters.Output() to do that.
            var parameters = new DynamicParameters(entityToInsert);
            foreach (var column in tableInfo.GeneratedColumns)
            {
                parameters.Output(entityToInsert, column);
            }

            var sql = InsertReturningQuery(tableInfo);
            var count = connection.Execute(sql, parameters, transaction, commandTimeout: commandTimeout);

            if (count == 0) return false;

            foreach (var column in tableInfo.GeneratedColumns)
            {
                var property = column.Property;
                var paramName = parameters.ParameterNames.Single(p => column.PropertyName.Equals(p, StringComparison.OrdinalIgnoreCase));

                property.SetValue(entityToInsert, Convert.ChangeType(parameters.Get<object>(paramName), property.PropertyType), null);
            }

            return true;
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
        public override bool Update<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            // Do a simple update if we have no outputs.
            if (!tableInfo.GeneratedColumns.Any())
            {
                return connection.Execute(UpdateQuery(tableInfo, columnsToUpdate), entityToUpdate, transaction, commandTimeout) > 0;
            }

            // We have outputs.
            // Oracle does not return RETURNING values in a result set; rather, it returns them as InputOutput parameters.
            // We need to wrap the incoming object in a DynamicParameters collection to get at the values.
            // While it does InputOutput binding, it does _not_ set size for strings by default; we have to call parameters.Output().
            var parameters = new DynamicParameters(entityToUpdate);
            foreach (var column in tableInfo.GeneratedColumns)
            {
                parameters.Output(entityToUpdate, column);
            }

            var sql = UpdateReturningQuery(tableInfo, columnsToUpdate);
            var count = connection.Execute(sql, parameters, transaction, commandTimeout: commandTimeout);

            if (count == 0) return false;

            foreach (var column in tableInfo.GeneratedColumns)
            {
                var property = column.Property;
                var paramName = parameters.ParameterNames.Single(p => column.PropertyName.Equals(p, StringComparison.OrdinalIgnoreCase));

                property.SetValue(entityToUpdate, Convert.ChangeType(parameters.Get<object>(paramName), property.PropertyType), null);
            }

            return true;
        }

        /// <summary>
        /// Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <returns>true if the entity was inserted</returns>
        public override async Task<bool> InsertAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToInsert)
        {
            if (!tableInfo.GeneratedColumns.Any())
            {
                return await connection.ExecuteAsync(InsertQuery(tableInfo), entityToInsert, transaction, commandTimeout) > 0;
            }

            // Oracle does not return RETURNING values in a result set; rather, it returns them as InputOutput parameters.
            // We need to wrap the incoming object in a DynamicParameters collection to get at the values.
            // While it does InputOutput binding, it does _not_ set size for strings by default; we have to call parameters.Output() to do that.
            var parameters = new DynamicParameters(entityToInsert);
            foreach (var column in tableInfo.GeneratedColumns)
            {
                parameters.Output(entityToInsert, column);
            }

            var sql = InsertReturningQuery(tableInfo);
            var count = await connection.ExecuteAsync(sql, parameters, transaction, commandTimeout: commandTimeout);

            if (count == 0) return false;

            foreach (var column in tableInfo.GeneratedColumns)
            {
                var property = column.Property;
                var paramName = parameters.ParameterNames.Single(p => column.PropertyName.Equals(p, StringComparison.OrdinalIgnoreCase));

                property.SetValue(entityToInsert, Convert.ChangeType(parameters.Get<object>(paramName), property.PropertyType), null);
            }

            return true;
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
        public override async Task<bool> UpdateAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            // Do a simple update if we have no outputs.
            if (!tableInfo.GeneratedColumns.Any())
            {
                return connection.Execute(UpdateQuery(tableInfo, columnsToUpdate), entityToUpdate, transaction, commandTimeout) > 0;
            }

            // We have outputs.
            // Oracle does not return RETURNING values in a result set; rather, it returns them as InputOutput parameters.
            // We need to wrap the incoming object in a DynamicParameters collection to get at the values.
            // While it does InputOutput binding, it does _not_ set size for strings by default; we have to call parameters.Output().
            var parameters = new DynamicParameters(entityToUpdate);
            foreach (var column in tableInfo.GeneratedColumns)
            {
                parameters.Output(entityToUpdate, column);
            }

            var sql = UpdateReturningQuery(tableInfo, columnsToUpdate);
            var count = await connection.ExecuteAsync(sql, parameters, transaction, commandTimeout: commandTimeout);

            if (count == 0) return false;

            foreach (var column in tableInfo.GeneratedColumns)
            {
                var property = column.Property;
                var paramName = parameters.ParameterNames.Single(p => column.PropertyName.Equals(p, StringComparison.OrdinalIgnoreCase));

                property.SetValue(entityToUpdate, Convert.ChangeType(parameters.Get<object>(paramName), property.PropertyType), null);
            }

            return true;
        }

        /// <summary>
        /// Oracle-specific implementation of an Exists query.
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <returns>A sql statement that selects true if a record matches</returns>
        public override string ExistsQuery(TableInfo tableInfo, string sql)
        {
            var q = new SqlParser(sql ?? "");

            if (q.Sql.StartsWith(";", StringComparison.Ordinal))
                return q.Sql.Substring(1);

            if (!q.IsSelect)
            {
                var wc = string.IsNullOrWhiteSpace(q.Sql) ? $"where {EscapeWhereList(tableInfo.KeyColumns)}" : q.Sql;

                if (string.IsNullOrEmpty(q.FromClause))
                    return $"select case when exists (select * from {EscapeTableName(tableInfo)} {wc}) then 1 else 0 end as rec_exists from dual";
                else
                    return $"select case when exists (select * {wc}) then 1 else 0 end as rec_exists from dual";
            }

            return $"select case when exists ({q.Sql}) then 1 else 0 end as rec_exists from dual";

        }

        /// <summary>
        /// Constructs a paged sql statement
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="page">the page to request</param>
        /// <param name="pageSize">the size of the page to request</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <param name="parameters">the dynamic parameters for the query</param>
        /// <returns>A paginated sql statement</returns>
        /// <remarks>
        /// Oracle supports binding the pagination values as <paramref name="parameters"/>.
        /// </remarks>
        public override string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql, DynamicParameters parameters)
        {
            var q = new SqlParser(GetListQuery(tableInfo, sql));
            var pageSkip = (page - 1) * pageSize;
            var sqlOrderBy = string.Empty;

            if (string.IsNullOrEmpty(q.OrderByClause) && tableInfo.KeyColumns.Any())
            {
                sqlOrderBy = $"order by {EscapeColumnn(tableInfo.KeyColumns.First().PropertyName)}";
            }

            // Oracle supports binding the offset and page size.
            // Use variable names that are unlikely to be used as parameters, and that are safe to use with ODP.NET and Dapper.
            // NOTE: this explicitly won't work with Oracle.ManagedDataAccess 12.1.x, only 12.2 and later.
            // It works with all versions of Oracle.ManagedDataAccess.Core, however...
            parameters.Add("PAGE_SKIP$$", pageSkip, DbType.Int64);
            parameters.Add("PAGE_SIZE$$", pageSize, DbType.Int64);

            return $"{q.Sql} {sqlOrderBy} offset :PAGE_SKIP$$ rows fetch next :PAGE_SIZE$$ rows only";
        }

        /// <summary>
        /// Returns the format for parameters
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public override string EscapeParameters(IEnumerable<ColumnInfo> columns) => string.Join(", ", columns.Select(ci => string.IsNullOrWhiteSpace(ci.SequenceName) ? EscapeParameter(ci.PropertyName) : ci.SequenceName + ".nextval"));

        /// <summary>
        /// Returns the format for parameters
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public string EscapeReturnParameters(IEnumerable<ColumnInfo> columns) => string.Join(", ", columns.Select(ci => EscapeParameter(ci.PropertyName)));

        /// <summary>
        /// Applies a schema name is one is specified
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <returns></returns>
        public override string EscapeTableName(TableInfo tableInfo) =>
            (!string.IsNullOrEmpty(tableInfo.SchemaName) ? EscapeTableName(tableInfo.SchemaName) + "." : null) + EscapeTableName(tableInfo.TableName);

        /// <summary>
        /// Returns the format for table name
        /// </summary>
        public override string EscapeTableName(string value) => $"\"{value.ToUpperInvariant()}\"";

        /// <summary>
        /// Returns the format for column
        /// </summary>
        public override string EscapeColumnn(string value) => $"\"{value.ToUpperInvariant()}\"";

        /// <summary>
        /// Returns the format for parameter
        /// </summary>
        public override string EscapeParameter(string value) => $":{value}";
    }
}

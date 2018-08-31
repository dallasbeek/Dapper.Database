using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database.Adapters
{
    /// <summary>
    /// The Oracle database adapter.
    /// </summary>
    public partial class OracleAdapter : SqlAdapter, ISqlAdapter
    {

        private Regex _reservedWords = new Regex("^name|size$");

        /// <summary>
        /// Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <returns>true if the entity was inserted</returns>
        public override bool Insert<T>( IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToInsert )
        {
            var cmd = new StringBuilder(InsertQuery(tableInfo));

            if (tableInfo.GeneratedColumns.Any())
            {
                cmd.Append($" RETURNING  {EscapeColumnList(tableInfo.GeneratedColumns)} INTO {EscapeParameters(tableInfo.GeneratedColumns)}");

                // Oracle does not return RETURNING values in a result set; rather, it returns them as InputOutput parameters.
                // We need to wrap the incoming object in a DynamicParameters collection to get at the values.
                // While it does InputOutput binding, it does _not_ set size for strings by default; we have to call parameters.Output() to do that.
                var parameters = new DynamicParameters(entityToInsert);
                foreach ( var column in tableInfo.GeneratedColumns )
                {
                    parameters.Output(entityToInsert, column);
                }

                var count = connection.Execute(cmd.ToString(), parameters, transaction, commandTimeout: commandTimeout);

                if (count == 0) return false;

                foreach ( var column in tableInfo.GeneratedColumns )
                {
                    var property = column.Property;
                    var paramName = parameters.ParameterNames.Single(p => column.ColumnName.Equals(p, StringComparison.OrdinalIgnoreCase));

                    property.SetValue(entityToInsert, Convert.ChangeType(parameters.Get<object>(paramName), property.PropertyType), null);
                }

                return true;
            }
            else
            {
                return connection.Execute(cmd.ToString(), entityToInsert, transaction, commandTimeout) > 0;
            }

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
        public override bool Update<T>( IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate )
        {
            var cmd = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

            if ( tableInfo.GeneratedColumns.Any() )
            {
                cmd.Append($" RETURNING  {EscapeColumnList(tableInfo.GeneratedColumns)} INTO {EscapeParameters(tableInfo.GeneratedColumns)}");

                // Oracle does not return RETURNING values in a result set; rather, it returns them as InputOutput parameters.
                // We need to wrap the incoming object in a DynamicParameters collection to get at the values.
                // While it does InputOutput binding, it does _not_ set size for strings by default; we have to call parameters.Output().
                var parameters = new DynamicParameters(entityToUpdate);
                foreach ( var column in tableInfo.GeneratedColumns )
                {
                    parameters.Output(entityToUpdate, column);
                }
                var count = connection.Execute(cmd.ToString(), parameters, transaction, commandTimeout: commandTimeout);

                if ( count == 0 ) return false;

                foreach ( var column in tableInfo.GeneratedColumns )
                {
                    var property = column.Property;
                    var paramName = parameters.ParameterNames.Single(p => column.ColumnName.Equals(p, StringComparison.OrdinalIgnoreCase));

                    property.SetValue(entityToUpdate, Convert.ChangeType(parameters.Get<object>(paramName), property.PropertyType), null);
                }

                return true;
            }
            else
            {
                return connection.Execute(cmd.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
            }
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
        public override async Task<bool> InsertAsync<T>( IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToInsert )
        {
            var cmd = new StringBuilder(InsertQuery(tableInfo));

            if ( tableInfo.GeneratedColumns.Any() )
            {
                cmd.Append($" RETURNING  {EscapeColumnList(tableInfo.GeneratedColumns)} INTO {EscapeParameters(tableInfo.GeneratedColumns)}");

                // Oracle does not return RETURNING values in a result set; rather, it returns them as InputOutput parameters.
                // We need to wrap the incoming object in a DynamicParameters collection to get at the values.
                // Oracle does not return RETURNING values in a result set; rather, it returns them as InputOutput parameters.
                // We need to wrap the incoming object in a DynamicParameters collection to get at the values.
                // While it does InputOutput binding, it does _not_ set size for strings by default; we have to call parameters.Output().
                var parameters = new DynamicParameters(entityToInsert);
                foreach ( var column in tableInfo.GeneratedColumns )
                {
                    parameters.Output(entityToInsert, column);
                }
                var count = await connection.ExecuteAsync(cmd.ToString(), parameters, transaction, commandTimeout: commandTimeout);

                if ( count == 0 ) return false;

                foreach ( var column in tableInfo.GeneratedColumns )
                {
                    var property = column.Property;
                    var paramName = parameters.ParameterNames.Single(p => column.ColumnName.Equals(p, StringComparison.OrdinalIgnoreCase));

                    property.SetValue(entityToInsert, Convert.ChangeType(parameters.Get<object>(paramName), property.PropertyType), null);
                }

                return true;
            }
            else
            {
                return await connection.ExecuteAsync(cmd.ToString(), entityToInsert, transaction, commandTimeout) > 0;
            }

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
        public override async Task<bool> UpdateAsync<T>( IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate )
        {
            var cmd = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

            if (tableInfo.GeneratedColumns.Any())
            {
                cmd.Append($" RETURNING  {EscapeColumnList(tableInfo.GeneratedColumns)} INTO {EscapeParameters(tableInfo.GeneratedColumns)}");

                // Oracle does not return RETURNING values in a result set; rather, it returns them as InputOutput parameters.
                // We need to wrap the incoming object in a DynamicParameters collection to get at the values.
                // While it does InputOutput binding, it does _not_ set size for strings by default; we have to call parameters.Output().
                var parameters = new DynamicParameters(entityToUpdate);
                foreach ( var column in tableInfo.GeneratedColumns )
                {
                    parameters.Output(entityToUpdate, column);
                }
                var count = await connection.ExecuteAsync(cmd.ToString(), parameters, transaction, commandTimeout: commandTimeout);

                if ( count == 0 ) return false;

                foreach ( var column in tableInfo.GeneratedColumns )
                {
                    var property = column.Property;
                    var paramName = parameters.ParameterNames.Single(p => column.ColumnName.Equals(p, StringComparison.OrdinalIgnoreCase));

                    property.SetValue(entityToUpdate, Convert.ChangeType(parameters.Get<object>(paramName), property.PropertyType), null);
                }

                return true;
            }
            else
            {
                return await connection.ExecuteAsync(cmd.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
            }
        }

        /// <summary>
        /// Default implementation of an Exists query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <returns>A sql statement that selects true if a record matches</returns>
        public override string ExistsQuery( TableInfo tableInfo, string sql )
        {
            var q = new SqlParser(sql ?? "");

            if ( q.Sql.StartsWith(";") )
                return q.Sql.Substring(1);

            if ( !q.IsSelect )
            {
                var wc = string.IsNullOrWhiteSpace(q.Sql) ? $"where {EscapeWhereList(tableInfo.KeyColumns)}" : q.Sql;

                if ( string.IsNullOrEmpty(q.FromClause) )
                    return $"select case when exists (select * from { EscapeTableName(tableInfo)} {wc}) then 1 else 0 end as rec_exists from dual";
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
        /// <returns>A paginated sql statement</returns>
        public override string GetPageListQuery( TableInfo tableInfo, long page, long pageSize, string sql )
        {
            var q = new SqlParser(GetListQuery(tableInfo, sql));
            var pageSkip = (page - 1) * pageSize;
            var pageTake = pageSkip + pageSize;
            var sqlOrderBy = "order by (select null)";
            var sqlOrderByRemoved = q.Sql;

            if ( string.IsNullOrEmpty(q.OrderByClause) )
            {
                if ( tableInfo.KeyColumns.Any() )
                {
                    sqlOrderBy = $"order by {EscapeColumnn(tableInfo.KeyColumns.First().ColumnName)}";
                }
            }
            else
            {
                sqlOrderBy = q.OrderByClause;
                sqlOrderByRemoved = sqlOrderByRemoved.Replace(q.OrderByClause, "");
            }

            var columnsOnly = $"page_inner.* FROM ({sqlOrderByRemoved}) page_inner";

            return $"select * from (select row_number() over ({sqlOrderBy}) page_rn, {columnsOnly}) page_outer where page_rn > {pageSkip} and page_rn <= {pageTake}";

        }

        /// <summary>
        /// Returns the format for parameters
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public override string EscapeParameters( IEnumerable<ColumnInfo> columns ) => string.Join(", ", columns.Select(ci => string.IsNullOrWhiteSpace(ci.SequenceName) ? EscapeParameter(ci.PropertyName) : ci.SequenceName + ".nextval"));

        /// <summary>
        /// Applies a schema name is one is specified
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <returns></returns>
        public override string EscapeTableName( TableInfo tableInfo ) =>
            (!string.IsNullOrEmpty(tableInfo.SchemaName) ? EscapeTableName(tableInfo.SchemaName) + "." : null) + EscapeTableName(tableInfo.TableName);

        /// <summary>
        /// Returns the format for table name
        /// </summary>
        public override string EscapeTableName( string value ) => $"{value}";

        /// <summary>
        /// Returns the format for column
        /// </summary>
        public override string EscapeColumnn( string value ) => _reservedWords.IsMatch(value.ToLower()) ? $"\"{value}\"" : $"{value}";

        /// <summary>
        /// Returns the format for parameter
        /// </summary>
        public override string EscapeParameter( string value ) => $":{value}";
    }
}

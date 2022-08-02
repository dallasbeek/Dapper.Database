using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{
    /// <summary>
    ///     The SQL Server database adapter.
    /// </summary>
    public class SqlServerAdapter : SqlAdapter
    {
        /// <summary>
        ///     Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <returns>true if the entity was inserted</returns>
        public override bool Insert<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout,
            TableInfo tableInfo, T entityToInsert)
        {
            var command = new StringBuilder(InsertQuery(tableInfo));

            if (!tableInfo.GeneratedColumns.Any())
                return connection.Execute(command.ToString(), entityToInsert, transaction, commandTimeout) > 0;

            IEnumerable<dynamic> r;

            if (tableInfo.SqlServerSelectComputed)
            {
                var selectCommand = new StringBuilder($"select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");

                selectCommand.Append(tableInfo.KeyColumns.Any(k => k.IsIdentity)
                    ? $"where {EscapeColumn(tableInfo.KeyColumns.First(k => k.IsIdentity).ColumnName)} = @@IDENTITY;"
                    : $"where {EscapeWhereList(tableInfo.KeyColumns)};");

                if (!(connection.Execute(command.ToString(), entityToInsert, transaction, commandTimeout) > 0))
                {
                    return false;
                };

                r = connection.Query(selectCommand.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout);
            }
            else
            {
                r = connection.Query(command.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout);
            }

            var values = r.ToList();

            if (!values.Any()) return false;

            ApplyGeneratedValues(tableInfo, entityToInsert, (IDictionary<string, object>) values[0]);
            return true;

        }

        /// <inheritdoc />
        protected override bool UpdateInternal<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            var command = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

            if (!tableInfo.GeneratedColumns.Any())
                return connection.Execute(command.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
      

            IEnumerable<dynamic> r;

            if (tableInfo.SqlServerSelectComputed)
            {
                var selectCommand = new StringBuilder($"select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");

                selectCommand.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

                if (!(connection.Execute(command.ToString(), entityToUpdate, transaction, commandTimeout) > 0))
                {
                    return false;
                };

                r = connection.Query(selectCommand.ToString(), entityToUpdate, transaction, commandTimeout: commandTimeout);
            }
            else
            {
                r = connection.Query(command.ToString(), entityToUpdate, transaction, commandTimeout: commandTimeout).ToList();
            }

            var values = r.ToList();

            if (!values.Any()) return false;

            ApplyGeneratedValues(tableInfo, entityToUpdate, (IDictionary<string, object>) values[0]);
            return true;

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
        public override async Task<bool> InsertAsync<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, T entityToInsert)
        {
            var command = new StringBuilder(InsertQuery(tableInfo));

            if (!tableInfo.GeneratedColumns.Any())
                return await connection.ExecuteAsync(command.ToString(), entityToInsert, transaction, commandTimeout) > 0;

            IEnumerable<dynamic> r;

            if (tableInfo.SqlServerSelectComputed)
            {
                var selectCommand = new StringBuilder($"select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");

                selectCommand.Append(tableInfo.KeyColumns.Any(k => k.IsIdentity)
                    ? $"where {EscapeColumn(tableInfo.KeyColumns.First(k => k.IsIdentity).ColumnName)} = @@IDENTITY;"
                    : $"where {EscapeWhereList(tableInfo.KeyColumns)};");

                if (!(await connection.ExecuteAsync(command.ToString(), entityToInsert, transaction, commandTimeout) > 0))
                {
                    return false;
                }

                r = await connection.QueryAsync(selectCommand.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout);

            }
            else
            {
                r = await connection.QueryAsync(command.ToString(), entityToInsert, transaction, commandTimeout);
            }

            var values = r.ToList();

            if (!values.Any()) return false;

            ApplyGeneratedValues(tableInfo, entityToInsert, (IDictionary<string, object>) values[0]);
            return true;

        }

        /// <inheritdoc />
        protected override async Task<bool> UpdateInternalAsync<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            var command = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

            if (!tableInfo.GeneratedColumns.Any())
                return await connection.ExecuteAsync(command.ToString(), entityToUpdate, transaction, commandTimeout) > 0;

            IEnumerable<dynamic> r;

            if (tableInfo.SqlServerSelectComputed)
            {
                var selectCommand = new StringBuilder($"select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");

                selectCommand.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

                if (!(await connection.ExecuteAsync(command.ToString(), entityToUpdate, transaction, commandTimeout) > 0))
                {
                    return false;
                }

                r = await connection.QueryAsync(selectCommand.ToString(), entityToUpdate, transaction, commandTimeout: commandTimeout);
            }
            else
            {
                r = await connection.QueryAsync(command.ToString(), entityToUpdate, transaction, commandTimeout);
            }

            var values = r.ToList();

            if (!values.Any()) return false;

            ApplyGeneratedValues(tableInfo, entityToUpdate, (IDictionary<string, object>) values[0]);
            return true;

        }

        /// <summary>
        ///     Constructs a paged sql statement
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="page">the page to request</param>
        /// <param name="pageSize">the size of the page to request</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <param name="parameters">the dynamic parameters for the query</param>
        /// <returns>A paginated sql statement</returns>
        public override string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql,
            DynamicParameters parameters)
        {
            var sqlParser = new SqlParser(GetListQuery(tableInfo, sql));
            var pageSkip = (page - 1) * pageSize;
            var sqlOrderBy = string.Empty;

            if (string.IsNullOrEmpty(sqlParser.OrderByClause) && tableInfo.KeyColumns.Any())
                sqlOrderBy = $"order by {EscapeColumn(tableInfo.KeyColumns.First().ColumnName)}";

            parameters.Add(PageSizeParamName, pageSize, DbType.Int64);
            parameters.Add(PageSkipParamName, pageSkip, DbType.Int64);

            return
                $"{sqlParser.Sql} {sqlOrderBy} offset {EscapeParameter(PageSkipParamName)} rows fetch next {EscapeParameter(PageSizeParamName)} rows only";
        }

        /// <summary>
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <returns></returns>
        public override string EscapeTableName(TableInfo tableInfo) =>
            (!string.IsNullOrEmpty(tableInfo.SchemaName) ? EscapeTableName(tableInfo.SchemaName) + "." : null) +
            EscapeTableName(tableInfo.TableName);

        /// <summary>
        ///     implementation of an insert query.
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <returns>An insert sql statement</returns>
        protected override string BuildInsertQuery(TableInfo tableInfo)
            => tableInfo.GeneratedColumns.Any() && !tableInfo.SqlServerSelectComputed
                ? $"insert into {EscapeTableName(tableInfo)} ({EscapeColumnList(tableInfo.InsertColumns)}) output {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, "inserted")} values ({EscapeParameters(tableInfo.InsertColumns)}) "
                : base.BuildInsertQuery(tableInfo);

        /// <summary>
        ///     Default implementation of an update query.
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="columnsToUpdate">columns to be updated</param>
        /// <returns>An update sql statement</returns>
        protected override string BuildUpdateQuery(TableInfo tableInfo, IEnumerable<string> columnsToUpdate)
        {
            if (!tableInfo.GeneratedColumns.Any()) return base.BuildUpdateQuery(tableInfo, columnsToUpdate);

            var toUpdate = columnsToUpdate?.ToArray() ?? Array.Empty<string>();

            var updates = tableInfo.UpdateColumns.Where(ci => !toUpdate.Any() || toUpdate.Contains(ci.PropertyName));
            
            return tableInfo.SqlServerSelectComputed 
                ? $"update {EscapeTableName(tableInfo)} set {EscapeAssignmentList(updates)} where {EscapeWhereList(tableInfo.ComparisonColumns)}"
                : $"update {EscapeTableName(tableInfo)} set {EscapeAssignmentList(updates)} output {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, "inserted")} where {EscapeWhereList(tableInfo.ComparisonColumns)}";
        }
    }
}

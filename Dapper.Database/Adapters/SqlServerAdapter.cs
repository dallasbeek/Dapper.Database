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

            if (tableInfo.GeneratedColumns.Any())
            {
                var values = connection
                    .Query(command.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout).ToList();

                if (!values.Any()) return false;

                ApplyGeneratedValues(tableInfo, entityToInsert, (IDictionary<string, object>) values[0]);
                return true;
            }

            return connection.Execute(command.ToString(), entityToInsert, transaction, commandTimeout) > 0;
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
        public override bool Update<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout,
            TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            var command = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

            if (tableInfo.GeneratedColumns.Any())
            {
                var values = connection
                    .Query(command.ToString(), entityToUpdate, transaction, commandTimeout: commandTimeout).ToList();

                if (!values.Any()) return false;

                ApplyGeneratedValues(tableInfo, entityToUpdate, (IDictionary<string, object>) values[0]);
                return true;
            }

            return connection.Execute(command.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
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
            var cmd = new StringBuilder(InsertQuery(tableInfo));

            if (tableInfo.GeneratedColumns.Any())
            {
                var values = (await connection.QueryAsync(cmd.ToString(), entityToInsert, transaction, commandTimeout))
                    .ToList();

                if (!values.Any()) return false;

                ApplyGeneratedValues(tableInfo, entityToInsert, (IDictionary<string, object>) values[0]);
                return true;
            }

            return await connection.ExecuteAsync(cmd.ToString(), entityToInsert, transaction, commandTimeout) > 0;
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
        public override async Task<bool> UpdateAsync<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            var command = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

            if (tableInfo.GeneratedColumns.Any())
            {
                var values =
                    (await connection.QueryAsync(command.ToString(), entityToUpdate, transaction, commandTimeout))
                    .ToList();

                if (!values.Any()) return false;

                ApplyGeneratedValues(tableInfo, entityToUpdate, (IDictionary<string, object>) values[0]);
                return true;
            }

            return await connection.ExecuteAsync(command.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
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
            => tableInfo.GeneratedColumns.Any()
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

            var toUpdate = columnsToUpdate?.ToArray() ?? new string[0];

            var updates = tableInfo.UpdateColumns.Where(ci => !toUpdate.Any() || toUpdate.Contains(ci.PropertyName));
            return
                $"update {EscapeTableName(tableInfo)} set {EscapeAssignmentList(updates)} output {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, "inserted")} where {EscapeWhereList(tableInfo.ComparisonColumns)}";
        }
    }
}

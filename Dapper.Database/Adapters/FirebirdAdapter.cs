using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{
    /// <summary>
    ///     The Firebird database adapter.
    /// </summary>
    public class FirebirdAdapter : SqlAdapter
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
            command.Append($" RETURNING  {EscapeColumnListWithAliases(tableInfo.GeneratedColumns)};");

            var values = connection.Query(command.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout)
                .ToList();

            if (!values.Any()) return false;

            ApplyGeneratedValues(tableInfo, entityToInsert, (IDictionary<string, object>) values[0]);
            return true;
        }

        /// <inheritdoc />
        protected override bool UpdateInternal<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            var command = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

            if (tableInfo.GeneratedColumns.Any())
            {
                command.Append($" RETURNING  {EscapeColumnListWithAliases(tableInfo.GeneratedColumns)};");

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
            var command = new StringBuilder(InsertQuery(tableInfo));

            if (tableInfo.GeneratedColumns.Any())
            {
                command.Append($" RETURNING  {EscapeColumnListWithAliases(tableInfo.GeneratedColumns)};");

                var result =
                    await connection.QueryAsync(command.ToString(), entityToInsert, transaction, commandTimeout);

                var values = result.ToList();

                if (!values.Any()) return false;

                ApplyGeneratedValues(tableInfo, entityToInsert, (IDictionary<string, object>) values[0]);
                return true;
            }

            return await connection.ExecuteAsync(command.ToString(), entityToInsert, transaction, commandTimeout) > 0;
        }

        /// <inheritdoc />
        protected override async Task<bool> UpdateInternalAsync<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            var command = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

            if (tableInfo.GeneratedColumns.Any())
            {
                command.Append($" RETURNING  {EscapeColumnListWithAliases(tableInfo.GeneratedColumns)};");

                var result =
                    await connection.QueryAsync(command.ToString(), entityToUpdate, transaction, commandTimeout);

                var values = result.ToList();

                if (!values.Any()) return false;

                ApplyGeneratedValues(tableInfo, entityToUpdate, (IDictionary<string, object>) values[0]);
                return true;
            }

            return await connection.ExecuteAsync(command.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
        }

        /// <summary>
        ///     Default implementation of an Exists query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <returns>A sql statement that selects true if a record matches</returns>
        public override string ExistsQuery(TableInfo tableInfo, string sql)
        {
            var q = new SqlParser(sql ?? "");

            if (q.Sql.StartsWith(";"))
                return q.Sql.Substring(1);

            if (!q.IsSelect)
                return string.IsNullOrEmpty(q.FromClause)
                    ? $"select first 1 1 from {EscapeTableName(tableInfo)} where exists (select 1 from {EscapeTableName(tableInfo)} {q.Sql});"
                    : $"select first 1 1 from {EscapeTableName(tableInfo)} where exists (select 1 {q.Sql});";
            return $"select first 1 1 from {EscapeTableName(tableInfo)} where exists ({q.Sql});";
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
        public override string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql,
            DynamicParameters parameters)
        {
            var q = new SqlParser(GetListQuery(tableInfo, sql));
            var pageSkip = (page - 1) * pageSize;

            var sqlOrderBy = string.Empty;

            if (string.IsNullOrEmpty(q.OrderByClause) && tableInfo.KeyColumns.Any())
                sqlOrderBy = $"order by {EscapeColumn(tableInfo.KeyColumns.First().PropertyName)}";


            parameters.Add(PageSizeParamName, pageSize, DbType.Int64);
            parameters.Add(PageSkipParamName, pageSkip, DbType.Int64);

            return
                $"select first {EscapeParameter(PageSizeParamName)} skip {EscapeParameter(PageSkipParamName)} * from ({q.Sql}) page_inner {sqlOrderBy}";
        }

        /// <summary>
        ///     Applies a schema name is one is specified
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <returns></returns>
        public override string EscapeTableName(TableInfo tableInfo) =>
            (!string.IsNullOrEmpty(tableInfo.SchemaName) ? EscapeTableName(tableInfo.SchemaName) + "." : null) +
            EscapeTableName(tableInfo.TableName);

        /// <summary>
        ///     Returns the format for table name
        /// </summary>
        public override string EscapeTableName(string value) => $"{value}";

        /// <summary>
        ///     Returns the format for column
        /// </summary>
        public override string EscapeColumn(string value) => $"{value}";
    }
}

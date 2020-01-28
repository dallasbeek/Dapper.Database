using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{
    /// <summary>
    ///     The SQL Server Compact Edition database adapter.
    /// </summary>
    public class SqlCeServerAdapter : SqlAdapter
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
                var selectCommand =
                    new StringBuilder(
                        $"select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");

                selectCommand.Append(tableInfo.KeyColumns.Any(k => k.IsIdentity)
                    ? $"where {EscapeColumn(tableInfo.KeyColumns.First(k => k.IsIdentity).ColumnName)} = @@IDENTITY;"
                    : $"where {EscapeWhereList(tableInfo.KeyColumns)};");

                var wasClosed = connection.State == ConnectionState.Closed;
                if (wasClosed) connection.Open();

                connection.Execute(command.ToString(), entityToInsert, transaction, commandTimeout);
                var r = connection.Query(selectCommand.ToString(), entityToInsert, transaction,
                    commandTimeout: commandTimeout);

                if (wasClosed) connection.Close();

                var values = r.ToList();

                if (!values.Any()) return false;

                ApplyGeneratedValues(tableInfo, entityToInsert, (IDictionary<string, object>) values[0]);
                return true;
            }

            return connection.Execute(command.ToString(), entityToInsert, transaction, commandTimeout) > 0;
        }

        /// <inheritdoc />
        protected override bool UpdateInternal<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            var command = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

            if (tableInfo.GeneratedColumns.Any())
            {
                var selectCommand =
                    new StringBuilder(
                        $"select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
                selectCommand.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

                connection.Execute(command.ToString(), entityToUpdate, transaction, commandTimeout);
                var result = connection.Query(selectCommand.ToString(), entityToUpdate, transaction,
                    commandTimeout: commandTimeout);

                var values = result.ToList();

                if (!values.Any()) return false;

                ApplyGeneratedValues(tableInfo, entityToUpdate, (IDictionary<string, object>) values[0]);

                return true;
            }

            return connection.Execute(command.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
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
                var selectCommand =
                    new StringBuilder(
                        $"select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");

                selectCommand.Append(tableInfo.KeyColumns.Any(k => k.IsIdentity)
                    ? $"where {EscapeColumn(tableInfo.KeyColumns.First(k => k.IsIdentity).ColumnName)} = @@IDENTITY;"
                    : $"where {EscapeWhereList(tableInfo.KeyColumns)};");

                var wasClosed = connection.State == ConnectionState.Closed;
                if (wasClosed) connection.Open();

                await connection.ExecuteAsync(command.ToString(), entityToInsert, transaction, commandTimeout);
                var result = await connection.QueryAsync(selectCommand.ToString(), entityToInsert, transaction,
                    commandTimeout);

                if (wasClosed) connection.Close();

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
                var selectCommand =
                    new StringBuilder(
                        $"select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
                selectCommand.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

                await connection.ExecuteAsync(command.ToString(), entityToUpdate, transaction, commandTimeout);
                var result = await connection.QueryAsync(selectCommand.ToString(), entityToUpdate, transaction,
                    commandTimeout);

                var values = result.ToList();

                if (!values.Any()) return false;

                ApplyGeneratedValues(tableInfo, entityToUpdate, (IDictionary<string, object>) values[0]);

                return true;
            }

            return await connection.ExecuteAsync(command.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
        }
    }
}

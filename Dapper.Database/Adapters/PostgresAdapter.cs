using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{
    /// <summary>
    ///     The Postgres database adapter.
    /// </summary>
    public class PostgresAdapter : SqlAdapter
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
                command.Append($" RETURNING  {EscapeColumnListWithAliases(tableInfo.GeneratedColumns)};");

                var values = connection
                    .Query(command.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout).ToList();

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

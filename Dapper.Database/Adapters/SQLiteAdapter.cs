using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{
    /// <summary>
    ///     The SQLite database adapter.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLiteAdapter : SqlAdapter
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

            command.Append(
                $"; select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
            command.Append(tableInfo.KeyColumns.Any(keyColumn => keyColumn.IsIdentity)
                ? $"where {EscapeColumn(tableInfo.KeyColumns.First(keyColumn => keyColumn.IsIdentity).ColumnName)} = last_insert_rowid();"
                : $"where {EscapeWhereList(tableInfo.KeyColumns)};");

            var gridReader = connection.QueryMultiple(command.ToString(), entityToInsert, transaction, commandTimeout);

            var values = gridReader.Read().ToList();

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

            command.Append(
                $"; select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
            command.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

            var gridReader = connection.QueryMultiple(command.ToString(), entityToUpdate, transaction, commandTimeout);

            var values = gridReader.Read().ToList();

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
                return await connection.ExecuteAsync(command.ToString(), entityToInsert, transaction, commandTimeout) >
                       0;
            command.Append(
                $"; select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");

            command.Append(tableInfo.KeyColumns.Any(keyColumn => keyColumn.IsIdentity)
                ? $"where {EscapeColumn(tableInfo.KeyColumns.First(keyColumn => keyColumn.IsIdentity).ColumnName)} = last_insert_rowid();"
                : $"where {EscapeWhereList(tableInfo.KeyColumns)};");

            var gridReader =
                await connection.QueryMultipleAsync(command.ToString(), entityToInsert, transaction, commandTimeout);

            var values = gridReader.Read().ToList();

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
                return await connection.ExecuteAsync(command.ToString(), entityToUpdate, transaction, commandTimeout) >
                       0;
            command.Append(
                $"; select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
            command.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

            var gridReader =
                await connection.QueryMultipleAsync(command.ToString(), entityToUpdate, transaction, commandTimeout);

            var values = gridReader.Read().ToList();

            if (!values.Any()) return false;

            ApplyGeneratedValues(tableInfo, entityToUpdate, (IDictionary<string, object>) values[0]);

            return true;
        }
    }
}

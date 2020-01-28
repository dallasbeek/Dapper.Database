using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{
    /// <summary>
    ///     The Oracle database adapter for modern Oracle databases.
    /// </summary>
    public partial class OracleAdapter
    {
        /// <inheritdoc />
        public override async Task<bool> InsertAsync<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, T entityToInsert)
        {
            var sql = InsertQuery(tableInfo);

            if (!tableInfo.GeneratedColumns.Any())
                return connection.Execute(sql, entityToInsert, transaction, commandTimeout) > 0;

            var parameters = BuildInsertParameters(tableInfo, entityToInsert);

            var count = ResolveRowCount(
                await connection.ExecuteAsync(sql, parameters, transaction, commandTimeout),
                parameters
            );

            if (count == 0) return false;

            foreach (var column in tableInfo.GeneratedColumns)
            {
                var property = column.Property;
                var paramName = parameters.ParameterNames.Single(p =>
                    column.PropertyName.Equals(p, StringComparison.OrdinalIgnoreCase));

                property.SetValue(entityToInsert,
                    Convert.ChangeType(parameters.Get<object>(paramName), property.PropertyType), null);
            }

            return true;
        }

        /// <inheritdoc />
        protected override async Task<bool> UpdateInternalAsync<T>(IDbConnection connection, IDbTransaction transaction,
            int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            var sql = UpdateQuery(tableInfo, columnsToUpdate);

            // Do a simple update if we have no outputs.
            if (!tableInfo.GeneratedColumns.Any())
                return connection.Execute(sql, entityToUpdate, transaction, commandTimeout) > 0;

            // We have outputs.
            var parameters = BuildUpdateParameters(tableInfo, entityToUpdate);

            var count = ResolveRowCount(
                await connection.ExecuteAsync(sql, parameters, transaction, commandTimeout),
                parameters
            );

            if (count == 0) return false;

            foreach (var column in tableInfo.GeneratedColumns)
            {
                var property = column.Property;
                var paramName = parameters.ParameterNames.Single(p =>
                    column.PropertyName.Equals(p, StringComparison.OrdinalIgnoreCase));

                property.SetValue(entityToUpdate,
                    Convert.ChangeType(parameters.Get<object>(paramName), property.PropertyType), null);
            }

            return true;
        }
    }
}

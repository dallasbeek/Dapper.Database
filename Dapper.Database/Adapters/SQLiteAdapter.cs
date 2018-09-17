using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{
    /// <summary>
    /// The SQLite database adapter.
    /// </summary>
    public partial class SQLiteAdapter : SqlAdapter, ISqlAdapter
    {
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
            var cmd = new StringBuilder(InsertQuery(tableInfo));

            if (tableInfo.GeneratedColumns.Any())
            {
                cmd.Append($"; select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");

                if (tableInfo.KeyColumns.Any(k => k.IsIdentity))
                {
                    cmd.Append($"where {EscapeColumnn(tableInfo.KeyColumns.First(k => k.IsIdentity).ColumnName)} = last_insert_rowid();");
                }
                else
                {
                    cmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");
                }

                var multi = connection.QueryMultiple(cmd.ToString(), entityToInsert, transaction, commandTimeout);

                var vals = multi.Read().ToList();

                if (!vals.Any()) return false;

                var rvals = ((IDictionary<string, object>)vals[0]);

                foreach (var key in rvals.Keys)
                {
                    var rval = rvals[key];
                    var p = tableInfo.GeneratedColumns.Single(gp => gp.PropertyName == key).Property;
                    p.SetValue(entityToInsert, Convert.ChangeType(rval, p.PropertyType), null);
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
        public override bool Update<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            var cmd = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

            if (tableInfo.GeneratedColumns.Any())
            {
                cmd.Append($"; select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
                cmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

                var multi = connection.QueryMultiple(cmd.ToString(), entityToUpdate, transaction, commandTimeout);

                var vals = multi.Read().ToList();

                if (!vals.Any()) return false;

                var rvals = ((IDictionary<string, object>)vals[0]);

                foreach (var key in rvals.Keys)
                {
                    var rval = rvals[key];
                    var p = tableInfo.GeneratedColumns.Single(gp => gp.PropertyName == key).Property;
                    p.SetValue(entityToUpdate, Convert.ChangeType(rval, p.PropertyType), null);
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
        public override async Task<bool> InsertAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToInsert)
        {
            var cmd = new StringBuilder(InsertQuery(tableInfo));

            if (tableInfo.GeneratedColumns.Any())
            {
                cmd.Append($"; select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");

                if (tableInfo.KeyColumns.Any(k => k.IsIdentity))
                {
                    cmd.Append($"where {EscapeColumnn(tableInfo.KeyColumns.First(k => k.IsIdentity).ColumnName)} = last_insert_rowid();");
                }
                else
                {
                    cmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");
                }

                var multi = await connection.QueryMultipleAsync(cmd.ToString(), entityToInsert, transaction, commandTimeout);

                var vals = multi.Read().ToList();

                if (!vals.Any()) return false;

                var rvals = ((IDictionary<string, object>)vals[0]);

                foreach (var key in rvals.Keys)
                {
                    var rval = rvals[key];
                    var p = tableInfo.GeneratedColumns.Single(gp => gp.PropertyName == key).Property;
                    p.SetValue(entityToInsert, Convert.ChangeType(rval, p.PropertyType), null);
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
        public override async Task<bool> UpdateAsync<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToUpdate, IEnumerable<string> columnsToUpdate)
        {
            var cmd = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

            if (tableInfo.GeneratedColumns.Any())
            {
                cmd.Append($"; select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
                cmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

                var multi = await connection.QueryMultipleAsync(cmd.ToString(), entityToUpdate, transaction, commandTimeout);

                var vals = multi.Read().ToList();

                if (!vals.Any()) return false;

                var rvals = ((IDictionary<string, object>)vals[0]);

                foreach (var key in rvals.Keys)
                {
                    var rval = rvals[key];
                    var p = tableInfo.GeneratedColumns.Single(gp => gp.PropertyName == key).Property;
                    p.SetValue(entityToUpdate, Convert.ChangeType(rval, p.PropertyType), null);
                }

                return true;
            }
            else
            {
                return await connection.ExecuteAsync(cmd.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
            }
        }
    }
}

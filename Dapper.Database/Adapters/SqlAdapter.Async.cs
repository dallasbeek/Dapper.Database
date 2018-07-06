using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Dapper;
using Dapper.Database;
using System.Threading.Tasks;

/// <summary>
/// The interface for all Dapper.Database database operations
/// Implementing this is each provider's model.
/// </summary>
public partial interface ISqlAdapter
{
    /// <summary>
    /// Inserts an entity into table "Ts"
    /// </summary>
    /// <param name="connection">Open SqlConnection</param>
    /// <param name="transaction">The transaction to run under, null (the default) if none</param>
    /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
    /// <param name="tableInfo">Table information</param>
    /// <param name="entityToInsert">Entity to insert</param>
    /// <returns>true if the entity was inserted</returns>
    Task<bool> InsertAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToInsert);

    /// <summary>
    /// updates an entity into table "Ts"
    /// </summary>
    /// <param name="connection">Open SqlConnection</param>
    /// <param name="transaction">The transaction to run under, null (the default) if none</param>
    /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
    /// <param name="tableInfo">Table information</param>
    /// <param name="entityToUpdate">Entity to update</param>
    /// <param name="columnsToUpdate">A list of columns to update</param>
    /// <returns>true if the entity was updated</returns>
    Task<bool> UpdateAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToUpdate, IEnumerable<string> columnsToUpdate);

}

/// <summary>
/// Base class for SqlAdapter handlers - provides default/common handling for different database engines
/// </summary>
public partial class SqlAdapter
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
    public virtual async Task<bool> InsertAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToInsert)
    {
        return await new Task<bool>(() => false); ;
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
    public virtual async Task<bool> UpdateAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToUpdate, IEnumerable<string> columnsToUpdate)
    {
        return await new Task<bool>(() => false); ;
    }

}


/// <summary>
/// The SQL Server database adapter.
/// </summary>
public partial class SqlServerAdapter : SqlAdapter, ISqlAdapter
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
    public override async Task<bool> InsertAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToInsert)
    {
        var cmd = new StringBuilder(InsertQuery(tableInfo));

        if (tableInfo.GeneratedColumns.Any() && tableInfo.KeyColumns.Any())
        {
            cmd.Append($"select {EscapeColumnList(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");

            if (tableInfo.KeyColumns.Any(k => k.IsIdentity))
            {
                cmd.Append($"where {EscapeColumnn(tableInfo.KeyColumns.First(k => k.IsIdentity).ColumnName)} = SCOPE_IDENTITY();");
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
                var p = tableInfo.GeneratedColumns.Single(gp => gp.ColumnName == key).Property;
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
    public override async Task<bool> UpdateAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToUpdate, IEnumerable<string> columnsToUpdate)
    {
        var cmd = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

        if (tableInfo.GeneratedColumns.Any() && tableInfo.KeyColumns.Any())
        {
            cmd.Append($"select {EscapeColumnList(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
            cmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

            var multi =await connection.QueryMultipleAsync(cmd.ToString(), entityToUpdate, transaction, commandTimeout);

            var vals = multi.Read().ToList();

            if (!vals.Any()) return false;

            var rvals = ((IDictionary<string, object>)vals[0]);

            foreach (var key in rvals.Keys)
            {
                var rval = rvals[key];
                var p = tableInfo.GeneratedColumns.Single(gp => gp.ColumnName == key).Property;
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

/// <summary>
/// The SQL Server Compact Edition database adapter.
/// </summary>
public partial class SqlCeServerAdapter : SqlAdapter, ISqlAdapter
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
    public override async Task<bool> InsertAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToInsert)
    {
        var cmd = new StringBuilder(InsertQuery(tableInfo));

        if (tableInfo.GeneratedColumns.Any() && tableInfo.KeyColumns.Any())
        {

            var selectcmd = new StringBuilder($"select {EscapeColumnList(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");

            if (tableInfo.KeyColumns.Any(k => k.IsIdentity))
            {
                selectcmd.Append($"where {EscapeColumnn(tableInfo.KeyColumns.First(k => k.IsIdentity).ColumnName)} = @@IDENTITY;");
            }
            else
            {
                selectcmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");
            }

            var wasClosed = connection.State == ConnectionState.Closed;
            if (wasClosed) connection.Open();

            await connection.ExecuteAsync(cmd.ToString(), entityToInsert, transaction, commandTimeout);
            var r = await connection.QueryAsync(selectcmd.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout);

            if (wasClosed) connection.Close();

            var vals = r.ToList();

            if (!vals.Any()) return false;

            var rvals = ((IDictionary<string, object>)vals[0]);

            foreach (var key in rvals.Keys)
            {
                var rval = rvals[key];
                var p = tableInfo.GeneratedColumns.Single(gp => gp.ColumnName == key).Property;
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
    public override async Task<bool> UpdateAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToUpdate, IEnumerable<string> columnsToUpdate)
    {
        var cmd = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

        if (tableInfo.GeneratedColumns.Any() && tableInfo.KeyColumns.Any())
        {
            var selectcmd = new StringBuilder($"select {EscapeColumnList(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
            selectcmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

            await connection.ExecuteAsync(cmd.ToString(), entityToUpdate, transaction, commandTimeout);
            var r = await connection.QueryAsync(selectcmd.ToString(), entityToUpdate, transaction, commandTimeout: commandTimeout);

            var vals = r.ToList();

            if (!vals.Any()) return false;

            var rvals = ((IDictionary<string, object>)vals[0]);

            foreach (var key in rvals.Keys)
            {
                var rval = rvals[key];
                var p = tableInfo.GeneratedColumns.Single(gp => gp.ColumnName == key).Property;
                p.SetValue(entityToUpdate, Convert.ChangeType(rval, p.PropertyType), null);
            }

            return true;
        }
        else
        {
            return await connection.ExecuteAsync (cmd.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
        }
    }

}

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
    public override async Task<bool> InsertAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToInsert)
    {
        var cmd = new StringBuilder(InsertQuery(tableInfo));

        if (tableInfo.GeneratedColumns.Any() && tableInfo.KeyColumns.Any())
        {
            cmd.Append($"select {EscapeColumnList(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");

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
                var p = tableInfo.GeneratedColumns.Single(gp => gp.ColumnName == key).Property;
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
    public override async Task<bool> UpdateAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToUpdate, IEnumerable<string> columnsToUpdate)
    {
        var cmd = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

        if (tableInfo.GeneratedColumns.Any() && tableInfo.KeyColumns.Any())
        {
            cmd.Append($"select {EscapeColumnList(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
            cmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

            var multi = await connection.QueryMultipleAsync(cmd.ToString(), entityToUpdate, transaction, commandTimeout);

            var vals = multi.Read().ToList();

            if (!vals.Any()) return false;

            var rvals = ((IDictionary<string, object>)vals[0]);

            foreach (var key in rvals.Keys)
            {
                var rval = rvals[key];
                var p = tableInfo.GeneratedColumns.Single(gp => gp.ColumnName == key).Property;
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

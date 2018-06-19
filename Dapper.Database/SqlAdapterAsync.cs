using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;


public partial interface ISqlAdapter
{
    /// <summary>
    /// Inserts <paramref name="entityToInsert"/> into the database, returning the Id of the row created.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <param name="commandTimeout">The command timeout to use.</param>
    /// <param name="tableName">The table to insert into.</param>
    /// <param name="columnList">The columns to set with this insert.</param>
    /// <param name="parameterList">The parameters to set for this insert.</param>
    /// <param name="keyProperties">The key columns in this table.</param>
    /// <param name="entityToInsert">The entity to insert.</param>
    /// <returns>true if inserted</returns>
    Task<bool> InsertAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert);
}

public partial class SqlServerAdapter
{
    /// <summary>
    /// Inserts <paramref name="entityToInsert"/> into the database, returning the Id of the row created.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <param name="commandTimeout">The command timeout to use.</param>
    /// <param name="tableName">The table to insert into.</param>
    /// <param name="columnList">The columns to set with this insert.</param>
    /// <param name="parameterList">The parameters to set for this insert.</param>
    /// <param name="keyProperties">The key columns in this table.</param>
    /// <param name="entityToInsert">The entity to insert.</param>
    /// <returns>true if inserted</returns>
    public async Task<bool> InsertAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert)
    {
        var cmd = $"INSERT INTO {tableName} ({columnList}) values ({parameterList}); SELECT SCOPE_IDENTITY() id";
        var multi = await connection.QueryMultipleAsync(cmd, entityToInsert, transaction, commandTimeout).ConfigureAwait(false);

        var first = multi.Read().FirstOrDefault();
        if (first == null || first.id == null) return false;

        var id = (int)first.id;
        var pi = keyProperties as PropertyInfo[] ?? keyProperties.ToArray();
        if (pi.Length == 0) return false;

        var idp = pi[0];
        idp.SetValue(entityToInsert, Convert.ChangeType(id, idp.PropertyType), null);

        return true;
    }
}

public partial class SqlCeServerAdapter
{
    /// <summary>
    /// Inserts <paramref name="entityToInsert"/> into the database, returning the Id of the row created.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <param name="commandTimeout">The command timeout to use.</param>
    /// <param name="tableName">The table to insert into.</param>
    /// <param name="columnList">The columns to set with this insert.</param>
    /// <param name="parameterList">The parameters to set for this insert.</param>
    /// <param name="keyProperties">The key columns in this table.</param>
    /// <param name="entityToInsert">The entity to insert.</param>
    /// <returns>true if inserted</returns>
    public async Task<bool> InsertAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert)
    {
        var cmd = $"INSERT INTO {tableName} ({columnList}) VALUES ({parameterList})";
        await connection.ExecuteAsync(cmd, entityToInsert, transaction, commandTimeout).ConfigureAwait(false);
        var r = (await connection.QueryAsync<dynamic>("SELECT @@IDENTITY id", transaction: transaction, commandTimeout: commandTimeout).ConfigureAwait(false)).ToList();

        if (r[0] == null || r[0].id == null) return false;
        var id = (int)r[0].id;

        var pi = keyProperties as PropertyInfo[] ?? keyProperties.ToArray();
        if (pi.Length == 0) return false;

        var idp = pi[0];
        idp.SetValue(entityToInsert, Convert.ChangeType(id, idp.PropertyType), null);

        return true;
    }
}

public partial class MySqlAdapter
{
    /// <summary>
    /// Inserts <paramref name="entityToInsert"/> into the database, returning the Id of the row created.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <param name="commandTimeout">The command timeout to use.</param>
    /// <param name="tableName">The table to insert into.</param>
    /// <param name="columnList">The columns to set with this insert.</param>
    /// <param name="parameterList">The parameters to set for this insert.</param>
    /// <param name="keyProperties">The key columns in this table.</param>
    /// <param name="entityToInsert">The entity to insert.</param>
    /// <returns>true if inserted</returns>
    public async Task<bool> InsertAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName,
        string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert)
    {
        var cmd = $"INSERT INTO {tableName} ({columnList}) VALUES ({parameterList})";
        await connection.ExecuteAsync(cmd, entityToInsert, transaction, commandTimeout).ConfigureAwait(false);
        var r = await connection.QueryAsync<dynamic>("SELECT LAST_INSERT_ID() id", transaction: transaction, commandTimeout: commandTimeout).ConfigureAwait(false);

        var id = r.First().id;
        if (id == null) return false;
        var pi = keyProperties as PropertyInfo[] ?? keyProperties.ToArray();
        if (pi.Length == 0) return Convert.ToInt32(id);

        var idp = pi[0];
        idp.SetValue(entityToInsert, Convert.ChangeType(id, idp.PropertyType), null);

        return true;
    }
}

public partial class PostgresAdapter
{
    /// <summary>
    /// Inserts <paramref name="entityToInsert"/> into the database, returning the Id of the row created.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <param name="commandTimeout">The command timeout to use.</param>
    /// <param name="tableName">The table to insert into.</param>
    /// <param name="columnList">The columns to set with this insert.</param>
    /// <param name="parameterList">The parameters to set for this insert.</param>
    /// <param name="keyProperties">The key columns in this table.</param>
    /// <param name="entityToInsert">The entity to insert.</param>
    /// <returns>true if inserted</returns>
    public async Task<bool> InsertAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert)
    {
        var sb = new StringBuilder();
        sb.AppendFormat("INSERT INTO {0} ({1}) VALUES ({2})", tableName, columnList, parameterList);

        // If no primary key then safe to assume a join table with not too much data to return
        var propertyInfos = keyProperties as PropertyInfo[] ?? keyProperties.ToArray();
        if (propertyInfos.Length == 0)
        {
            sb.Append(" RETURNING *");
        }
        else
        {
            sb.Append(" RETURNING ");
            bool first = true;
            foreach (var property in propertyInfos)
            {
                if (!first)
                    sb.Append(", ");
                first = false;
                sb.Append(property.Name);
            }
        }

        var results = await connection.QueryAsync(sb.ToString(), entityToInsert, transaction, commandTimeout).ConfigureAwait(false);

        // Return the key by assinging the corresponding property in the object - by product is that it supports compound primary keys
        var id = 0;
        foreach (var p in propertyInfos)
        {
            var value = ((IDictionary<string, object>)results.First())[p.Name.ToLower()];
            p.SetValue(entityToInsert, value, null);
            if (id == 0)
                id = Convert.ToInt32(value);
        }
        return true;
    }
}

public partial class SQLiteAdapter
{
    /// <summary>
    /// Inserts <paramref name="entityToInsert"/> into the database, returning the Id of the row created.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <param name="commandTimeout">The command timeout to use.</param>
    /// <param name="tableName">The table to insert into.</param>
    /// <param name="columnList">The columns to set with this insert.</param>
    /// <param name="parameterList">The parameters to set for this insert.</param>
    /// <param name="keyProperties">The key columns in this table.</param>
    /// <param name="entityToInsert">The entity to insert.</param>
    /// <returns>true if inserted</returns>
    public async Task<bool> InsertAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert)
    {
        var cmd = $"INSERT INTO {tableName} ({columnList}) VALUES ({parameterList}); SELECT last_insert_rowid() id";
        var multi = await connection.QueryMultipleAsync(cmd, entityToInsert, transaction, commandTimeout).ConfigureAwait(false);

        var id = (int)multi.Read().First().id;
        var pi = keyProperties as PropertyInfo[] ?? keyProperties.ToArray();
        if (pi.Length == 0) return false;

        var idp = pi[0];
        idp.SetValue(entityToInsert, Convert.ChangeType(id, idp.PropertyType), null);

        return true;
    }
}

public partial class FbAdapter
{
    /// <summary>
    /// Inserts <paramref name="entityToInsert"/> into the database, returning the Id of the row created.
    /// </summary>
    /// <param name="connection">The connection to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <param name="commandTimeout">The command timeout to use.</param>
    /// <param name="tableName">The table to insert into.</param>
    /// <param name="columnList">The columns to set with this insert.</param>
    /// <param name="parameterList">The parameters to set for this insert.</param>
    /// <param name="keyProperties">The key columns in this table.</param>
    /// <param name="entityToInsert">The entity to insert.</param>
    /// <returns>true if inserted</returns>
    public async Task<bool> InsertAsync(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert)
    {
        var cmd = $"insert into {tableName} ({columnList}) values ({parameterList})";
        await connection.ExecuteAsync(cmd, entityToInsert, transaction, commandTimeout).ConfigureAwait(false);

        var propertyInfos = keyProperties as PropertyInfo[] ?? keyProperties.ToArray();
        var keyName = propertyInfos[0].Name;
        var r = await connection.QueryAsync($"SELECT FIRST 1 {keyName} ID FROM {tableName} ORDER BY {keyName} DESC", transaction: transaction, commandTimeout: commandTimeout).ConfigureAwait(false);

        var id = r.First().ID;
        if (id == null) return false;
        if (propertyInfos.Length == 0) return Convert.ToInt32(id);

        var idp = propertyInfos[0];
        idp.SetValue(entityToInsert, Convert.ChangeType(id, idp.PropertyType), null);

        return true;
    }
}

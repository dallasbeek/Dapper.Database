using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using Dapper;
using Dapper.Database;
using Dapper.Database.Adapters;

///// <summary>
///// The Firebase SQL adapeter.
///// </summary>
//public partial class FbAdapter : SqlAdapter, ISqlAdapter
//{
//    /// <summary>
//    /// Inserts <paramref name="entityToInsert"/> into the database, returning the Id of the row created.
//    /// </summary>
//    /// <param name="connection">The connection to use.</param>
//    /// <param name="transaction">The transaction to use.</param>
//    /// <param name="commandTimeout">The command timeout to use.</param>
//    /// <param name="tableName">The table to insert into.</param>
//    /// <param name="columnList">The columns to set with this insert.</param>
//    /// <param name="parameterList">The parameters to set for this insert.</param>
//    /// <param name="keyProperties">The key columns in this table.</param>
//    /// <param name="entityToInsert">The entity to insert.</param>
//    /// <returns>true if inserted</returns>
//    public bool Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<ColumnInfo> keyProperties, object entityToInsert)
//    {
//        var cmd = $"insert into {tableName} ({columnList}) values ({parameterList})";
//        connection.Execute(cmd, entityToInsert, transaction, commandTimeout);

//        var propertyInfos = keyProperties.Select(p => p.Property).ToArray();
//        var keyName = propertyInfos[0].Name;
//        var r = connection.Query($"SELECT FIRST 1 {keyName} ID FROM {tableName} ORDER BY {keyName} DESC", transaction: transaction, commandTimeout: commandTimeout);

//        var id = r.First().ID;
//        if (id != null && propertyInfos.Any())
//        {
//            var idp = propertyInfos[0];
//            idp.SetValue(entityToInsert, Convert.ChangeType(id, idp.PropertyType), null);
//        }

//        return id != null;
//    }

//    /// <summary>
//    /// Returns the format for table name
//    /// </summary>
//    public override string EscapeTableName(string value) => $"{value}";

//    /// <summary>
//    /// Returns the format for column
//    /// </summary>
//    public override string EscapeColumnn(string value) => $"{value}";


//}

///// <summary>
///// The MySQL database adapter.
///// </summary>
//public partial class MySqlAdapter : SqlAdapter, ISqlAdapter
//{
//    /// <summary>
//    /// Inserts an entity into table "Ts"
//    /// </summary>
//    /// <param name="connection">Open SqlConnection</param>
//    /// <param name="transaction">The transaction to run under, null (the default) if none</param>
//    /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
//    /// <param name="tableInfo">Table information</param>
//    /// <param name="entityToInsert">Entity to insert</param>
//    /// <returns>true if the entity was inserted</returns>
//    public bool Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<ColumnInfo> keyProperties, object entityToInsert)
//    {
//        var cmd = $"insert into {tableName} ({columnList}) values ({parameterList})";
//        connection.Execute(cmd, entityToInsert, transaction, commandTimeout);
//        var r = connection.Query("Select LAST_INSERT_ID() id", transaction: transaction, commandTimeout: commandTimeout);

//        var id = r.First().id;
//        if (id == null) return false;
//        var propertyInfos = keyProperties.Select(p => p.Property).ToArray();
//        if (propertyInfos.Length == 0) return Convert.ToInt32(id);

//        var idp = propertyInfos[0];
//        idp.SetValue(entityToInsert, Convert.ChangeType(id, idp.PropertyType), null);

//        return id != null;
//    }

//    /// <summary>
//    /// Applies a schema name is one is specified
//    /// </summary>
//    /// <param name="tableInfo"></param>
//    /// <returns></returns>
//    public override string EscapeTableName(TableInfo tableInfo) =>
//        (!string.IsNullOrEmpty(tableInfo.SchemaName) ? EscapeTableName(tableInfo.SchemaName) + "." : null) + EscapeTableName(tableInfo.TableName);

//    /// <summary>
//    /// Returns the format for table name
//    /// </summary>
//    public override string EscapeTableName(string value) => $"`{value}`";

//    /// <summary>
//    /// Returns the format for column
//    /// </summary>
//    public override string EscapeColumnn(string value) => $"`{value}`";

//}

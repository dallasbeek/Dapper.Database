using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Dapper;
using Dapper.Database;


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
    bool Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToInsert);

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
    bool Update(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToUpdate, IEnumerable<string> columnsToUpdate);

    /// <summary>
    /// constructs an insert query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <returns>An insert sql statement</returns>
    string InsertQuery(TableInfo tableInfo);

    /// <summary>
    /// Returns an update query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="columnsToUpdate">columns to be updated</param>
    /// <returns>An update sql statement</returns>
    string UpdateQuery(TableInfo tableInfo, IEnumerable<string> columnsToUpdate);

    /// <summary>
    /// Returns a count query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <returns>A count sql statement</returns>
    string CountQuery(TableInfo tableInfo, string sql);

    /// <summary>
    /// Returns a delete query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <returns>A delete sql statement</returns>
    string DeleteQuery(TableInfo tableInfo, string sql);

    /// <summary>
    /// Returns an exists query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <returns>An exists sql statement</returns>
    string ExistsQuery(TableInfo tableInfo, string sql);

    /// <summary>
    /// Returns a get query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <param name="fromCache"></param>
    /// <returns>A get sql statement</returns>
    string GetQuery(TableInfo tableInfo, string sql, bool fromCache = false);

    /// <summary>
    /// Returns a get list query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <returns>A get list statement</returns>
    string GetListQuery(TableInfo tableInfo, string sql);


    /// <summary>
    /// Returns a get paged list query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="page">the page requested</param>
    /// <param name="pageSize">number of records to return</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <returns>A paginated get sql statement</returns>
    string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql);
}

/// <summary>
/// Base class for SqlAdapter handlers - provides default/common handling for different database engines
/// </summary>
public abstract partial class SqlAdapter
{
    /// <summary>
    /// Regex to determine if sql has select/execute/call
    /// </summary>
    protected static readonly Regex rxSelect = new Regex(@"\A\s*(SELECT|EXECUTE|CALL)\s", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);

    /// <summary>
    /// Regex to determine if sql has delete
    /// </summary>
    protected static readonly Regex rxDelete = new Regex(@"\A\s*DELETE\s", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);

    /// <summary>
    /// Regex to determine if sql has from
    /// </summary>
    protected static readonly Regex rxFrom = new Regex(@"\A\s*FROM\s", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);

    /// <summary>
    /// Regex to select columns from sql
    /// </summary>
    protected static readonly Regex rxColumns = new Regex(@"\A\s*SELECT\s+((?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|.)*?)(?<!,\s+)\bFROM\b", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

    /// <summary>
    /// Regex to retrieve order by
    /// </summary>
    protected static readonly Regex rxOrderBy = new Regex(@"\bORDER\s+BY\s+(?!.*?(?:\)|\s+)AS\s)(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\w\(\)\[\]\.])+(?:\s+(?:ASC|DESC))?(?:\s*,\s*(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\w\(\)\[\]\.])+(?:\s+(?:ASC|DESC))?)*", RegexOptions.RightToLeft | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

    /// <summary>
    /// Regex to retrieve order by
    /// </summary>
    protected static readonly Regex rxWhere = new Regex(@"\bWHERE\s", RegexOptions.RightToLeft | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);
    //protected static readonly Regex rxDistinct = new Regex(@"\ADISTINCT\s", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

    /// <summary>
    /// Cache for Get Queries
    /// </summary>
    private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> GetQueries = new ConcurrentDictionary<RuntimeTypeHandle, string>();

    /// <summary>
    /// Cache for Insert Queries
    /// </summary>
    private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> InsertQueries = new ConcurrentDictionary<RuntimeTypeHandle, string>();
 
    /// <summary>
    /// Cache for Update Queries
    /// </summary>
    private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> UpdateQueries = new ConcurrentDictionary<RuntimeTypeHandle, string>();

    /// <summary>
    /// Inserts an entity into table "Ts"
    /// </summary>
    /// <param name="connection">Open SqlConnection</param>
    /// <param name="transaction">The transaction to run under, null (the default) if none</param>
    /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="entityToInsert">Entity to insert</param>
    /// <returns>true if the entity was inserted</returns>
    public virtual bool Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToInsert)
    {
        return false;
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
    public virtual bool Update(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToUpdate, IEnumerable<string> columnsToUpdate)
    {
        return false;
    }

    /// <summary>
    /// Default implementation of an insert query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <returns>An insert sql statement</returns>
    public virtual string InsertQuery(TableInfo tableInfo)
    {
        return InsertQueries.Acquire(
            tableInfo.ClassType.TypeHandle,
            () => true,
            () => $"insert into { EscapeTableName(tableInfo)} ({EscapeColumnList(tableInfo.InsertColumns)}) values ({EscapeParameters(tableInfo.InsertColumns)}); "
        );
    }


    /// <summary>
    /// Default implementation of an update query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="columnsToUpdate">columns to be updated</param>
    /// <returns>An update sql statement</returns>
    public virtual string UpdateQuery(TableInfo tableInfo, IEnumerable<string> columnsToUpdate)
    {
        return UpdateQueries.Acquire(
            tableInfo.ClassType.TypeHandle,
            () => columnsToUpdate == null || !columnsToUpdate.Any(),
            () =>
            {
                var updates = tableInfo.UpdateColumns.Where(ci => (columnsToUpdate == null || !columnsToUpdate.Any() || columnsToUpdate.Contains(ci.PropertyName)));
                return $"update {EscapeTableName(tableInfo)} set {EscapeAssignmentList(updates)} where {EscapeWhereList(tableInfo.KeyColumns)}; ";
            }
        );

    }

    /// <summary>
    /// Default implementation of a count query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <returns>A sql statement that selects the count of matching records</returns>
    public virtual string CountQuery(TableInfo tableInfo, string sql)
    {
        var q = sql ?? "";

        if (q.StartsWith(";"))
            return q.Substring(1);

        var m = rxOrderBy.Match(q);

        if (m.Success)
        {
            q = q.Replace(m.Groups[0].ToString(), "");
        }

        if (!rxSelect.IsMatch(q))
        {
            if (!rxFrom.IsMatch(q))
                return $"select count(*) from { EscapeTableName(tableInfo)} {q}";
            else
                return $"select count(*) {q}";

        }

        return $"select count(*) from ({q}) calc_inner";
    }

    /// <summary>
    /// Default implementation of a delete query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <returns>A sql statement that deletes</returns>
    public virtual string DeleteQuery(TableInfo tableInfo, string sql)
    {
        var q = sql ?? "";

        if (q.StartsWith(";"))
            return q.Substring(1);

        var m = rxOrderBy.Match(q);

        if (m.Success)
        {
            q = q.Replace(m.Groups[0].ToString(), "");
        }

        if (!rxDelete.IsMatch(q))
        {
            if (!rxFrom.IsMatch(q))
                return $"delete from { EscapeTableName(tableInfo)} {q}";
            else
                return $"delete {q}";

        }
        return $"delete from ({q}) calc_inner";
    }

    /// <summary>
    /// Default implementation of an Exists query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <returns>A sql statement that selects true if a record matches</returns>
    public virtual string ExistsQuery(TableInfo tableInfo, string sql)
    {
        var q = sql ?? "";

        if (q.StartsWith(";"))
            return q.Substring(1);

        if (!rxSelect.IsMatch(q))
        {
            var wc = string.IsNullOrWhiteSpace(q) ? $"where {EscapeWhereList(tableInfo.KeyColumns)}" : q;

            if (!rxFrom.IsMatch(q))
                return $"select 1 where exists (select 1 from { EscapeTableName(tableInfo)} {wc})";
            else
                return $"select 1 where exists (select 1 {wc})";

        }
        return $"select 1 where exists ({sql})";
    }

    /// <summary>
    /// Default implementation of a Get Query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <param name="cache">true if this query should be cached</param>
    /// <returns>A sql statement that selects a single item</returns>
    public virtual string GetQuery(TableInfo tableInfo, string sql, bool cache = false)
    {
        var q = sql ?? "";

        if (q.StartsWith(";"))
            return q.Substring(1);

        if (!rxSelect.IsMatch(q))
        {
            return GetQueries.Acquire(
                tableInfo.ClassType.TypeHandle,
                () => cache && string.IsNullOrEmpty(q),
                () =>
                {
                    var wc = string.IsNullOrWhiteSpace(q) ? $"where {EscapeWhereList(tableInfo.KeyColumns)}" : q;

                    if (!rxFrom.IsMatch(q))
                        return $"select {EscapeColumnList(tableInfo.SelectColumns, tableInfo.TableName)} from { EscapeTableName(tableInfo)} {wc}";
                    else
                        return $"select {EscapeColumnList(tableInfo.SelectColumns, tableInfo.TableName)} {wc}";
                }
            );

        }
        return sql;

    }


    /// <summary>
    /// Default implementation of a Get List query
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <returns>A sql statement</returns>
    public virtual string GetListQuery(TableInfo tableInfo, string sql)
    {
        var q = sql ?? "";

        if (q.StartsWith(";"))
            return q.Substring(1);

        if (!rxSelect.IsMatch(q))
        {
            var wc = string.IsNullOrWhiteSpace(q) ? $"where {EscapeWhereList(tableInfo.KeyColumns)}" : q;

            if (!rxFrom.IsMatch(q))
                return $"select {EscapeColumnList(tableInfo.SelectColumns, tableInfo.TableName)} from { EscapeTableName(tableInfo)} {wc}";
            else
                return $"select {EscapeColumnList(tableInfo.SelectColumns, tableInfo.TableName)} {wc}";
        }
        return sql;

    }

    /// <summary>
    /// Default implementation of a a paged sql statement
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="page">the page to request</param>
    /// <param name="pageSize">the size of the page to request</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <returns>A paginated sql statement</returns>
    public virtual string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql)
    {
        var selectQuery = GetListQuery(tableInfo, sql);

        var m = rxColumns.Match(selectQuery);
        var g = m.Groups[1];
        var sqlSelectRemoved = selectQuery.Substring(g.Index);
        var sqlOrderBy = string.Empty;

        var pageSkip = (page - 1) * pageSize;

        m = rxOrderBy.Match(selectQuery);

        if (m.Success)
        {
            g = m.Groups[0];
            sqlOrderBy = g.ToString();
        }
        else if (tableInfo.KeyColumns.Any())
        {
            sqlOrderBy = $"order by {EscapeColumnn(tableInfo.KeyColumns.First().ColumnName)}";
        }


        return $"select {rxOrderBy.Replace(sqlSelectRemoved, "", 1)} {sqlOrderBy} limit {pageSize} offset {pageSkip}";

    }

    /// <summary>
    /// Returns the format for table name
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <returns></returns>
    public virtual string EscapeTableName(TableInfo tableInfo) => EscapeTableName(tableInfo.TableName);

    /// <summary>
    /// Returns the format for table name
    /// </summary>
    public virtual string EscapeTableName(string value) => $"[{value}]";

    /// <summary>
    /// Returns the format for column
    /// </summary>
    public virtual string EscapeColumnn(string value) => $"[{value}]";

    /// <summary>
    /// Returns the format for parameter
    /// </summary>
    public virtual string EscapeParameter(string value) => $"@{value}";

    /// <summary>
    /// Returns the format for columns
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="tableName"></param> 
    /// <returns></returns>
    public virtual string EscapeColumnList(IEnumerable<ColumnInfo> columns, string tableName = null) => string.Join(", ", columns.Select(ci => (tableName != null ? EscapeTableName(tableName) + "." : "") + EscapeColumnn(ci.ColumnName) + (ci.ColumnName != ci.PropertyName ? " AS " + EscapeColumnn(ci.PropertyName) : "")));

    /// <summary>
    /// Returns the format for where clause
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    public virtual string EscapeWhereList(IEnumerable<ColumnInfo> columns) => string.Join(" and ", columns.Select(ci => $"{EscapeColumnn(ci.ColumnName)} = {EscapeParameter(ci.PropertyName)}"));

    /// <summary>
    /// Returns the format for parameters
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    public virtual string EscapeParameters(IEnumerable<ColumnInfo> columns) => string.Join(", ", columns.Select(ci => EscapeParameter(ci.PropertyName)));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    public virtual string EscapeAssignmentList(IEnumerable<ColumnInfo> columns) => string.Join(", ", columns.Select(ci => $"{EscapeColumnn(ci.ColumnName)} = {EscapeParameter(ci.PropertyName)}"));


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
    public override bool Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToInsert)
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

            var multi = connection.QueryMultiple(cmd.ToString(), entityToInsert, transaction, commandTimeout);

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
    public override bool Update(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToUpdate, IEnumerable<string> columnsToUpdate)
    {
        var cmd = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

        if (tableInfo.GeneratedColumns.Any() && tableInfo.KeyColumns.Any())
        {
            cmd.Append($"select {EscapeColumnList(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
            cmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

            var multi = connection.QueryMultiple(cmd.ToString(), entityToUpdate, transaction, commandTimeout);

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
            return connection.Execute(cmd.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
        }
    }

    /// <summary>
    /// Constructs a paged sql statement
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="page">the page to request</param>
    /// <param name="pageSize">the size of the page to request</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <returns>A paginated sql statement</returns>
    public override string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql)
    {
        var selectQuery = GetListQuery(tableInfo, sql);

        var m = rxColumns.Match(selectQuery);
        var g = m.Groups[1];
        var sqlSelectRemoved = selectQuery.Substring(g.Index);
        var sqlOrderBy = "order by (select null)";

        var pageSkip = (page - 1) * pageSize;
        var pageTake = pageSkip + pageSize;

        m = rxOrderBy.Match(selectQuery);

        if (m.Success)
        {
            g = m.Groups[0];
            sqlOrderBy = g.ToString();
        }
        else if (tableInfo.KeyColumns.Any())
        {
            sqlOrderBy = $"order by {EscapeColumnn(tableInfo.KeyColumns.First().ColumnName)}";
        }

        // to prevent duplicate columns in subquery
        //if (sqlSelectRemoved.Contains("*") && tableInfo.KeyColumns.Any())
        //{
        //    var keyColumns = EscapeColumnList(tableInfo.KeyColumns);

        //    var joinQuery = $"select {keyColumns}, row_number() over ({sqlOrderBy}) page_rn {sqlSelectRemoved.Substring(g.Length)}";
        //    var sqlOrderWhereRemoved = rxOrderBy.Replace(sqlSelectRemoved, "", 1);
        //    var w = rxWhere.Match(sqlOrderWhereRemoved);
        //    if (w.Success)
        //    {
        //        sqlOrderWhereRemoved = sqlOrderWhereRemoved.Substring(0,  w.Index);
        //    }
        //    var qq = $"select {sqlOrderWhereRemoved} join ({joinQuery}) page_join on xxxx where page_rn > {pageSkip} and page_rn <= {pageTake}";

        //}

        var columnsOnly = $"page_inner.* FROM (select {rxOrderBy.Replace(sqlSelectRemoved, "", 1)}) page_inner";

        return $"select * from (select  row_number() over ({sqlOrderBy}) page_rn, {columnsOnly}) page_outer where page_rn > {pageSkip} and page_rn <= {pageTake}";

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <returns></returns>
    public override string EscapeTableName(TableInfo tableInfo) =>
        (!string.IsNullOrEmpty(tableInfo.SchemaName) ? EscapeTableName(tableInfo.SchemaName) + "." : null) + EscapeTableName(tableInfo.TableName);

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
    public override bool Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToInsert)
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

            connection.Execute(cmd.ToString(), entityToInsert, transaction, commandTimeout);
            var r = connection.Query(selectcmd.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout);

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
    public override bool Update(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToUpdate, IEnumerable<string> columnsToUpdate)
    {
        var cmd = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

        if (tableInfo.GeneratedColumns.Any() && tableInfo.KeyColumns.Any())
        {
            var selectcmd = new StringBuilder($"select {EscapeColumnList(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
            selectcmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

            connection.Execute(cmd.ToString(), entityToUpdate, transaction, commandTimeout);
            var r = connection.Query(selectcmd.ToString(), entityToUpdate, transaction, commandTimeout: commandTimeout);

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
            return connection.Execute(cmd.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
        }
    }

    /// <summary>
    /// Constructs a paged sql statement
    /// </summary>
    /// <param name="tableInfo">table information about the entity</param>
    /// <param name="page">the page to request</param>
    /// <param name="pageSize">the size of the page to request</param>
    /// <param name="sql">a sql statement or partial statement</param>
    /// <returns>A paginated sql statement</returns>
    public override string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql)
    {
        var selectQuery = GetListQuery(tableInfo, sql);

        var m = rxColumns.Match(selectQuery);
        var g = m.Groups[1];
        var sqlSelectRemoved = selectQuery.Substring(g.Index);
        var sqlOrderBy = string.Empty;

        var pageSkip = (page - 1) * pageSize;

        m = rxOrderBy.Match(selectQuery);

        if (m.Success)
        {
            g = m.Groups[0];
            sqlOrderBy = g.ToString();
        }
        else if (tableInfo.KeyColumns.Any())
        {
            sqlOrderBy = $"order by {EscapeColumnn(tableInfo.KeyColumns.First().ColumnName)}";
        }

        return $"select {rxOrderBy.Replace(sqlSelectRemoved, "", 1)} {sqlOrderBy} offset {pageSkip} rows fetch next {pageSize} rows only";

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
    public override bool Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToInsert)
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

            var multi = connection.QueryMultiple(cmd.ToString(), entityToInsert, transaction, commandTimeout);

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
    public override bool Update(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToUpdate, IEnumerable<string> columnsToUpdate)
    {
        var cmd = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

        if (tableInfo.GeneratedColumns.Any() && tableInfo.KeyColumns.Any())
        {
            cmd.Append($"select {EscapeColumnList(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
            cmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

            var multi = connection.QueryMultiple(cmd.ToString(), entityToUpdate, transaction, commandTimeout);

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
            return connection.Execute(cmd.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
        }
    }
}

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

///// <summary>
///// The Postgres database adapter.
///// </summary>
//public partial class PostgresAdapter : SqlAdapter, ISqlAdapter
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
//        var sb = new StringBuilder();
//        sb.AppendFormat("insert into {0} ({1}) values ({2})", tableName, columnList, parameterList);

//        // If no primary key then safe to assume a join table with not too much data to return
//        var propertyInfos = keyProperties.Select(p => p.Property).ToArray();
//        if (propertyInfos.Length == 0)
//        {
//            sb.Append(" RETURNING *");
//        }
//        else
//        {
//            sb.Append(" RETURNING ");
//            var first = true;
//            foreach (var property in propertyInfos)
//            {
//                if (!first)
//                    sb.Append(", ");
//                first = false;
//                sb.Append(property.Name);
//            }
//        }

//        var results = connection.Query(sb.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout).ToList();

//        // Return the key by assinging the corresponding property in the object - by product is that it supports compound primary keys
//        var id = 0;
//        foreach (var p in propertyInfos)
//        {
//            var value = ((IDictionary<string, object>)results[0])[p.Name.ToLower()];
//            p.SetValue(entityToInsert, value, null);
//            if (id == 0)
//                id = Convert.ToInt32(value);
//        }
//        return id > 0;
//    }


//    /// <summary>
//    /// Applies a schema name is one is specified
//    /// </summary>
//    /// <param name="tableInfo"></param>
//    /// <returns></returns>
//    public override string EscapeTableName(TableInfo tableInfo) =>
//        (!string.IsNullOrEmpty(tableInfo.SchemaName) ? EscapeTableName(tableInfo.SchemaName) + "." : null) + EscapeTableName(tableInfo.TableName);

//    /// <summary>
//    /// Returns the format for column
//    /// </summary>
//    public override string EscapeColumnn(string value) => $"\"{value}\"";

//}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using Dapper;
using Dapper.Database;
using Dapper.Database.Adapters;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{
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
            var q = new SqlParser(GetListQuery(tableInfo, sql));
            var pageSkip = (page - 1) * pageSize;

            var sqlOrderBy = string.Empty;

            if (string.IsNullOrEmpty(q.OrderByClause) && tableInfo.KeyColumns.Any())
            {
                sqlOrderBy = $"order by {EscapeColumnn(tableInfo.KeyColumns.First().ColumnName)}";
            }

            //return $"{q.Sql} {sqlOrderBy} limit {pageSize} offset {pageSkip}";
            return $"{q.Sql} {sqlOrderBy} offset {pageSkip} rows fetch next {pageSize} rows only";

            //var selectQuery = GetListQuery(tableInfo, sql);

            //var m = rxColumns.Match(selectQuery);
            //var g = m.Groups[1];
            //var sqlSelectRemoved = selectQuery.Substring(g.Index);
            //var sqlOrderBy = string.Empty;

            //var pageSkip = (page - 1) * pageSize;

            //m = rxOrderBy.Match(selectQuery);

            //if (m.Success)
            //{
            //    g = m.Groups[0];
            //    sqlOrderBy = g.ToString();
            //}
            //else if (tableInfo.KeyColumns.Any())
            //{
            //    sqlOrderBy = $"order by {EscapeColumnn(tableInfo.KeyColumns.First().ColumnName)}";
            //}

            //return $"select {rxOrderBy.Replace(sqlSelectRemoved, "", 1)} {sqlOrderBy} offset {pageSkip} rows fetch next {pageSize} rows only";

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
                return await connection.ExecuteAsync(cmd.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
            }
        }

    }
}

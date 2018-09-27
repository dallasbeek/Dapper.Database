using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{
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
        public override bool Insert<T>(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, T entityToInsert)
        {
            var cmd = new StringBuilder(InsertQuery(tableInfo));

            if (tableInfo.GeneratedColumns.Any())
            {
                var vals = connection.Query(cmd.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout).ToList();

                if (!vals.Any()) return false;

                var rvals = ((IDictionary<string, object>)vals[0]);

                foreach (var key in rvals.Keys)
                {
                    var rval = rvals[key];
                    var p = tableInfo.GeneratedColumns.Single(gp => gp.PropertyName.Equals(key, StringComparison.OrdinalIgnoreCase)).Property;
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
                var vals = connection.Query(cmd.ToString(), entityToUpdate, transaction, commandTimeout: commandTimeout).ToList();

                if (!vals.Any()) return false;

                var rvals = ((IDictionary<string, object>)vals[0]);

                foreach (var key in rvals.Keys)
                {
                    var rval = rvals[key];
                    var p = tableInfo.GeneratedColumns.Single(gp => gp.PropertyName.Equals(key, StringComparison.OrdinalIgnoreCase)).Property;
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
                var rslt = await connection.QueryAsync(cmd.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout);

                var vals = rslt.ToList();

                if (!vals.Any()) return false;

                var rvals = ((IDictionary<string, object>)vals[0]);

                foreach (var key in rvals.Keys)
                {
                    var rval = rvals[key];
                    var p = tableInfo.GeneratedColumns.Single(gp => gp.PropertyName.Equals(key, StringComparison.OrdinalIgnoreCase)).Property;
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
                var rslt = await connection.QueryAsync(cmd.ToString(), entityToUpdate, transaction, commandTimeout: commandTimeout);

                var vals = rslt.ToList();

                if (!vals.Any()) return false;

                var rvals = ((IDictionary<string, object>)vals[0]);

                foreach (var key in rvals.Keys)
                {
                    var rval = rvals[key];
                    var p = tableInfo.GeneratedColumns.Single(gp => gp.PropertyName.Equals(key, StringComparison.OrdinalIgnoreCase)).Property;
                    p.SetValue(entityToUpdate, Convert.ChangeType(rval, p.PropertyType), null);
                }

                return true;
            }
            else
            {
                return await connection.ExecuteAsync(cmd.ToString(), entityToUpdate, transaction, commandTimeout) > 0;
            }
        }

        /// <summary>
        /// Constructs a paged sql statement
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="page">the page to request</param>
        /// <param name="pageSize">the size of the page to request</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <param name="parameters">the dynamic parameters for the query</param>
        /// <returns>A paginated sql statement</returns>
        public override string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql, DynamicParameters parameters)
        {
            var q = new SqlParser(GetListQuery(tableInfo, sql));
            var pageSkip = (page - 1) * pageSize;
            var sqlOrderBy = string.Empty;

            if (string.IsNullOrEmpty(q.OrderByClause) && tableInfo.KeyColumns.Any())
            {
                sqlOrderBy = $"order by {EscapeColumnn(tableInfo.KeyColumns.First().ColumnName)}";
            }

            parameters.Add(PageSizeParamName, pageSize, DbType.Int64);
            parameters.Add(PageSkipParamName, pageSkip, DbType.Int64);

            return $"{q.Sql} {sqlOrderBy} offset {EscapeParameter(PageSkipParamName)} rows fetch next {EscapeParameter(PageSizeParamName)} rows only";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <returns></returns>
        public override string EscapeTableName(TableInfo tableInfo) =>
            (!string.IsNullOrEmpty(tableInfo.SchemaName) ? EscapeTableName(tableInfo.SchemaName) + "." : null) + EscapeTableName(tableInfo.TableName);

        /// <summary>
        /// implementation of an insert query.
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <returns>An insert sql statement</returns>
        protected override string BuildInsertQuery(TableInfo tableInfo)
            => tableInfo.GeneratedColumns.Any()
            ? $"insert into {EscapeTableName(tableInfo)} ({EscapeColumnList(tableInfo.InsertColumns)}) output {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, "inserted")} values ({EscapeParameters(tableInfo.InsertColumns)}) "
            : base.BuildInsertQuery(tableInfo);

        /// <summary>
        /// Default implementation of an update query.
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="columnsToUpdate">columns to be updated</param>
        /// <returns>An update sql statement</returns>
        protected override string BuildUpdateQuery(TableInfo tableInfo, IEnumerable<string> columnsToUpdate)
        {
            if (tableInfo.GeneratedColumns.Any())
            {
                var updates = tableInfo.UpdateColumns.Where(ci => (columnsToUpdate == null || !columnsToUpdate.Any() || columnsToUpdate.Contains(ci.PropertyName)));
                return $"update {EscapeTableName(tableInfo)} set {EscapeAssignmentList(updates)} output {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, "inserted")} where {EscapeWhereList(tableInfo.KeyColumns)}";
            }
            return base.BuildUpdateQuery(tableInfo, columnsToUpdate);
        }

    }
}

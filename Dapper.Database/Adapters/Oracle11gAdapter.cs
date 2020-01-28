using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Dapper.Database.Adapters
{
    /// <summary>
    ///     Oracle database adapter for Oracle 11g.
    /// </summary>
    /// <seealso cref="OracleAdapter">For Oracle 12.1 and later.</seealso>
    // ReSharper disable once InconsistentNaming
    public class Oracle11gAdapter : OracleAdapter
    {
        /// <summary>
        ///     Simulates <c>INSERT..RETURNING</c> with a PL/SQL block that does an <c>INSERT</c> followed by a <c>SELECT</c>.
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <returns>the PL/SQL block</returns>
        /// <remarks>
        ///     <para>
        ///         ORACRAP: The managed ODP.Net drivers do not properly handle variable-sized outputs when running an
        ///         UPDATE..RETURNING against Oracle 11g.
        ///         I suspect this is the result of a difference in what is sent over the wire between 11.2 and 12.2, and the
        ///         managed driver not handling the difference.
        ///     </para>
        ///     <para>
        ///         This holds true even if we run UPDATE..RETURNING in a PL/SQL block.
        ///         However, it *does* properly work on INSERT..RETURNING and a PL/SQL block doing UPDATE, then SELECT.
        ///         There should be no risk of a race condition as the row should be locked for the duration of the execution.
        ///     </para>
        /// </remarks>
        protected override string BuildInsertQuery(TableInfo tableInfo)
        {
            // If we don't have any outputs, we don't need to do anything different from 12c.
            if (!tableInfo.GeneratedColumns.Any()) return base.BuildInsertQuery(tableInfo);

            // BEGIN
            //  insert...returning...;
            //  :ROW_COUNT$$ := sql%rowcount;
            // END;
            return $"begin {base.BuildInsertQuery(tableInfo)}; :{RowCountParamName} := sql%rowcount; end;";
        }

        /// <inheritdoc />
        protected override DynamicParameters BuildInsertParameters<T>(TableInfo tableInfo, T entityToInsert)
        {
            var parameters = base.BuildInsertParameters(tableInfo, entityToInsert);

            if (tableInfo.GeneratedColumns.Any())
                // Because PL/SQL blocks do not return a row count when executed, we have to get the row count from PL/SQL.
                // This is done via the builtin SQL%rowcount variable.
                parameters.Add(RowCountParamName, null, DbType.Int32, ParameterDirection.Output);

            return parameters;
        }

        /// <summary>
        ///     Simulates <c>UPDATE..RETURNING</c> with a PL/SQL block that does an <c>UPDATE</c> followed by a <c>SELECT</c>.
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <param name="columnsToUpdate"></param>
        /// <returns>the PL/SQL block</returns>
        /// <remarks>
        ///     <para>
        ///         ORACRAP: The managed ODP.Net drivers do not properly handle variable-sized outputs when running an
        ///         UPDATE..RETURNING against Oracle 11g.
        ///         I suspect this is the result of a difference in what is sent over the wire between 11.2 and 12.2, and the
        ///         managed driver not handling the difference.
        ///     </para>
        ///     <para>
        ///         This holds true even if we run UPDATE..RETURNING in a PL/SQL block.
        ///         However, it *does* properly work on INSERT..RETURNING and a PL/SQL block doing UPDATE, then SELECT.
        ///         There should be no risk of a race condition as the row should be locked for the duration of the execution.
        ///     </para>
        /// </remarks>
        protected override string BuildUpdateQuery(TableInfo tableInfo, IEnumerable<string> columnsToUpdate)
        {
            // If we don't have any outputs, we don't need to do anything different from 12c.
            if (!tableInfo.GeneratedColumns.Any()) return base.BuildUpdateQuery(tableInfo, columnsToUpdate);

            // BEGIN
            //  update...;
            //  select...;
            //  :ROW_COUNT$$ := sql%rowcount;
            // END;
            return new StringBuilder()
                .Append("begin ")
                .Append(BuildBaseUpdateQuery(tableInfo, columnsToUpdate))
                .Append($"; select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} ")
                .Append($"into {EscapeReturnParameters(tableInfo.GeneratedColumns)} ")
                .Append($"from {EscapeTableName(tableInfo)} ")
                .Append($"where {EscapeWhereList(tableInfo.ComparisonColumns)}; ")
                .Append($":{RowCountParamName} := SQL%rowcount; ")
                .Append("end;")
                .ToString();
        }

        /// <inheritdoc />
        protected override DynamicParameters BuildUpdateParameters<T>(TableInfo tableInfo, T entityToUpdate)
        {
            var parameters = base.BuildUpdateParameters(tableInfo, entityToUpdate);

            if (tableInfo.GeneratedColumns.Any())
                // Because PL/SQL blocks do not return a row count when executed, we have to get the row count from PL/SQL.
                // This is done via the builtin SQL%rowcount variable.
                parameters.Add(RowCountParamName, null, DbType.Int32, ParameterDirection.Output);

            return parameters;
        }

        /// <summary>
        ///     Constructs a paged sql statement
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="page">the page to request</param>
        /// <param name="pageSize">the size of the page to request</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <param name="parameters">the dynamic parameters for the query</param>
        /// <returns>A paginated sql statement</returns>
        /// <remarks>
        ///     Oracle supports binding <c>offset</c> and <paramref name="pageSize" /> as <paramref name="parameters" />.
        /// </remarks>
        public override string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql,
            DynamicParameters parameters)
        {
            var q = new SqlParser(GetListQuery(tableInfo, sql));
            var pageSkip = (page - 1) * pageSize;
            var sqlOrderBy = string.Empty;
            var sqlOrderByRemoved = q.Sql;

            if (string.IsNullOrEmpty(q.OrderByClause))
            {
                if (tableInfo.KeyColumns.Any())
                    sqlOrderBy = $"order by {EscapeColumn(tableInfo.KeyColumns.First().PropertyName)}";
            }
            else
            {
                sqlOrderBy = q.OrderByClause;
                sqlOrderByRemoved = sqlOrderByRemoved.Replace(q.OrderByClause, "");
            }

            parameters.Add(PageSizeParamName, pageSize, DbType.Int64);
            parameters.Add(PageSkipParamName, pageSkip, DbType.Int64);

            var columnsOnly = $"page_inner.* FROM ({sqlOrderByRemoved}) page_inner";

            return
                $"select * from (select row_number() over ({sqlOrderBy}) page_rn, {columnsOnly}) page_outer where page_rn > {EscapeParameter(PageSkipParamName)} and page_rn <= {EscapeParameter(PageSkipParamName)} + {EscapeParameter(PageSizeParamName)}";
        }
    }
}

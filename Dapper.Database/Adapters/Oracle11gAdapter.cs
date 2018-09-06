using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database.Adapters
{
    /// <summary>
    /// Oracle database adapter for Oracle 11g.
    /// </summary>
    public partial class Oracle11gAdapter : OracleAdapter
    {
        /// <summary>
        /// Simulates <c>INSERT..RETURNING</c> with a PL/SQL block that does an <c>INSERT</c> followed by a <c>SELECT</c>.
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <returns>the PL/SQL block</returns>
        /// <remarks>
        /// <para>
        /// ORACRAP: The managed ODP.Net drivers do not properly handle variable-sized outputs when running an UPDATE..RETURNING against Oracle 11g.
        /// I suspect this is the result of a difference in what is sent over the wire between 11.2 and 12.2, and the managed driver not handling the difference.
        /// </para>
        /// <para>
        /// This holds true even if we run UPDATE..RETURNING in a PL/SQL block.
        /// However, it *does* properly work on INSERT..RETURNING and a PL/SQL block doing UPDATE, then SELECT.
        /// There should be no risk of a race condition as the row should be locked for the duration of the execution.
        /// </para>
        /// </remarks>
        protected override string InsertReturningQuery(TableInfo tableInfo)
        {
            return $"begin {base.InsertReturningQuery(tableInfo)}; end;";
        }

        /// <summary>
        /// Simulates <c>UPDATE..RETURNING</c> with a PL/SQL block that does an <c>UPDATE</c> followed by a <c>SELECT</c>.
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <param name="columnsToUpdate"></param>
        /// <returns>the PL/SQL block</returns>
        /// <remarks>
        /// <para>
        /// ORACRAP: The managed ODP.Net drivers do not properly handle variable-sized outputs when running an UPDATE..RETURNING against Oracle 11g.
        /// I suspect this is the result of a difference in what is sent over the wire between 11.2 and 12.2, and the managed driver not handling the difference.
        /// </para>
        /// <para>
        /// This holds true even if we run UPDATE..RETURNING in a PL/SQL block.
        /// However, it *does* properly work on INSERT..RETURNING and a PL/SQL block doing UPDATE, then SELECT.
        /// There should be no risk of a race condition as the row should be locked for the duration of the execution.
        /// </para>
        /// </remarks>
        protected override string UpdateReturningQuery(TableInfo tableInfo, IEnumerable<string> columnsToUpdate)
        {
            // BEGIN update...; select...; END;
            return new StringBuilder()
                .Append("begin ")
                .Append(UpdateQuery(tableInfo, columnsToUpdate))
                .Append($"; select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} ")
                .Append($"into {EscapeReturnParameters(tableInfo.GeneratedColumns)} ")
                .Append($"from {EscapeTableName(tableInfo)} ")
                .Append($"where {EscapeWhereList(tableInfo.KeyColumns)};")
                .Append("end;")
                .ToString();
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
        /// <remarks>
        /// Oracle supports binding <c>offset</c> and <paramref name="pageSize"/> as <paramref name="parameters"/>.
        /// </remarks>
        public override string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql, DynamicParameters parameters)
        {
            var q = new SqlParser(GetListQuery(tableInfo, sql));
            var pageSkip = (page - 1) * pageSize;
            var pageTake = pageSkip + pageSize;
            var sqlOrderBy = string.Empty;
            var sqlOrderByRemoved = q.Sql;

            if (string.IsNullOrEmpty(q.OrderByClause))
            {
                if (tableInfo.KeyColumns.Any())
                {
                    sqlOrderBy = $"order by {EscapeColumnn(tableInfo.KeyColumns.First().PropertyName)}";
                }
            }
            else
            {
                sqlOrderBy = q.OrderByClause;
                sqlOrderByRemoved = sqlOrderByRemoved.Replace(q.OrderByClause, "");
            }

            // Oracle supports binding the offset and page size.
            // Use variable names that are unlikely to be used as parameters, and that are safe to use with ODP.NET and Dapper.
            // NOTE: this explicitly won't work with Oracle.ManagedDataAccess 12.1.x, only 12.2 and later.
            // It works with all versions of Oracle.ManagedDataAccess.Core, however...
            parameters.Add("PAGE_SKIP$$", pageSkip, DbType.Int64);
            parameters.Add("PAGE_TAKE$$", pageTake, DbType.Int64);

            var columnsOnly = $"page_inner.* FROM ({sqlOrderByRemoved}) page_inner";

            return $"select * from (select row_number() over ({sqlOrderBy}) page_rn, {columnsOnly}) page_outer where page_rn > :PAGE_SKIP$$ and page_rn <= :PAGE_TAKE$$";
        }
    }
}

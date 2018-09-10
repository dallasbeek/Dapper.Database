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
    public partial class SqlServerPre2012Adapter : SqlServerAdapter
    {

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
            var pageTake = pageSkip + pageSize;
            var sqlOrderBy = "order by (select null)";
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

            var columnsOnly = $"page_inner.* FROM ({sqlOrderByRemoved}) page_inner";

            parameters.Add(PageSizeParamName, pageTake, DbType.Int64);
            parameters.Add(PageSkipParamName, pageSkip, DbType.Int64);
            return $"select * from (select row_number() over ({sqlOrderBy}) page_rn, {columnsOnly}) page_outer where page_rn > {EscapeParameter(PageSkipParamName)} and page_rn <= {EscapeParameter(PageSizeParamName)}";
        }

    }
}

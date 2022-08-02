using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper.Database.Extensions;

namespace Dapper.Database.Adapters
{
    /// <summary>
    ///     The Oracle database adapter for modern Oracle databases.
    /// </summary>
    /// <remarks>
    ///     Supports 12.1 and later.
    /// </remarks>
    /// <seealso cref="Oracle11gAdapter" />
    public partial class OracleAdapter : SqlAdapter
    {
        /// <summary>
        ///     The variable name used for row count.
        /// </summary>
        /// <seealso cref="ResolveRowCount" />
        protected const string RowCountParamName = "ROW_COUNT$$";

        /// <inheritdoc />
        /// <remarks>
        ///     Oracle does not like bind variables starting with underscores, so we have to use different ones.
        /// </remarks>
        protected override string PageSizeParamName => "PAGE_SIZE$$";

        /// <inheritdoc />
        /// <remarks>
        ///     Oracle does not like bind variables starting with underscores, so we have to use different ones.
        /// </remarks>
        protected override string PageSkipParamName => "PAGE_SKIP$$";

        /// <summary>
        ///     Resolves row count depending on whether the statement return count is from PL/SQL or not.
        /// </summary>
        /// <param name="count">The statement return count (can be -1, 0, or a positive number).</param>
        /// <param name="parameters">The parameters used in the statement.</param>
        /// <returns>The actual count (zero or more).</returns>
        /// <remarks>
        ///     PL/SQL blocks will automatically return -1 for rows affected.
        ///     The only way to get an accurate count is to bind an additional variable in PL/SQL and capture the builtin variable
        ///     <c>ROW_COUNT$$</c>;
        ///     this is the name of the bind variable containing the value.
        /// </remarks>
        protected int ResolveRowCount(int count, DynamicParameters parameters) =>
            count >= 0 ? count : parameters.Get<int>(RowCountParamName);


        /// <inheritdoc cref="SqlAdapter.BuildInsertQuery" />
        protected string BuildBaseInsertQuery(TableInfo tableInfo) => base.BuildInsertQuery(tableInfo);

        /// <summary>
        ///     Oracle implementation of an insert query.
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <returns>An insert sql statement</returns>
        protected override string BuildInsertQuery(TableInfo tableInfo)
        {
            var sql = BuildBaseInsertQuery(tableInfo);
            if (tableInfo.GeneratedColumns.Any())
                sql =
                    $"{sql} returning {EscapeColumnList(tableInfo.GeneratedColumns)} into {EscapeReturnParameters(tableInfo.GeneratedColumns)}";

            return sql;
        }

        /// <summary>
        ///     Converts the specified entity to parameters for an INSERT statement.
        /// </summary>
        /// <typeparam name="T">the type of entity to bind</typeparam>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToInsert">the entity to be inserted</param>
        /// <returns></returns>
        protected virtual DynamicParameters BuildInsertParameters<T>(TableInfo tableInfo, T entityToInsert)
        {
            // Oracle does not return RETURNING values in a result set; rather, it returns them as InputOutput parameters.
            // We need to wrap the incoming object in a DynamicParameters collection to get at the values.
            // While it does InputOutput binding, it does _not_ set size for strings by default; we have to call parameters.Output() to do that.
            var parameters = new DynamicParameters(entityToInsert);
            foreach (var column in tableInfo.GeneratedColumns) parameters.Output(entityToInsert, column);

            return parameters;
        }

        /// <inheritdoc cref="SqlAdapter.BuildUpdateQuery" />
        protected string BuildBaseUpdateQuery(TableInfo tableInfo, IEnumerable<string> columnsToUpdate) =>
            base.BuildUpdateQuery(tableInfo, columnsToUpdate);

        /// <summary>
        ///     Oracle implementation of an update query.
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="columnsToUpdate">columns to be updated</param>
        /// <returns>An update sql statement</returns>
        protected override string BuildUpdateQuery(TableInfo tableInfo, IEnumerable<string> columnsToUpdate)
        {
            var sql = BuildBaseUpdateQuery(tableInfo, columnsToUpdate);
            if (tableInfo.GeneratedColumns.Any())
                sql =
                    $"{sql} returning {EscapeColumnList(tableInfo.GeneratedColumns)} into {EscapeReturnParameters(tableInfo.GeneratedColumns)}";

            return sql;
        }

        /// <summary>
        ///     Converts the specified entity to parameters for an UPDATE statement.
        /// </summary>
        /// <typeparam name="T">the type of entity to bind</typeparam>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToUpdate">the entity to be updated</param>
        /// <returns></returns>
        protected virtual DynamicParameters BuildUpdateParameters<T>(TableInfo tableInfo, T entityToUpdate)
        {
            // Oracle does not return RETURNING values in a result set; rather, it returns them as InputOutput parameters.
            // We need to wrap the incoming object in a DynamicParameters collection to get at the values.
            // While it does InputOutput binding, it does _not_ set size for strings by default; we have to call parameters.Output() to do that.
            var parameters = new DynamicParameters(entityToUpdate);
            foreach (var column in tableInfo.GeneratedColumns) parameters.Output(entityToUpdate, column);

            return parameters;
        }

        /// <summary>
        ///     Oracle-specific implementation of an Exists query.
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <returns>A sql statement that selects true if a record matches</returns>
        public override string ExistsQuery(TableInfo tableInfo, string sql)
        {
            var sqlParser = new SqlParser(sql ?? "");

            if (sqlParser.Sql.StartsWith(";", StringComparison.Ordinal))
                return sqlParser.Sql.Substring(1);

            if (!sqlParser.IsSelect)
                return string.IsNullOrEmpty(sqlParser.FromClause)
                    ? $"select case when exists (select * from {EscapeTableName(tableInfo)} {sqlParser.Sql}) then 1 else 0 end as rec_exists from dual"
                    : $"select case when exists (select * {sqlParser.Sql}) then 1 else 0 end as rec_exists from dual";

            return $"select case when exists ({sqlParser.Sql}) then 1 else 0 end as rec_exists from dual";
        }

        /// <inheritdoc />
        public override string GetPageListQuery(TableInfo tableInfo, long page, long pageSize, string sql,
            DynamicParameters parameters)
        {
            var sqlParser = new SqlParser(GetListQuery(tableInfo, sql));
            var pageSkip = (page - 1) * pageSize;
            var sqlOrderBy = string.Empty;

            if (string.IsNullOrEmpty(sqlParser.OrderByClause) && tableInfo.KeyColumns.Any())
                sqlOrderBy = $"order by {EscapeColumn(tableInfo.KeyColumns.First().PropertyName)}";

            parameters.Add(PageSizeParamName, pageSize, DbType.Int64);
            parameters.Add(PageSkipParamName, pageSkip, DbType.Int64);

            return
                $"{sqlParser.Sql} {sqlOrderBy} offset {EscapeParameter(PageSkipParamName)} rows fetch next {EscapeParameter(PageSizeParamName)} rows only";
        }

        /// <summary>
        ///     Returns the format for parameters
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public override string EscapeParameters(IEnumerable<ColumnInfo> columns) => string.Join(", ",
            columns.Select(ci =>
                string.IsNullOrWhiteSpace(ci.SequenceName)
                    ? EscapeParameter(ci.PropertyName)
                    : ci.SequenceName + ".nextval"));

        /// <summary>
        ///     Returns the format for parameters
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public string EscapeReturnParameters(IEnumerable<ColumnInfo> columns) =>
            string.Join(", ", columns.Select(ci => EscapeParameter(ci.PropertyName)));

        /// <summary>
        ///     Applies a schema name is one is specified
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <returns></returns>
        public override string EscapeTableName(TableInfo tableInfo) =>
            (!string.IsNullOrEmpty(tableInfo.SchemaName) ? EscapeTableName(tableInfo.SchemaName) + "." : null) +
            EscapeTableName(tableInfo.TableName);

        /// <summary>
        ///     Returns the format for table name
        /// </summary>
        public override string EscapeTableName(string value) => $"\"{value.ToUpperInvariant()}\"";

        /// <summary>
        ///     Returns the format for column
        /// </summary>
        public override string EscapeColumn(string value) => $"\"{value.ToUpperInvariant()}\"";

        /// <summary>
        ///     Returns the format for parameter
        /// </summary>
        public override string EscapeParameter(string value) => $":{value}";
    }
}

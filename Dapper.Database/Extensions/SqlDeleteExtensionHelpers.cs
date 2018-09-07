using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Dapper.Database.Extensions
{
    public static partial class SqlMapperExtensions
    {
        private struct GeneratedQuery
        {
            public GeneratedQuery(DynamicParameters parameters, string sqlStatement) : this()
            {
                Parameters = parameters;
                SqlStatement = sqlStatement;
            }

            public DynamicParameters Parameters { get; }
            public string SqlStatement { get; }
        }

        /// <summary>
        /// Builds a Delete query, formatted for the adapter specified for the type (MsSql, Oracle, Etc.).
        /// </summary>
        /// <typeparam name="T">The entity type to generate the query with</typeparam>
        /// <param name="connection">The connection used to target the language the SQL statement will be generated from</param>
        /// <param name="entityToDelete">The (T) entity, with some form of <see cref="KeyAttribute"/> specified to bound the query with.
        /// The values on the entity passed in will be used as the values in the <see cref="DynamicParameters"/> returned from the <see cref="GeneratedQuery"/>.</param>
        /// <returns>A <see cref="GeneratedQuery"/> containing the RawSql text and the <see cref="DynamicParameters"/> to bound the query with.</returns>
        private static GeneratedQuery GenerateDeleteQuery<T>(IDbConnection connection, T entityToDelete) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            var keys = tinfo.GetCompositeKeys("Delete");
            var dynParms = new DynamicParameters();
            foreach (var key in keys)
            {
                var value = key.GetValue(entityToDelete);
                dynParms.Add(key.ColumnName, value);
            }

            var qWhere = $" where {adapter.EscapeWhereList(tinfo.KeyColumns)}";
            return new GeneratedQuery(dynParms, adapter.DeleteQuery(tinfo, qWhere));
        }

        /// <summary>
        /// Builds a Delete query, formatted for the adapter specified for the type (MsSql, Oracle, Etc.).
        /// </summary>
        /// <typeparam name="T">The entity type to generate the query with</typeparam>
        /// <param name="connection">The connection used to target the language the SQL statement will be generated from</param>
        /// <param name="primaryKeyValue">The primary key value for the (T) type. This will be used to bound the query.</param>
        /// <returns>A <see cref="GeneratedQuery"/> containing the RawSql text and the <see cref="DynamicParameters"/> to bound the query with.</returns>
        private static GeneratedQuery GenerateDeleteQuery<T>(IDbConnection connection, object primaryKeyValue) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);

            var key = tinfo.GetSingleKey("Delete");
            var dynParms = new DynamicParameters();
            dynParms.Add(key.PropertyName, primaryKeyValue);
            var qWhere = $" where {adapter.EscapeWhereList(tinfo.KeyColumns)}";

            return new GeneratedQuery(dynParms, adapter.DeleteQuery(tinfo, qWhere));
        }

        /// <summary>
        /// Builds a Delete query, formatted for the adapter specified for the type (MsSql, Oracle, Etc.).
        /// NOTE: If a null is passed in to the whereClause, the generated delete statement will be unbounded (basically a DeleteAll statement)
        /// </summary>
        /// <typeparam name="T">The entity type to generate the query with</typeparam>
        /// <param name="connection">The connection used to target the language the SQL statement will be generated from</param>
        /// <param name="whereClause">The static where clause to attach to the end of a Delete statement</param>
        /// <returns>The static Delete statement</returns>
        private static string GenerateDeleteQuery<T>(IDbConnection connection, string whereClause)
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);

            return adapter.DeleteQuery(tinfo, whereClause);
        }
    }
}

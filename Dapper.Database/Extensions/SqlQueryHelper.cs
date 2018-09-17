using System.ComponentModel.DataAnnotations;
using System.Data;
using Dapper.Database.Adapters;
using System;


namespace Dapper.Database.Extensions
{
    public static partial class SqlMapperExtensions
    {
        private struct SqlQueryHelper
        {
            public SqlQueryHelper(Type type, IDbConnection connection) : this()
            {
                Adapter = GetFormatter(connection);
                TableInfo = TableInfoCache(type);
            }

            public ISqlAdapter Adapter { get; }
            public TableInfo TableInfo { get; }

            public GenQuery GenerateSingleKeyQuery(object primaryKeyValue, Func<TableInfo, string, string> adapterMethod)
            {
                var key = TableInfo.GetSingleKey();
                var dynParms = new DynamicParameters();
                dynParms.Add(key.PropertyName, primaryKeyValue);
                var qWhere = $" where {Adapter.EscapeWhereList(TableInfo.KeyColumns)}";

                return new GenQuery(dynParms, adapterMethod.Invoke(TableInfo, qWhere));
            }

            public GenQuery GenerateCompositeKeyQuery<T>(T entity, Func<TableInfo, string, string> adapterMethod)
            {
                var keys = TableInfo.GetCompositeKeys();
                var dynParms = new DynamicParameters();
                foreach (var key in keys)
                {
                    var value = key.GetValue(entity);
                    dynParms.Add(key.PropertyName, value);
                }
                var qWhere = $" where {Adapter.EscapeWhereList(TableInfo.KeyColumns)}";

                return new GenQuery(dynParms, adapterMethod.Invoke(TableInfo, qWhere));
            }
        }

        private struct GenQuery
        {
            public GenQuery(DynamicParameters parameters, string sqlStatement) : this()
            {
                Parameters = parameters;
                SqlStatement = sqlStatement;
            }

            public DynamicParameters Parameters { get; }
            public string SqlStatement { get; }
        }

    }
}

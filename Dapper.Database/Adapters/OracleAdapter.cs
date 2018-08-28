using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{
    /// <summary>
    /// The Postgres database adapter.
    /// </summary>
    public partial class OracleAdapter : SqlAdapter, ISqlAdapter
    {

        private Regex _reservedWords = new Regex("^name|size$");

        /// <summary>
        /// Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <returns>true if the entity was inserted</returns>
        public override bool Insert( IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToInsert )
        {
            var cmd = new StringBuilder(InsertQuery(tableInfo));

            if ( tableInfo.GeneratedColumns.Any() && tableInfo.KeyColumns.Any() )
            {
                //cmd.Append($" RETURNING {string.Join(", ", tableInfo.GeneratedColumns.Select(ci => $"{EscapeColumnn(ci.ColumnName)} into {EscapeParameter("ora" + ci.Property.Name)}"))}");
                cmd.Append($" RETURNING {string.Join(", ", tableInfo.GeneratedColumns.Select(ci => $"{EscapeColumnn(ci.ColumnName)}"))} into {string.Join(", ", tableInfo.GeneratedColumns.Select(ci => $"{EscapeParameter("ora" + ci.Property.Name)}"))}");

                //cmd = new StringBuilder("insert into Person (IdentityId, FirstName, LastName) values (Person_Seq.NEXTVAL, :FirstName, :LastName)  RETURNING IdentityId into :oraIdentityId");

                List<KeyValuePair<PropertyInfo, IDbDataParameter>> parameterMapping = null;

                var cb = new Action<IDbCommand>(dbcmd =>
                {

                    parameterMapping = tableInfo.GeneratedColumns.Select(t => new KeyValuePair<PropertyInfo, IDbDataParameter>(t.Property, dbcmd.CreateParameter())).ToList();

                    parameterMapping.ForEach(map =>
                    {
                        SqlMapper.ITypeHandler handler = null;
                        map.Value.ParameterName = $"{EscapeParameter("ora" + map.Key.Name)}";
                        map.Value.Direction = ParameterDirection.Output;
                        map.Value.Value = DBNull.Value;
#pragma warning disable 618
                        map.Value.DbType = SqlMapper.LookupDbType(map.Key.PropertyType, "n/a", false, out handler);
#pragma warning restore 618
                        dbcmd.Parameters.Add(map.Value);
                    });
                });

                var cmdDef = new CommandDefinition(cmd.ToString(), entityToInsert, transaction, commandTimeout, beforeExecute: cb);

                connection.Execute(cmdDef);


                foreach ( var param in parameterMapping )
                {
                    var rval = param.Value.Value;
                    param.Key.SetValue(entityToInsert, Convert.ChangeType(rval, param.Key.PropertyType), null);
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
        public override bool Update( IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToUpdate, IEnumerable<string> columnsToUpdate )
        {
            var cmd = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

            if ( tableInfo.GeneratedColumns.Any() && tableInfo.KeyColumns.Any() )
            {
                var selectcmd = new StringBuilder($"select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
                selectcmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

                connection.Execute(cmd.ToString(), entityToUpdate, transaction, commandTimeout);
                var r = connection.Query(selectcmd.ToString(), entityToUpdate, transaction, commandTimeout: commandTimeout);

                var vals = r.ToList();

                if ( !vals.Any() ) return false;

                var rvals = ((IDictionary<string, object>) vals[0]);

                foreach ( var key in rvals.Keys )
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
        /// Inserts an entity into table "Ts"
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="entityToInsert">Entity to insert</param>
        /// <returns>true if the entity was inserted</returns>
        public override async Task<bool> InsertAsync( IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToInsert )
        {
            var cmd = new StringBuilder(InsertQuery(tableInfo));

            if ( tableInfo.GeneratedColumns.Any() && tableInfo.KeyColumns.Any() )
            {

                var selectcmd = new StringBuilder($"select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");

                if ( tableInfo.KeyColumns.Any(k => k.IsIdentity) )
                {
                    selectcmd.Append($"where {EscapeColumnn(tableInfo.KeyColumns.First(k => k.IsIdentity).ColumnName)} = LAST_INSERT_ID();");
                }
                else
                {
                    selectcmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");
                }

                var wasClosed = connection.State == ConnectionState.Closed;
                if ( wasClosed ) connection.Open();

                await connection.ExecuteAsync(cmd.ToString(), entityToInsert, transaction, commandTimeout);
                var r = await connection.QueryAsync(selectcmd.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout);

                if ( wasClosed ) connection.Close();

                var vals = r.ToList();

                if ( !vals.Any() ) return false;

                var rvals = ((IDictionary<string, object>) vals[0]);

                foreach ( var key in rvals.Keys )
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
        public override async Task<bool> UpdateAsync( IDbConnection connection, IDbTransaction transaction, int? commandTimeout, TableInfo tableInfo, object entityToUpdate, IEnumerable<string> columnsToUpdate )
        {
            var cmd = new StringBuilder(UpdateQuery(tableInfo, columnsToUpdate));

            if ( tableInfo.GeneratedColumns.Any() && tableInfo.KeyColumns.Any() )
            {
                var selectcmd = new StringBuilder($"select {EscapeColumnListWithAliases(tableInfo.GeneratedColumns, tableInfo.TableName)} from {EscapeTableName(tableInfo)} ");
                selectcmd.Append($"where {EscapeWhereList(tableInfo.KeyColumns)};");

                await connection.ExecuteAsync(cmd.ToString(), entityToUpdate, transaction, commandTimeout);
                var r = await connection.QueryAsync(selectcmd.ToString(), entityToUpdate, transaction, commandTimeout: commandTimeout);

                var vals = r.ToList();

                if ( !vals.Any() ) return false;

                var rvals = ((IDictionary<string, object>) vals[0]);

                foreach ( var key in rvals.Keys )
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

        /// <summary>
        /// Default implementation of an Exists query
        /// </summary>
        /// <param name="tableInfo">table information about the entity</param>
        /// <param name="sql">a sql statement or partial statement</param>
        /// <returns>A sql statement that selects true if a record matches</returns>
        public override string ExistsQuery( TableInfo tableInfo, string sql )
        {
            var q = new SqlParser(sql ?? "");

            if ( q.Sql.StartsWith(";") )
                return q.Sql.Substring(1);

            if ( !q.IsSelect )
            {
                var wc = string.IsNullOrWhiteSpace(q.Sql) ? $"where {EscapeWhereList(tableInfo.KeyColumns)}" : q.Sql;

                if ( string.IsNullOrEmpty(q.FromClause) )
                    return $"select case when exists (select * from { EscapeTableName(tableInfo)} {wc}) then 1 else 0 end as rec_exists from dual";
                else
                    return $"select case when exists (select * {wc}) then 1 else 0 end as rec_exists from dual";

            }

            return $"select case when exists ({q.Sql}) then 1 else 0 end as rec_exists from dual";

        }

        /// <summary>
        /// Applies a schema name is one is specified
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <returns></returns>
        public override string EscapeTableName( TableInfo tableInfo ) =>
            (!string.IsNullOrEmpty(tableInfo.SchemaName) ? EscapeTableName(tableInfo.SchemaName) + "." : null) + EscapeTableName(tableInfo.TableName);

        /// <summary>
        /// Returns the format for table name
        /// </summary>
        public override string EscapeTableName( string value ) => $"{value}";

        /// <summary>
        /// Returns the format for column
        /// </summary>
        public override string EscapeColumnn( string value ) => _reservedWords.IsMatch(value.ToLower()) ? $"\"{value}\"" : $"{value}";

        /// <summary>
        /// Returns the format for parameter
        /// </summary>
        public override string EscapeParameter( string value ) => $":{value}";
    }
}

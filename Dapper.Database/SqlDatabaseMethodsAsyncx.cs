using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database
{
    public partial class SqlDatabase : ISqlDatabase, IDisposable
    {

        ///// <summary>
        ///// Count of entities
        ///// </summary>
        ///// <param name="sql">The sql clause to count</param>
        ///// <param name="parameters">The parameters of the where clause to delete</param>
        ///// <returns>Return Total Count of matching records</returns>
        ////public async Task<int> CountAsync(string sql, object parameters)
        ////{
        ////    return await ExecuteInternalAsync(() => _sharedConnection.ExecuteScalarAsync<int>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        ////}



    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Dapper.Database.Extensions;
using Xunit;
using System.Threading.Tasks;

#if NET452
using System.Transactions;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlServerCe;
#endif
using FactAttribute = Dapper.Tests.Database.SkippableFactAttribute;


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "ExecuteScalerAsync")]
        public async Task ExecuteScalerAsyncSql()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(102.29m, await db.ExecuteScalerAsync<decimal>("select listprice from product where productid = 806"));
            }
        }


        [Fact]
        [Trait("Category", "ExecuteScalerAsync")]
        public async Task ExecuteScalerAsyncSqlWithParameter()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(102.29m, await db.ExecuteScalerAsync<decimal>("select listprice from product where productid = @ProductId", new { ProductId = 806 }));
            }
        }

    }
}

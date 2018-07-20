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


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "ExecuteAsync")]
        public async Task ExecuteAsyncSql()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89,await db.ExecuteAsync("update product set color = 'Black' where Color = 'Black'"));
            }
        }


        [Fact]
        [Trait("Category", "ExecuteAsync")]
        public async Task ExecuteAsyncSqlWithParameter()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, await db.ExecuteAsync("update product set color = @Color where Color = @Color", new { Color = "Black" }));
            }
        }

    }
}

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
        [Trait("Category", "ExecuteAsync")]
        public async Task ExecuteAsyncSql()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, await db.ExecuteAsync("update product set color = 'Black' where Color = 'Black'"));
            }
        }


        [Fact]
        [Trait("Category", "ExecuteAsync")]
        public async Task ExecuteAsyncSqlWithParameter()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, await db.ExecuteAsync($"update product set color = {P}Color where Color = {P}Color", new { Color = "Black" }));
            }
        }

    }
}

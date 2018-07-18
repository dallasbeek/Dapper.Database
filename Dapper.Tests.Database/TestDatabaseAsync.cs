using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Dapper.Database.Extensions;
using Xunit;
using Dapper.Database;
using System.Data.SqlClient;
using System.Threading.Tasks;

#if NET452
using System.Transactions;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlServerCe;
#endif


namespace Dapper.Tests.Database
{
    public partial class SqlServerTestSuite
    {
        [ProviderFact]
        [Trait("Category", "Database")]
        public async Task DatabaseCountSqlAsync()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, await db.CountAsync("select count(*) from product where Color = @Color", new { Color = "Black" }));
            }
        }

        [ProviderFact]
        [Trait("Category", "Database")]
        public async Task DatabaseCountAsync()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, await db.CountAsync<Product>("where Color = @Color", new { Color = "Black" }));
            }
        }

    }
}

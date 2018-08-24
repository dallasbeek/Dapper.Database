using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Dapper.Database.Extensions;
using Xunit;

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
        [Trait("Category", "Execute")]
        public void ExecuteSql()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, db.Execute("update product set color = 'Black' where Color = 'Black'"));
            }
        }


        [Fact]
        [Trait("Category", "Execute")]
        public void ExecuteSqlWithParameter()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(89, db.Execute($"update product set color = {P}Color where Color = {P}Color", new { Color = "Black" }));
            }
        }

    }
}

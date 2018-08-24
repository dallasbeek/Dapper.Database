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

#if !NETSTANDARD1_3 && !NETCOREAPP1_0
        [Fact]
        [Trait("Category", "GetDataTable")]
        public void GetDataTable()
        {
            if (GetProvider() != Provider.SQLite)
            {
                using (var db = GetSqlDatabase())
                {
                    var dt = db.GetDataTable("select * from product where Color = 'Black'");
                    Assert.Equal(89, dt.Rows.Count);
                }
            }
        }


        [Fact]
        [Trait("Category", "GetDataTable")]
        public void GetDataTableWithParameter()
        {
            if (GetProvider() != Provider.SQLite)
            {
                using (var db = GetSqlDatabase())
                {
                    var dt = db.GetDataTable($"select * from product where Color = {P}Color", new { @Color = "Black" });
                    Assert.Equal(89, dt.Rows.Count);
                }
            }
        }
#endif

    }
}

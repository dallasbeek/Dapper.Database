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


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "ExecuteScaler")]
        public void ExecuteScalerSql()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(102.29m, db.ExecuteScaler<decimal>("select listprice from product where productid = 806"));
            }
        }


        [Fact]
        [Trait("Category", "ExecuteScaler")]
        public void ExecuteScalerSqlWithParameter()
        {
            using (var db = GetSqlDatabase())
            {
                Assert.Equal(102.29m, db.ExecuteScaler<decimal>("select listprice from product where productid = @ProductId", new { ProductId = 806 }));
            }
        }

    }
}

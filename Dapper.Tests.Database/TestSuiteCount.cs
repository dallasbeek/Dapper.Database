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
        [ProviderFact]
        [Trait("Category", "Count")]
        public void CountAll()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.Equal(295, connection.Count<Product>());
            }
        }

        [ProviderFact]
        [Trait("Category", "Count")]
        public void CountWithWhereClause()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.Equal(89, connection.Count<Product>("where Color = 'Black'" ));
            }
        }


        [ProviderFact]
        [Trait("Category", "Count")]
        public void CountWithWhereClauseParameter()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.Equal(89, connection.Count<Product>("where Color = @Color", new { Color = "Black" }));
            }
        }

        [ProviderFact]
        [Trait("Category", "Count")]
        public void CountWithSelectClause()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.Equal(89, connection.Count<Product>("select * from Product where Color = 'Black'"));
            }
        }

        [ProviderFact]
        [Trait("Category", "Count")]
        public void CountWithSelectClauseParameter()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.Equal(89, connection.Count<Product>("select * from Product where Color = @Color", new { Color = "Black" }));
            }
        }

        [ProviderFact]
        [Trait("Category", "Count")]
        public void CountShortCircuit()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.Equal(89, connection.Count<Product>(";select count(*) from Product where Color = @Color", new { Color = "Black" }));
            }
        }


    }
}

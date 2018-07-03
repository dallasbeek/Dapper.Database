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
        [Trait("Category", "Exists")]
        public void ExistsNoArgs()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.True(connection.Exists<Product>());
            }
        }


        [ProviderFact]
        [Trait("Category", "Exists")]
        public void ExistsByEntity()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new Product { ProductID = 806, GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") };
                Assert.True(connection.Exists(p));

                p.ProductID = -1;
                Assert.False(connection.Exists(p));
            }
        }

        [ProviderFact]
        [Trait("Category", "Exists")]
        public void ExistsByIntegerId()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.True(connection.Exists<Product>(806));
                Assert.False(connection.Exists<Product>(-1));
            }
        }

        [ProviderFact]
        [Trait("Category", "Exists")]
        public void ExistsByGuidIdWhereClause()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.True(connection.Exists<Product>("where rowguid = @GuidId", new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
                Assert.False(connection.Exists<Product>("where rowguid = @GuidId", new { GuidId = "1115D52B-8C29-4059-B899-75C53B5EE2E6" }));
            }
        }

        [ProviderFact]
        [Trait("Category", "Exists")]
        public void ExistsPartialBySelect()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.True(connection.Exists<Product>("select ProductId, rowguid AS GuidId, Name from Product where ProductId = @Id", new { Id = 806 }));
                Assert.False(connection.Exists<Product>("select ProductId, rowguid AS GuidId, Name from Product where ProductId = @Id", new { Id = -1 }));
            }
        }

        [ProviderFact]
        [Trait("Category", "Exists")]
        public void ExistsBySelect()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.True(connection.Exists<Product>("select *, rowguid AS GuidId  from Product where ProductId = @Id", new { Id = 806 }));
                Assert.False(connection.Exists<Product>("select *, rowguid AS GuidId  from Product where ProductId = @Id", new { Id = -1 }));
            }
        }

        [ProviderFact]
        [Trait("Category", "Exists")]
        public void ExistsShortCircuitSemiColon()
        {
            using (var connection = GetOpenConnection())
            {
                Assert.True( connection.Exists<Product>("; select 1 AS ProductId"));
                Assert.False(connection.Exists<Product>("; select 0 AS ProductId"));
            }
        }

    }
}

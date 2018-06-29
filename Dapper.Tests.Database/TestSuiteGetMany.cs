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
    public partial class SqlServerTestSuite
    {
        [Fact]
        [Trait("Category", "GetMany")]
        public void GetManySimpleAddSelect()
        {
            using (var connection = GetConnection())
            {
                var data = connection.GetMany<Customer>("WHERE FirstName = 'Kevin' ");
                Assert.Equal(4, data.Count());
            }
        }

        [Fact]
        [Trait("Category", "GetMany")]
        public void GetManySimpleParameterAddSelect()
        {
            using (var connection = GetConnection())
            {
                var data = connection.GetMany<Customer>("WHERE FirstName = @FirstName ", new { FirstName = "Kevin" });
                Assert.Equal(4, data.Count());
            }
        }

        [Fact]
        [Trait("Category", "GetMany")]
        public void GetManySimpleWithSelect()
        {
            using (var connection = GetConnection())
            {
                var data = connection.GetMany<Customer>("SELECT * FROM Customer WHERE FirstName = 'Kevin' ");
                Assert.Equal(4, data.Count());
            }
        }

        [Fact]
        [Trait("Category", "GetMany")]
        public void GetManySimpleParameterWithSelect()
        {
            using (var connection = GetConnection())
            {
                var data = connection.GetMany<Customer>("SELECT * FROM Customer WHERE FirstName = @FirstName ", new { FirstName = "Kevin" });
                Assert.Equal(4, data.Count());
            }
        }

        [Fact]
        [Trait("Category", "GetMany")]
        public void GetManyOneJoinFullMapped()
        {
            using (var connection = GetConnection())
            {
                var data = connection.GetMany<Product, ProductCategory, Product>(
                    (p,pc) =>
                    {
                        p.ProductCategory = pc;
                        return p;
                    },
                    @"select * from Product
                    join ProductCategory on ProductCategory.ProductCategoryID = Product.ProductCategoryID
                    where Product.Color = 'Multi'");
                Assert.Equal(8, data.Count());
                var p1 = data.Single(d => d.ProductID == 712);
                Assert.NotNull(p1.ProductCategory);
                Assert.Null(p1.ProductModel);
            }
        }

        [Fact]
        [Trait("Category", "GetMany")]
        public void GetManyOneJoinWithParameterMapped()
        {
            using (var connection = GetConnection())
            {
                var data = connection.GetMany<Product, ProductCategory, Product>(
                    (p, pc) =>
                    {
                        p.ProductCategory = pc;
                        return p;
                    },
                    @"select * from Product
                    join ProductCategory on ProductCategory.ProductCategoryID = Product.ProductCategoryID
                    where Product.Color = @Color", new { Color = "Multi" });
                Assert.Equal(8, data.Count());
                var p1 = data.Single(d => d.ProductID == 712);
                Assert.NotNull(p1.ProductCategory);
                Assert.Null(p1.ProductModel);
            }
        }

        [Fact]
        [Trait("Category", "GetMany")]
        public void GetManyTwoJoinFullMapped()
        {
            using (var connection = GetConnection())
            {
                var data = connection.GetMany<Product, ProductCategory, ProductModel, Product>(
                    (p, pc, pm) =>
                    {
                        p.ProductCategory = pc;
                        p.ProductModel = pm;
                        return p;
                    },
                    @"select * from Product
                    join ProductCategory on ProductCategory.ProductCategoryID = Product.ProductCategoryID
                    join ProductModel on ProductModel.ProductModelID = Product.ProductModelID
                    where Product.Color = 'Multi'");
                Assert.Equal(8, data.Count());
                var p1 = data.Single(d => d.ProductID == 712);
                Assert.NotNull(p1.ProductCategory);
                Assert.NotNull(p1.ProductModel);
            }
        }

        [Fact]
        [Trait("Category", "GetMany")]
        public void GetManyTwoJoinWithParameterMapped()
        {
            using (var connection = GetConnection())
            {
                var data = connection.GetMany<Product, ProductCategory, ProductModel, Product>(
                    (p, pc, pm) =>
                    {
                        p.ProductCategory = pc;
                        p.ProductModel = pm;
                        return p;
                    },
                    @"select * from Product
                    join ProductCategory on ProductCategory.ProductCategoryID = Product.ProductCategoryID
                    join ProductModel on ProductModel.ProductModelID = Product.ProductModelID
                    where Product.Color = @Color", new { Color = "Multi" });
                Assert.Equal(8, data.Count());
                var p1 = data.Single(d => d.ProductID == 712);
                Assert.NotNull(p1.ProductCategory);
                Assert.NotNull(p1.ProductModel);
            }
        }

    }
}

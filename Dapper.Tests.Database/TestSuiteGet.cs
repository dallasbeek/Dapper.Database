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
        private void Validate(Product p)
        {
            Assert.Equal(806, p.ProductID);
            Assert.Equal("ML Headset", p.Name);
            Assert.Equal("HS-2451", p.ProductNumber);
            Assert.Null(p.Color);
            Assert.Equal(45.4168m, p.StandardCost);
            Assert.Equal(102.29m, p.ListPrice);
            Assert.Null(p.Size);
            Assert.Null(p.Weight);
            Assert.Equal(15, p.ProductCategoryID);
            Assert.Equal(60, p.ProductModelID);
            Assert.Equal(new DateTime(2002, 7, 1), p.SellStartDate.Date);
            Assert.Equal(new DateTime(2003, 6, 30), p.SellEndDate.Value.Date);
            Assert.Null(p.DiscontinuedDate);
            Assert.Equal("no_image_available_small.gif", p.ThumbnailPhotoFileName);
            Assert.Equal(new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6"), p.GuidId);
            Assert.Equal(new DateTime(2004, 3, 11), p.ModifiedDate.Date);
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetByEntityId()
        {
            using (var connection = GetOpenConnection())
            {
                var p = new Product { ProductID = 806 };
                Validate(connection.Get(p));
            }
        }


        [Fact]
        [Trait("Category", "Get")]
        public void GetByIntegerId()
        {
            using (var connection = GetOpenConnection())
            {
                Validate(connection.Get<Product>(806));
            }
        }


        [Fact]
        [Trait("Category", "Get")]
        public void GetByGuidIdWhereClause()
        {
            using (var connection = GetOpenConnection())
            {
                Validate(connection.Get<Product>("WHERE rowguid = @GuidId", new { GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") }));
            }
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetPartialBySelect()
        {
            using (var connection = GetOpenConnection())
            {
                var p = connection.Get<Product>("select ProductId, rowguid AS GuidId, Name", new { Id = 806 });
            }
        }

        [Fact]
        [Trait("Category", "Get")]
        public void GetStarBySelect()
        {
            using (var connection = GetOpenConnection())
            {
                var p = connection.Get<Product>("select *", new { Id = 806 });
            }
        }

    }
}

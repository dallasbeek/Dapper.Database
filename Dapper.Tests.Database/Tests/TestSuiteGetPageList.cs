using System;
using System.Collections.Generic;
using Dapper.Database.Extensions;
using Xunit;
using System.Linq;
using System.Text.RegularExpressions;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "GetPageList")]
        public void GetPageListNoOrder()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetPageList<Product>(12, 10);
                Assert.Equal(10, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetPageList")]
        public void GetPageListWithWhereClause()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetPageList<Product>(4, 10, "where Color = 'Black'");
                Assert.Equal(10, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }


        [Fact]
        [Trait("Category", "GetPageList")]
        public void GetPageListAliasWithWhereClause()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetPageList<ProductAlias>(4, 10, "where Color = 'Black'");
                Assert.Equal(10, lst.Count());
                var item = lst.Single(p => p.Id == 816);
                Assert.Equal(816, item.Id);
                Assert.Equal("ML Mountain Front Wheel", item.Name);
                Assert.Equal("FW-M762", item.ProductNumber);
                Assert.Equal("Black", item.Color);
                Assert.Equal(92.8071m, item.StandardCost);

            }
        }

        [Fact]
        [Trait("Category", "GetPageList")]
        public void GetPageListWithWhereClauseParameter()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetPageList<Product>(4, 10, $"where Color = {P}Color", new { Color = "Black" });
                Assert.Equal(10, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact()]
        [Trait("Category", "GetPageList")]
        public void GetPageListOrder()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetPageList<Product>(15, 10, "order by lower(Name)");
                Assert.Equal(10, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }


        [Fact]
        [Trait("Category", "GetPageList")]
        public void GetPageListWithWhereOrderClause()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetPageList<Product>(5, 10, "where Color = 'Black' order by lower(Name)");
                Assert.Equal(10, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                Assert.Equal(6, lst.ToList().IndexOf(item));
                ValidateProduct816(item);
            }
        }


        [Fact]
        [Trait("Category", "GetPageList")]
        public void GetPageListWithWhereOrderClauseParameter()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetPageList<Product>(5, 10, $"where Color = {P}Color order by lower(Name)", new { Color = "Black" });
                Assert.Equal(10, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetPageList")]
        public void GetPageListWithSelectClause()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetPageList<Product>(4, 10, "select p.*, p.rowguid as GuidId from Product p where p.Color = 'Black'");
                Assert.Equal(10, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }
        [Fact]
        [Trait("Category", "GetPageList")]
        public void GetPageListWithSelectOrderClause()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetPageList<Product>(5, 10, "select p.*, p.rowguid as GuidId from Product p where p.Color = 'Black' order by lower(Name)");
                Assert.Equal(10, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetPageList")]
        public void GetPageListWithSelectClauseParameter()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetPageList<Product>(4, 10, $"select p.*, p.rowguid as GuidId from Product p where p.Color = {P}Color", new { Color = "Black" });
                Assert.Equal(10, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetPageList")]
        public void GetPageListWithSelectClauseOrderParameter()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetPageList<Product>(5, 10, $"select p.*, p.rowguid as GuidId from Product p where p.Color = {P}Color order by lower(Name)", new { Color = "Black" });
                Assert.Equal(10, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetPageList")]
        public void GetPageListPartialBySelect()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetPageList<Product>(4, 10, $"select ProductId, rowguid AS GuidId, Name from Product where Color = {P}Color", new { Color = "Black" });
                Assert.Equal(10, lst.Count());
                var p = lst.Single(a => a.ProductID == 816);
                Assert.Equal(816, p.ProductID);
                Assert.Equal("ML Mountain Front Wheel", p.Name);
                Assert.Null(p.ProductNumber);
                Assert.Equal(new Guid("5E3E5033-9A77-4DCA-8B7F-DFED78EFA08A"), p.GuidId);
            }
        }

        [Fact]
        [Trait("Category", "GetPageList")]
        public void GetPageListOneJoinUnmapped()
        {
            using (var db = GetSqlDatabase())
            {
                var sizeColumn = GetProvider() == Provider.Oracle ? "\"SIZE\"" : "Size";
                var lst = db.GetPageList<Product, ProductCategory>(4, 10,
                    $@"select  P.ProductID, P.Name, P.ProductNumber, P.Color, P.StandardCost, P.ListPrice, P.{sizeColumn}, 
                    P.Weight, P.ProductModelID, P.SellStartDate, P.SellEndDate, P.DiscontinuedDate, 
                    P.ThumbNailPhoto, P.ThumbnailPhotoFileName, P.rowguid, P.ModifiedDate, PC.ProductCategoryID, 
                    PC.ParentProductCategoryID
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    where Color = {P}Color", new { Color = "Black" });
                Assert.Equal(10, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);

                item.ProductCategoryID = 21;
                item.GuidId = new Guid("5e3e5033-9a77-4dca-8b7f-dfed78efa08a");

                ValidateProduct816(item);
                Assert.NotNull(item.ProductCategory);
            }
        }

        [Fact]
        [Trait("Category", "GetPageList")]
        public void GetPageListOneJoinMapped()
        {
            using (var db = GetSqlDatabase())
            {
                var sizeColumn = GetProvider() == Provider.Oracle ? "\"SIZE\"" : "Size";
                var lst = db.GetPageList<Product, ProductCategory, Product>(4, 10,
                    (pr, pc) =>
                    {
                        pr.ProductCategory = pc;
                        return pr;
                    },

                    $@"select  P.ProductID, P.Name, P.ProductNumber, P.Color, P.StandardCost, P.ListPrice, P.{sizeColumn}, 
                    P.Weight, P.ProductModelID, P.SellStartDate, P.SellEndDate, P.DiscontinuedDate, 
                    P.ThumbNailPhoto, P.ThumbnailPhotoFileName, P.rowguid, P.ModifiedDate, PC.ProductCategoryID, 
                    PC.ParentProductCategoryID
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    where Color = {P}Color", new { Color = "Black" });
                Assert.Equal(10, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);

                item.ProductCategoryID = 21;
                item.GuidId = new Guid("5e3e5033-9a77-4dca-8b7f-dfed78efa08a");

                ValidateProduct816(item);
                Assert.NotNull(item.ProductCategory);
            }
        }

        //[Fact(Provider.SQLite)]
        //[Trait("Category", "GetPageList")]
        //public void GetPageListOneJoinMapped()
        //{
        //    using (var connection = GetConnection())
        //    {
        //        var lst = connection.GetPageList<Product, ProductCategory, Product>(
        //            (pr, pc) =>
        //            {
        //                pr.ProductCategory = pc;
        //                return pr;
        //            },
        //            @"select P.*, P.rowguid AS GuidId, PC.* 
        //            from Product P
        //            join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
        //            where Color = {P}Color", new { Color = "Black" });
        //        Assert.Equal(89, lst.Count());
        //        var item = lst.Single(p => p.ProductID == 816);
        //        ValidateProduct816(item);
        //        ValidateProductCategory21(item.ProductCategory);
        //    }
        //}

        //[Fact(Provider.SQLite)]
        //[Trait("Category", "GetPageList")]
        //public void GetPageListTwoJoinsUnmapped()
        //{
        //    using (var connection = GetConnection())
        //    {
        //        var lst = connection.GetPageList<Product, ProductCategory, ProductModel>(
        //            @"select P.*, P.rowguid AS GuidId, PC.*, PM.*
        //            from Product P
        //            join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
        //            join ProductModel PM on PM.ProductModelID = P.ProductModelID
        //            where Color = {P}Color", new { Color = "Black" });
        //        Assert.Equal(89, lst.Count());
        //        var item = lst.Single(p => p.ProductID == 816);
        //        ValidateProduct816(item);
        //        ValidateProductCategory21(item.ProductCategory);
        //        ValidateProductModel45(item.ProductModel);
        //    }
        //}

        //[Fact(Provider.SQLite)]
        //[Trait("Category", "GetPageList")]
        //public void GetPageListTwoJoinsMapped()
        //{
        //    using (var connection = GetConnection())
        //    {
        //        var lst = connection.GetPageList<Product, ProductCategory, ProductModel, Product>(
        //            (pr, pc, pm) =>
        //            {
        //                pr.ProductCategory = pc;
        //                pr.ProductModel = pm;
        //                return pr;
        //            },
        //            @"select P.*, P.rowguid AS GuidId, PC.*, PM.*
        //            from Product P
        //            join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
        //            join ProductModel PM on PM.ProductModelID = P.ProductModelID
        //            where Color = {P}Color", new { Color = "Black" });
        //        Assert.Equal(89, lst.Count());
        //        var item = lst.Single(p => p.ProductID == 816);
        //        ValidateProduct816(item);
        //        ValidateProductCategory21(item.ProductCategory);
        //        ValidateProductModel45(item.ProductModel);
        //    }
        //}

    }
}

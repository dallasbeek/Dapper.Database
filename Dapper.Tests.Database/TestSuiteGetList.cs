using System;
using Dapper.Database.Extensions;
using Xunit;
using System.Linq;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [Fact]
        [Trait("Category", "GetList")]
        public void GetListAll()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetList<Product>();
                Assert.Equal(295, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }


        [Fact]
        [Trait("Category", "GetList")]
        public void GetListWithWhereClause()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetList<Product>("where Color = 'Black'");
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }


        [Fact]
        [Trait("Category", "GetList")]
        public void GetListWithWhereClauseParameter()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetList<Product>("where Color = @Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetList")]
        public void GetListWithSelectClause()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetList<Product>("select *, rowguid as GuidId from Product where Color = 'Black'");
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetList")]
        public void GetListWithSelectClauseParameter()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetList<Product>("select *, rowguid as GuidId from Product where Color = @Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetList")]
        public void GetListShortCircuit()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetList<Product>(";select *, rowguid as GuidId from Product where Color = @Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "GetList")]
        public void GetListPartialBySelect()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetList<Product>("select ProductId, rowguid AS GuidId, Name from Product where Color = @Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var p = lst.Single(a => a.ProductID == 816);
                Assert.Equal(816, p.ProductID);
                Assert.Equal("ML Mountain Front Wheel", p.Name);
                Assert.Null(p.ProductNumber);
                Assert.Equal(new Guid("5E3E5033-9A77-4DCA-8B7F-DFED78EFA08A"), p.GuidId);
            }
        }

        [Fact]
        [Trait("Category", "GetList")]
        public void GetListOneJoinUnmapped()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetList<Product, ProductCategory>(
                    @"select  P.ProductID, P.Name, P.ProductNumber, P.Color, P.StandardCost, P.ListPrice, P.Size, 
                    P.Weight, P.ProductModelID, P.SellStartDate, P.SellEndDate, P.DiscontinuedDate, 
                    P.ThumbNailPhoto, P.ThumbnailPhotoFileName, P.rowguid as GuidId, P.ModifiedDate, PC.ProductCategoryID, 
                    PC.ParentProductCategoryID
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    where Color = @Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
                ValidateProductCategory21(item.ProductCategory);
            }
        }

        [Fact]
        [Trait("Category", "GetList")]
        public void GetListOneJoinMapped()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetList<Product, ProductCategory, Product>(
                    (pr, pc) =>
                    {
                        pr.ProductCategory = pc;
                        return pr;
                    },
                    @"select  P.ProductID, P.Name, P.ProductNumber, P.Color, P.StandardCost, P.ListPrice, P.Size, 
                    P.Weight, P.ProductModelID, P.SellStartDate, P.SellEndDate, P.DiscontinuedDate, 
                    P.ThumbNailPhoto, P.ThumbnailPhotoFileName, P.rowguid as GuidId, P.ModifiedDate, PC.ProductCategoryID, 
                    PC.ParentProductCategoryID
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    where Color = @Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
                ValidateProductCategory21(item.ProductCategory);
            }
        }

        [Fact]
        [Trait("Category", "GetList")]
        public void GetListTwoJoinsUnmapped()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetList<Product, ProductCategory, ProductModel>(
                    @"select  P.ProductID, P.Name, P.ProductNumber, P.Color, P.StandardCost, P.ListPrice, P.Size, 
                    P.Weight, P.ProductModelID, P.SellStartDate, P.SellEndDate, P.DiscontinuedDate, 
                    P.ThumbNailPhoto, P.ThumbnailPhotoFileName, P.rowguid as GuidId, P.ModifiedDate, PC.ProductCategoryID, 
                    PC.ParentProductCategoryID, PM.ProductModelID, PM.CatalogDescription
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where Color = @Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
                ValidateProductCategory21(item.ProductCategory);
                ValidateProductModel45(item.ProductModel);
            }
        }

        [Fact]
        [Trait("Category", "GetList")]
        public void GetListTwoJoinsMapped()
        {
            using (var db = GetSqlDatabase())
            {
                var lst = db.GetList<Product, ProductCategory, ProductModel, Product>(
                    (pr, pc, pm) =>
                    {
                        pr.ProductCategory = pc;
                        pr.ProductModel = pm;
                        return pr;
                    },
                    @"select  P.ProductID, P.Name, P.ProductNumber, P.Color, P.StandardCost, P.ListPrice, P.Size, 
                    P.Weight, P.ProductModelID, P.SellStartDate, P.SellEndDate, P.DiscontinuedDate, 
                    P.ThumbNailPhoto, P.ThumbnailPhotoFileName, P.rowguid as GuidId, P.ModifiedDate, PC.ProductCategoryID, 
                    PC.ParentProductCategoryID, PM.ProductModelID, PM.CatalogDescription
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where Color = @Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
                ValidateProductCategory21(item.ProductCategory);
                ValidateProductModel45(item.ProductModel);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using Dapper.Database.Extensions;
using Xunit;
using System.Linq;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {

        [ProviderFact]
        [Trait("Category", "GetList")]
        public void GetListAll()
        {
            using (var connection = GetOpenConnection())
            {
                var lst = connection.GetList<Product>();
                Assert.Equal(295, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }


        [ProviderFact]
        [Trait("Category", "GetList")]
        public void GetListWithWhereClause()
        {
            using (var connection = GetOpenConnection())
            {
                var lst = connection.GetList<Product>("where Color = 'Black'");
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }


        [ProviderFact]
        [Trait("Category", "GetList")]
        public void GetListWithWhereClauseParameter()
        {
            using (var connection = GetOpenConnection())
            {
                var lst = connection.GetList<Product>("where Color = @Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [ProviderFact]
        [Trait("Category", "GetList")]
        public void GetListWithSelectClause()
        {
            using (var connection = GetOpenConnection())
            {
                var lst = connection.GetList<Product>("select *, rowguid as GuidId from Product where Color = 'Black'");
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [ProviderFact]
        [Trait("Category", "GetList")]
        public void GetListWithSelectClauseParameter()
        {
            using (var connection = GetOpenConnection())
            {
                var lst = connection.GetList<Product>("select *, rowguid as GuidId from Product where Color = @Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [ProviderFact]
        [Trait("Category", "GetList")]
        public void GetListShortCircuit()
        {
            using (var connection = GetOpenConnection())
            {
                var lst = connection.GetList<Product>(";select *, rowguid as GuidId from Product where Color = @Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [ProviderFact]
        [Trait("Category", "GetList")]
        public void GetListPartialBySelect()
        {
            using (var connection = GetOpenConnection())
            {
                var lst = connection.GetList<Product>("select ProductId, rowguid AS GuidId, Name from Product where Color = @Color", new { Color = "Black" });
                Assert.Equal(89, lst.Count());
                var p = lst.Single(a => a.ProductID == 816);
                Assert.Equal(816, p.ProductID);
                Assert.Equal("ML Mountain Front Wheel", p.Name);
                Assert.Null(p.ProductNumber);
                Assert.Equal(new Guid("5E3E5033-9A77-4DCA-8B7F-DFED78EFA08A"), p.GuidId);
            }
        }

        [ProviderFact]
        [Trait("Category", "GetList")]
        public void GetListOneJoinUnmapped()
        {
            using (var connection = GetConnection())
            {
                var lst = connection.GetList<Product, ProductCategory>(
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

        [ProviderFact]
        [Trait("Category", "GetList")]
        public void GetListOneJoinMapped()
        {
            using (var connection = GetConnection())
            {
                var lst = connection.GetList<Product, ProductCategory, Product>(
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

        [ProviderFact]
        [Trait("Category", "GetList")]
        public void GetListTwoJoinsUnmapped()
        {
            using (var connection = GetConnection())
            {
                var lst = connection.GetList<Product, ProductCategory, ProductModel>(
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

        [ProviderFact]
        [Trait("Category", "GetList")]
        public void GetListTwoJoinsMapped()
        {
            using (var connection = GetConnection())
            {
                var lst = connection.GetList<Product, ProductCategory, ProductModel, Product>(
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

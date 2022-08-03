using System;
using Xunit;

namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    protected void ValidateProduct806(Product p)
    {
        Assert.NotNull(p);
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

    protected void ValidateProductCategory15(ProductCategory p)
    {
        Assert.NotNull(p);
        Assert.Equal(15, p.ProductCategoryID);
        //Assert.Equal(2, p.ParentProductCategoryID);
        Assert.Equal("Headsets", p.Name);
        //Assert.Equal(new Guid("7C782BBE-5A16-495A-AA50-10AFE5A84AF2"), p.GuidId);
    }

    protected void ValidateProductModel60(ProductModel p)
    {
        Assert.NotNull(p);
        Assert.Equal(60, p.ProductModelID);
        Assert.Equal("ML Headset", p.Name);
        Assert.Null(p.CatalogDescription);
        Assert.Equal(new DateTime(2002, 6, 1), p.ModifiedDate.Date);
        //Assert.Equal(new Guid("6BA9F3B6-E08B-4AC2-A725-B41114C2A283"), p.GuidId);
    }

    protected void ValidateProduct816(Product p)
    {
        Assert.NotNull(p);
        Assert.Equal(816, p.ProductID);
        Assert.Equal("ML Mountain Front Wheel", p.Name);
        Assert.Equal("FW-M762", p.ProductNumber);
        Assert.Equal("Black", p.Color);
        Assert.Equal(92.8071m, p.StandardCost);
        Assert.Equal(209.025m, p.ListPrice);
        Assert.Null(p.Size);
        Assert.Null(p.Weight);
        Assert.Equal(45, p.ProductModelID);
        Assert.Equal(new DateTime(2002, 7, 1), p.SellStartDate.Date);
        Assert.Equal(new DateTime(2003, 6, 30), p.SellEndDate.Value.Date);
        Assert.Null(p.DiscontinuedDate);
        Assert.Equal("wheel_small.gif", p.ThumbnailPhotoFileName);
        Assert.Equal(new Guid("5E3E5033-9A77-4DCA-8B7F-DFED78EFA08A"), p.GuidId);
        Assert.Equal(new DateTime(2004, 3, 11), p.ModifiedDate.Date);
    }

    protected void ValidateProductCategory21(ProductCategory p)
    {
        Assert.NotNull(p);
        Assert.Equal(21, p.ProductCategoryID);
        //Assert.Equal(2, p.ParentProductCategoryID);
        Assert.Equal("Wheels", p.Name);
        //Assert.Equal(new Guid("7C782BBE-5A16-495A-AA50-10AFE5A84AF2"), p.GuidId);
    }

    protected void ValidateProductModel45(ProductModel p)
    {
        Assert.NotNull(p);
        Assert.Equal(45, p.ProductModelID);
        Assert.Equal("ML Mountain Front Wheel", p.Name);
        Assert.Null(p.CatalogDescription);
        Assert.Equal(new DateTime(2002, 6, 1), p.ModifiedDate.Date);
        //Assert.Equal(new Guid("6BA9F3B6-E08B-4AC2-A725-B41114C2A283"), p.GuidId);
    }
}

using System;
using Xunit;

namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        private string GetMultiTwoParamQuery
        {
            get
            {
                return
                    $@"select  P.*, P.rowguid as GuidId, PC.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    where P.ProductID = {P}ProductId";
            }
        }

        private string GetMultiThreeParamQuery
        {
            get
            {
                return
                    $@"select  P.*, P.rowguid as GuidId, PC.*, PM.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where P.ProductID = {P}ProductId";
            }
        }


        private string GetListMultiTwoParamQuery
        {
            get
            {
                return
                    $@"select  P.*, P.rowguid as GuidId, PC.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    where P.Color = {P}Color";
            }
        }

        private string GetListMultiThreeParamQuery
        {
            get
            {
                return
                    $@"select  P.*, P.rowguid as GuidId, PC.*, PM.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where P.Color = {P}Color";
            }
        }


        private string GetFirstTwoParamQuery
        {
            get
            {
                return

            $@"select  P.*, P.rowguid as GuidId, PC.* 
            from Product P
            join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
            where Color = {P}Color and ProductId >= {P}ProductId order by ProductId";

            }
        }

        private string GetFirstThreeParamQuery
        {
            get
            {
                return
                    $@"select  P.*, P.rowguid as GuidId, PC.*, PM.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where Color = {P}Color and ProductId >= {P}ProductId order by ProductId";
            }
        }
    }
}

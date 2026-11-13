// ReSharper disable once CheckNamespace

using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

[SuppressMessage("ReSharper", "UseRawString")]
public abstract partial class TestSuite
{
    private string GetMultiTwoParamQuery =>
        $@"select  P.*, P.rowguid as GuidId, PC.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    where P.ProductID = {P}ProductId";

    private string GetMultiThreeParamQuery =>
        $@"select  P.*, P.rowguid as GuidId, PC.*, PM.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where P.ProductID = {P}ProductId";

    private string GetMultiThreeNoParamQuery =>
        @"select  P.*, P.rowguid as GuidId, PC.*, PM.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where P.ProductID = 806";

    private string GetMultiFourParamQuery =>
        $@"select  P.*, P.rowguid as GuidId, PC.*, PM.*, PMPD.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    join ProductModelProductDescription PMPD on PMPD.ProductModelID = PM.ProductModelID
                    where P.ProductID = {P}ProductId and PMPD.Culture = 'en    '";

    private string GetMultiFourNoParamQuery =>
        @"select  P.*, P.rowguid as GuidId, PC.*, PM.*, PMPD.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    join ProductModelProductDescription PMPD on PMPD.ProductModelID = PM.ProductModelID
                    where P.ProductID = 806 and PMPD.Culture = 'en    '";


    private string GetListMultiTwoParamQuery =>
        $@"select  P.*, P.rowguid as GuidId, PC.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    where P.Color = {P}Color";

    private string GetListMultiTwoNoParamQuery =>
        @"select  P.*, P.rowguid as GuidId, PC.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    where P.Color = 'Black'";

    private string GetListMultiThreeParamQuery =>
        $@"select  P.*, P.rowguid as GuidId, PC.*, PM.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where P.Color = {P}Color";

    private string GetListMultiThreeNoParamQuery =>
        @"select  P.*, P.rowguid as GuidId, PC.*, PM.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where P.Color = 'Black'";

    private string GetListMultiFourParamQuery =>
        $@"select  P.*, P.rowguid as GuidId, PC.*, PM.*, PMPD.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    join ProductModelProductDescription PMPD on PMPD.ProductModelID = PM.ProductModelID
                    where P.Color = {P}Color and PMPD.Culture = 'en    '";

    private string GetListMultiFourNoParamQuery =>
        @"select  P.*, P.rowguid as GuidId, PC.*, PM.*, PMPD.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    join ProductModelProductDescription PMPD on PMPD.ProductModelID = PM.ProductModelID
                    where P.Color = 'Black' and PMPD.Culture = 'en    '";

    private string GetFirstTwoParamQuery =>
        $@"select  P.*, P.rowguid as GuidId, PC.* 
            from Product P
            join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
            where Color = {P}Color and ProductId >= {P}ProductId order by ProductId";

    private string GetFirstTwoNoParamQuery =>
        @"select  P.*, P.rowguid as GuidId, PC.* 
            from Product P
            join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
            where Color = 'Black' and ProductId >= 816 order by ProductId";

    private string GetFirstThreeParamQuery =>
        $@"select  P.*, P.rowguid as GuidId, PC.*, PM.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where Color = {P}Color and ProductId >= {P}ProductId order by ProductId";

    private string GetFirstThreeNoParamQuery =>
        @"select  P.*, P.rowguid as GuidId, PC.*, PM.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where Color = 'Black' and ProductId >= 816 order by ProductId";

    private string GetFirstFourParamQuery =>
        $@"select  P.*, P.rowguid as GuidId, PC.*, PM.*, PMPD.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    join ProductModelProductDescription PMPD on PMPD.ProductModelID = PM.ProductModelID
                    where P.ProductID = {P}ProductId and PMPD.Culture = 'en    ' order by ProductId";

    private string GetFirstFourNoParamQuery =>
        @"select  P.*, P.rowguid as GuidId, PC.*, PM.*, PMPD.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    join ProductModelProductDescription PMPD on PMPD.ProductModelID = PM.ProductModelID
                    where P.ProductID = 816 and PMPD.Culture = 'en    ' order by ProductId";
}

namespace Dapper.Database.Tests;

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


    private string GetListMultiTwoParamQuery =>
        $@"select  P.*, P.rowguid as GuidId, PC.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    where P.Color = {P}Color";

    private string GetListMultiThreeParamQuery =>
        $@"select  P.*, P.rowguid as GuidId, PC.*, PM.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where P.Color = {P}Color";


    private string GetFirstTwoParamQuery =>
        $@"select  P.*, P.rowguid as GuidId, PC.* 
            from Product P
            join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
            where Color = {P}Color and ProductId >= {P}ProductId order by ProductId";

    private string GetFirstThreeParamQuery =>
        $@"select  P.*, P.rowguid as GuidId, PC.*, PM.*
                    from Product P
                    join ProductCategory PC on PC.ProductCategoryID = P.ProductCategoryID
                    join ProductModel PM on PM.ProductModelID = P.ProductModelID
                    where Color = {P}Color and ProductId >= {P}ProductId order by ProductId";
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

namespace Dapper.Database.Tests;

public partial class SqlServerTestSuite
{
    [Fact]
    [Trait("Category", "GetMultiple")]
    public void GetMultiple()
    {
        using var db = GetSqlDatabase();
        using var trans = db.GetTransaction();
        var dt = db.GetMultiple(@"
                    select * from Product where Color = 'Black';
                    select * from ProductCategory where productcategoryid = '21';");
        Assert.Equal(89, dt.Read(typeof(Product)).Count());

        var pc = (ProductCategory)dt.ReadSingle(typeof(ProductCategory));
        ValidateProductCategory21(pc);
        trans.Complete();
    }

    [Fact]
    [Trait("Category", "GetMultiple")]
    public void GetMultipleWithParameter()
    {
        using var db = GetSqlDatabase();
        using var trans = db.GetTransaction();
        var dt = db.GetMultiple($@"
                    select * from Product where Color = {P}Color;
                    select * from ProductCategory where productcategoryid = {P}ProductCategoryId;",
            new { Color = "Black", ProductCategoryId = 21 });
        Assert.Equal(89, dt.Read(typeof(Product)).Count());

        var pc = (ProductCategory)dt.ReadSingle(typeof(ProductCategory));
        ValidateProductCategory21(pc);
        trans.Complete();
    }

    [Fact]
    [Trait("Category", "GetList")]
    public void GetListUsingTableValueParamter()
    {
        using var db = GetSqlDatabase();
        var dataTable = new DataTable("DT");
        dataTable.Columns.Add("ProductId", typeof(int));
        dataTable.Rows.Add(816);
        dataTable.Rows.Add(731);

        var lst = db.GetList<Product>(@"
                    SELECT P.*, P.rowguid as GuidId FROM Product P
                    INNER JOIN @productIdTVP PTVP ON PTVP.ProductId = P.ProductId",
            new { productIdTVP = dataTable.AsTableValuedParameter("[dbo].[ProductIdTable]") });

        Assert.Equal(2, lst.Count());
        var item = lst.Single(p => p.ProductID == 816);
        ValidateProduct816(item);
    }

    [Fact]
    [Trait("Category", "Update")]
    public void UpdateTimestamp()
    {
        using var db = GetSqlDatabase();
        var p = new PersonTimestamp { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        Assert.True(db.Insert(p));
        Assert.NotNull(p.ConcurrencyToken);
        var token1 = p.ConcurrencyToken;

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(db.Update(p), "ConcurrencyToken matched, update succeeded");
        Assert.NotEqual(token1, p.ConcurrencyToken);

        // Simulate an independent change
        db.Execute("update Person set Age = 1 where GuidId = @GuidId", p);

        p.FirstName = "Alice";
        p.LastName = "Jones";
        Assert.ThrowsAny<OptimisticConcurrencyException>(() => db.Update(p));

        var gp = db.Get<PersonTimestamp>(p.GuidId);

        Assert.Equal("Greg", gp.FirstName);
        Assert.Equal("Smith", gp.LastName);
        Assert.NotEqual(gp.ConcurrencyToken, p.ConcurrencyToken);
    }

    [Fact]
    [Trait("Category", "Upsert")]
    public void UpsertTimestamp()
    {
        using var db = GetSqlDatabase();
        var p = new PersonTimestamp { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        Assert.True(db.Upsert(p));
        Assert.NotNull(p.ConcurrencyToken);
        var token1 = p.ConcurrencyToken;

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(db.Upsert(p), "ConcurrencyToken matched, update succeeded");
        Assert.NotEqual(token1, p.ConcurrencyToken);

        // Simulate an independent change
        db.Execute("update Person set Age = 1 where GuidId = @GuidId", p);

        p.FirstName = "Alice";
        p.LastName = "Jones";
        Assert.ThrowsAny<OptimisticConcurrencyException>(() => db.Upsert(p));

        var gp = db.Get<PersonTimestamp>(p.GuidId);

        Assert.Equal("Greg", gp.FirstName);
        Assert.Equal("Smith", gp.LastName);
        Assert.NotEqual(gp.ConcurrencyToken, p.ConcurrencyToken);
    }


    [Fact]
    [Trait("Category", "Delete")]
    public void DeleteConstraint()
    {
        using var db = GetSqlDatabase();
        var ex = Assert.ThrowsAny<SqlException>(() => db.Delete<ProductModel>(20));
        Assert.Contains("FK_ProductModelProductDescription_ProductModel", ex.Message);
    }


    [Fact]
    [Trait("Category", "Delete")]
    public async Task DeleteConstraintAsync()
    {
        using var db = GetSqlDatabase();
        var ex = await Assert.ThrowsAnyAsync<SqlException>(() => db.DeleteAsync<ProductModel>(20));
        Assert.Contains("FK_ProductModelProductDescription_ProductModel", ex.Message);
    }


    [Fact]
    [Trait("Category", "Insert")]
    public void InsertHasTrigger()
    {
        var dnow = DateTime.UtcNow;
        using var db = GetSqlDatabase();
        var p = new AccountModel { FirstName = "Jim", LastName = "Beam" };

        Assert.Equal(DateTime.MinValue, p.CreatedOn);

        Assert.True(db.Insert(p));
        Assert.True(p.AccountId > 0);
        Assert.NotNull(p.ConcurrencyToken);
        Assert.NotEqual(DateTime.MinValue, p.CreatedOn);
        Assert.InRange(p.CreatedOn, dnow.AddSeconds(-1), dnow.AddSeconds(1));

        var gp = db.Get<AccountModel>(p.AccountId);

        Assert.Equal(p.AccountId, gp.AccountId);
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.FirstName + " " + p.LastName, gp.FirstName + " " + gp.LastName);
        Assert.InRange(gp.CreatedOn, dnow.AddSeconds(-1), dnow.AddSeconds(1));
    }


    [Fact]
    [Trait("Category", "Insert")]
    public async Task InsertHasTriggerAsync()
    {
        var dnow = DateTime.UtcNow;
        using var db = GetSqlDatabase();
        var p = new AccountModel { FirstName = "Jim", LastName = "Beam" };

        Assert.Equal(DateTime.MinValue, p.CreatedOn);

        Assert.True(await db.InsertAsync(p));
        Assert.True(p.AccountId > 0);
        Assert.NotNull(p.ConcurrencyToken);
        Assert.NotEqual(DateTime.MinValue, p.CreatedOn);
        Assert.InRange(p.CreatedOn, dnow.AddSeconds(-1), dnow.AddSeconds(1));

        var gp = db.Get<AccountModel>(p.AccountId);

        Assert.Equal(p.AccountId, gp.AccountId);
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.FirstName + " " + p.LastName, gp.FirstName + " " + gp.LastName);
        Assert.InRange(gp.CreatedOn, dnow.AddSeconds(-1), dnow.AddSeconds(1));
    }

    [Fact]
    [Trait("Category", "Update")]
    public void UpdateHasTrigger()
    {
        var dnow = DateTime.UtcNow;
        using var db = GetSqlDatabase();
        var p = new AccountModel { FirstName = "Sally", LastName = "Walker" };
        Assert.True(db.Insert(p));
        Assert.NotNull(p.ConcurrencyToken);
        var token1 = p.ConcurrencyToken;

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(db.Update(p), "ConcurrencyToken matched, update succeeded");
        Assert.NotEqual(token1, p.ConcurrencyToken);

        var gp = db.Get<AccountModel>(p.AccountId);

        Assert.Equal(p.AccountId, gp.AccountId);
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.FirstName + " " + p.LastName, gp.FirstName + " " + gp.LastName);
        Assert.InRange(gp.CreatedOn, dnow.AddSeconds(-1), dnow.AddSeconds(1));
    }

    [Fact]
    [Trait("Category", "UpdateAsync")]
    public async Task UpdateHasTriggerAsync()
    {
        var dnow = DateTime.UtcNow;
        using var db = GetSqlDatabase();
        var p = new AccountModel { FirstName = "Sally", LastName = "Walker" };
        Assert.True(await db.InsertAsync(p));
        Assert.NotNull(p.ConcurrencyToken);
        var token1 = p.ConcurrencyToken;

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(await db.UpdateAsync(p), "ConcurrencyToken matched, update succeeded");
        Assert.NotEqual(token1, p.ConcurrencyToken);

        var gp = db.Get<AccountModel>(p.AccountId);

        Assert.Equal(p.AccountId, gp.AccountId);
        Assert.Equal(p.FirstName, gp.FirstName);
        Assert.Equal(p.FirstName + " " + p.LastName, gp.FirstName + " " + gp.LastName);
        Assert.InRange(gp.CreatedOn, dnow.AddSeconds(-1), dnow.AddSeconds(1));
    }
}

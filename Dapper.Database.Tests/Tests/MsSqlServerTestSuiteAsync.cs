using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

[SuppressMessage("ReSharper", "UseRawString")]
public partial class MsSqlServerTestSuite
{
    [Fact]
    [Trait("Category", "GetMultipleAsync")]
    public async Task GetMultipleAsync()
    {
        using var db = GetSqlDatabase();
        using var trans = db.GetTransaction();
        var dt = await db.GetMultipleAsync(@"
                    select * from Product where Color = 'Black';
                    select * from ProductCategory where productcategoryid = '21';");
        Assert.Equal(89, (await dt.ReadAsync(typeof(Product))).Count());

        var pc = (ProductCategory)await dt.ReadSingleAsync(typeof(ProductCategory));
        ValidateProductCategory21(pc);
        trans.Complete();
    }

    [Fact]
    [Trait("Category", "GetMultipleAsync")]
    public async Task GetMultipleWithParameterAsync()
    {
        using var db = GetSqlDatabase();
        using var trans = db.GetTransaction();
        var dt = await db.GetMultipleAsync($@"
                    select * from Product where Color = {P}Color;
                    select * from ProductCategory where productcategoryid = {P}ProductCategoryId;",
            new { Color = "Black", ProductCategoryId = 21 });
        Assert.Equal(89, (await dt.ReadAsync(typeof(Product))).Count());

        var pc = (ProductCategory)await dt.ReadSingleAsync(typeof(ProductCategory));
        ValidateProductCategory21(pc);
        trans.Complete();
    }

    [Fact]
    [Trait("Category", "GetListAsync")]
    public async Task GetListUsingTableValueParameterAsync()
    {
        using var db = GetSqlDatabase();
        var dataTable = new DataTable("DT");
        dataTable.Columns.Add("ProductId", typeof(int));
        dataTable.Rows.Add(816);
        dataTable.Rows.Add(731);

        // ReSharper disable StringLiteralTypo
        var lst = (await db.GetListAsync<Product>(@"
                    SELECT P.*, P.rowguid as GuidId FROM Product P
                    INNER JOIN @productIdTVP PTVP ON PTVP.ProductId = P.ProductId",
            new { productIdTVP = dataTable.AsTableValuedParameter("[dbo].[ProductIdTable]") })).ToList();
        // ReSharper restore StringLiteralTypo

        Assert.Equal(2, lst.Count);
        var item = lst.Single(p => p.ProductID == 816);
        ValidateProduct816(item);
    }

    [Fact]
    [Trait("Category", "UpdateAsync")]
    public async Task UpdateTimestampAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonTimestamp { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        Assert.True(await db.InsertAsync(p));
        Assert.NotNull(p.ConcurrencyToken);
        var token1 = p.ConcurrencyToken;

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(await db.UpdateAsync(p), "ConcurrencyToken matched, update succeeded");
        Assert.NotEqual(token1, p.ConcurrencyToken);

        // Simulate an independent change
        await db.ExecuteAsync("update Person set Age = 1 where GuidId = @GuidId", p);

        p.FirstName = "Alice";
        p.LastName = "Jones";
        await Assert.ThrowsAnyAsync<OptimisticConcurrencyException>(() => db.UpdateAsync(p));

        var gp = await db.GetAsync<PersonTimestamp>(p.GuidId);

        Assert.Equal("Greg", gp.FirstName);
        Assert.Equal("Smith", gp.LastName);
        Assert.NotEqual(gp.ConcurrencyToken, p.ConcurrencyToken);
    }

    [Fact]
    [Trait("Category", "UpsertAsync")]
    public async Task UpsertTimestampAsync()
    {
        using var db = GetSqlDatabase();
        var p = new PersonTimestamp { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
        Assert.True(await db.UpsertAsync(p));
        Assert.NotNull(p.ConcurrencyToken);
        var token1 = p.ConcurrencyToken;

        p.FirstName = "Greg";
        p.LastName = "Smith";
        Assert.True(await db.UpsertAsync(p), "ConcurrencyToken matched, update succeeded");
        Assert.NotEqual(token1, p.ConcurrencyToken);

        // Simulate an independent change
        await db.ExecuteAsync("update Person set Age = 1 where GuidId = @GuidId", p);

        p.FirstName = "Alice";
        p.LastName = "Jones";
        await Assert.ThrowsAnyAsync<OptimisticConcurrencyException>(() => db.UpsertAsync(p));

        var gp = await db.GetAsync<PersonTimestamp>(p.GuidId);

        Assert.Equal("Greg", gp.FirstName);
        Assert.Equal("Smith", gp.LastName);
        Assert.NotEqual(gp.ConcurrencyToken, p.ConcurrencyToken);
    }
}

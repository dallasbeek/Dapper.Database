﻿using System;
using Xunit;
using FactAttribute = Xunit.SkippableFactAttribute;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    [Fact]
    [Trait("Category", "Exists")]
    public void ExistsNoArgs()
    {
        using var db = GetSqlDatabase();
        Assert.True(db.Exists<Product>());
    }

    [Fact]
    [Trait("Category", "Exists")]
    public void ExistsByEntity()
    {
        using var db = GetSqlDatabase();
        var p = new Product { ProductID = 806, GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") };
        Assert.True(db.Exists(p));

        p.ProductID = -1;
        Assert.False(db.Exists(p));
    }

    [Fact]
    [Trait("Category", "Exists")]
    public void ExistsByAliasIntegerId()
    {
        using var db = GetSqlDatabase();
        Assert.True(db.Exists<ProductAlias>(806));
        Assert.False(db.Exists<ProductAlias>(-1));
    }

    [Fact]
    [Trait("Category", "Exists")]
    public void ExistsByIntegerId()
    {
        using var db = GetSqlDatabase();
        Assert.True(db.Exists<Product>(806));
        Assert.False(db.Exists<Product>(-1));
    }

    [Fact]
    [Trait("Category", "Exists")]
    public void ExistsByGuidIdWhereClause()
    {
        using var db = GetSqlDatabase();
        if (GetProvider() == Provider.Firebird || GetProvider() == Provider.SQLite)
        {
            Assert.True(db.Exists<Product>($"where rowguid = {P}GuidId",
                new { GuidId = "23B5D52B-8C29-4059-B899-75C53B5EE2E6" }));
            Assert.False(db.Exists<Product>($"where rowguid = {P}GuidId",
                new { GuidId = "1115D52B-8C29-4059-B899-75C53B5EE2E6" }));
        }
        else
        {
            Assert.True(db.Exists<Product>($"where rowguid = {P}GuidId",
                new { GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") }));
            Assert.False(db.Exists<Product>($"where rowguid = {P}GuidId",
                new { GuidId = new Guid("1115D52B-8C29-4059-B899-75C53B5EE2E6") }));
        }
    }

    [Fact]
    [Trait("Category", "Exists")]
    public void ExistsPartialBySelect()
    {
        using var db = GetSqlDatabase();
        Assert.True(db.Exists<Product>(
            $"select p.ProductId, p.rowguid AS GuidId, Name from Product p where p.ProductId = {P}Id",
            new { Id = 806 }));
        Assert.False(db.Exists<Product>(
            $"select p.ProductId, p.rowguid AS GuidId, Name from Product p where p.ProductId = {P}Id",
            new { Id = -1 }));
    }

    [Fact]
    [Trait("Category", "Exists")]
    public void ExistsBySelect()
    {
        using var db = GetSqlDatabase();
        Assert.True(db.Exists<Product>($"select p.*, p.rowguid AS GuidId  from Product p where p.ProductId = {P}Id",
            new { Id = 806 }));
        Assert.False(db.Exists<Product>(
            $"select p.*, p.rowguid AS GuidId  from Product p where p.ProductId = {P}Id", new { Id = -1 }));
    }

    [Fact]
    [Trait("Category", "Exists")]
    public void ExistsShortCircuitSemiColon()
    {
        using var db = GetSqlDatabase();
        var trueSql = "; select 1 AS ProductId";
        var falseSql = "; select 0 AS ProductId";
        if (GetProvider() == Provider.Firebird)
        {
            trueSql += " from RDB$Database";
            falseSql += " from RDB$Database";
        }
        else if (GetProvider() == Provider.Oracle)
        {
            trueSql += " from dual";
            falseSql += " from dual";
        }

        Assert.True(db.Exists<Product>(trueSql));
        Assert.False(db.Exists<Product>(falseSql));
    }

    [Fact]
    [Trait("Category", "Exists")]
    public void ExistsBySelectNoType()
    {
        using var db = GetSqlDatabase();
        Assert.True(db.Exists("select 1 from Product p where p.ProductId = 806"));
        Assert.False(db.Exists("select 1 from Product p where p.ProductId = -1"));
    }

    [Fact]
    [Trait("Category", "Exists")]
    public void ExistsBySelectNoTypeParameter()
    {
        using var db = GetSqlDatabase();
        Assert.True(db.Exists($"select 1 from Product p where p.ProductId = {P}Id",
            new { Id = 806 }));
        Assert.False(db.Exists(
            $"select 1 from Product p where p.ProductId = {P}Id", new { Id = -1 }));
    }
}

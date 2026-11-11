# Dapper.Database

Alternative database-first micro-ORM, built as an extension layer on top of [Dapper](https://github.com/DapperLib/Dapper). It adds CRUD convenience methods (`Get`, `GetList`, `GetPageList`, `Insert`, `Update`, `Upsert`, `Delete`, `Exists`, `Count`, etc.), optimistic concurrency support, and SQL dialect adapters for multiple database providers — without the overhead of a full ORM like Entity Framework.

Supported providers: SQL Server, SQL Server Compact Edition, PostgreSQL, MySQL/MariaDB, SQLite, Oracle, and Firebird.

Targets: `netstandard2.0`, `net462`, `net8.0`, `net10.0`.

Installation
------------

```powershell
dotnet add package Dapper.Database
```

Quick Start
-----------

Define a POCO and decorate it with attributes describing the table and keys:

```csharp
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Product")]
public class Product
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Color { get; set; }

    public decimal ListPrice { get; set; }

    [IgnoreInsert]
    public DateTime? UpdatedOn { get; set; }
}
```

Use `SqlDatabase` to open/close the connection and run CRUD operations. `SqlDatabase` implements `ISqlDatabase`, which wraps an `IDbConnection` and handles connection lifetime and SQL dialect generation for you:

```csharp
using Dapper.Database;
using Microsoft.Data.SqlClient;

using (var db = new SqlDatabase(new StringConnectionService<SqlConnection>(connectionString)))
{
    // Create
    var product = new Product { Name = "Volvo", Color = "Black", ListPrice = 25000m };
    db.Insert(product);

    // Read
    var byId = db.Get<Product>(product.Id);
    var blackProducts = db.GetList<Product>("where Color = @Color", new { Color = "Black" });

    // Update
    product.ListPrice = 23500m;
    db.Update(product);

    // Delete
    db.Delete(product);
}
```

You can also use the extension methods directly on any open `IDbConnection` (the lower-level API that `SqlDatabase` builds on):

```csharp
using var connection = new SqlConnection(connectionString);
connection.Open();

var product = connection.Get<Product>(806);
```

Every operation has an async counterpart (`InsertAsync`, `GetAsync`, `UpdateAsync`, `DeleteAsync`, `UpsertAsync`, `GetListAsync`, `GetPageListAsync`, ...):

```csharp
await using var db = new SqlDatabase(new StringConnectionService<SqlConnection>(connectionString));

var product = await db.GetAsync<Product>(806);
product.ListPrice = 23500m;
await db.UpdateAsync(product);
```

CRUD Examples
-------------

### `Get`

Get one specific entity by primary key:

```csharp
var product = db.Get<Product>(806);
```

or with a `where` clause and parameters:

```csharp
var product = db.Get<Product>("where ProductId = @Id", new { Id = 2323 });
```

### `GetList`

```csharp
var products = db.GetList<Product>("where Color = @Color", new { Color = "Black" });
```

### `GetPageList`

Get page 5, with 10 records per page:

```csharp
var page = db.GetPageList<Product>(5, 10, "where Color = @Color order by Name", new { Color = "Black" });
```

### `GetFirst`

```csharp
var cheapest = db.GetFirst<Product>("where Color = @Color order by ListPrice", new { Color = "Black" });
```

### `Count` / `Exists`

```csharp
var count = db.Count<Product>("where Color = @Color", new { Color = "Black" });
var exists = db.Exists<Product>("where Color = @Color", new { Color = "Black" });
```

### `Insert`

```csharp
db.Insert(new Product { Name = "Volvo", Color = "Black", ListPrice = 25000m });
```

Insert a batch:

```csharp
db.InsertList(new[]
{
    new Product { Name = "Volvo", Color = "Black" },
    new Product { Name = "Saab", Color = "Red" }
});
```

### `Update`

Update a full entity:

```csharp
db.Update(new Product { Id = 1, Name = "Saab", Color = "Red" });
```

Or limit the update to specific columns:

```csharp
db.Update(new Product { Id = 1, Name = "Saab" }, new[] { "Name" });
```

Update many rows matching a condition without loading them first:

```csharp
db.UpdateMany<Product>("where Color = @OldColor", new[] { "Color" }, new { Color = "Blue", OldColor = "Black" });
```

### `Upsert`

Insert the record if it doesn't exist, otherwise update it. Optional callbacks let you set audit columns depending on which path was taken:

```csharp
db.Upsert(
    new Product { Id = 1, Name = "Saab", Color = "Red" },
    new[] { "Name", "Color", "UpdatedOn" },
    insert => insert.UpdatedOn = DateTime.UtcNow,
    update => update.UpdatedOn = DateTime.UtcNow);
```

### `Delete`

By key:

```csharp
db.Delete<Product>(1);
```

By entity:

```csharp
db.Delete(product);
```

By `where` clause:

```csharp
db.Delete<Product>("where Color = @Color", new { Color = "Red" });
```

All rows:

```csharp
db.DeleteAll<Product>();
```

### `Execute` / `ExecuteScalar`

For stored procedures or raw SQL that doesn't fit the query builder. Prefixing with `;` short-circuits the query builder logic:

```csharp
db.Execute("; EXEC usp_RecalculatePrices @Color", new { Color = "Black" });

var total = db.ExecuteScalar<int>("; SELECT COUNT(*) FROM Product WHERE Color = @Color", new { Color = "Black" });
```

### `GetMultiple` (multiple result sets)

```csharp
using var grid = db.GetMultiple("; SELECT * FROM Product; SELECT * FROM Category");
var products = grid.Read<Product>();
var categories = grid.Read<Category>();
```

### Transactions

`SqlDatabase` can wrap operations in a transaction:

```csharp
using (var db = new SqlDatabase(new StringConnectionService<SqlConnection>(connectionString)))
{
    db.BeginTransaction();
    try
    {
        db.Insert(new Product { Name = "Volvo" });
        db.Insert(new Product { Name = "Saab" });
        db.CompleteTransaction();
    }
    catch
    {
        db.RollbackTransaction();
        throw;
    }
}
```

Attributes
----------

A number of attributes can be used to decorate your classes to control mapping, generation, and concurrency behavior.

### From `System.ComponentModel.DataAnnotations`

#### `[Table]`

Specifies the SQL table to use. For databases that support schema, generated queries will include a schema if specified.

```csharp
[Table("User", Schema = "Security")]
public class User
{
}
```

#### `[Column]`

Optional attribute that allows mapping a property to an alternately named column.

```csharp
public class User
{
    [Column("UserName")]
    public string Name { get; set; }
}
```

#### `[DatabaseGenerated]`

Attribute for computed columns and identity columns. Logic will refresh generated properties after insert and update.

```csharp
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Identity { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string FullName { get; set; }
}
```

#### `[Key]`

Attribute to be used for primary keys.

```csharp
public class User
{
    [Key]
    public int Id { get; set; }
}
```

> ?? Keys are considered non-nullable, regardless of type.

#### `[Required]`

Attribute for fields that require a non-null value. Used to perform null checks in SQL where necessary.

```csharp
#nullable disable

public class User
{
    [Required]
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    [Required]
    public string LastName { get; set; }
}
```

Note that non-null value types (and non-null reference types in C# 8.0+) are also honored. The following is equivalent to the above:

```csharp
#nullable enable

public class User
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
}
```

#### `[ConcurrencyCheck]`

Attribute for columns that should be checked on update and delete statements, for optimistic concurrency.

```csharp
public class User
{
    [ConcurrencyCheck]
    public DateTime? ModifiedOn { get; set; }
}
```

#### `[Timestamp]`

Attribute for a database-generated "row version" column that should be checked on update and delete statements, for optimistic concurrency.

```csharp
public class User
{
    [Timestamp]
    public byte[] RowVersion { get; set; }
}
```

### From `Dapper.Database.Attributes`

#### `[IgnoreInsert]`

Ignores the property on an insert statement.

```csharp
public class User
{
    [IgnoreInsert]
    public DateTime? ModifiedOn { get; set; }
}
```

#### `[IgnoreUpdate]`

Ignores the property on update statements.

```csharp
public class User
{
    [IgnoreUpdate]
    public DateTime CreatedOn { get; set; }
}
```

#### `[IgnoreSelect]`

Ignores the property on select statements.

```csharp
public class User
{
    [IgnoreSelect]
    public string Password { get; set; }
}
```

#### `[ReadOnly]`

Ignores the property on insert and update.

```csharp
public class User
{
    [ReadOnly]
    public string SpecialInfo { get; set; }
}
```

#### `[Ignore]`

Indicates that there isn't a database-backed column.

```csharp
public class User
{
    [Ignore]
    public string NoDbColumn { get; set; }
}
```

### Full example

```csharp
[Table("Location", Schema = "dbo")]
public class Location
{
    [Column, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Column]
    public virtual string Code { get; set; }

    [Column, ConcurrencyCheck]
    public virtual string Name { get; set; }

    [Column("Database"), ConcurrencyCheck]
    public virtual string DatabaseName { get; set; }

    [Column, IgnoreUpdate]
    public virtual DateTime CreatedOn { get; set; }

    [Column, IgnoreInsert]
    public virtual DateTime? UpdatedOn { get; set; }

    [Ignore]
    public virtual int NoDbColumn { get; set; }
}
```

Code Generation
----------------

A T4 code generation template is available to scaffold entity classes from an existing database: [Code Generator](https://github.com/dallasbeek/Dapper.Database/tree/master/T4Templates).

Full API Reference
-------------------

The full list of extension methods available on `IDbConnection`/`ISqlDatabase` (sync and async variants for every CRUD operation, plus multi-type mapping overloads for joins):

```csharp
// Insert
bool Insert<T>(T entityToInsert);
Task<bool> InsertAsync<T>(T entityToInsert);

bool InsertList<T>(IEnumerable<T> entitiesToInsert);
Task<bool> InsertListAsync<T>(IEnumerable<T> entitiesToInsert);

// Update
bool Update<T>(T entityToUpdate);
bool Update<T>(T entityToUpdate, IEnumerable<string> columnsToUpdate);

Task<bool> UpdateAsync<T>(T entityToUpdate);
Task<bool> UpdateAsync<T>(T entityToUpdate, IEnumerable<string> columnsToUpdate);

bool UpdateList<T>(IEnumerable<T> entitiesToUpdate);
bool UpdateList<T>(IEnumerable<T> entitiesToUpdate, IEnumerable<string> columnsToUpdate);

Task<bool> UpdateListAsync<T>(IEnumerable<T> entitiesToUpdate);
Task<bool> UpdateListAsync<T>(IEnumerable<T> entitiesToUpdate, IEnumerable<string> columnsToUpdate);

int UpdateMany<T>(string whereClause, IEnumerable<string> columnsToUpdate, object parameters);
Task<int> UpdateManyAsync<T>(string whereClause, IEnumerable<string> columnsToUpdate, object parameters);

// Update Or Insert
bool Upsert<T>(T entityToUpsert);
bool Upsert<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate);
bool Upsert<T>(T entityToUpsert, Action<T> insertAction, Action<T> updateAction);
bool Upsert<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction);

Task<bool> UpsertAsync<T>(T entityToUpsert);
Task<bool> UpsertAsync<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate);
Task<bool> UpsertAsync<T>(T entityToUpsert, Action<T> insertAction, Action<T> updateAction);
Task<bool> UpsertAsync<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction);

bool UpsertList<T>(IEnumerable<T> entitiesToUpsert);
bool UpsertList<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate);
bool UpsertList<T>(IEnumerable<T> entitiesToUpsert, Action<T> insertAction, Action<T> updateAction);
bool UpsertList<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction);

Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert);
Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate);
Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert, Action<T> insertAction, Action<T> updateAction);
Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction);

// Delete
bool Delete<T>(T entityToDelete);
bool Delete<T>(object primaryKeyValue);
bool Delete<T>(string whereClause);
bool Delete<T>(string whereClause, object parameters);
bool DeleteAll<T>();

Task<bool> DeleteAsync<T>(T entityToDelete);
Task<bool> DeleteAsync<T>(object primaryKeyValue);
Task<bool> DeleteAsync<T>(string whereClause);
Task<bool> DeleteAsync<T>(string whereClause, object parameters);
Task<bool> DeleteAllAsync<T>();

// Query
int Count(string fullSql);
int Count(string fullSql, object parameters);
int Count<T>(string sql = null);
int Count<T>(string sql, object parameters);

Task<int> CountAsync(string fullSql);
Task<int> CountAsync(string fullSql, object parameters);
Task<int> CountAsync<T>(string sql = null);
Task<int> CountAsync<T>(string sql, object parameters);

bool Exists(string fullSql = null);
bool Exists(string fullSql, object parameters);
bool Exists<T>(T entityToCheck);
bool Exists<T>(object primaryKey);
bool Exists<T>(string sql = null);
bool Exists<T>(string sql, object parameters);

Task<bool> ExistsAsync(string fullSql = null);
Task<bool> ExistsAsync(string fullSql, object parameters);
Task<bool> ExistsAsync<T>(T entityToCheck);
Task<bool> ExistsAsync<T>(object primaryKey);
Task<bool> ExistsAsync<T>(string sql = null);
Task<bool> ExistsAsync<T>(string sql, object parameters);

T Get<T>(T entityToGet);
T Get<T>(object primaryKey);
T Get<T>(string sql, object parameters);
T1 Get<T1, T2>(string sql, object parameters, string splitOn = null);
T1 Get<T1, T2, T3>(string sql, string splitOn = null);
T1 Get<T1, T2, T3>(string sql, object parameters, string splitOn = null);
T1 Get<T1, T2, T3, T4>(string sql, string splitOn = null);
T1 Get<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null);
TRet Get<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null);
TRet Get<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null);
TRet Get<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null);
TRet Get<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null);
TRet Get<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null);
TRet Get<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null);

Task<T> GetAsync<T>(T entityToGet);
Task<T> GetAsync<T>(object primaryKey);
Task<T> GetAsync<T>(string sql, object parameters);
Task<T1> GetAsync<T1, T2>(string sql, object parameters, string splitOn = null);
Task<T1> GetAsync<T1, T2, T3>(string sql, string splitOn = null);
Task<T1> GetAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null);
Task<T1> GetAsync<T1, T2, T3, T4>(string sql, string splitOn = null);
Task<T1> GetAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null);
Task<TRet> GetAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null);
Task<TRet> GetAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null);
Task<TRet> GetAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null);
Task<TRet> GetAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null);
Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null);
Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null);

T GetFirst<T>(string sql = null);
T GetFirst<T>(string sql, object parameters);
T1 GetFirst<T1, T2>(string sql, string splitOn = null);
T1 GetFirst<T1, T2>(string sql, object parameters, string splitOn = null);
T1 GetFirst<T1, T2, T3>(string sql, string splitOn = null);
T1 GetFirst<T1, T2, T3>(string sql, object parameters, string splitOn = null);
T1 GetFirst<T1, T2, T3, T4>(string sql, string splitOn = null);
T1 GetFirst<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null);
TRet GetFirst<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null);
TRet GetFirst<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null);
TRet GetFirst<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null);
TRet GetFirst<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null);
TRet GetFirst<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null);
TRet GetFirst<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null);

Task<T> GetFirstAsync<T>(string sql = null);
Task<T> GetFirstAsync<T>(string sql, object parameters);
Task<T1> GetFirstAsync<T1, T2>(string sql, string splitOn = null);
Task<T1> GetFirstAsync<T1, T2>(string sql, object parameters, string splitOn = null);
Task<T1> GetFirstAsync<T1, T2, T3>(string sql, string splitOn = null);
Task<T1> GetFirstAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null);
Task<T1> GetFirstAsync<T1, T2, T3, T4>(string sql, string splitOn = null);
Task<T1> GetFirstAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null);
Task<TRet> GetFirstAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null);
Task<TRet> GetFirstAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null);
Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null);
Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null);
Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null);
Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null);

IEnumerable<T> GetList<T>(string sql = null);
IEnumerable<T> GetList<T>(string sql, object parameters);
IEnumerable<T1> GetList<T1, T2>(string sql, string splitOn = null);
IEnumerable<T1> GetList<T1, T2>(string sql, object parameters, string splitOn = null);
IEnumerable<T1> GetList<T1, T2, T3>(string sql, string splitOn = null);
IEnumerable<T1> GetList<T1, T2, T3>(string sql, object parameters, string splitOn = null);
IEnumerable<T1> GetList<T1, T2, T3, T4>(string sql, string splitOn = null);
IEnumerable<T1> GetList<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null);
IEnumerable<TRet> GetList<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null);
IEnumerable<TRet> GetList<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null);
IEnumerable<TRet> GetList<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null);
IEnumerable<TRet> GetList<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null);
IEnumerable<TRet> GetList<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null);
IEnumerable<TRet> GetList<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null);

Task<IEnumerable<T>> GetListAsync<T>(string sql = null);
Task<IEnumerable<T>> GetListAsync<T>(string sql, object parameters);
Task<IEnumerable<T1>> GetListAsync<T1, T2>(string sql, string splitOn = null);
Task<IEnumerable<T1>> GetListAsync<T1, T2>(string sql, object parameters, string splitOn = null);
Task<IEnumerable<T1>> GetListAsync<T1, T2, T3>(string sql, string splitOn = null);
Task<IEnumerable<T1>> GetListAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null);
Task<IEnumerable<T1>> GetListAsync<T1, T2, T3, T4>(string sql, string splitOn = null);
Task<IEnumerable<T1>> GetListAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null);
Task<IEnumerable<TRet>> GetListAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null);
Task<IEnumerable<TRet>> GetListAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null);
Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null);
Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null);
Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null);
Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null);

IPagedEnumerable<T> GetPageList<T>(int page, int pageSize, string sql = null);
IPagedEnumerable<T> GetPageList<T>(int page, int pageSize, string sql, object parameters);
IPagedEnumerable<T1> GetPageList<T1, T2>(int page, int pageSize, string sql, string splitOn = null);
IPagedEnumerable<T1> GetPageList<T1, T2>(int page, int pageSize, string sql, object parameters, string splitOn = null);
IPagedEnumerable<T1> GetPageList<T1, T2, T3>(int page, int pageSize, string sql, string splitOn = null);
IPagedEnumerable<T1> GetPageList<T1, T2, T3>(int page, int pageSize, string sql, object parameters, string splitOn = null);
IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(int page, int pageSize, string sql, string splitOn = null);
IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(int page, int pageSize, string sql, object parameters, string splitOn = null);
IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, string splitOn = null);
IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null);
IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null);
IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null);
IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null);
IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null);

Task<IPagedEnumerable<T>> GetPageListAsync<T>(int page, int pageSize, string sql, object parameters);
Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2>(int page, int pageSize, string sql, string splitOn = null);
Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2>(int page, int pageSize, string sql, object parameters, string splitOn = null);
Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3>(int page, int pageSize, string sql, string splitOn = null);
Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3>(int page, int pageSize, string sql, object parameters);
Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3, T4>(int page, int pageSize, string sql, string splitOn = null);
Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3, T4>(int page, int pageSize, string sql, object parameters, string splitOn = null);
Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, string splitOn = null);
Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null);
Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null);
Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null);
Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null);
Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null);

DataTable GetDataTable(string fullSql);
DataTable GetDataTable(string fullSql, object parameters);

GridReader GetMultiple(string fullSql);
GridReader GetMultiple(string fullSql, object parameters);

Task<GridReader> GetMultipleAsync(string fullSql);
Task<GridReader> GetMultipleAsync(string fullSql, object parameters);

// Execute Methods
int Execute(string fullSql);
int Execute(string fullSql, object parameters);

Task<int> ExecuteAsync(string fullSql);
Task<int> ExecuteAsync(string fullSql, object parameters);

T ExecuteScalar<T>(string fullSql);
T ExecuteScalar<T>(string fullSql, object parameters);

Task<T> ExecuteScalarAsync<T>(string fullSql);
Task<T> ExecuteScalarAsync<T>(string fullSql, object parameters);
```

[Column]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.schema.columnattribute
[ConcurrencyCheck]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.concurrencycheckattribute
[DatabaseGenerated]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.schema.databasegeneratedattribute
[Key]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.keyattribute
[Required]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.requiredattribute
[Table]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.schema.tableattribute
[Timestamp]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.timestampattribute

Sample Project
--------------

See the [ContactApp](ContactApp) Razor Pages sample for a complete end-to-end example (schema setup via DbUp, T4-generated entities, and full CRUD usage against SQL Server).


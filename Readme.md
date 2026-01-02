Dapper.Database - more extensions for dapper and SqlDatabase connection manager
========================================

Features
--------
Dapper.Database contains a number of helper methods for inserting, getting,
updating and deleting records.

The full list of extension methods in Dapper.Database:

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
Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3>(int page, int pageSize, string sql, object parameters, string splitOn = null);
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

There is also a SqlDatabase implementation that will handle opening and closing connections and transaction management.
``` csharp
using (var db = new SqlDatabase(new StringConnectionService<SqlConnection>("connectionstring")))
{
    var count = db.Count<Product>();
}
```

Attributes
----------

There are a number of attributes you can use to decorate your classes. 

### From `System.ComponentModel.DataAnnotations`

#### [TableAttribute]

Specifies the SQL table to use.  For databases that support schema generated queries will include a schema if specified.

```csharp
[Table("User", Schema = "Security")]
public class User
{
}
```

#### [ColumnAttribute]

Optional attribute that allows mapping a property to an alternately named column.

```csharp
public class User
{
    [Column("UserName")]
    public string Name { get; set; }
}
```

#### [DatabaseGeneratedAttribute]


Attribute for computed columns and identity columns.  Logic will refresh generated properties after insert and update.

```csharp
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Identity { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string FullName { get; set; }
}
```

#### [KeyAttribute]

Attribute for to be used for primary keys.  

```csharp
public class User
{
    [Key]
    public int Id { get; set; }
}
```

ℹ️ Keys are considered non-nullable, regardless of type.

#### [RequiredAttribute]

Attribute for fields that require a non-null value.
Used to perform null checks in SQL where necessary.

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

Note that non-null value types (and non-null reference types in C# 8.0+) are also honored.

The following is equivalent to the above:

```csharp
#nullable enable

public class User
{
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
}
```

#### [ConcurrencyCheckAttribute]

Attribute for columns that should be checked on update and delete statements, for optimistic concurrency.

```csharp
public class User
{
    [ConcurrencyCheck]
    public DateTime? ModifiedOn { get; set; }
}
```

#### [TimestampAttribute]

Attribute for a database-generated "row version" column that should be checked on update and delete statements, for optimistic concurrency.

```csharp
public class User
{
    [Timestamp]
    public byte[] RowVersion { get; set; }
}
```


### From `Dapper.Database.Attributes`

#### IgnoreInsertAttribute

Ignores the property on an insert statement.

```csharp
public class User
{
    [IgnoreInsert]
    public DateTime? ModifiedOn { get; set; }
}
```

#### IgnoreUpdateAttribute

Ignores the property on update statements.

```csharp
public class User
{
    [IgnoreUpdate]
    public DateTime CreatedOn { get; set; }
}
```

#### IgnoreSelectAttribute

Ignores the property on select statements.

```csharp
public class User
{
    [IgnoreSelect]
    public string Password { get; set; }
}
```

#### ReadOnlyAttribute

Ignores the property on insert and update.

```csharp
public class User
{
    [ReadOnly]
    public string SpecialInfo { get; set; }
}
```

#### IgnoreAttribute

Indicates that there isn't a database backing column.

```csharp
public class User
{
    [Ignore]
    public string NoDbColumn { get; set; }
}
```

[ColumnAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.schema.columnattribute
[ConcurrencyCheckAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.concurrencycheckattribute
[DatabaseGeneratedAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.schema.databasegeneratedattribute
[KeyAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.keyattribute
[RequiredAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.requiredattribute
[TableAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.schema.tableattribute
[TimestampAttribute]: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.timestampattribute

### Example

An example implementation with attribute markup.

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
-------

There is also a T4 code generation template. [Code Generator](https://github.com/dallasbeek/Dapper.Database/tree/master/T4Templates)

`Get` methods
-------

Get one specific entity based on id

```csharp
var product = connection.Get<Product>(806);
```

or with a where clause

```csharp
var product = connection.Get<Product>("where productId = @PId", new { PId = 2323 });
```

`GetList` methods
-------

```csharp
var products = connection.GetList<Product>("where Color = 'Black'");
```

`GetPagedList` methods (get page 5, 10 records)
-------

```csharp
var products = connection.GetPageList<Product>(5, 10, "where Color = 'Black' order by Name")
```

`Insert` methods
-------

Insert one entity

```csharp
connection.Insert(new Car { Name = "Volvo" });
```

`Update` methods
-------
Update one specific entity

```csharp
connection.Update(new Car() { Id = 1, Name = "Saab" });
```

you can also limit which columns to update
```
connection.Update(new Car() { Id = 1, Name = "Saab" }, new string[] {"Name"});
```

`Delete` methods
-------
Delete an entity by the specified `[Key]` property

```csharp
connection.Delete<Car>(1)
```

or by where clause

```csharp
connection.Delete<Car>("where Name = 'Audi'")
```

`Upsert` methods
-------
Insert if it doesn't exist or updates the record, callbacks can be used to update properties
```
connection.Upsert(
    new Car()
    , new[] { "LastName", "CreatedOn", "UpdatedOn" }
    , (insert) => insert.CreatedOn = DateTime.Now()
    , (update) => update.UpdatedOn = DateTime.Now()
);
```

`Execute` methods
-------
Execute is used to execute a stored procedure. Use first char ';' short circuits query builder logic.
```
connection.Execute("; EXEC stored_procedure @a, new {a = 'param'});
```


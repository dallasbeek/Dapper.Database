Dapper.Database - more extensions for dapper and SqlDatabase connection manager
========================================
[![Build status](https://ci.appveyor.com/api/projects/status/t7ajnmavtjdpkw0a?svg=true)](https://ci.appveyor.com/project/dallasbeek/dapper-database)

Features
--------
Dapper.Database contains a number of helper methods for inserting, getting,
updating and deleting records.

The full list of extension methods in Dapper.Database right now are:

```csharp
bool Insert<T>(T entityToInsert);

bool Update<T>(T entityToUpdate);
bool Update<T>(T entityToUpdate, IEnumerable<string> columnsToUpdate);

bool Upsert<T>(T entityToUpsert);
bool Upsert<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate);
bool Upsert<T>(T entityToUpsert, Action<T> insertAction, Action<T> updateAction);
bool Upsert<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction);

bool Delete<T>(T entityToDelete);
bool Delete<T>(object primaryKey);
bool Delete<T>(string sql = null);
bool Delete<T>(string sql, object parameters);

int Count(string sql);
int Count(string sql, object parameters);
int Count<T>(string sql = null);
int Count<T>(string sql, object parameters);

bool Exists(string sql = null);
bool Exists(string sql, object parameters);
bool Exists<T>(T entityToExists);
bool Exists<T>(object primaryKey);
bool Exists<T>(string sql = null);
bool Exists<T>(string sql, object parameters);

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
```

All the above methods are also available as async Methods
``` csharp
Task<bool> InsertAsync<T>(T entityToInsert);
```

There is also a SqlDatabase implementation that will handle opening and closing connections and transaction management.
``` csharp
using (var db = new SqlDatabase(new StringConnectionService<SqlConnection>("connectionstring")))
{
    var count = db.Count<Product>();
}
```

There are a number of attributes you can use to decorate your classes. 

(System.ComponentModel.DataAnnotations)
``` csharp
[Table("Person", Schema = "Sales")] //Schema will be used for databases that support
public class Persona

[Key] // non autoincrement
public string StringId { get; set; }

[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto Increment key column
public int IdentityId { get; set; }

[DatabaseGenerated(DatabaseGeneratedOption.Computed)] // These columns are refreshed during insert/update actions
public string FullName { get; set; }

[Column("rowguid")] // Database column is different than property
public virtual Guid GuidId { get; set; }
```

(Dapper.Database.Attributes)
``` csharp
[IgnoreInsert] // Column is not set on insert
public DateTime? UpdatedOn { get; set; }

[IgnoreUpdate] // Column is not modified on update
public DateTime? CreatedOn { get; set; }

[IgnoreSelect] // Column is not retrieved on Get/GetList etc.
public string Notes { get; set; }

[ReadOnly] // Column is excluded from insert/update
public string Notes { get; set; }

[Ignore] // Excluded (use this when adding custom properties that are not db backed)
public string NoDbColumn { get; set; } 
```

There is also a T4 code generation template.  

`Get` methods
-------

Get one specific entity based on id

```csharp
var product = connection.Get<Product>(806);
```

or with a where clause

```csharp
var product = connection.Get<Product>("WHERE ProductId = @PId", new { PId = 2323 });
```

`GetList` methods
-------

```csharp
var products = connection.GetList<Product>("where Color = 'Black'");
```

`GetPagedList` methods (get page 5, 10 records)
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
	, (update) => u.UpdatedOn = DateTime.Now()
);
```


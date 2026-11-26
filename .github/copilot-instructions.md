# Copilot Instructions — Dapper.Database

## Repository Overview
Dapper.Database is a database-first micro-ORM built as an extension layer on top of Dapper. It adds CRUD convenience methods (`Get`, `GetList`, `GetPageList`, `Insert`, `Update`, `Upsert`, `Delete`, `Exists`, `Count`, etc.), optimistic concurrency support, and SQL dialect adapters for multiple database providers.

### Projects
- **Dapper.Database\Dapper.Database.csproj** — the core library. Multi-targets `netstandard2.0;net462;net8.0;net10.0`. Keep this project portable; avoid APIs unavailable on `netstandard2.0` or `net462` without `#if` guards.
- **Dapper.Database.Tests\Dapper.Database.Tests.csproj** — xUnit test suite. Multi-targets `net48;net10.0`. Uses Testcontainers for integration tests against SQL Server, PostgreSQL, MySQL, MariaDB, CockroachDB, Firebird, and SQLite/SQL CE for local file-based providers.
- **ContactApp\ContactApp.csproj** — a Razor Pages sample/demo app (ASP.NET Core, `net10.0`) showing real-world usage of the library.

## Architecture Notes
- **Adapters** (`Dapper.Database\Adapters\`): one `ISqlAdapter` implementation per database provider (SQL Server, SQL Server pre-2012, SQL CE, MySQL, PostgreSQL, Oracle, Oracle 11g, Firebird, SQLite). When adding provider-specific SQL generation, implement/extend the adapter rather than branching in shared code.
- **Extensions** (`Dapper.Database\Extensions\`): the public CRUD surface (`SqlGetExtensions`, `SqlInsertExtensions`, `SqlUpdateExtensions`, `SqlUpsertExtensions`, `SqlDeleteExtensions`, `SqlExistsExtensions`, `SqlCountExtensions`, `SqlGetPageListExtensions`, etc.) as extension methods on `IDbConnection`/`ISqlDatabase`.
- **SqlDatabase** (`Dapper.Database\SqlDatabase*.cs`): the primary `ISqlDatabase` implementation wrapping a connection, split into partial-class-like files per operation (Get, Insert, Update, Upsert, Delete, Count, Exists, Execute, ExecuteScalar, GetDataTable, GetFirst, GetList, GetMultiple, GetPageList).
- **Attributes** (`Dapper.Database\Attributes\Attributes.cs`): mapping attributes (table name, key, computed/ignored columns, etc.) used to drive the mapper via `TableInfo` and `MappingCache`.
- **Mapper** (`Dapper.Database\Mapper\`): internal mapping/reflection cache layer used to resolve entity-to-table metadata efficiently.

## Conventions
- Target the **lowest common denominator** API surface in `Dapper.Database` itself (netstandard2.0/net462 compatible) unless a feature is intentionally gated behind `#if NET8_0_OR_GREATER` or similar.
- Each public CRUD operation typically has sync, async, and often a "multiple"/"list" variant — mirror existing method overload patterns (naming, parameter order, `CommandType`, `transaction`, `commandTimeout` args) when adding new ones.
- New database providers should get their own adapter file in `Adapters\` and be wired into adapter resolution (see `SqlAdapter.cs`/`ConnectionServices.cs`) rather than adding provider checks inline elsewhere.
- Follow the existing `.editorconfig` rules in the repo.

## Testing
- Tests live in `Dapper.Database.Tests\Tests\` and are organized per CRUD operation, each with sync + async files (e.g. `TestSuiteGet.cs` / `TestSuiteGetAsync.cs`).
- Provider-specific suites (`TestSuitesMsSqlServer.cs`, `TestSuitesPostgres.cs`, `TestSuitesMySql.cs`, `TestSuitesMariaDb.cs`, `TestSuitesCockroachDb.cs`, `TestSuitesOracle.cs`, `TestSuitesFirebird.cs`, `TestSuitesSqlCE.cs`, `TestSuitesSqlLite.cs`) use Testcontainers to spin up real databases; some use `Xunit.SkippableFact` to skip when a provider/environment isn't available (e.g. SQL CE is Windows/net48-only).
- When adding a new CRUD feature, add both sync and async test coverage and run it across the relevant provider test suites.
- Do not ask the user to build the project manually — build/test validation should be performed by the agent itself.

## Framework/Target Guidance
- This workspace targets **.NET Standard 2.0, .NET Framework 4.6.2, .NET 8, .NET 10, and .NET Framework 4.8**. .NET 8 and .NET 10 are both LTS; prefer .NET 8 or .NET 10 APIs only behind appropriate TFM guards, never assume a single-TFM codebase.
- `ContactApp` is a **Razor Pages** project — prefer Razor Pages patterns over Blazor or MVC-style conventions when modifying or extending it.

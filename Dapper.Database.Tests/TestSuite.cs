using System;
using System.Data;

namespace Dapper.Database.Tests;

public abstract partial class TestSuite
{
    protected static readonly bool IsAppVeyor =
        Environment.GetEnvironmentVariable("Appveyor")?.ToUpperInvariant() == "TRUE";

    protected virtual string P => "@";

    public abstract Provider GetProvider();

    public abstract ISqlDatabase GetSqlDatabase();

    protected abstract void CheckSkip();


#pragma warning disable xUnit1013 // Public method should be marked as test
    /// <summary>
    ///     Resets Dapper type map and type handlers.
    /// </summary>
    public static void ResetDapperTypes()
    {
        // Because each database requires a slightly different type map, we have to reset it back to "normal" each time.
        // As the only DbType we modify is DbType.Guid, we only need to re-add it to fix it.
        SqlMapper.AddTypeMap(typeof(Guid), DbType.Guid);
        SqlMapper.AddTypeMap(typeof(Guid?), DbType.Guid);

        SqlMapper.ResetTypeHandlers();
    }

#pragma warning restore xUnit1013 // Public method should be marked as test
}

using System;
using System.Data;
using Dapper.Database;


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        protected static readonly bool IsAppVeyor = Environment.GetEnvironmentVariable("Appveyor")?.ToUpperInvariant() == "TRUE";

        public abstract Provider GetProvider();

        public abstract ISqlDatabase GetSqlDatabase();

        protected abstract void CheckSkip();

        protected virtual string P => "@";


#pragma warning disable xUnit1013 // Public method should be marked as test
        /// <summary>
        /// Resets Dapper type map and type handlers.
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
}

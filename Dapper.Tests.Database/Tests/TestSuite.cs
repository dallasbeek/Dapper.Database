using System;
using Dapper.Database;


namespace Dapper.Tests.Database
{
    public abstract partial class TestSuite
    {
        protected static readonly bool IsAppVeyor = Environment.GetEnvironmentVariable("Appveyor")?.ToUpperInvariant() == "TRUE";

        public abstract Provider GetProvider();

        public abstract ISqlDatabase GetSqlDatabase();

        protected virtual string P { get{ return "@"; } }

    }
}

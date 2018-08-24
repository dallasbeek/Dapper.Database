using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Dapper.Tests.Database
{
    public enum Provider
    {
        SqlServer,
        SqlCE,
        SQLite,
        MySql,
        Postgres,
        Firebird,
        Oracle
    }
}

using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Xunit;
using Xunit.Sdk;
using Dapper.Database.Extensions;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Collections.Generic;
using Dapper.Database;

#if NET452
using System.Data.SqlServerCe;
using System.Transactions;
#endif

namespace Dapper.Tests.Database
{
    // The test suites here implement TestSuiteBase so that each provider runs
    // the entire set of tests without declarations per method
    // If we want to support a new provider, they need only be added here - not in multiple places

    public abstract partial class TestManagerSuite
    {
        protected static readonly bool IsAppVeyor = Environment.GetEnvironmentVariable("Appveyor")?.ToUpperInvariant() == "TRUE";

        public abstract IConnectionService GetConnectionService();

        public EntityManager GetEntityManager()
        {
            return new EntityManager(GetConnectionService());
        }


        [Fact]
        public void InsertSimple()
        {
            using (var em = GetEntityManager())
            {
                var cp = new PersonIdentity { FirstName = "Dallas" };
                Assert.True(em.Insert(cp));
                Assert.True(cp.IdentityId > 0);
            }
        }

        [Fact]
        public void InsertTransactionRollback()
        {
            var em = GetEntityManager();
            var cp = new PersonIdentity { FirstName = "Dallas" };
            using (var trans = em.GetTransaction())
            {
                Assert.True(em.Insert(cp));
                Assert.True(cp.IdentityId > 0);
            }
            Assert.Null(em.Get(cp));
            em.Dispose();
        }


        [Fact]
        public void InsertTransactionCommit()
        {
            var em = GetEntityManager();
            var cp = new PersonIdentity { FirstName = "Dallas" };
            using (var trans = em.GetTransaction())
            {
                Assert.True(em.Insert(cp));
                Assert.True(cp.IdentityId > 0);
                trans.Complete();
            }
            Assert.NotNull(em.Get(cp));
            em.Dispose();
        }

    }

    public class SqlServerManagerSuite : TestManagerSuite
    {
        private const string DbName = "tempdb";
        public static string ConnectionString =>
            IsAppVeyor
                ? @"Server=(local)\SQL2017;Database=tempdb;User ID=sa;Password=Password12!"
                : $"Data Source=(local)\\Dallas;Initial Catalog={DbName};Integrated Security=True";

        public override IConnectionService GetConnectionService() => new StringConnectionService<SqlConnection>(ConnectionString);

        static SqlServerManagerSuite()
        {
            //using (var connection = new SqlConnection(ConnectionString))
            //{
            //    // ReSharper disable once AccessToDisposedClosure
            //    Action<string> dropTable = name => connection.Execute($"IF OBJECT_ID('{name}', 'U') IS NOT NULL DROP TABLE [{name}]; ");
            //    connection.Open();
            //    dropTable("Customers");
            //    connection.Execute(@"CREATE TABLE [dbo].[Customers](
	           //     [Id] [int] IDENTITY(1,1) NOT NULL,
	           //     [IId] [int] NULL,
	           //     [GId] [uniqueidentifier] NULL,
	           //     [SId] [varchar](100) NULL,
	           //     [FirstName] [nvarchar](100) NULL,
	           //     [LastName] [nvarchar](100) NULL,
	           //     [FullName]  AS ((isnull([FirstName],'')+' ')+isnull([LastName],'')),
	           //     [Age] [int] NULL,
	           //     [UpdatedOn] [datetime] NULL,
	           //     [CreatedOn] [datetime] NULL
            //    );");
            //}
        }


        [Fact]
        public void TestConnectionService()
        {
            var svc = GetConnectionService();

            var dbConnection = svc.GetConnection();

            Assert.IsType<SqlConnection>(dbConnection);
        }
    }
}

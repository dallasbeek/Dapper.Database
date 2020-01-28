using System.Data.SqlClient;
using System.IO;
using Dapper.Database;
using Dapper.Database.Adapters;
using Dapper.Database.Extensions;
using Xunit;
using System.Linq;
using FactAttribute = Xunit.SkippableFactAttribute;
using System.Threading.Tasks;
using System.Data;
using System;

namespace Dapper.Tests.Database
{
    [Trait("Provider", "SqlServer")]
    public partial class SqlServerTestSuite : TestSuite
    {
        private const string DbName = "tempdb";
        public static string ConnectionString =>
            IsAppVeyor
                ? $"Server=(local)\\SQL2017;Database={DbName};User ID=sa;Password=Password12!"
                : $"Data Source=(localdb)\\mssqllocaldb;Initial Catalog={DbName};Integrated Security=True";

        protected override void CheckSkip()
        {
            Skip.If(_skip, "Skipping Sql Server Tests - no server.");
        }

        public override ISqlDatabase GetSqlDatabase()
        {
            CheckSkip();
            return new SqlDatabase(new StringConnectionService<SqlConnection>(ConnectionString));
        }


        public override Provider GetProvider() => Provider.SqlServer;

        private static readonly bool _skip;

        static SqlServerTestSuite()
        {
            SqlDatabase.CacheQueries = false;
            ResetDapperTypes();

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // For paginated queries prior to 2012 sql server uses row_number over
                    var sqlVersion = connection.ServerVersion;
                    if (!string.IsNullOrEmpty(sqlVersion) && sqlVersion.Length > 2)
                    {
                        var mv = int.Parse(sqlVersion.Substring(0, 2));
                        if (mv < 11)
                        {
                            SqlMapperExtensions.AddSqlAdapter<SqlConnection>(new SqlServerPre2012Adapter());
                        }
                    }

                    var awfile = File.ReadAllText(".\\Scripts\\sqlserverawlite.sql");
                    connection.Execute(awfile);
                    connection.Execute("delete from [Person]");

                }
            }
            catch (SqlException e)
            {
                if (e.Message.Contains("The server was not found ") || e.Message.Contains("Cannot open database"))
                    _skip = true;
                else
                    throw;
            }
        }

        #region Sql Server Tests Only
        [Fact]
        [Trait("Category", "GetMultiple")]
        public void GetMultiple()
        {
            using (var db = GetSqlDatabase())
            {
                using (var trans = db.GetTransaction())
                {
                    var dt = db.GetMultiple(@"
                    select * from Product where Color = 'Black';
                    select * from ProductCategory where productcategoryid = '21';");
                    Assert.Equal(89, dt.Read(typeof(Product)).Count());

                    var pc = (ProductCategory)dt.ReadSingle(typeof(ProductCategory));
                    ValidateProductCategory21(pc);
                    trans.Complete();
                }
            }
        }

        [Fact]
        [Trait("Category", "GetMultiple")]
        public void GetMultipleWithParameter()
        {
            using (var db = GetSqlDatabase())
            {
                using (var trans = db.GetTransaction())
                {
                    var dt = db.GetMultiple($@"
                    select * from Product where Color = {P}Color;
                    select * from ProductCategory where productcategoryid = {P}ProductCategoryId;",
                        new { Color = "Black", ProductCategoryId = 21 });
                    Assert.Equal(89, dt.Read(typeof(Product)).Count());

                    var pc = (ProductCategory)dt.ReadSingle(typeof(ProductCategory));
                    ValidateProductCategory21(pc);
                    trans.Complete();
                }
            }
        }

        [Fact]
        [Trait("Category", "GetMultipleAsync")]
        public async Task GetMultipleAsync()
        {
            using (var db = GetSqlDatabase())
            {
                using (var trans = db.GetTransaction())
                {
                    var dt = await db.GetMultipleAsync(@"
                    select * from Product where Color = 'Black';
                    select * from ProductCategory where productcategoryid = '21';");
                    Assert.Equal(89, dt.Read(typeof(Product)).Count());

                    var pc = (ProductCategory)dt.ReadSingle(typeof(ProductCategory));
                    ValidateProductCategory21(pc);
                    trans.Complete();
                }
            }
        }

        [Fact]
        [Trait("Category", "GetMultipleAsync")]
        public async Task GetMultipleAsyncWithParameter()
        {
            using (var db = GetSqlDatabase())
            {
                using (var trans = db.GetTransaction())
                {
                    var dt = await db.GetMultipleAsync($@"
                    select * from Product where Color = {P}Color;
                    select * from ProductCategory where productcategoryid = {P}ProductCategoryId;",
                        new { Color = "Black", ProductCategoryId = 21 });
                    Assert.Equal(89, dt.Read(typeof(Product)).Count());

                    var pc = (ProductCategory)dt.ReadSingle(typeof(ProductCategory));
                    ValidateProductCategory21(pc);
                    trans.Complete();
                }
            }
        }

        [Fact]
        [Trait("Category", "GetList")]
        public void GetListUsingTableValueParamter()
        {
            using (var db = GetSqlDatabase())
            {
                var dataTable = new DataTable("DT");
                dataTable.Columns.Add("ProductId", typeof(int));
                dataTable.Rows.Add(816);
                dataTable.Rows.Add(731);

                var lst = db.GetList<Product>(sql: @"
                    SELECT P.*, P.rowguid as GuidId FROM Product P
                    INNER JOIN @productIdTVP PTVP ON PTVP.ProductId = P.ProductId",
                    parameters: new { productIdTVP = dataTable.AsTableValuedParameter("[dbo].[ProductIdTable]") });

                Assert.Equal(2, lst.Count());
                var item = lst.Single(p => p.ProductID == 816);
                ValidateProduct816(item);
            }
        }

        [Fact]
        [Trait("Category", "Update")]
        public void UpdateTimestamp()
        {
            using (var db = GetSqlDatabase())
            {
                var p = new PersonTimestamp { GuidId = Guid.NewGuid(), FirstName = "Alice", LastName = "Jones" };
                Assert.True(db.Insert(p));
                Assert.NotNull(p.ConcurrencyToken);
                var token1 = p.ConcurrencyToken;

                p.FirstName = "Greg";
                p.LastName = "Smith";
                Assert.True(db.Update(p), "ConcurrencyToken matched");
                Assert.NotEqual(token1, p.ConcurrencyToken);

                // Simulate an independent change
                db.Execute("update Person set Age = 1 where GuidId = @GuidId", p);

                p.FirstName = "Alice";
                p.LastName = "Jones";
                Assert.False(db.Update(p), "ConcurrencyToken should be different");

                var gp = db.Get<PersonTimestamp>(p.GuidId);

                Assert.Equal("Greg", gp.FirstName);
                Assert.Equal("Smith", gp.LastName);
                Assert.NotEqual(gp.ConcurrencyToken, p.ConcurrencyToken);
            }
        }

        #endregion
    }
}

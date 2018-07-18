//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;

//using Dapper.Database.Extensions;
//using Xunit;
//using Dapper.Database;
//using System.Data.SqlClient;
//using System.Threading.Tasks;

//#if NET452
//using System.Transactions;
//using System.ComponentModel.DataAnnotations;
//using System.Data.SqlServerCe;
//#endif


//namespace Dapper.Tests.Database
//{
//    public partial class SqlServerTestSuite
//    {


//        [ProviderFact]
//        [Trait("Category", "SqlDb")]
//        public void SqlDbCountSql()
//        {
//            using (var db = GetDatabase())
//            {
//                Assert.Equal(89, db.Count("select count(*) from product where Color = @Color", new { Color = "Black" }));
//            }
//        }

//        [ProviderFact]
//        [Trait("Category", "SqlDb")]
//        public void SqlDbCount()
//        {
//            using (var db = GetDatabase())
//            {
//                Assert.Equal(89, db.Count<Product>("where Color = @Color", new { Color = "Black" }));
//            }
//        }

//        [ProviderFact]
//        [Trait("Category", "SqlDb")]
//        public void SqlDbExistsByEntity()
//        {
//            using (var db = GetDatabase())
//            {
//                var p = new Product { ProductID = 806, GuidId = new Guid("23B5D52B-8C29-4059-B899-75C53B5EE2E6") };
//                Assert.True(db.Exists(p));

//                p.ProductID = -1;
//                Assert.False(db.Exists(p));
//            }
//        }

//        [ProviderFact]
//        [Trait("Category", "SqlDb")]
//        public void SqlDbExistsBySelect()
//        {
//            using (var db = GetDatabase())
//            {
//                Assert.True(db.Exists<Product>("select *, rowguid AS GuidId  from Product where ProductId = @Id", new { Id = 806 }));
//                Assert.False(db.Exists<Product>("select *, rowguid AS GuidId  from Product where ProductId = @Id", new { Id = -1 }));
//            }
//        }

//    }
//}

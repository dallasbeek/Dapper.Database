//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using Dapper.Database;
//using Xunit;

//namespace Dapper.Tests.Database.Tests
//{
//    public class TableInfoTests
//    {
//        [Fact]
//        public void IsNullable()
//        {
//            var t = new TableInfo(typeof(Person));

//            var columns = t.Columns.ToDictionary(c => c.ColumnName);

//            Assert.False(columns["IdentityId"].IsNullable, "struct");
//            Assert.True(columns["Age"].IsNullable, "nullable struct");
//            Assert.True(columns["Title"].IsNullable, "No attribute");
//            Assert.False(columns["FirstName"].IsNullable, "RequiredAttribute");
//            Assert.True(columns["MiddleName"].IsNullable, "Nullable reference");
//            Assert.False(columns["LastName"].IsNullable, "Non-nullable reference");
//            Assert.False(columns["Nickname"].IsNullable, "Required nullable reference");
//            Assert.False(columns["ComplicatedProperty"].IsNullable, "Non-nullable reference");
//        }

//        private class Person
//        {
//            [Key]
//            public string StringId { get; set; }
//            public int IdentityId { get; set; }
//            public int? Age { get; set; }
//#nullable disable
//            public string Title { get; set; }
//            [Required]
//            public string FirstName { get; set; }
//#nullable enable
//#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
//            public string? MiddleName { get; set; }
//            public string LastName { get; set; }
//            [Required]
//            public string? Nickname { get; set; }
//            public Dictionary<Dictionary<string, string?>?, string?> ComplicatedProperty { get; set; }
//#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
//#nullable restore
//        }
//    }
//}

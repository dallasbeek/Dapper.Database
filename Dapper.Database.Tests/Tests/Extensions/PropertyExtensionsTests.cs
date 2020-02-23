//using System;
//using System.Collections.Generic;
//using Xunit;
//using Dapper.Database.Extensions;


//namespace Dapper.Tests.Database.Tests.Extensions
//{
//    public partial class PropertyExtensionsTests
//    {

//        [Fact]
//        public void IsNullable()
//        {
//            var type = typeof(Person);

//            Assert.False(type.GetProperty("IdentityId").IsNullable());
//            Assert.True(type.GetProperty("Age").IsNullable());
//            Assert.True(type.GetProperty("FirstName").IsNullable(), "pre-nullable annotations");
//            Assert.True(type.GetProperty("MiddleName").IsNullable(), "explicitly nullable");
//            Assert.False(type.GetProperty("LastName").IsNullable(), "explicitly not nullable");
//            Assert.False(type.GetProperty("ComplicatedProperty").IsNullable(), "explicitly not nullable (though its members are)");
//        }

//        private class Person
//        {
//            public int IdentityId { get; set; }
//            public int? Age { get; set; }
//#nullable disable
//            public string FirstName { get; set; }
//#nullable enable
//#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
//            public string? MiddleName { get; set; }
//            public string LastName { get; set; }
//            public Dictionary<Dictionary<string, string?>?, string?> ComplicatedProperty { get; set; }
//#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
//#nullable restore
//        }
//    }
//}

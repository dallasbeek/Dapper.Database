using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Dapper.Database.Extensions;
using Dapper;
using Xunit;
using System.ComponentModel.DataAnnotations.Schema;

#if NET452
using System.Transactions;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlServerCe;
#endif


namespace Dapper.Tests.Database
{

    [Table( "ObjectQ" )]
    public class ObjectQ
    {
#if NET452
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
#else
        [Dapper.Database.Extensions.DatabaseGenerated( Dapper.Database.Extensions.DatabaseGeneratedOption.Identity )]
#endif
        public int Id { get; set; }
        [IgnoreInsert]
        public string IgnoreInsert { get; set; }
        [IgnoreUpdate]
        public string IgnoreUpdate { get; set; }
        [IgnoreSelect]
        public string IgnoreSelect { get; set; }
#if NET452
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
#else
        [Dapper.Database.Extensions.DatabaseGenerated( Dapper.Database.Extensions.DatabaseGeneratedOption.Computed )]
#endif
        public string Computed { get; set; }
        [ReadOnly]
        public string Readonly { get; set; }
    }


    [Table( "ObjectX" )]
    public class ObjectX
    {
        [Key]
        public string ObjectXId { get; set; }
        public string Name { get; set; }
    }

    [Table( "ObjectY" )]
    public class ObjectY
    {
        [Key]
        public int ObjectYId { get; set; }
        public string Name { get; set; }
    }

    [Table( "ObjectZ" )]
    public class ObjectZ
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public interface IUser
    {
        [Key]
#if NET452
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
#else
        [Dapper.Database.Extensions.DatabaseGenerated( Dapper.Database.Extensions.DatabaseGeneratedOption.Identity )]
#endif
        int Id { get; set; }
        string Name { get; set; }
        int Age { get; set; }
    }

    public class User : IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public interface INullableDate
    {
        [Key]
#if NET452
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
#else
        [Dapper.Database.Extensions.DatabaseGenerated( Dapper.Database.Extensions.DatabaseGeneratedOption.Identity )]
#endif
        int Id { get; set; }
        DateTime? DateValue { get; set; }
    }

    public class NullableDate : INullableDate
    {
        public int Id { get; set; }
        public DateTime? DateValue { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Table( "Stuff" )]
    public class Stuff
    {
        [Key]
#if NET452
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
#else
        [Dapper.Database.Extensions.DatabaseGenerated( Dapper.Database.Extensions.DatabaseGeneratedOption.Identity )]
#endif
        public short TheId { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }
    }

    [Table( "Automobiles" )]
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Ignore]
        public string Computed { get; set; }
    }

    [Table( "Results" )]
    public class Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }

    [Table( "GenericType" )]
    public class GenericType<T>
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }

    [Table( "PkGuid" )]
    public class PKGuid
    {
        [Key]
        public Guid GuidId { get; set; }
        public string Name { get; set; }

#if NET452
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
#else
        [Dapper.Database.Extensions.DatabaseGenerated( Dapper.Database.Extensions.DatabaseGeneratedOption.Identity )]
#endif
        [Column( "Name" )]
        public string ComputedName { get; set; }
    }
}

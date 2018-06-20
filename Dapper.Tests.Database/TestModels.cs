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

    [Table( "Customers" )]
    public class CustomerAttribute
    {
#if NET452
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
#else
        [Dapper.Database.Extensions.DatabaseGenerated( Dapper.Database.Extensions.DatabaseGeneratedOption.Identity )]
#endif
        public int Id { get; set; }
        [IgnoreInsert]
        public string FirstName { get; set; }
        [IgnoreUpdate]
        public string LastName { get; set; }
        [IgnoreSelect]
        public int? Age { get; set; }
#if NET452
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
#else
        [Dapper.Database.Extensions.DatabaseGenerated( Dapper.Database.Extensions.DatabaseGeneratedOption.Computed )]
#endif
        public string FullName { get; set; }
        [ReadOnly]
        public Guid  GId { get; set; }
    }

    [Table( "Customers" )]
    public class CustomerStringId
    {
        [Key]
        public string SId { get; set; }
        public string FirstName { get; set; }

    }

    [Table( "Customers" )]
    public class CustomerIntegerId
    {
        [Key]
        public int IId { get; set; }
        public string FirstName { get; set; }

    }

    public interface ICustomer
    {
        [Key]
#if NET452
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
#else
        [Dapper.Database.Extensions.DatabaseGenerated( Dapper.Database.Extensions.DatabaseGeneratedOption.Identity )]
#endif
        int Id { get; set; }
        string FirstName { get; set; }
        int Age { get; set; }
        DateTime? UpdatedOn { get; set; }
    }

    [Table("Customers")]
    public class CustomerProxy : ICustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }


    public class CustomerMapped
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
    }

    [Table( "Customers" )]
    public class CustomerShortId
    {
        [Key]
#if NET452
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
#else
        [Dapper.Database.Extensions.DatabaseGenerated( Dapper.Database.Extensions.DatabaseGeneratedOption.Identity )]
#endif
        public short Id { get; set; }
        public string FirstName { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    [Table( "Customers" )]
    public class Car
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        [Ignore]
        public string Computed { get; set; }
    }

    [Table( "Customers" )]
    public class Result
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
    }

    [Table( "Customers" )]
    public class GenericType<T>
    {
        [Key]
        public string SId { get; set; }
        public string FirstName { get; set; }
    }

    [Table( "Customers" )]
    public class CustomersGuidId
    {
        [Key]
        public Guid GId { get; set; }
        public string FirstName { get; set; }

#if NET452
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
#else
        [Dapper.Database.Extensions.DatabaseGenerated( Dapper.Database.Extensions.DatabaseGeneratedOption.Identity )]
#endif
        [Column( "Name" )]
        public string ComputedName { get; set; }
    }
}

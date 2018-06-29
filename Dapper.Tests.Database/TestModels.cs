using System;
using Dapper.Database.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Tests.Database
{

    [Table( "Customers" )]
    public class CustomerAttribute
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [IgnoreInsert]
        public string FirstName { get; set; }
        [IgnoreUpdate]
        public string LastName { get; set; }
        [IgnoreSelect]
        public int? Age { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

    [Table("Customers")]
    public class CustomerComposite
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        public int IId { get; set; }

        [Key]
        public Guid GId { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get; set; }

        public int? Age { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column( "Name" )]
        public string ComputedName { get; set; }
    }
}

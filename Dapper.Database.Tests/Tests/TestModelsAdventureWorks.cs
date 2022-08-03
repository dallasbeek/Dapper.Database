using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Database.Attributes;

namespace Dapper.Database.Tests;

/// <summary>
///     A class which represents the Address table.
/// </summary>
[Table("Address")]
public class Address
{
    [Column]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int AddressID { get; set; }

    [Column] [Required] [StringLength(60)] public virtual string AddressLine1 { get; set; }

    [Column] [StringLength(60)] public virtual string AddressLine2 { get; set; }

    [Column] [Required] [StringLength(30)] public virtual string City { get; set; }

    [Column] [Required] [StringLength(50)] public virtual string StateProvince { get; set; }

    [Column] [Required] [StringLength(50)] public virtual string CountryRegion { get; set; }

    [Column] [Required] [StringLength(15)] public virtual string PostalCode { get; set; }

    [Column("rowguid")] public virtual Guid GuidId { get; set; }

    [Column] public virtual DateTime ModifiedDate { get; set; }
}

/// <summary>
///     A class which represents the Customer table.
/// </summary>
[Table("Customer")]
public class Customer
{
    [Column]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int CustomerID { get; set; }

    [Column] public virtual bool NameStyle { get; set; }

    [Column] [StringLength(8)] public virtual string Title { get; set; }

    [Column] [Required] [StringLength(50)] public virtual string FirstName { get; set; }

    [Column] [StringLength(50)] public virtual string MiddleName { get; set; }

    [Column] [Required] [StringLength(50)] public virtual string LastName { get; set; }

    [Column] [StringLength(10)] public virtual string Suffix { get; set; }

    [Column] [StringLength(128)] public virtual string CompanyName { get; set; }

    [Column] [StringLength(256)] public virtual string SalesPerson { get; set; }

    [Column] [StringLength(50)] public virtual string EmailAddress { get; set; }

    [Column] [StringLength(25)] public virtual string Phone { get; set; }

    [Column]
    [Required]
    [StringLength(128)]
    public virtual string PasswordHash { get; set; }

    [Column] [Required] [StringLength(10)] public virtual string PasswordSalt { get; set; }

    [Column("rowguid")] public virtual Guid GuidId { get; set; }

    [Column] public virtual DateTime ModifiedDate { get; set; }

    [Ignore] public List<Address> Addresses { get; set; }
}

/// <summary>
///     A class which represents the CustomerAddress table.
/// </summary>
[Table("CustomerAddress")]
public class CustomerAddress
{
    [Column] [Key] public virtual int CustomerID { get; set; }

    [Column] public virtual int AddressID { get; set; }

    [Column] [Required] [StringLength(50)] public virtual string AddressType { get; set; }

    [Column("rowguid")] public virtual Guid GuidId { get; set; }

    [Column] public virtual DateTime ModifiedDate { get; set; }
}

/// <summary>
///     A class which represents the Product table.
/// </summary>
[Table("Product")]
public class Product
{
    [Column]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int ProductID { get; set; }

    [Column] [Required] [StringLength(50)] public virtual string Name { get; set; }

    [Column] [Required] [StringLength(25)] public virtual string ProductNumber { get; set; }

    [Column] [StringLength(15)] public virtual string Color { get; set; }

    [Column] public virtual decimal StandardCost { get; set; }

    [Column] public virtual decimal ListPrice { get; set; }

    [Column] [StringLength(5)] public virtual string Size { get; set; }

    [Column] public virtual decimal? Weight { get; set; }

    [Column] public virtual int? ProductCategoryID { get; set; }

    [Column] public virtual int? ProductModelID { get; set; }

    [Column] public virtual DateTime SellStartDate { get; set; }

    [Column] public virtual DateTime? SellEndDate { get; set; }

    [Column] public virtual DateTime? DiscontinuedDate { get; set; }

    [Column] public virtual byte[] ThumbNailPhoto { get; set; }

    [Column] [StringLength(50)] public virtual string ThumbnailPhotoFileName { get; set; }

    [Column("rowguid")] public virtual Guid GuidId { get; set; }

    [Column] public virtual DateTime ModifiedDate { get; set; }

    [Ignore] public ProductCategory ProductCategory { get; set; }

    [Ignore] public ProductModel ProductModel { get; set; }
}

/// <summary>
///     A class which represents the Product table.
/// </summary>
[Table("Product")]
public class ProductAlias
{
    [Key]
    [Column("ProductID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Column] [Required] [StringLength(50)] public virtual string Name { get; set; }

    [Column] [Required] [StringLength(25)] public virtual string ProductNumber { get; set; }

    [Column] [StringLength(15)] public virtual string Color { get; set; }

    [Column] public virtual decimal StandardCost { get; set; }

    [Column] public virtual decimal ListPrice { get; set; }

    [Column] [StringLength(5)] public virtual string Size { get; set; }

    [Column] public virtual decimal? Weight { get; set; }

    [Column] public virtual int? ProductCategoryID { get; set; }

    [Column] public virtual int? ProductModelID { get; set; }

    [Column] public virtual DateTime SellStartDate { get; set; }

    [Column] public virtual DateTime? SellEndDate { get; set; }

    [Column] public virtual DateTime? DiscontinuedDate { get; set; }

    [Column] public virtual byte[] ThumbNailPhoto { get; set; }

    [Column] [StringLength(50)] public virtual string ThumbnailPhotoFileName { get; set; }

    [Column("rowguid")] public virtual Guid GuidId { get; set; }

    [Column] public virtual DateTime ModifiedDate { get; set; }

    [Ignore] public ProductCategory ProductCategory { get; set; }

    [Ignore] public ProductModel ProductModel { get; set; }
}

/// <summary>
///     A class which represents the ProductCategory table.
/// </summary>
[Table("ProductCategory")]
public class ProductCategory
{
    [Column]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int ProductCategoryID { get; set; }

    [Column] public virtual int? ParentProductCategoryID { get; set; }

    [Column] [Required] [StringLength(50)] public virtual string Name { get; set; }

    [Column("rowguid")] public virtual Guid GuidId { get; set; }

    [Column] public virtual DateTime ModifiedDate { get; set; }

    [Ignore] public ProductCategory ParentProductCategory { get; set; }
}

/// <summary>
///     A class which represents the ProductDescription table.
/// </summary>
[Table("ProductDescription")]
public class ProductDescription
{
    [Column]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int ProductDescriptionID { get; set; }

    [Column]
    [Required]
    [StringLength(400)]
    public virtual string Description { get; set; }

    [Column("rowguid")] public virtual Guid GuidId { get; set; }

    [Column] public virtual DateTime ModifiedDate { get; set; }
}

/// <summary>
///     A class which represents the ProductModel table.
/// </summary>
[Table("ProductModel")]
public class ProductModel
{
    [Column]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int ProductModelID { get; set; }

    [Column] [Required] [StringLength(50)] public virtual string Name { get; set; }

    [Column] public virtual string CatalogDescription { get; set; }

    [Column("rowguid")] public virtual Guid GuidId { get; set; }

    [Column] public virtual DateTime ModifiedDate { get; set; }

    [Ignore] public List<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }
}

/// <summary>
///     A class which represents the ProductModelProductDescription table.
/// </summary>
[Table("ProductModelProductDescription")]
public class ProductModelProductDescription
{
    [Column] [Key] public virtual int ProductModelID { get; set; }

    [Column] public virtual int ProductDescriptionID { get; set; }

    [Column] [Required] [StringLength(6)] public virtual string Culture { get; set; }

    [Column("rowguid")] public virtual Guid GuidId { get; set; }

    [Column] public virtual DateTime ModifiedDate { get; set; }

    [Ignore] public List<SalesOrderHeader> SalesOrderHeader { get; set; }
}

/// <summary>
///     A class which represents the SalesOrderDetail table.
/// </summary>
[Table("SalesOrderDetail")]
public class SalesOrderDetail
{
    [Column] [Key] public virtual int SalesOrderID { get; set; }

    [Column]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int SalesOrderDetailID { get; set; }

    [Column] public virtual short OrderQty { get; set; }

    [Column] public virtual int ProductID { get; set; }

    [Column] public virtual decimal UnitPrice { get; set; }

    [Column] public virtual decimal UnitPriceDiscount { get; set; }

    [Column]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public virtual decimal LineTotal { get; set; }

    [Column("rowguid")] public virtual Guid GuidId { get; set; }

    [Column] public virtual DateTime ModifiedDate { get; set; }
}

/// <summary>
///     A class which represents the SalesOrderHeader table.
/// </summary>
[Table("SalesOrderHeader")]
public class SalesOrderHeader
{
    [Column]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int SalesOrderID { get; set; }

    [Column] public virtual byte RevisionNumber { get; set; }

    [Column] public virtual DateTime OrderDate { get; set; }

    [Column] public virtual DateTime DueDate { get; set; }

    [Column] public virtual DateTime? ShipDate { get; set; }

    [Column] public virtual byte Status { get; set; }

    [Column] public virtual bool OnlineOrderFlag { get; set; }

    [Column]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public virtual string SalesOrderNumber { get; set; }

    [Column] [StringLength(25)] public virtual string PurchaseOrderNumber { get; set; }

    [Column] [StringLength(15)] public virtual string AccountNumber { get; set; }

    [Column] public virtual int CustomerID { get; set; }

    [Column] public virtual int? ShipToAddressID { get; set; }

    [Column] public virtual int? BillToAddressID { get; set; }

    [Column] [Required] [StringLength(50)] public virtual string ShipMethod { get; set; }

    [Column] [StringLength(15)] public virtual string CreditCardApprovalCode { get; set; }

    [Column] public virtual decimal SubTotal { get; set; }

    [Column] public virtual decimal TaxAmt { get; set; }

    [Column] public virtual decimal Freight { get; set; }

    [Column]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public virtual decimal TotalDue { get; set; }

    [Column] public virtual string Comment { get; set; }

    [Column("rowguid")] public virtual Guid GuidId { get; set; }

    [Column] public virtual DateTime ModifiedDate { get; set; }

    [Ignore] public Customer Customer { get; set; }

    [Ignore] public Address ShipToAddress { get; set; }

    [Ignore] public Address BillToAddress { get; set; }

    [Ignore] public List<SalesOrderDetail> LineItems { get; set; }
}

[Table("Product")]
public class ProductKeyAlias
{
    [Column("ProductID")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }

    [Column] [Required] [StringLength(50)] public virtual string Name { get; set; }

    [Column] [Required] [StringLength(25)] public virtual string ProductNumber { get; set; }

    [Column] [StringLength(15)] public virtual string Color { get; set; }

    [Column] public virtual decimal StandardCost { get; set; }

    [Column] public virtual decimal ListPrice { get; set; }

    [Column] [StringLength(5)] public virtual string Size { get; set; }

    [Column] public virtual decimal? Weight { get; set; }

    [Column] public virtual int? ProductCategoryID { get; set; }

    [Column] public virtual int? ProductModelID { get; set; }

    [Column] public virtual DateTime SellStartDate { get; set; }

    [Column] public virtual DateTime? SellEndDate { get; set; }

    [Column] public virtual DateTime? DiscontinuedDate { get; set; }

    [Column] public virtual byte[] ThumbNailPhoto { get; set; }

    [Column] [StringLength(50)] public virtual string ThumbnailPhotoFileName { get; set; }

    [Column("rowguid")] public virtual Guid GuidId { get; set; }

    [Column] public virtual DateTime ModifiedDate { get; set; }

    [Ignore] public ProductCategory ProductCategory { get; set; }

    [Ignore] public ProductModel ProductModel { get; set; }
}

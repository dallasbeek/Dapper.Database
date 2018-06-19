using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.Concurrent;
using System.Reflection.Emit;

using Dapper;
using Dapper.Database;

#if NETSTANDARD1_3
using DataException = System.InvalidOperationException;
#else
using System.Threading;
#endif

#if NET451
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endif


namespace Dapper.Database.Extensions
{

#if NETSTANDARD1_3 || NETSTANDARD2_0 
    /// <summary>
    /// Specifies that this field is a primary key in the database
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
    }

    /// <summary>
    /// Specifies how the database generates values for a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DatabaseGeneratedAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the DatabaseGeneratedAttribute class.
        /// </summary>
        /// <param name="option"></param>
        public DatabaseGeneratedAttribute(DatabaseGeneratedOption option)
        {
            DatabaseGeneratedOption = option;
        }

        /// <summary>
        /// Gets or sets the pattern used to generate values for the property in the database.
        /// </summary>
        public DatabaseGeneratedOption DatabaseGeneratedOption { get; private set; }
    }

    /// <summary>
    /// I
    /// </summary>
    public enum DatabaseGeneratedOption
    {
        /// <summary>
        /// The database generates a value when a row is inserted or updated.
        /// </summary>
        Computed,
        /// <summary>
        /// The database generates a value when a row is inserted.
        /// </summary>
        Identity,
        /// <summary>
        /// The database does not generate values.
        /// </summary>
        None
    }
#endif

    /// <summary>
    /// Specifies whether a property should be completely ignored
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreAttribute : Attribute
    {
        /// <summary>
        /// Specifies whether a property should be completely ignored
        /// </summary>
        public IgnoreAttribute()
        {
        }
    }

    /// <summary>
    /// Specifies whether a field is insertable in the database.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreInsertAttribute : Attribute
    {
        /// <summary>
        /// Specifies whether a field is insertable in the database.
        /// </summary>
        public IgnoreInsertAttribute()
        {
        }
    }

    /// <summary>
    /// Specifies whether a field is updatable in the database.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreUpdateAttribute : Attribute
    {
        /// <summary>
        /// Specifies whether a field is updatable in the database.
        /// </summary>
        public IgnoreUpdateAttribute()
        {
        }
    }

    /// <summary>
    /// Specifies whether a field should be returned from the database.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreSelectAttribute : Attribute
    {
        /// <summary>
        /// Specifies whether a field should be returned from the database.
        /// </summary>
        public IgnoreSelectAttribute()
        {
        }
    }

    /// <summary>
    /// Specifies whether a field is read only (same as computed).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ReadOnlyAttribute : Attribute
    {
        /// <summary>
        /// Specifies whether a field is read only (same as computed).
        /// </summary>
        public ReadOnlyAttribute()
        {
        }
    }

}

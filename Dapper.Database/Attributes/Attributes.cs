using System;

namespace Dapper.Database.Attributes
{

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

    /// <summary>
    /// Oracle sequence
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SequenceAttribute : Attribute
    {
        /// <summary>
        /// Used to select identities from Oracle
        /// </summary>
        public SequenceAttribute( string name)
        {
            Name = name;
        }

        /// <summary>
        /// Name of the Oracle sequence
        /// </summary>
        public string Name { get; private set; }
    }

}

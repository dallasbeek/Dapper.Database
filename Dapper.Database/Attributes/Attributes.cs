using System;

namespace Dapper.Database.Attributes
{
    /// <summary>
    ///     Specifies whether a property should be completely ignored
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreAttribute : Attribute
    {
    }

    /// <summary>
    ///     Specifies whether a field is able to be inserted in the database.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreInsertAttribute : Attribute
    {
    }

    /// <summary>
    ///     Specifies whether a field is able to be updated in the database.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreUpdateAttribute : Attribute
    {
    }

    /// <summary>
    ///     Specifies whether a field should be returned from the database.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreSelectAttribute : Attribute
    {
    }

    /// <summary>
    ///     Specifies whether a field is read only (same as computed).
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class ReadOnlyAttribute : Attribute
    {
    }

    /// <summary>
    ///     Oracle sequence
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class SequenceAttribute : Attribute
    {
        /// <summary>
        ///     Used to select identities from Oracle
        /// </summary>
        public SequenceAttribute(string name) => Name = name;

        /// <summary>
        ///     Name of the Oracle sequence
        /// </summary>
        public string Name { get; }
    }

    /// <summary>
    ///     Specifies whether a field is read only (same as computed).
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class SqlServerSelectComputedAttribute : Attribute
    {
    }

}

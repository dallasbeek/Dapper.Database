using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Dapper.Database.Attributes;
using Dapper.Database.Extensions;
using static Dapper.Database.Extensions.SqlMapperExtensions;

namespace Dapper.Database
{
    /// <summary>
    /// </summary>
    public class TableInfo
    {
        private readonly Lazy<IEnumerable<ColumnInfo>> _generatedColumns;

        private readonly Lazy<IEnumerable<ColumnInfo>> _insertColumns;
        private readonly Lazy<IEnumerable<ColumnInfo>> _keyColumns;
        private readonly Lazy<IEnumerable<PropertyInfo>> _propertyList;
        private readonly Lazy<IEnumerable<ColumnInfo>> _selectColumns;
        private readonly Lazy<IEnumerable<ColumnInfo>> _updateColumns;
        private readonly Lazy<IEnumerable<ColumnInfo>> _concurrencyCheckColumns;

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tableNameMapper"></param>
        public TableInfo(Type type, TableNameMapperDelegate tableNameMapper)
        {
            ClassType = type;

            if (tableNameMapper != null)
            {
                TableName = TableNameMapper(type);
            }
            else
            {
                var tableAttr = type.GetCustomAttributes(false).SingleOrDefaultOfType("TableAttribute");

                if (tableAttr != null)
                {
                    TableName = tableAttr.Name;
                    if (tableAttr.Schema != null) SchemaName = tableAttr.Schema;
                }
                else
                {
                    TableName = type.Name + "s";
                    if (type.IsInterface && TableName.StartsWith("I"))
                        TableName = TableName.Substring(1);
                }
            }

            ColumnInfos = type.GetProperties()
                .Where(typeProperty => !typeProperty.GetCustomAttributes(false).AnyOfType<IgnoreAttribute>())
                .Select(typeProperty =>
                {
                    var attributes = typeProperty.GetCustomAttributes(false);
                    var columnAtt = attributes.SingleOrDefaultOfType("ColumnAttribute");
                    var seqAtt = attributes.SingleOrDefaultOfType<SequenceAttribute>();
                    var genAtt = attributes.SingleOrDefaultOfType<DatabaseGeneratedAttribute>();
                    var hasReadOnlyAttribute = attributes.AnyOfType<ReadOnlyAttribute>();

                    // Microsoft implies that TimestampAttribute is equivalent to ConcurrencyCheck + IsGenerated.
                    // @see https://www.learnentityframeworkcore.com/configuration/data-annotation-attributes/timestamp-attribute
                    var hasTimestampAttribute = attributes.OfType<TimestampAttribute>().Any();

                    var ci = new ColumnInfo
                    {
                        Property = typeProperty,
                        ColumnName = columnAtt?.Name ?? typeProperty.Name,
                        PropertyName = typeProperty.Name,
                        IsKey = attributes.AnyOfType<KeyAttribute>(),
                        IsIdentity = genAtt?.DatabaseGeneratedOption == DatabaseGeneratedOption.Identity
                                     || seqAtt != null,
                        IsGenerated = genAtt != null && genAtt.DatabaseGeneratedOption != DatabaseGeneratedOption.None
                                      || seqAtt != null
                                      || hasTimestampAttribute,
                        IsConcurrencyToken = hasTimestampAttribute
                                             || attributes.AnyOfType<ConcurrencyCheckAttribute>(),
                        ExcludeOnSelect = attributes.AnyOfType<IgnoreSelectAttribute>(),
                        SequenceName = seqAtt?.Name
                    };

                    ci.ExcludeOnInsert = ci.IsGenerated && seqAtt == null
                                         || attributes.AnyOfType<IgnoreInsertAttribute>()
                                         || hasReadOnlyAttribute;

                    ci.ExcludeOnUpdate = ci.IsGenerated
                                         || attributes.AnyOfType<IgnoreUpdateAttribute>()
                                         || hasReadOnlyAttribute;

                    if (!ci.IsGenerated) return ci;

                    var parameter = Expression.Parameter(type);
                    var property = Expression.Property(parameter, ci.Property);
                    var conversion = Expression.Convert(property, typeof(object));
                    var lambda = Expression.Lambda(conversion, parameter);
                    ci.Output = lambda;

                    return ci;
                })
                .ToArray();

            if (!ColumnInfos.Any(columnInfo => columnInfo.IsKey))
            {
                var idProp = ColumnInfos.FirstOrDefault(columnInfo =>
                    string.Equals(columnInfo.PropertyName, "id", StringComparison.CurrentCultureIgnoreCase));

                if (idProp != null)
                    idProp.IsKey = idProp.IsGenerated =
                        idProp.IsIdentity = idProp.ExcludeOnInsert = idProp.ExcludeOnUpdate = true;
            }

            _insertColumns =
                new Lazy<IEnumerable<ColumnInfo>>(() => ColumnInfos.Where(ci => !ci.ExcludeOnInsert), true);
            _updateColumns =
                new Lazy<IEnumerable<ColumnInfo>>(() => ColumnInfos.Where(ci => !ci.ExcludeOnUpdate), true);
            _selectColumns =
                new Lazy<IEnumerable<ColumnInfo>>(() => ColumnInfos.Where(ci => !ci.ExcludeOnSelect), true);
            _keyColumns = new Lazy<IEnumerable<ColumnInfo>>(() => ColumnInfos.Where(ci => ci.IsKey), true);
            _generatedColumns = new Lazy<IEnumerable<ColumnInfo>>(() => ColumnInfos.Where(ci => ci.IsGenerated), true);
            _concurrencyCheckColumns = new Lazy<IEnumerable<ColumnInfo>>(() => ColumnInfos.Where(ci => ci.IsConcurrencyToken), true);
            _propertyList = new Lazy<IEnumerable<PropertyInfo>>(() => ColumnInfos.Select(ci => ci.Property), true);
        }

        /// <summary>
        /// </summary>
        public Type ClassType { get; }

        /// <summary>
        /// </summary>
        public string TableName { get; }

        /// <summary>
        /// </summary>
        public string SchemaName { get; }

        /// <summary>
        /// </summary>
        private IEnumerable<ColumnInfo> ColumnInfos { get; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> InsertColumns => _insertColumns.Value;

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> UpdateColumns => _updateColumns.Value;

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> SelectColumns => _selectColumns.Value;

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> KeyColumns => _keyColumns.Value;

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> GeneratedColumns => _generatedColumns.Value;

        /// <summary>
        /// Gets the set of columns to use in a <c>WHERE</c> clause in an <c>UPDATE</c> or <c>DELETE</c> statement as part of concurrency management.
        /// </summary>
        /// <value>A sequnce of zero or more columns.</value>
        public IEnumerable<ColumnInfo> ConcurrencyCheckColumns => _concurrencyCheckColumns.Value;

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ColumnInfo GetSingleKey()
        {
            var keys = _keyColumns.Value;
            var columnInfos = keys as ColumnInfo[] ?? keys.ToArray();
            if (keys != null && columnInfos.Length != 1)
                throw new DataException("<T> only supports an entity with a single [Key]");

            return columnInfos.SingleOrDefault();
        }

        /// <summary>
        ///     Gets a list of all key columns defined on the table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> GetCompositeKeys()
        {
            var keys = _keyColumns.Value;
            var compositeKeys = keys as ColumnInfo[] ?? keys.ToArray();
            if (!compositeKeys.Any())
                throw new DataException("<T> does not have a [Key]");
            return compositeKeys;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<PropertyInfo> PropertyList => _propertyList.Value;

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public bool HasSequenceName => ColumnInfos.Any(ci => !string.IsNullOrWhiteSpace(ci.SequenceName));
    }

    /// <summary>
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// </summary>
        public bool IsGenerated { get; set; }

        /// <summary>
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// </summary>
        public bool ExcludeOnInsert { get; set; }

        /// <summary>
        /// </summary>
        public bool ExcludeOnUpdate { get; set; }

        /// <summary>
        /// </summary>
        public bool ExcludeOnSelect { get; set; }

        /// <summary>
        /// </summary>
        public PropertyInfo Property { get; set; }

        /// <summary>
        /// </summary>
        public string SequenceName { get; set; }

        /// <summary>
        /// </summary>
        public LambdaExpression Output { get; set; }

        /// <summary>
        /// Indicates whether this column should be included in a <c>WHERE</c> clause in an <c>UPDATE</c> or <c>DELETE</c> statement as part of concurrency management.
        /// </summary>
        /// <seealso cref="ConcurrencyCheckAttribute"/>
        /// <seealso cref="TimestampAttribute"/>
        public bool IsConcurrencyToken { get; set; }

        /// <summary>
        ///     Gets the value of the specified column for a given instance of the object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public object GetValue<T>(T instance) => Property.GetValue(instance);
    }
}

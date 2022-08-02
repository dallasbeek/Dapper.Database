﻿using System;
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
        private readonly Lazy<IEnumerable<ColumnInfo>> _comparisonColumns;
        private readonly Lazy<IEnumerable<ColumnInfo>> _concurrencyCheckColumns;
        private readonly Lazy<IEnumerable<ColumnInfo>> _generatedColumns;

        private readonly Lazy<IEnumerable<ColumnInfo>> _insertColumns;
        private readonly Lazy<IEnumerable<ColumnInfo>> _keyColumns;
        //private readonly Lazy<IEnumerable<PropertyInfo>> _propertyList;
        private readonly Lazy<IEnumerable<ColumnInfo>> _selectColumns;
        private readonly Lazy<IEnumerable<ColumnInfo>> _updateColumns;

        /// <summary>
        ///     Creates a new TableInfo for the specified type with the default table mapper.
        /// </summary>
        /// <param name="type">The entity <see cref="Type" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type" />
        /// </exception>
        public TableInfo(Type type)
            : this(type, null)
        {
        }

        /// <summary>
        ///     Creates a new TableInfo for the specified type, with optional table mapper.
        /// </summary>
        /// <param name="type">The entity <see cref="Type" />.</param>
        /// <param name="tableNameMapper">A delegate for how to generate the table name from the <paramref name="type" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type" />
        /// </exception>
        public TableInfo(Type type, TableNameMapperDelegate tableNameMapper)
        {
            ClassType = type ?? throw new ArgumentNullException(nameof(type));

            if (tableNameMapper != null)
            {
                TableName = TableNameMapper(type);
            }
            else
            {
                var tableAttr = type.GetCustomAttributes(false).SingleOrDefaultOfType(nameof(TableAttribute));

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

            SqlServerSelectComputed = SqlDatabase.SqlServerSelectComputed || type.GetCustomAttributes(false)
                .SingleOrDefaultOfType(nameof(SqlServerSelectComputedAttribute)) != null;

            Columns = type.GetProperties()
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
                        IsGenerated = (genAtt != null && genAtt.DatabaseGeneratedOption != DatabaseGeneratedOption.None)
                                      || seqAtt != null
                                      || hasTimestampAttribute,
                        IsConcurrencyToken = hasTimestampAttribute
                                             || attributes.AnyOfType<ConcurrencyCheckAttribute>(),
                        ExcludeOnSelect = attributes.AnyOfType<IgnoreSelectAttribute>(),
                        SequenceName = seqAtt?.Name
                    };

                    ci.IsNullable = !ci.IsKey // do not allow Keys to be nullable
                                    && !attributes
                                        .AnyOfType<
                                            RequiredAttribute>() // Required cannot be null. LATER: do we want to validate empty values? Using this for pre-C# 8 nullable enforcement
                                    && ci.Property.IsNullable();

                    ci.ExcludeOnInsert = (ci.IsGenerated && seqAtt == null)
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

            if (!Columns.Any(columnInfo => columnInfo.IsKey))
            {
                var idProp = Columns.FirstOrDefault(columnInfo =>
                    string.Equals(columnInfo.PropertyName, "id", StringComparison.CurrentCultureIgnoreCase));

                if (idProp != null)
                    idProp.IsKey = idProp.IsGenerated =
                        idProp.IsIdentity = idProp.ExcludeOnInsert = idProp.ExcludeOnUpdate = true;
            }

            _insertColumns =
                new Lazy<IEnumerable<ColumnInfo>>(() => Columns.Where(ci => !ci.ExcludeOnInsert), true);
            _updateColumns =
                new Lazy<IEnumerable<ColumnInfo>>(() => Columns.Where(ci => !ci.ExcludeOnUpdate), true);
            _selectColumns =
                new Lazy<IEnumerable<ColumnInfo>>(() => Columns.Where(ci => !ci.ExcludeOnSelect), true);
            _keyColumns = new Lazy<IEnumerable<ColumnInfo>>(() => Columns.Where(ci => ci.IsKey), true);
            _generatedColumns = new Lazy<IEnumerable<ColumnInfo>>(() => Columns.Where(ci => ci.IsGenerated), true);
            _concurrencyCheckColumns =
                new Lazy<IEnumerable<ColumnInfo>>(() => Columns.Where(ci => ci.IsConcurrencyToken), true);
            _comparisonColumns =
                new Lazy<IEnumerable<ColumnInfo>>(() => Columns.Where(ci => ci.IsKey || ci.IsConcurrencyToken), true);

            //_propertyList = new Lazy<IEnumerable<PropertyInfo>>(() => Columns.Select(ci => ci.Property), true);
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
        internal IEnumerable<ColumnInfo>
            Columns { get; } // LATER: before making public maybe make this a list-dictionary hybrid?

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
        ///     Gets the set of columns to use in optimistic concurrency checks.
        /// </summary>
        /// <value>A sequence of zero or more columns.</value>
        public IEnumerable<ColumnInfo> ConcurrencyCheckColumns => _concurrencyCheckColumns.Value;

        /// <summary>
        ///     Gets the set of columns to use in a <c>WHERE</c> clause in an <c>UPDATE</c> or <c>DELETE</c> statement.
        ///     Columns should consist of key columns as well as those involved with optimistic concurrency checks.
        /// </summary>
        /// <value>A sequence of zero or more columns.</value>
        public IEnumerable<ColumnInfo> ComparisonColumns => _comparisonColumns.Value;

        /// <summary>
        ///     Returns computed columns with select query vs. output clause.
        ///     This corrects issues related to triggers applied and error "The target table '{Table}' of the DML statement cannot
        ///     have any enabled triggers"
        /// </summary>
        public bool SqlServerSelectComputed { get; }

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
        ///     Indicates whether this column can be set to a null value.
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        ///     Indicates whether this column should be included in a <c>WHERE</c> clause in an <c>UPDATE</c> or <c>DELETE</c>
        ///     statement as part of concurrency management.
        /// </summary>
        /// <seealso cref="ConcurrencyCheckAttribute" />
        /// <seealso cref="TimestampAttribute" />
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

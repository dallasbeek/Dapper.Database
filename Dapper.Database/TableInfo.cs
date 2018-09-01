using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper.Database.Attributes;
using Dapper.Database.Extensions;
using static Dapper.Database.Extensions.SqlMapperExtensions;

#if NETSTANDARD1_3
using DataException = System.InvalidOperationException;
#endif

namespace Dapper.Database
{

    /// <summary>
    /// 
    /// </summary>
    public class TableInfo
    {

        private readonly Lazy<IEnumerable<ColumnInfo>> _insertColumns;
        private readonly Lazy<IEnumerable<ColumnInfo>> _updateColumns;
        private readonly Lazy<IEnumerable<ColumnInfo>> _selectColumns;
        private readonly Lazy<IEnumerable<ColumnInfo>> _keyColumns;
        private readonly Lazy<IEnumerable<ColumnInfo>> _generatedColumns;
        private readonly Lazy<IEnumerable<PropertyInfo>> _propertyList;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tablenameMapper"></param>
        public TableInfo(Type type, TableNameMapperDelegate tablenameMapper)
        {
            ClassType = type;

            if (tablenameMapper != null)
            {
                TableName = TableNameMapper(type);
            }
            else
            {
                //NOTE: This as dynamic trick should be able to handle both our own Table-attribute as well as the one in EntityFramework 
                var tableAttr = type
#if NETSTANDARD1_3
                .GetTypeInfo()
#endif
                .GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic;

                if (tableAttr != null)
                {
                    TableName = tableAttr.Name;
                    if (tableAttr.Schema != null)
                    {
                        SchemaName = tableAttr.Schema;
                    }
                }
                else
                {
                    TableName = type.Name + "s";
                    if (type.IsInterface() && TableName.StartsWith("I"))
                        TableName = TableName.Substring(1);
                }
            }

            ColumnInfos = type.GetProperties()
                .Where(t => t.GetCustomAttributes(typeof(IgnoreAttribute), false).Count() == 0)
                .Select(t =>
                {
                    var columnAtt = t.GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "ColumnAttribute") as dynamic;

                    var ci = new ColumnInfo
                    {
                        Property = t,
                        ColumnName = columnAtt?.Name ?? t.Name,
                        PropertyName = t.Name,
                        IsKey = t.GetCustomAttributes(true).Any(a => a is KeyAttribute),
                        IsIdentity = t.GetCustomAttributes(true).Any(a => a is DatabaseGeneratedAttribute
                          && (a as DatabaseGeneratedAttribute).DatabaseGeneratedOption == DatabaseGeneratedOption.Identity),
                        IsGenerated = t.GetCustomAttributes(true).Any(a => a is DatabaseGeneratedAttribute
                            && (a as DatabaseGeneratedAttribute).DatabaseGeneratedOption != DatabaseGeneratedOption.None),
                        ExcludeOnSelect = t.GetCustomAttributes(true).Any(a => a is IgnoreSelectAttribute)
                    };

                    ci.ExcludeOnInsert = ci.IsGenerated
                        || t.GetCustomAttributes(true).Any(a => a is IgnoreInsertAttribute)
                        || t.GetCustomAttributes(true).Any(a => a is ReadOnlyAttribute);

                    ci.ExcludeOnUpdate = ci.IsGenerated
                        || t.GetCustomAttributes(true).Any(a => a is IgnoreUpdateAttribute)
                        || t.GetCustomAttributes(true).Any(a => a is ReadOnlyAttribute);

                    return ci;
                })
                .ToArray();

            if (!ColumnInfos.Any(k => k.IsKey))
            {
                var idProp = ColumnInfos.FirstOrDefault(p => string.Equals(p.PropertyName, "id", StringComparison.CurrentCultureIgnoreCase));

                if (idProp != null)
                {
                    idProp.IsKey = idProp.IsGenerated = idProp.IsIdentity = idProp.ExcludeOnInsert = idProp.ExcludeOnUpdate = true;
                }
            }

            _insertColumns = new Lazy<IEnumerable<ColumnInfo>>(() => ColumnInfos.Where(ci => !ci.ExcludeOnInsert), true);
            _updateColumns = new Lazy<IEnumerable<ColumnInfo>>(() => ColumnInfos.Where(ci => !ci.ExcludeOnUpdate), true);
            _selectColumns = new Lazy<IEnumerable<ColumnInfo>>(() => ColumnInfos.Where(ci => !ci.ExcludeOnSelect), true);
            _keyColumns = new Lazy<IEnumerable<ColumnInfo>>(() => ColumnInfos.Where(ci => ci.IsKey), true);
            _generatedColumns = new Lazy<IEnumerable<ColumnInfo>>(() => ColumnInfos.Where(ci => ci.IsGenerated), true);
            _propertyList = new Lazy<IEnumerable<PropertyInfo>>(() => ColumnInfos.Select(ci => ci.Property), true);
        }
     
        /// <summary>
        /// 
        /// </summary>
        public Type ClassType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string SchemaName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<ColumnInfo> ColumnInfos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public ColumnInfo GetSingleKey(string method)
        {
            var keys = _keyColumns.Value;
            if (keys.Count() > 1)
                throw new DataException($"{method}<T> only supports an entity with a single [Key] or [ExplicitKey] property");

            return keys.SingleOrDefault();
        }
        
        /// <summary>
        /// Gets a list of all key columns defined on the table
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> GetCompositeKeys(string method)
        {
            return ColumnInfos.Where(p => p.IsKey).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> InsertColumns => _insertColumns.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> UpdateColumns => _updateColumns.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> SelectColumns => _selectColumns.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> KeyColumns => _keyColumns.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> GeneratedColumns => _generatedColumns.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> PropertyList =>_propertyList.Value;

    }

    /// <summary>
    /// 
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsGenerated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ExcludeOnInsert { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ExcludeOnUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ExcludeOnSelect { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PropertyInfo Property { get; set; }

        /// <summary>
        /// Gets the value of the specified column for a given instance of the object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public object GetValue<T>(T instance)
        {
            return Property.GetValue(instance);
        }
    }
}

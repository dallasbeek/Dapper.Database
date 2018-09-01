using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DynamicParameters"/>.
    /// </summary>
    public static class DynamicParametersExtensions
    {
        /// <summary>
        /// Allows you to automatically populate a target property/field from output parameters. It actually
        /// creates an InputOutput parameter, so you can still pass data in.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">this</param>
        /// <param name="target">The object whose property/field you wish to populate.</param>
        /// <param name="column">A <see cref="ColumnInfo"/> targeting a property/field of the target (or descendant thereof.)</param>
        /// <param name="dbType"></param>
        /// <param name="size">The size to set on the parameter. Defaults to 0, or DbString.DefaultLength in case of strings.</param>
        /// <returns>The DynamicParameters instance</returns>
        /// <seealso cref="DynamicParameters.Output{T}(T, Expression{Func{T, object}}, System.Data.DbType?, int?)"/>
        public static DynamicParameters Output<T>(this DynamicParameters parameters, T target, ColumnInfo column, DbType? dbType = null, int? size = null) 
            => parameters.Output(target, (Expression<Func<T, object>>)column.Output, dbType, size);
    }
}

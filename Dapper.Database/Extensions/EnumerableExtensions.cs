using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// Internal helpers for chained LINQ expressions.
    /// </summary>
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Returns the only dynamic element of an <see cref="IEnumerable{T}"/> that has a specific type name.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="typeName"></param>
        /// <returns>The item (if found) as a <see langword="dynamic" />, or null if not found.</returns>
        internal static dynamic SingleOrDefaultOfType<TSource>(this IEnumerable<TSource> source, string typeName) => source.SingleOrDefault(item => item.GetType().Name == typeName) as dynamic;


        /// <summary>
        /// Returns the only element of an <see cref="IEnumerable"/> that is a specific type or a default value if the sequence is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        internal static T SingleOrDefaultOfType<T>(this IEnumerable source) => source.OfType<T>().SingleOrDefault();

        /// <summary>
        /// Returns whether any elements of an <see cref="IEnumerable"/> are of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        internal static bool AnyOfType<T>(this IEnumerable source) => source.OfType<T>().Any();
    }
}

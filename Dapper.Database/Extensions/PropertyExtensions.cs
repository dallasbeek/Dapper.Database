using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Dapper.Database.Extensions
{
    /// <summary>
    ///     Internal helpers for types.
    /// </summary>
    internal static class PropertyExtensions
    {
        /// <summary>
        ///     Gets whether a specified property is nullable.
        /// </summary>
        /// <param name="property"></param>
        /// <returns>
        ///     true if the property is nullable, false otherwise.
        /// </returns>
        public static bool IsNullable(this PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types#how-to-identify-a-nullable-value-type
            if (property.PropertyType.IsValueType) return Nullable.GetUnderlyingType(property.PropertyType) != null;

            // Handle C# 8 nullable reference types.
            // https://codeblog.jonskeet.uk/2019/02/10/nullableattribute-and-c-8/
            // https://stackoverflow.com/questions/58453972/how-to-use-net-reflection-to-check-for-nullable-reference-type
            var nullable = property.CustomAttributes
                .FirstOrDefault(x => x.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute");
            if (nullable != null && nullable.ConstructorArguments.Count == 1)
            {
                var attributeArgument = nullable.ConstructorArguments[0];
                if (attributeArgument.ArgumentType == typeof(byte[]))
                {
                    var args = (ReadOnlyCollection<CustomAttributeTypedArgument>)attributeArgument.Value;
                    if (args.Count > 0 && args[0].ArgumentType == typeof(byte)) return (byte)args[0].Value == 2;
                }
                else if (attributeArgument.ArgumentType == typeof(byte))
                {
                    return (byte)attributeArgument.Value == 2;
                }
            }

            var context = property.DeclaringType?.CustomAttributes.FirstOrDefault(x =>
                x.AttributeType.FullName == "System.Runtime.CompilerServices.NullableContextAttribute");

            if (context != null &&
                context.ConstructorArguments.Count == 1 &&
                context.ConstructorArguments[0].ArgumentType == typeof(byte))
                return (byte)context.ConstructorArguments[0].Value == 2;

            // No nullable attribute present
            return true;
        }
    }
}

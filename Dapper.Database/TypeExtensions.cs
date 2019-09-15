using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dapper.Database
{
    internal static class TypeExtensions
    {
        public static string Name(this Type type) =>
            type.Name;

        public static bool IsValueType(this Type type) =>
            type.IsValueType;

        public static bool IsEnum(this Type type) =>
            type.IsEnum;

        public static bool IsGenericType(this Type type) =>
            type.IsGenericType;

        public static bool IsInterface(this Type type) =>
            type.IsInterface;


        public static TypeCode GetTypeCode(Type type) => Type.GetTypeCode(type);

        public static MethodInfo GetPublicInstanceMethod(this Type type, string name, Type[] types)
        {
            return type.GetMethod(name, BindingFlags.Instance | BindingFlags.Public, null, types, null);
        }
    }
}

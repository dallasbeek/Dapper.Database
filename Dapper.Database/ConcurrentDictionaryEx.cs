using System;
using System.Collections.Concurrent;

namespace Dapper.Database
{
    /// <summary>
    /// </summary>
    internal static class ConcurrentDictionaryEx
    {
        /// <summary>
        /// </summary>
        /// <param name="concurrentDict"></param>
        /// <param name="handle"></param>
        /// <param name="fromCache"></param>
        /// <param name="retrieve"></param>
        /// <returns></returns>
        public static string Acquire(this ConcurrentDictionary<RuntimeTypeHandle, string> concurrentDict,
            RuntimeTypeHandle handle, Func<bool> fromCache, Func<string> retrieve)
        {
            if (!fromCache() || !SqlDatabase.CacheQueries) return retrieve();

            if (concurrentDict.TryGetValue(handle, out _)) return retrieve();
            var sql = retrieve();
            concurrentDict[handle] = sql;
            return sql;
        }
    }
}

using System;
using System.Collections.Concurrent;

namespace Dapper.Database
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ConcurrentDictionaryEx
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="concurrentDict"></param>
        /// <param name="handle"></param>
        /// <param name="fromcache"></param>
        /// <param name="retrieve"></param>
        /// <returns></returns>
        public static string Acquire(this ConcurrentDictionary<RuntimeTypeHandle, string> concurrentDict, RuntimeTypeHandle handle, Func<bool> fromcache, Func<string> retrieve)
        {

            if (!fromcache() || Environment.GetEnvironmentVariable("NoCache")?.ToUpperInvariant() == "TRUE")
            {
                return retrieve();
            }

            if (!concurrentDict.TryGetValue(handle, out string sql))
            {
                sql = retrieve();
                concurrentDict[handle] = sql;
                return sql;
            }
            return retrieve();
        }

    }
}

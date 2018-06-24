using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Database
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConcurrentDictionaryEx
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
            if (fromcache())
            {
                if (!concurrentDict.TryGetValue(handle, out string sql))
                {
                    sql = retrieve();
                    concurrentDict[handle] = sql;
                    return sql;
                }
                else
                {
                    return sql;
                }
            }
            return retrieve();
        }
    }
}

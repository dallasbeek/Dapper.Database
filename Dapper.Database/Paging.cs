using System;
using System.Collections.Generic;

namespace Dapper.Database
{
    /// <summary>
    /// Represents a paged result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedEnumerable<T> : IEnumerable<T>
    {
        /// <summary>
        /// Current Page Requested
        /// </summary>
        long CurrentPage { get; }

        /// <summary>
        /// Size of the Page Requested
        /// </summary>
        long PageSize { get; }

        /// <summary>
        /// Total Matching records
        /// </summary>
        long TotalCount { get; }
    }

    /// <summary>
    /// Paged Result List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>, IPagedEnumerable<T>
    {
        /// <summary>
        /// Current Page
        /// </summary>
        public long CurrentPage { get; set; }

        /// <summary>
        /// Page Size Requested
        /// </summary>
        public long PageSize { get; set; }

        /// <summary>
        /// Total count of matching records
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="source"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        public PagedList(IEnumerable<T> source, long currentPage, long pageSize, long totalCount)
        {
            if (pageSize == 0)
                throw new ArgumentOutOfRangeException("pageSize", "pageSize must be greater than zero");

            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;

            AddRange(source);
        }

    }
}

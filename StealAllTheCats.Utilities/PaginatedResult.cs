using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StealAllTheCats.Utilities
{
    /// <summary>
    /// Class PaginatedResult.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedResult<T>
    {
        private const int defaultPageSize = 10;
        private const int maxPageSize = 20;

        /// <summary>
        /// Gets the total count of items.
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Gets the page size.
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Gets the page number.
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// Gets the list of items.
        /// </summary>
        public List<T> Items { get; private set; }

        /// <summary>
        /// PaginatedResult Constructor.
        /// </summary>
        public PaginatedResult()
        {

        }

        /// <summary>
        /// PaginatedResult Constructor.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        internal PaginatedResult(int pageNumber, int pageSize = defaultPageSize)
        {
            this.PageSize = pageSize;
            Page = pageNumber;

            if (this.PageSize < 0 || this.PageSize > maxPageSize)
            {
                this.PageSize = defaultPageSize;
            }
            if (pageNumber < 0)
            {
                Page = 0;
            }
        }

        /// <summary>
        /// Sets pagination.
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        internal async Task<PaginatedResult<T>> Paginate(IQueryable<T> queryable)
        {
            TotalCount = queryable.Count();

            if (PageSize > TotalCount)
            {
                PageSize = TotalCount;
                Page = 0;
            }

            int skip = Page * PageSize;
            if (skip + PageSize > TotalCount)
            {
                skip = TotalCount - PageSize;
                Page = TotalCount / PageSize - 1;
            }

            Items = await queryable.Skip(skip)
                .Take(PageSize)
                .ToListAsync();
            return this;
        }
    }
}

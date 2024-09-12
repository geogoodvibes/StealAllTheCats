using Microsoft.AspNetCore.Mvc;

namespace StealAllTheCats.Dto
{
    /// <summary>
    /// PaginatedResultDto Class.
    /// </summary>
    public class PaginatedResultDto<T> : ActionResult
    {
        /// <summary>
        /// Total count of items.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Page number.
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// List of items.
        /// </summary>
        public List<T> Items { get;  set; }


        public PaginatedResultDto()
        {

        }
    }
}
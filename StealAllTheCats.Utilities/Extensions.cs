namespace StealAllTheCats.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// Sets the pagination.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public static async Task<PaginatedResult<T>> Paginate<T>(this IQueryable<T> source,
                                                int pageSize, int pageNumber)
        {
            return await new PaginatedResult<T>(pageNumber, pageSize).Paginate(source);
        }
    }
}

using StealAllTheCats.Dto;
using StealAllTheCats.Dto.Cats;
using StealAllTheCats.Utilities;

namespace StealAllTheCats.Business.Interfaces
{
    /// <summary>
    /// ICatRepository Interface.
    /// </summary>
    public interface ICatRepository
    {
        /// <summary>
        /// Gets a paged list of cats.
        /// </summary>
        /// <param name="tag">The tag parameter.</param>
        /// <param name="page">The page parameter.</param>
        /// <param name="pageSize">The pageSize parameter.</param>
        /// <returns></returns>
        Task<PaginatedResult<GetCatResponseDto>> GetCatsAsync(
            int page,
            int pageSize);

        /// <summary>
        /// Gets a paged list of cats filtered by tag.
        /// </summary>
        /// <param name="tag">The tag parameter.</param>
        /// <param name="page">The page parameter.</param>
        /// <param name="pageSize">The pageSize parameter.</param>
        /// <returns></returns>
        Task<PaginatedResult<GetCatResponseDto>> GetCatsAsync(
            string tag,
            int page,
            int pageSize);

        /// <summary>
        /// Gets the cat by cat identifier.
        /// </summary>
        /// <param name="catId">The cat identifier.</param>
        /// <returns></returns>
        Task<GetCatResponseDto> GetCatAsync(
            int catId);

        /// <summary>
        /// Add a list of cats.
        /// </summary>
        /// <param name="addCatListDto">The list of cats.</param>
        /// <returns></returns>
        Task<int> AddCatAsync(List<AddCatRequestDto> addCatListDto);
    }
}

using StealAllTheCats.Dto.Cats;
using StealAllTheCats.Utilities;

namespace StealAllTheCats.Business.Interfaces
{
    /// <summary>
    /// ICatRepository Interface.
    /// </summary>
    public interface ICatRepository
    {
        Task<PaginatedResult<GetCatResponseDto>> GetCatsAsync(
            string tag,
            int page,
            int pageSize);

        Task<GetCatResponseDto> GetCatAsync(
            int catId);

        Task<int> AddCatAsync(
            AddCatRequestDto addCatDto);

        Task DownloadImageFileById(
            int catId);
    }
}

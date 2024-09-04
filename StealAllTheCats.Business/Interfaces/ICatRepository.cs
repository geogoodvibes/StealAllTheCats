using StealAllTheCats.Dto.Cats;
using StealAllTheCats.Utilities;

namespace StealAllTheCats.Business.Interfaces
{
    /// <summary>
    /// ICatRepository Interface.
    /// </summary>
    public interface ICatRepository
    {
        Task<PaginatedResult<GetCatApiResponseDto>> GetCatsAsync(
            string tag,
            int page,
            int pageSize);

        Task<GetCatApiResponseDto> GetCatAsync(
            int catId);

        Task<int> AddCatAsync(List<AddCatRequestDto> addCatListDto);

        Task DownloadImageFileById(
            int catId);
    }
}

using StealAllTheCats.Dto.Cats;

namespace StealAllTheCats.Business.Interfaces
{
    /// <summary>
    /// ICatRepository Interface.
    /// </summary>
    public interface ICatRepository
    {
        Task<List<GetCatResponseDto>> GetCatsAsync();

        Task<GetCatResponseDto> GetCatAsync(int catId);

        //Task<int> AddCatAsync(AddCatRequestDto addCatDto);

        //Task UpdateCatAsync(int catId, UpdateCatRequestDto updateCatDto);

        Task DeleteCatAsync(int catId);

        Task DownloadFileById(int catId);
    }
}

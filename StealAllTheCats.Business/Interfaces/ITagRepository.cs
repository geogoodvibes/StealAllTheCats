using StealAllTheCats.Dto.Tags;

namespace StealAllTheCats.Business.Interfaces
{
    /// <summary>
    /// ITagRepository Interface.
    /// </summary>
    public interface ITagRepository
    {
        Task<List<GetTagResponseDto>> GetTagsAsync();

        Task<GetTagResponseDto> GetTagAsync(int tagId);

        Task<int> AddTagAsync(AddTagRequestDto addTagDto);
    }
}

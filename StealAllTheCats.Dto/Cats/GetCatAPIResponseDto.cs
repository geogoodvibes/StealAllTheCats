using System.Text.Json.Serialization;

namespace StealAllTheCats.Dto.Cats
{
    /// <summary>
    /// Class GetCatApiResponseDto.
    /// </summary>
    public class GetCatApiResponseDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("breeds")]
        public GetBreedResponseDto[]? Breeds { get; set; }
    }
}

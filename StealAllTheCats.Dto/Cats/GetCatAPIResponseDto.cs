using StealAllTheCats.Dto.Tags;
using System.Text.Json.Serialization;

namespace StealAllTheCats.Dto.Cats
{
    /// <summary>
    /// Class GetCatApiResponseDto.
    /// </summary>
    public class GetCatApiResponseDto
    {
        /// <summary>
        /// Gets or sets the identifier from CaaS API.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the url from CaaS API.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the width from CaaS API.
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height from CaaS API.
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the breeds from CaaS API.
        /// </summary>
        [JsonPropertyName("breeds")]
        public GetBreedResponseDto[]? Breeds { get; set; }
    }
}

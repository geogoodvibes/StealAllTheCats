using System.Text.Json.Serialization;

namespace StealAllTheCats.Dto.Tags
{
    public class GetBreedResponseDto
    {
        /// <summary>
        /// Gets or sets the identifier of the breed.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the breed.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the temperament of the breed.
        /// </summary>
        [JsonPropertyName("temperament")]
        public string? Temperament { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace StealAllTheCats.Dto.Tags
{
    public class AddBreedRequestDto
    {
        /// <summary>
        /// Gets or sets the identifier of the breed.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the breed.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the temperament of the breed.
        /// </summary>
        public string? Temperament { get; set; }
    }
}

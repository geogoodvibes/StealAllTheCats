using System.Text.Json.Serialization;

namespace StealAllTheCats.Dto.Cats
{
    public class AddBreedRequestDto
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Temperament { get; set; }
    }
}

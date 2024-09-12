using StealAllTheCats.Dto.Tags;

namespace StealAllTheCats.Dto.Cats
{
    public class GetCatResponseDto
    {
        /// <summary>
        /// Gets or sets the identifier of the cat.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the image returned from CaaS API.
        /// </summary>
        public string? CatApiId { get; set; }

        /// <summary>
        /// Gets or sets the width of the image returned from CaaS API.
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the image returned from CaaS API.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the Image of the cat.
        /// </summary>
        public string? ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the image url returned from CaaS API.
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the date creation of the cat.
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// List of tags.
        /// </summary>
        public virtual ICollection<GetTagResponseDto>? Tags { get; set; }
    }
}

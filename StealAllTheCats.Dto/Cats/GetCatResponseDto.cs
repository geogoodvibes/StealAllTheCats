using StealAllTheCats.Dto.Tags;
using System.ComponentModel.DataAnnotations;

namespace StealAllTheCats.Dto.Cats
{
    public class GetCatResponseDto
    {
        /// <summary>
        /// ID of the cat
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID of the image returned from CaaS API
        /// </summary>
        public string? CatApiId { get; set; }

        /// <summary>
        /// Width of the image returned from CaaS API
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Height of the image returned from CaaS API
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Image of the cat
        /// </summary>
        public string? ImagePath { get; set; }

        /// <summary>
        /// Image filename
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// DateTime creation of the cat
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// List of tags.
        /// </summary>
        public virtual ICollection<GetTagResponseDto>? Tags { get; set; }
    }
}

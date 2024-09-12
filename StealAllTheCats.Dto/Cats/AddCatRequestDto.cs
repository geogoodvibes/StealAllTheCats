using StealAllTheCats.Dto.Tags;
using System.ComponentModel.DataAnnotations;

namespace StealAllTheCats.Dto.Cats
{
    public class AddCatRequestDto
    {
        /// <summary>
        /// Gets or sets the identifier of the image returned from CaaS API.
        /// </summary>
        [Required]
        public string CatApiId { get; set; }

        /// <summary>
        /// Gets or sets the Width of the image returned from CaaS API.
        /// </summary>
        [Required]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the Height of the image returned from CaaS API.
        /// </summary>
        [Required]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the image of the cat.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Gets or sets the image path of the cat.
        /// </summary>
        [Required]
        [MaxLength(215)]
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the breeds and temperament of the cat.
        /// </summary>
        public AddBreedRequestDto[]? Breeds { get; set; }

        /// <summary>
        /// Gets or sets the image URL returned from CaaS API.
        /// </summary>
        [MaxLength(256)]
        [Required]
        public string Url { get; set; }

        public AddCatRequestDto()
        {
            //NOOP
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace StealAllTheCats.Dto.Cats
{
    public class AddCatRequestDto
    {
        /// <summary>
        /// ID of the image returned from CaaS API
        /// </summary>
        [Required]
        public string CatApiId { get; set; }

        /// <summary>
        /// Width of the image returned from CaaS API
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of the image returned from CaaS API
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Image of the cat
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Image of the cat
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Breeds and temperament of the cat
        /// </summary>
        public AddBreedRequestDto[]? Breeds { get; set; }

        /// <summary>
        /// Image filename
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

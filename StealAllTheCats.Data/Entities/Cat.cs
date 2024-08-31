using System.ComponentModel.DataAnnotations;

namespace StealAllTheCats.Data.Entities
{
    public class Cat
    {
        /// <summary>
        /// ID of the cat
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID of the image returned from CaaS API
        /// </summary>
        [Required]
        public string CatApiId { get; set; }

        /// <summary>
        /// Width of the image returned from CaaS API
        /// </summary>
        [Required]
        public int Width { get; set; }

        /// <summary>
        /// Height of the image returned from CaaS API
        /// </summary>
        [Required]
        public int Height { get; set; }

        /// <summary>
        /// Image of the cat
        /// </summary>
        [Required]
        public byte[] Image { get; set; }

        /// <summary>
        /// Image filename
        /// </summary>
        [MaxLength(256)]
        [Required]
        public string ImageFilepath { get; set; }

        /// <summary>
        /// DateTime creation of the cat
        /// </summary>
        public DateTime Created { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    }
}

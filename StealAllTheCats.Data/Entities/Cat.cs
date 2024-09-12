using System.ComponentModel.DataAnnotations;

namespace StealAllTheCats.Data.Entities
{
    /// <summary>
    /// Class Cat.
    /// </summary>
    public class Cat
    {
        /// <summary>
        /// Gets or sets the identifier of the cat.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the image returned from CaaS API.
        /// </summary>
        [Required]
        public string CatApiId { get; set; }

        /// <summary>
        /// Gets or sets the width of the image returned from CaaS API.
        /// </summary>
        [Required]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the image returned from CaaS API.
        /// </summary>
        [Required]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the image path of the cat.
        /// </summary>
        [Required]
        [MaxLength(215)]
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the image url returned from CaaS API.
        /// </summary>
        [MaxLength(256)]
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the date creation of the cat.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the list of tags.
        /// </summary>
        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        /// <summary>
        /// Cat Constructor.
        /// </summary>
        public Cat()
        {
            //NOOP
        }
    }
}

namespace StealAllTheCats.Data.Entities
{
    /// <summary>
    /// Class Tag.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Gets or sets the identifier of the tag.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the associated cat identifier.
        /// </summary>
        public int? CatId { get; set; }

        /// <summary>
        /// Gets or sets the associated cat.
        /// </summary>
        public virtual Cat Cat { get; set; }

        /// <summary>
        /// Gets or sets the date of creation of the tag.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Tag Constructor.
        /// </summary>
        public Tag()
        {

        }
    }
}

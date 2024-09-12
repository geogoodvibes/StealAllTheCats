namespace StealAllTheCats.Data.Entities
{
    /// <summary>
    /// Class Tag.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Id of the tag
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the tag
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the cat id.
        /// </summary>
        public int? CatId { get; set; }

        /// <summary>
        /// Cat of the candidate
        /// </summary>
        public virtual Cat Cat { get; set; }

        /// <summary>
        /// DateTime creation of the tag
        /// </summary>
        public DateTime Created { get; set; }

        public Tag()
        {

        }
    }
}

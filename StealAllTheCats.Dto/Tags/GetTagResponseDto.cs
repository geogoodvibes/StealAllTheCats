namespace StealAllTheCats.Dto.Tags
{
    /// <summary>
    /// Class GetTagResponseDto.
    /// </summary>
    public class GetTagResponseDto
    {
        /// <summary>
        /// Id of the tag
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the tag
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the cat id.
        /// </summary>
        public int? CatId { get; set; }

        /// <summary>
        /// DateTime creation of the tag
        /// </summary>
        public DateTime Created { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using StealAllTheCats.Data.Entities;
using StealAllTheCats.Data.EntityTypeConfigurations;

namespace StealAllTheCats.Data.Context
{
    public class StealAllTheCatsContext : DbContext
    {
        public StealAllTheCatsContext(DbContextOptions<StealAllTheCatsContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase(databaseName: "StealAllTheCatsDb");
            optionsBuilder
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new TagEntityTypeConfiguration().Configure(modelBuilder.Entity<Tag>());
            new CatEntityTypeConfiguration().Configure(modelBuilder.Entity<Cat>());
        }

        /// <summary>
        /// Gets or sets the cats.
        /// </summary>
        public virtual DbSet<Cat> Cats { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        public virtual DbSet<Tag> Tags { get; set; }
    }
}

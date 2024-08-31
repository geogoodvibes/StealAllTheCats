using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StealAllTheCats.Data.Entities;

namespace StealAllTheCats.Data.EntityTypeConfigurations
{
    /// <summary>
    /// The <see cref="IEntityTypeConfiguration{T}"/> where T is <see cref="Cat"/>.
    /// </summary>
    public class CatEntityTypeConfiguration : IEntityTypeConfiguration<Cat>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Cat> builder)
        {
            builder.ToTable("Cats", "dbo");

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CatApiId)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(c => c.Width)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(c => c.Height)
                .IsRequired();

            builder.Property(c => c.Image)
                .IsRequired()
                .HasColumnType("blob");

            builder.Property(c => c.ImageFilepath)
                .IsRequired()
                .HasMaxLength(256);

            builder
                .HasMany(d => d.Tags)
                .WithOne(c => c.Cat);

            builder.Property(c => c.Created).HasDefaultValueSql("getdate()");

        }
    }
}

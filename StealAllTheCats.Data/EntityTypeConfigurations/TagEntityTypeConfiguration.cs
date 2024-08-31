using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StealAllTheCats.Data.Entities;

namespace StealAllTheCats.Data.EntityTypeConfigurations
{
    /// <summary>
    /// The <see cref="IEntityTypeConfiguration{T}"/> where T is <see cref="Tag"/>.
    /// </summary>
    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags", "dbo");

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder
                .HasOne(c => c.Cat)
                .WithMany(d => d.Tags)
                .HasForeignKey(c => c.CatId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Created).HasDefaultValueSql("getdate()");
        }
    }
}

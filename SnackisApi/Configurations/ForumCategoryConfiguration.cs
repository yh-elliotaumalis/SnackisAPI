using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackisApi.Models;

namespace SnackisApi.Configurations;

public class ForumCategoryConfiguration : IEntityTypeConfiguration<ForumCategory>
{
    public void Configure(EntityTypeBuilder<ForumCategory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.HasMany(x => x.Threads)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);
    }
}

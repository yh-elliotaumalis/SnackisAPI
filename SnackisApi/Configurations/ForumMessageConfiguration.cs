using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackisApi.Models;

namespace SnackisApi.Configurations;

public class ForumMessageConfiguration : IEntityTypeConfiguration<ForumMessage>
{
    public void Configure(EntityTypeBuilder<ForumMessage> builder)
    {
        builder.HasOne(x => x.Sender)
            .WithMany()
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Recipient)
            .WithMany()
            .HasForeignKey(x => x.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

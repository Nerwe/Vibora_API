using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibora_API.Models.DB;

namespace Vibora_API.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .HasKey(p => p.ID);

            builder
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(5000);

            builder
                .Property(p => p.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("NOW()");

            builder
                .Property(p => p.LastUpdatedDate)
                .IsRequired()
                .HasDefaultValueSql("NOW()");

            builder
                .Property(p => p.Score)
                .IsRequired()
                .HasDefaultValue(0);

            builder
                .Property(p => p.IsHidden)
                .IsRequired()
                .HasDefaultValue(false);

            builder
                .Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserID);

            builder
                .HasOne(p => p.Thread)
                .WithMany(t => t.Posts)
                .HasForeignKey(p => p.ThreadID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

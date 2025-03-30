using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibora_API.Models.DB;

namespace Vibora_API.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasKey(c => c.ID);

            builder
                .Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .Property(c => c.Score)
                .IsRequired()
                .HasDefaultValue(0);

            builder
                .Property(c => c.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("NOW()");

            builder
                .Property(c => c.IsHidden)
                .IsRequired()
                .HasDefaultValue(false);

            builder
                .Property(c => c.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);


            builder
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserID);

            builder
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vibora_API.Configurations
{
    public class ThreadConfiguration : IEntityTypeConfiguration<Models.DB.Thread>
    {
        public void Configure(EntityTypeBuilder<Models.DB.Thread> builder)
        {
            builder
                .HasKey(t => t.ID);

            builder
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(t => t.IsHidden)
                .IsRequired()
                .HasDefaultValue(false);

            builder
                .Property(t => t.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder
                .HasOne(t => t.User)
                .WithMany(u => u.Threads)
                .HasForeignKey(t => t.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

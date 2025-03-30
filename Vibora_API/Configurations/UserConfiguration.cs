using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibora_API.Models.DB;

namespace Vibora_API.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.ID);

            builder
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(u => u.Password)
                .IsRequired();

            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(u => u.CreatedDate)
                .IsRequired()
                .HasColumnType("timestamp")
                .HasPrecision(0)
                .HasDefaultValueSql("NOW()");

            builder
               .Property(u => u.LastActiveDate)
               .IsRequired()
               .HasColumnType("timestamp")
               .HasPrecision(0)
               .HasDefaultValueSql("NOW()");

            builder
                .Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(false);

            builder
                .Property(u => u.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRole>(
                    l => l.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleID),
                    r => r.HasOne<User>().WithMany().HasForeignKey(e => e.UserID));
        }
    }
}

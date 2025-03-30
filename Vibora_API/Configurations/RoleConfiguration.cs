using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibora_API.Models.DB;
using Vibora_API.Models.Enums;

namespace Vibora_API.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .HasKey(r => r.ID);

            builder
                .Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasMany(r => r.Permissions)
                .WithMany(u => u.Roles)
                .UsingEntity<RolePermission>(
                    l => l.HasOne<Permission>().WithMany().HasForeignKey(e => e.PermissionId),
                    r => r.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId));

            var roles = Enum
                .GetValues<RoleEnum>()
                .Select(r => new Role
                {
                    ID = (int)r,
                    Title = r.ToString()
                });

            builder.HasData(roles);
        }
    }
}

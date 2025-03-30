using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibora_API.Models.DB;
using Vibora_API.Models.Enums;

namespace Vibora_API.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder
                .HasKey(p => p.ID);

            builder
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100);

            var permissions = Enum
                .GetValues<PermissionEnum>()
                .Select(p => new Permission
                {
                    ID = (int)p,
                    Title = p.ToString()
                });

            builder.HasData(permissions);
        }
    }
}

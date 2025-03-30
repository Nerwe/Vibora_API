using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibora_API.Auth;
using Vibora_API.Models.DB;
using Vibora_API.Models.Enums;

namespace Vibora_API.Configurations
{
    public class RolePermissionConfiguration(AuthorizationOptions authOptions) : IEntityTypeConfiguration<RolePermission>
    {
        private readonly AuthorizationOptions _authOptions = authOptions;

        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder
                .HasData(ParseRolePermissions());
        }

        private RolePermission[] ParseRolePermissions()
        {
            return _authOptions.RolePermissions
                .SelectMany(rp => rp.Permissions
                .Select(p => new RolePermission
                {
                    RoleId = (int)Enum.Parse<RoleEnum>(rp.Role),
                    PermissionId = (int)Enum.Parse<PermissionEnum>(p)
                }))
                .ToArray();
        }
    }
}

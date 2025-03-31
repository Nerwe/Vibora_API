using Microsoft.AspNetCore.Authorization;

namespace Vibora_API.Auth
{
    public class PermissionRequirement(string permission)
        : IAuthorizationRequirement
    {
        public string Permission { get; } = permission;
    }
}

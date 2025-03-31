using Microsoft.AspNetCore.Authorization;
using Vibora_API.Models.Enums;

namespace Vibora_API.Auth
{
    public class HasPermissionAtribute : AuthorizeAttribute
    {
        public HasPermissionAtribute(PermissionEnum permission)
            : base(policy: permission.ToString())
        {
        }
    }
}

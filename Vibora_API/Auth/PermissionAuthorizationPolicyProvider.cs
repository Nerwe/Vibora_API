﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Vibora_API.Auth
{
    public class PermissionAuthorizationPolicyProvider
         : DefaultAuthorizationPolicyProvider
    {
        public PermissionAuthorizationPolicyProvider(
            IOptions<Microsoft.AspNetCore.Authorization.AuthorizationOptions> options)
            : base(options)
        {
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);

            if (policy != null) return policy;

            return new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(policyName))
                .Build();
        }
    }
}

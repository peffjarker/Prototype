using Microsoft.AspNetCore.Authorization;
using Prototype.Models;
using System.Security.Claims;

namespace Prototype.Authorization
{
    /// <summary>
    /// Custom authorization handler that respects shadow roles.
    /// When a user is shadowing, only the shadow role's permissions are granted.
    /// </summary>
    public class ShadowAwareAuthorizationHandler : AuthorizationHandler<ShadowAwareRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ShadowAwareRequirement requirement)
        {
            var user = context.User;

            // User must be authenticated
            if (user?.Identity?.IsAuthenticated != true)
            {
                return Task.CompletedTask;
            }

            // Check if user is shadowing
            bool isShadowing = user.HasClaim(c => c.Type == "shadow_role");

            if (isShadowing)
            {
                // When shadowing, ONLY check the shadow role's group
                // The ShadowLoginService has already replaced group claims with the shadow role
                if (HasRequiredGroup(user, requirement.Policy))
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                // Not shadowing - check if user is admin OR has the required group
                bool isAdmin = user.HasClaim("groups", "34292b7e-74b4-4c44-b0c8-deb3859f44b2") ||
                               user.HasClaim(c => c.Type == "preferred_username" &&
                                   c.Value.Equals("jeffp@cornwelltools.com", StringComparison.OrdinalIgnoreCase));

                if (isAdmin)
                {
                    context.Succeed(requirement);
                }
                else if (HasRequiredGroup(user, requirement.Policy))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }

        private static bool HasRequiredGroup(ClaimsPrincipal user, string policy)
        {
            return policy switch
            {
                AppPolicies.CustomerService => user.HasClaim("groups", "84b76a76-52e2-4310-9767-3f40b7515ae6"),
                AppPolicies.Purchasing => user.HasClaim("groups", "5076c722-89d1-4afb-bfa0-e261a37728c2"),
                AppPolicies.TechCredit => user.HasClaim("groups", "2780fc34-4d65-4445-b4b5-1fe59780f5a6"),
                AppPolicies.Admin => user.HasClaim("groups", "34292b7e-74b4-4c44-b0c8-deb3859f44b2") ||
                                     user.HasClaim(c => c.Type == "preferred_username" &&
                                         c.Value.Equals("jeffp@cornwelltools.com", StringComparison.OrdinalIgnoreCase)),
                AppPolicies.AdminOrCustomerService => user.HasClaim("groups", "34292b7e-74b4-4c44-b0c8-deb3859f44b2") ||
                                                      user.HasClaim(c => c.Type == "preferred_username" &&
                                                          c.Value.Equals("jeffp@cornwelltools.com", StringComparison.OrdinalIgnoreCase)) ||
                                                      user.HasClaim("groups", "84b76a76-52e2-4310-9767-3f40b7515ae6"),
                _ => false
            };
        }
    }

    /// <summary>
    /// Authorization requirement for shadow-aware policy
    /// </summary>
    public class ShadowAwareRequirement : IAuthorizationRequirement
    {
        public string Policy { get; }

        public ShadowAwareRequirement(string policy)
        {
            Policy = policy;
        }
    }
}

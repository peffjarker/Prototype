using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Prototype.Components.Services
{
    public interface IShadowLoginService
    {
        string? ShadowRole { get; }
        void SetShadowRole(string? role);
        void ClearShadowRole();
        bool IsShadowing { get; }
        Task<ClaimsPrincipal> ApplyShadowRole(ClaimsPrincipal user);
    }

    public class ShadowLoginService : IShadowLoginService
    {
        private string? _shadowRole;

        public string? ShadowRole => _shadowRole;
        public bool IsShadowing => !string.IsNullOrEmpty(_shadowRole);

        public void SetShadowRole(string? role)
        {
            _shadowRole = role;
        }

        public void ClearShadowRole()
        {
            _shadowRole = null;
        }

        public Task<ClaimsPrincipal> ApplyShadowRole(ClaimsPrincipal user)
        {
            if (string.IsNullOrEmpty(_shadowRole))
            {
                return Task.FromResult(user);
            }

            // Get the shadow role's group ID
            var shadowGroupId = _shadowRole switch
            {
                "CustomerService" => "84b76a76-52e2-4310-9767-3f40b7515ae6",
                "Purchasing" => "5076c722-89d1-4afb-bfa0-e261a37728c2",
                "TechCredit" => "2780fc34-4d65-4445-b4b5-1fe59780f5a6",
                _ => null
            };

            if (shadowGroupId == null)
            {
                return Task.FromResult(user);
            }

            // Create a new identity with all claims except existing group claims
            var claims = user.Claims.Where(c => c.Type != "groups").ToList();

            // Add the shadow role group claim
            claims.Add(new Claim("groups", shadowGroupId));
            claims.Add(new Claim("shadow_role", _shadowRole));

            // Get the name and role claim types from the original identity if it's a ClaimsIdentity
            var originalIdentity = user.Identity as ClaimsIdentity;
            var nameClaimType = originalIdentity?.NameClaimType ?? ClaimTypes.Name;
            var roleClaimType = originalIdentity?.RoleClaimType ?? ClaimTypes.Role;

            var identity = new ClaimsIdentity(
                claims,
                user.Identity?.AuthenticationType,
                nameClaimType,
                roleClaimType);

            return Task.FromResult(new ClaimsPrincipal(identity));
        }
    }
}

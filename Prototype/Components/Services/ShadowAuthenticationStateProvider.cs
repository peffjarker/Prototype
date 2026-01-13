using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using System.Security.Claims;

namespace Prototype.Components.Services
{
    public class ShadowAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly IShadowLoginService _shadowLoginService;

        public ShadowAuthenticationStateProvider(
            ILoggerFactory loggerFactory,
            IShadowLoginService shadowLoginService)
            : base(loggerFactory)
        {
            _shadowLoginService = shadowLoginService;
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

        protected override async Task<bool> ValidateAuthenticationStateAsync(
            AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            // Always valid - shadow role can be changed at any time
            return true;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var state = await base.GetAuthenticationStateAsync();
            var user = state.User;

            if (_shadowLoginService.IsShadowing && user.Identity?.IsAuthenticated == true)
            {
                user = await _shadowLoginService.ApplyShadowRole(user);
            }

            return new AuthenticationState(user);
        }

        public void TriggerAuthenticationStateChange()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}

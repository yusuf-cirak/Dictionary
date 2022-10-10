using Blazored.LocalStorage;
using Dictionary.WebApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Dictionary.WebApp.Infrastructure.Auth
{
    public sealed class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationState _anonymous;

        public AuthStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var apiToken = await _localStorageService.GetToken();
            if (string.IsNullOrEmpty(apiToken)) return _anonymous;


            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.ReadJwtToken(apiToken);

            var cp = new ClaimsPrincipal(new ClaimsIdentity(securityToken.Claims,"jwtAuthType"));

            return new AuthenticationState(cp);

        }

        public void NotifyUserLogin(string userName, Guid userId)
        {
            var cp = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                new Claim(ClaimTypes.Name,userName)
            },"jwtAuthType"));

            var authState = Task.FromResult(new AuthenticationState(cp));

            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {

        }
    }
}

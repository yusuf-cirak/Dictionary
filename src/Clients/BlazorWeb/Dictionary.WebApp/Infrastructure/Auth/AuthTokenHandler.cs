using Blazored.LocalStorage;
using Dictionary.WebApp.Infrastructure.Extensions;

namespace Dictionary.WebApp.Infrastructure.Auth
{
    public sealed class AuthTokenHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;

        public AuthTokenHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorageService.GetToken();
            if (!string.IsNullOrEmpty(token) && request.Headers.Authorization is null)
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            return await base.SendAsync(request, cancellationToken);
        }
    }
}

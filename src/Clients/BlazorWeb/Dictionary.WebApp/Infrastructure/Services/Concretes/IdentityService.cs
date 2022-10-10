using Blazored.LocalStorage;
using Dictionary.Common.Features.Users.Commands.LoginUser;
using Dictionary.WebApp.Infrastructure.Auth;
using Dictionary.WebApp.Infrastructure.Extensions;
using Dictionary.WebApp.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Text.Json;

namespace Dictionary.WebApp.Infrastructure.Services.Concretes
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _LocalStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public IdentityService(HttpClient httpClient, ILocalStorageService syncLocalStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _LocalStorageService = syncLocalStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> IsLoggedIn() => !string.IsNullOrEmpty(await _LocalStorageService.GetToken());

        public async Task<string> GetUserName() => await _LocalStorageService.GetUserName();
        public async Task<Guid> GetUserId() => await _LocalStorageService.GetUserId();

        public async Task<string> GetUserToken() => await _LocalStorageService.GetToken();



        public async Task<bool> Login(LoginUserCommandRequest request)
        {
            string responseStr = string.Empty;

            var httpResponse = await _httpClient.PostAsJsonAsync("/api/auth/login", request);

            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new Exception(responseStr);
                }
                return false;
            }

            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<LoginUserCommandResponse>(responseStr);

            if (!string.IsNullOrEmpty(response.AccessToken))
            {
                await _LocalStorageService.SetUserId(response.Id);
                await _LocalStorageService.SetUserName(response.UserName);
                await _LocalStorageService.SetToken(response.AccessToken);

                ((AuthStateProvider)_authenticationStateProvider).NotifyUserLogin(response.UserName,response.Id);

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", response.AccessToken);

                return true;
            }

            return false;

        }


        public async Task Logout()
        {
            await _LocalStorageService.ClearAsync();

            ((AuthStateProvider)_authenticationStateProvider).NotifyUserLogout();

            _httpClient.DefaultRequestHeaders.Authorization = null;

        }
    }
}

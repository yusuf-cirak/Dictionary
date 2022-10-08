using Blazored.LocalStorage;
using Dictionary.Common.Features.Users.Commands.LoginUser;
using Dictionary.WebApp.Infrastructure.Extensions;
using Dictionary.WebApp.Infrastructure.Services.Abstractions;
using System.Net.Http.Json;
using System.Text.Json;

namespace Dictionary.WebApp.Infrastructure.Services.Concretes
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly ISyncLocalStorageService _syncLocalStorageService;

        public IdentityService(HttpClient httpClient, ISyncLocalStorageService syncLocalStorageService)
        {
            _httpClient = httpClient;
            _syncLocalStorageService = syncLocalStorageService;
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(_syncLocalStorageService.GetToken());

        public string GetUserName() => _syncLocalStorageService.GetUserName();
        public Guid GetUserId() => _syncLocalStorageService.GetUserId();

        public string GetUserToken() => _syncLocalStorageService.GetToken();



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
                _syncLocalStorageService.SetUserId(response.Id);
                _syncLocalStorageService.SetUserName(response.UserName);
                _syncLocalStorageService.SetToken(response.AccessToken);

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", response.AccessToken);

                return true;
            }

            return false;

        }


        public void Logout()
        {
            _syncLocalStorageService.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}

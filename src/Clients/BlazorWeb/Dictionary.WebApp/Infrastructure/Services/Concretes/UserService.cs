using Dictionary.Common.Features.Users.Commands.ChangePassword;
using Dictionary.Common.Features.Users.Commands.LoginUser;
using Dictionary.Common.Features.Users.Commands.Update;
using Dictionary.Common.Features.Users.Queries;
using Dictionary.WebApp.Infrastructure.Services.Abstractions;
using System.Net.Http.Json;
using System.Text.Json;

namespace Dictionary.WebApp.Infrastructure.Services.Concretes
{
    public sealed class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDetailViewModel> GetUserDetail(Guid userId)
        {
            var userDetail = await _httpClient.GetFromJsonAsync<UserDetailViewModel>($"api/users/{userId}");
            return userDetail!;
        }
        public async Task<UserDetailViewModel> GetUserDetail(string userName)
        {
            var userDetail = await _httpClient.GetFromJsonAsync<UserDetailViewModel>($"api/users/{userName}");
            return userDetail!;
        }

        public async Task<bool> UpdateUser(UpdateUserCommandRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync("api/users", request);

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
        {
            var request = new ChangeUserPasswordCommandRequest(Guid.Empty, oldPassword, newPassword);

            var response = await _httpClient.PutAsJsonAsync("api/auth/password", request);



            if (response != null && !response.IsSuccessStatusCode)
            {

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {

                   var responseStr = await response.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new Exception(responseStr);
                }
                return false;
            }

            return response.IsSuccessStatusCode;

        }
    }
}

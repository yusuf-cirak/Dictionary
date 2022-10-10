using Blazored.LocalStorage;

namespace Dictionary.WebApp.Infrastructure.Extensions
{
    public static class LocalStorageServiceExtensions
    {

        public static async Task SetToken(this ILocalStorageService localStorageService, string accessToken)
        {
            await localStorageService.SetItemAsStringAsync("accessToken", accessToken);
        }

        public static async Task<string> GetToken(this ILocalStorageService localStorageService)
        {
            return await localStorageService.GetItemAsync<string>("accessToken");
        }

        public static async Task SetUserName(this ILocalStorageService localStorageService, string userName)
        {
            await localStorageService.SetItemAsStringAsync("userName", userName);
        }

        public static async Task<string> GetUserName(this ILocalStorageService localStorageService)
        {
            return await localStorageService.GetItemAsStringAsync("userName");
        }

        public static async Task SetUserId(this ILocalStorageService localStorageService, Guid userId)
        {
            await localStorageService.SetItemAsStringAsync("userId", userId.ToString());
        }

        public static async Task<Guid> GetUserId(this ILocalStorageService localStorageService)
        {
            return await localStorageService.GetItemAsync<Guid>("userId");
        }

        public static async Task<bool> IsUserLoggedIn(this ILocalStorageService localStorageService)
        {
            return !string.IsNullOrEmpty(await GetToken(localStorageService));
        }
    }
}

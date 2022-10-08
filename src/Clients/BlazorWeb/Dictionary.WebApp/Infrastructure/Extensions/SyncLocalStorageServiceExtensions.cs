using Blazored.LocalStorage;

namespace Dictionary.WebApp.Infrastructure.Extensions
{
    public static class SyncLocalStorageServiceExtensions
    {

        public static void SetToken(this ISyncLocalStorageService syncLocalStorageService,string accessToken)
        {
            syncLocalStorageService.SetItemAsString("accessToken", accessToken);
        }

        public static string GetToken(this ISyncLocalStorageService syncLocalStorageService)
        {
            return syncLocalStorageService.GetItemAsString("accessToken");
        }

        public static void SetUserName(this ISyncLocalStorageService syncLocalStorageService, string userName)
        {
            syncLocalStorageService.SetItemAsString("userName", userName);
        }

        public static string GetUserName(this ISyncLocalStorageService syncLocalStorageService)
        {
            return syncLocalStorageService.GetItemAsString("userName");
        }

        public static void SetUserId(this ISyncLocalStorageService syncLocalStorageService, Guid userId)
        {
            syncLocalStorageService.SetItemAsString("userId", userId.ToString());
        }

        public static Guid GetUserId(this ISyncLocalStorageService syncLocalStorageService)
        {
            return syncLocalStorageService.GetItem<Guid>("userId");
        }

        public static bool IsUserLoggedIn(this ISyncLocalStorageService syncLocalStorageService)
        {
            return !string.IsNullOrEmpty(GetToken(syncLocalStorageService));
        }
    }
}

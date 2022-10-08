using Dictionary.Common.Features.Users.Commands.LoginUser;

namespace Dictionary.WebApp.Infrastructure.Services.Abstractions
{
    public interface IIdentityService
    {
        bool IsLoggedIn { get; }

        Guid GetUserId();
        string GetUserName();
        string GetUserToken();
        Task<bool> Login(LoginUserCommandRequest request);
        void Logout();
    }
}
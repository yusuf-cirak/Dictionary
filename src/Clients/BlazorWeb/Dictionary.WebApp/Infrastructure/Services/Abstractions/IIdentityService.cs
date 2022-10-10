using Dictionary.Common.Features.Users.Commands.LoginUser;

namespace Dictionary.WebApp.Infrastructure.Services.Abstractions
{
    public interface IIdentityService
    {
        Task<Guid> GetUserId();
        Task<string> GetUserName();
        Task<string> GetUserToken();
        Task<bool> IsLoggedIn();
        Task<bool> Login(LoginUserCommandRequest request);
        Task Logout();
    }
}
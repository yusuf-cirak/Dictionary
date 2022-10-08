using Dictionary.Common.Features.Users.Commands.Update;
using Dictionary.Common.Features.Users.Queries;

namespace Dictionary.WebApp.Infrastructure.Services.Abstractions
{
    public interface IUserService
    {
        Task<bool> ChangeUserPassword(string oldPassword, string newPassword);
        Task<UserDetailViewModel> GetUserDetail(Guid userId);
        Task<UserDetailViewModel> GetUserDetail(string userName);
        Task<bool> UpdateUser(UpdateUserCommandRequest request);
    }
}
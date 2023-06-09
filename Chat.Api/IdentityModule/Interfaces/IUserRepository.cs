using Chat.Api.IdentityModule.Models;

namespace Chat.Api.IdentityModule.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsUserExistAsync(UserModel userModel);
        Task<bool> CreateUserAsync(UserModel userModel);
        Task<UserModel?> GetUserByEmailAsync(string email);
        Task<List<UserModel>> GetUsersByUserIdOrEmailAsync(string userId, string email);
    }
}
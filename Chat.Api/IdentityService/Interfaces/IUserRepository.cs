using Chat.Api.IdentityService.Models;

namespace Chat.Api.IdentityService.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsUserExistAsync(UserModel userModel);
        Task<bool> CreateUserAsync(UserModel userModel);
        Task<UserModel?> GetUserAsync(string email, string password);
        Task<List<UserModel>> GetUsersByUserIdOrEmailAsync(string userId, string email);
    }
}
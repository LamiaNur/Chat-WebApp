using Chat.Api.IdentityModule.Models;

namespace Chat.Api.IdentityModule.Interfaces
{
    public interface IAccessRepository
    {
        Task<bool> SaveAccessModelAsync(AccessModel accessModel);
        Task<bool> DeleteAllTokenByAppId(string appId);
        Task<bool> DeleteAllTokensByUserId(string userId);
        Task<AccessModel?> GetAccessModelByIdAsync(string id);
    }
}
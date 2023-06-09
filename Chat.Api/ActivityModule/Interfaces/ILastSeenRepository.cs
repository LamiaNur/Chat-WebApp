using Chat.Api.ActivityModule.Models;

namespace Chat.Api.ActivityModule.Interfaces
{
    public interface ILastSeenRepository
    {
        Task<bool> SaveLastSeenModelAsync(LastSeenModel lastSeenModel);
        Task<LastSeenModel?> GetLastSeenModelByUserIdAsync(string userId);
        Task<List<LastSeenModel>> GetLastSeenModelsByUserIdsAsync(List<string> userIds);
    }
}
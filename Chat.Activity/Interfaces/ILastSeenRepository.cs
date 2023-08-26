using Chat.Activity.Api.Models;

namespace Chat.Activity.Api.Interfaces
{
    public interface ILastSeenRepository
    {
        Task<bool> SaveLastSeenModelAsync(LastSeenModel lastSeenModel);
        Task<LastSeenModel?> GetLastSeenModelByUserIdAsync(string userId);
        Task<List<LastSeenModel>> GetLastSeenModelsByUserIdsAsync(List<string> userIds);
    }
}
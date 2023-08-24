using Chat.Activity.Models;

namespace Chat.Activity.Interfaces
{
    public interface ILastSeenRepository
    {
        Task<bool> SaveLastSeenModelAsync(LastSeenModel lastSeenModel);
        Task<LastSeenModel?> GetLastSeenModelByUserIdAsync(string userId);
        Task<List<LastSeenModel>> GetLastSeenModelsByUserIdsAsync(List<string> userIds);
    }
}
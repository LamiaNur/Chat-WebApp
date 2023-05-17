using Chat.Api.ActivityService.Models;

namespace Chat.Api.ActivityService.Interfaces
{
    public interface ILastSeenRepository
    {
        Task<bool> SaveLastSeenModelAsync(LastSeenModel lastSeenModel);
        Task<LastSeenModel?> GetLastSeenModelByUserIdAsync(string userId);
    }
}
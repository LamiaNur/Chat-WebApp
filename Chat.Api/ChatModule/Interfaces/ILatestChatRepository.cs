using Chat.Api.ChatModule.Models;

namespace Chat.Api.ChatModule.Interfaces
{
    public interface ILatestChatRepository
    {
        Task<bool> SaveLatestChatModelAsync(LatestChatModel latestChatModel);
        Task<LatestChatModel?> GetLatestChatAsync(string userId, string sendTo);
        Task<List<LatestChatModel>> GetLatestChatModelsAsync(string userId, int offset, int limit);
    }
}
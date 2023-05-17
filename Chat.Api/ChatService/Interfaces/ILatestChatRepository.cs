using Chat.Api.ChatService.Models;

namespace Chat.Api.ChatService.Interfaces
{
    public interface ILatestChatRepository
    {
        Task<bool> SaveLatestChatModelAsync(LatestChatModel latestChatModel);
        Task<LatestChatModel?> GetLatestChatAsync(string userId, string sendTo);
        Task<List<LatestChatModel>> GetLatestChatModelsAsync(string userId, int offset, int limit);
    }
}
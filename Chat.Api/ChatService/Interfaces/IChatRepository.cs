using Chat.Api.ChatService.Models;

namespace Chat.Api.ChatService.Interfaces
{
    public interface IChatRepository
    {
        Task<bool> SaveChatModelAsync(ChatModel chatModel);
        Task<List<ChatModel>> GetChatModelsAsync(string userId, string sendTo, int offset, int limit);
    }
}
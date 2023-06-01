using Chat.Api.ChatModule.Models;

namespace Chat.Api.ChatModule.Interfaces
{
    public interface IChatRepository
    {
        Task<bool> SaveChatModelAsync(ChatModel chatModel);
        Task<List<ChatModel>> GetChatModelsAsync(string userId, string sendTo, int offset, int limit);
    }
}
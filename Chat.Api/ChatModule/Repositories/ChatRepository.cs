using System.Composition;
using Chat.Api.ChatModule.Interfaces;
using Chat.Api.ChatModule.Models;
using Chat.Api.CoreModule.Database.Interfaces;
using Chat.Api.CoreModule.Database.Models;
using Chat.Api.CoreModule.Services;
using MongoDB.Driver;

namespace Chat.Api.ChatModule.Repositories
{
    [Export(typeof(IChatRepository))]
    [Shared]
    public class ChatRepository : IChatRepository
    {
        private readonly IMongoDbContext _dbContext;
        private readonly DatabaseInfo _databaseInfo;

        public ChatRepository()
        {
            _dbContext = DIService.Instance.GetService<IMongoDbContext>();
            _databaseInfo = DIService.Instance.GetConfiguration().GetSection("DatabaseInfo").Get<DatabaseInfo>();
        }

        public async Task<bool> SaveChatModelAsync(ChatModel chatModel)
        {
            return await _dbContext.SaveItemAsync(_databaseInfo, chatModel);
        }

        public async Task<List<ChatModel>> GetChatModelsAsync(string userId, string sendTo, int offset, int limit)
        {
            var userIdFilter = Builders<ChatModel>.Filter.Eq("UserId", userId);
            var sendToFilter = Builders<ChatModel>.Filter.Eq("SendTo", sendTo);
            var andFilter = Builders<ChatModel>.Filter.And(userIdFilter, sendToFilter);
            var alterUserIdFilter = Builders<ChatModel>.Filter.Eq("UserId", sendTo);
            var alterSendToFilter = Builders<ChatModel>.Filter.Eq("SendTo", userId);
            var alterAndFilter = Builders<ChatModel>.Filter.And(alterUserIdFilter, alterSendToFilter);
            var orFilter = Builders<ChatModel>.Filter.Or(andFilter, alterAndFilter);
            return await _dbContext.GetItemsByFilterDefinitionAsync<ChatModel>(_databaseInfo, orFilter, offset, limit);
        }
    }
}
using Chat.Api.Core.Database.Interfaces;
using Chat.Api.Core.Services;
using Chat.Api.Core.Database.Models;
using Chat.Api.ChatService.Models;
using MongoDB.Driver;
using Chat.Api.ChatService.Interfaces;
using System.Composition;

namespace Chat.Api.ChatService.Repositories
{
    [Export(typeof(ILatestChatRepository))]
    [Shared]
    public class LatestChatRepository : ILatestChatRepository
    {
        private readonly IMongoDbContext _dbContext;
        private readonly DatabaseInfo _databaseInfo;

        public LatestChatRepository()
        {
            _dbContext = DIService.Instance.GetService<IMongoDbContext>();
            _databaseInfo = DIService.Instance.GetService<IConfiguration>().GetSection("DatabaseInfo").Get<DatabaseInfo>();
        }

        public async Task<bool> SaveLatestChatModelAsync(LatestChatModel latestChatModel)
        {
            return await _dbContext.SaveItemAsync(_databaseInfo, latestChatModel);
        }

        public async Task<LatestChatModel?> GetLatestChatAsync(string userId, string sendTo)
        {
            var userIdFilter = Builders<LatestChatModel>.Filter.Eq("UserId", userId);
            var sendToFilter = Builders<LatestChatModel>.Filter.Eq("SendTo", sendTo);
            var andFilter = Builders<LatestChatModel>.Filter.And(userIdFilter, sendToFilter);
            var alterUserIdFilter = Builders<LatestChatModel>.Filter.Eq("UserId", sendTo);
            var alterSendToFilter = Builders<LatestChatModel>.Filter.Eq("SendTo", userId);
            var alterAndFilter = Builders<LatestChatModel>.Filter.And(alterUserIdFilter, alterSendToFilter);
            var orFilter = Builders<LatestChatModel>.Filter.Or(andFilter, alterAndFilter);
            
            return await _dbContext.GetItemByFilterDefinitionAsync<LatestChatModel>(_databaseInfo, orFilter);
        }

        public async Task<List<LatestChatModel>> GetLatestChatModelsAsync(string userId, int offset, int limit)
        {
            var userIdFilter = Builders<LatestChatModel>.Filter.Eq("UserId", userId);
            var sendToFilter = Builders<LatestChatModel>.Filter.Eq("SendTo", userId);
            var orFilter = Builders<LatestChatModel>.Filter.Or(userIdFilter, sendToFilter);
            return await _dbContext.GetItemsByFilterDefinitionAsync<LatestChatModel>(_databaseInfo, orFilter, offset, limit);
        }
    }
}
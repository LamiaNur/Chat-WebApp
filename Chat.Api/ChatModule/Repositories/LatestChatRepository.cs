using Chat.Api.ChatModule.Interfaces;
using Chat.Api.ChatModule.Models;
using Chat.Framework.Attributes;
using Chat.Framework.Database.Interfaces;
using Chat.Framework.Database.Models;
using MongoDB.Driver;

namespace Chat.Api.ChatModule.Repositories
{
    [ServiceRegister(typeof(ILatestChatRepository), ServiceLifetime.Singleton)]
    public class LatestChatRepository : ILatestChatRepository
    {
        private readonly IMongoDbContext _dbContext;
        private readonly DatabaseInfo _databaseInfo;

        public LatestChatRepository(IMongoDbContext mongoDbContext, IConfiguration configuration)
        {
            _dbContext = mongoDbContext;
            _databaseInfo = configuration.GetSection("DatabaseInfo").Get<DatabaseInfo>();
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
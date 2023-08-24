using Chat.Activity.Interfaces;
using Chat.Activity.Models;
using Chat.Framework.Attributes;
using Chat.Framework.Database.Interfaces;
using Chat.Framework.Database.Models;
using MongoDB.Driver;

namespace Chat.Activity.Repositories
{
    [ServiceRegister(typeof(ILastSeenRepository), ServiceLifetime.Singleton)]
    public class LastSeenRepository : ILastSeenRepository
    {
        private readonly DatabaseInfo _databaseInfo;
        private readonly IMongoDbContext _dbContext;

        public LastSeenRepository(IMongoDbContext mongoDbContext, IConfiguration configuration)
        {
            _databaseInfo = configuration.GetSection("DatabaseInfo").Get<DatabaseInfo>();
            _dbContext = mongoDbContext;
        }

        public async Task<bool> SaveLastSeenModelAsync(LastSeenModel lastSeenModel)
        {
            return await _dbContext.SaveItemAsync(_databaseInfo, lastSeenModel);
        }

        public async Task<LastSeenModel?> GetLastSeenModelByUserIdAsync(string userId)
        {
            var userIdFilter = Builders<LastSeenModel>.Filter.Eq("UserId", userId);
            return await _dbContext.GetItemByFilterDefinitionAsync(_databaseInfo, userIdFilter);
        }

        public async Task<List<LastSeenModel>> GetLastSeenModelsByUserIdsAsync(List<string> userIds)
        {
            var userIdsFilter = Builders<LastSeenModel>.Filter.In("UserId", userIds);
            return await _dbContext.GetItemsByFilterDefinitionAsync(_databaseInfo, userIdsFilter);
        }
    }
}
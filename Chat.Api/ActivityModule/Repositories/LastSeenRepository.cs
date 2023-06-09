using System.Composition;
using Chat.Api.ActivityModule.Interfaces;
using Chat.Api.ActivityModule.Models;
using Chat.Api.CoreModule.Database.Interfaces;
using Chat.Api.CoreModule.Database.Models;
using Chat.Api.CoreModule.Services;
using MongoDB.Driver;

namespace Chat.Api.ActivityModule.Repositories
{
    [Export(typeof(ILastSeenRepository))]
    [Shared]
    public class LastSeenRepository : ILastSeenRepository
    {
        private readonly DatabaseInfo _databaseInfo; 
        private readonly IMongoDbContext _dbContext;
        
        public LastSeenRepository()
        {
            _databaseInfo = DIService.Instance.GetConfiguration().GetSection("DatabaseInfo").Get<DatabaseInfo>();
            _dbContext = DIService.Instance.GetService<IMongoDbContext>();
        }   

        public async Task<bool> SaveLastSeenModelAsync(LastSeenModel lastSeenModel)
        {
            return await _dbContext.SaveItemAsync(_databaseInfo, lastSeenModel);
        }

        public async Task<LastSeenModel?> GetLastSeenModelByUserIdAsync(string userId)
        {
            var userIdFilter = Builders<LastSeenModel>.Filter.Eq("UserId", userId);
            return await _dbContext.GetItemByFilterDefinitionAsync<LastSeenModel>(_databaseInfo, userIdFilter);
        }

        public async Task<List<LastSeenModel>> GetLastSeenModelsByUserIdsAsync(List<string> userIds)
        {
            var userIdsFilter = Builders<LastSeenModel>.Filter.In("UserId", userIds);
            return await _dbContext.GetItemsByFilterDefinitionAsync<LastSeenModel>(_databaseInfo, userIdsFilter);
        }
    }
}
using Chat.Api.Core.Database.Interfaces;
using Chat.Api.Core.Database.Models;
using Chat.Api.ActivityService.Interfaces;
using MongoDB.Driver;
using Chat.Api.ActivityService.Models;
using System.Composition;
using Chat.Api.Core.Services;

namespace Chat.Api.ActivityService.Repositories
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
    }
}
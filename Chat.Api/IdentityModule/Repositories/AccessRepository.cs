using Chat.Api.IdentityModule.Interfaces;
using Chat.Api.IdentityModule.Models;
using Chat.Framework.Attributes;
using Chat.Framework.Database.Interfaces;
using Chat.Framework.Database.Models;
using MongoDB.Driver;

namespace Chat.Api.IdentityModule.Repositories
{
    [ServiceRegister(typeof(IAccessRepository), ServiceLifetime.Singleton)]
    public class AccessRepository : IAccessRepository
    {
        private readonly DatabaseInfo _databaseInfo;
        private readonly IMongoDbContext _dbContext;
        
        public AccessRepository(IMongoDbContext mongoDbContext, IConfiguration configuration)
        {
            _databaseInfo = configuration.GetSection("DatabaseInfo").Get<DatabaseInfo>();
            _dbContext = mongoDbContext;
        }

        public async Task<bool> SaveAccessModelAsync(AccessModel accessModel)
        {
            return await _dbContext.SaveItemAsync(_databaseInfo, accessModel);
        }

        public async Task<bool> DeleteAllTokenByAppId(string appId)
        {
            var filter = Builders<AccessModel>.Filter.Eq("AppId", appId);
            return await _dbContext.DeleteItemsByFilterDefinitionAsync(_databaseInfo, filter);
        }

        public async Task<bool> DeleteAllTokensByUserId(string userId)
        {
            var filter = Builders<AccessModel>.Filter.Eq("UserId", userId);
            return await _dbContext.DeleteItemsByFilterDefinitionAsync<AccessModel>(_databaseInfo, filter);
        }

        public async Task<AccessModel?> GetAccessModelByIdAsync(string id)
        {
            return await _dbContext.GetItemByIdAsync<AccessModel>(_databaseInfo, id);
        }
    }
}
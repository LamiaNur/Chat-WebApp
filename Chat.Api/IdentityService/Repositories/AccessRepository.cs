using Chat.Api.Core.Database.Interfaces;
using Chat.Api.Core.Database.Models;
using Chat.Api.IdentityService.Models;
using Chat.Api.IdentityService.Interfaces;
using MongoDB.Driver;
using Chat.Api.Core.Services;
using System.Composition;

namespace Chat.Api.IdentityService.Repositories
{
    [Export(typeof(IAccessRepository))]
    [Export("AccessRepository", typeof(IAccessRepository))]
    [Shared]
    public class AccessRepository : IAccessRepository
    {
        private readonly DatabaseInfo _databaseInfo;
        private readonly IMongoDbContext _dbContext;
        
        public AccessRepository()
        {
            _databaseInfo = DIService.Instance.GetService<IConfiguration>().GetSection("DatabaseInfo").Get<DatabaseInfo>();
            _dbContext = DIService.Instance.GetService<IMongoDbContext>();
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
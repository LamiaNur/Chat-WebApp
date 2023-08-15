using System.Composition;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Api.IdentityModule.Models;
using Chat.Framework.Database.Interfaces;
using Chat.Framework.Database.Models;
using Chat.Framework.Services;
using MongoDB.Driver;

namespace Chat.Api.IdentityModule.Repositories
{
    [Export(typeof(IAccessRepository))]
    [Shared]
    public class AccessRepository : IAccessRepository
    {
        private readonly DatabaseInfo _databaseInfo;
        private readonly IMongoDbContext _dbContext;
        
        public AccessRepository()
        {
            _databaseInfo = DIService.Instance.GetConfiguration().GetSection("DatabaseInfo").Get<DatabaseInfo>();
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
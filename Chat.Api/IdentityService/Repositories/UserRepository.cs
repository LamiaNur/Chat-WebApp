using System.Composition;
using Chat.Api.Core.Database.Interfaces;
using Chat.Api.Core.Database.Models;
using Chat.Api.Core.Services;
using Chat.Api.IdentityService.Interfaces;
using Chat.Api.IdentityService.Models;
using MongoDB.Driver;

namespace Chat.Api.IdentityService.Repositories
{
    [Export(typeof(IUserRepository))]
    [Shared]
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseInfo _databaseInfo;
        private readonly IMongoDbContext _dbContext;
        
        public UserRepository()
        {
            _databaseInfo = DIService.Instance.GetService<IConfiguration>().GetSection("DatabaseInfo").Get<DatabaseInfo>();
            _dbContext = DIService.Instance.GetService<IMongoDbContext>();
        }

        public async Task<bool> IsUserExistAsync(UserModel userModel)
        {
            var idFilter = Builders<UserModel>.Filter.Eq("Id", userModel.Id);
            var emailFilter = Builders<UserModel>.Filter.Eq("Email", userModel.Email);
            var filter = Builders<UserModel>.Filter.Or(idFilter, emailFilter);
            var userItem = await _dbContext.GetItemByFilterDefinitionAsync<UserModel>(_databaseInfo, filter);
            return userItem != null;
        }

        public async Task<UserModel?> GetUserAsync(string email, string password)
        {
            var emailFilter = Builders<UserModel>.Filter.Eq("Email", email);
            var passwordFilter = Builders<UserModel>.Filter.Eq("Password", password);
            var filter = Builders<UserModel>.Filter.And(emailFilter, passwordFilter);
            var userModel = await _dbContext.GetItemByFilterDefinitionAsync<UserModel>(_databaseInfo, filter);
            return userModel;
        }

        public async Task<bool> CreateUserAsync(UserModel userModel)
        {
            return await _dbContext.SaveItemAsync(_databaseInfo, userModel);
        }

        public async Task<List<UserModel>> GetUsersByUserIdOrEmailAsync(string userId, string email) 
        {
            var idFilter = Builders<UserModel>.Filter.Eq("Id", userId);
            var emailFilter = Builders<UserModel>.Filter.Eq("Email", email);
            var filter = Builders<UserModel>.Filter.Or(idFilter, emailFilter);
            return await _dbContext.GetItemsByFilterDefinitionAsync<UserModel>(_databaseInfo, filter);
        }
    }
}
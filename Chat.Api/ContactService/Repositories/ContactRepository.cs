using System.Composition;
using Chat.Api.ContactService.Interfaces;
using Chat.Api.ContactService.Models;
using Chat.Api.Core.Database.Interfaces;
using Chat.Api.Core.Database.Models;
using Chat.Api.Core.Services;
using MongoDB.Driver;

namespace Chat.Api.ContactService.Repositories
{
    [Export(typeof(IContactRepository))]
    [Shared]
    public class ContactRepository : IContactRepository
    {
        private readonly DatabaseInfo _databaseInfo;
        private readonly IMongoDbContext _dbContext;
        
        public ContactRepository()
        {
            _databaseInfo = DIService.Instance.GetConfiguration().GetSection("DatabaseInfo").Get<DatabaseInfo>();
            _dbContext = DIService.Instance.GetService<IMongoDbContext>();
        }

        public async Task<bool> SaveContactAsync(Contact contact)
        {
            return await _dbContext.SaveItemAsync(_databaseInfo, contact);
        }

        public async Task<List<Contact>> GetUserContactsAsync(string userId)
        {
            var userAFilter = Builders<Contact>.Filter.Eq("UserA.Id", userId);
            var userBFilter = Builders<Contact>.Filter.Eq("UserB.Id", userId);
            var userFilter = Builders<Contact>.Filter.Or(userAFilter, userBFilter);
            var pendingFilter = Builders<Contact>.Filter.Eq("IsPending", false);
            var filter = Builders<Contact>.Filter.And(userFilter, pendingFilter);
            return await _dbContext.GetItemsByFilterDefinitionAsync<Contact>(_databaseInfo, filter);
        }

        public async Task<List<Contact>> GetContactRequestsAsync(string userId)
        {   
            var userBFilter = Builders<Contact>.Filter.Eq("UserB.Id", userId);
            var requestUserIdFilter = Builders<Contact>.Filter.Ne("RequestUserId", userId);
            var pendingFilter = Builders<Contact>.Filter.Eq("IsPending", true);
            var filter = Builders<Contact>.Filter.And(userBFilter,requestUserIdFilter,pendingFilter);
            return await _dbContext.GetItemsByFilterDefinitionAsync<Contact>(_databaseInfo, filter);
        }

        public async Task<List<Contact>> GetPendingContactsAsync(string userId)
        {   
            var userAFilter = Builders<Contact>.Filter.Eq("UserA.Id", userId);
            var requestUserIdFilter = Builders<Contact>.Filter.Eq("RequestUserId", userId);
            var pendingFilter = Builders<Contact>.Filter.Eq("IsPending", true);
            var filter = Builders<Contact>.Filter.And(userAFilter,requestUserIdFilter,pendingFilter);
            return await _dbContext.GetItemsByFilterDefinitionAsync<Contact>(_databaseInfo, filter);
        }

        public async Task<Contact?> GetContactByIdAsync(string contactId)
        {
            return await _dbContext.GetItemByIdAsync<Contact>(_databaseInfo, contactId);
        }

        public async Task<bool> DeleteContactById(string contactId)
        {
            return await _dbContext.DeleteItemByIdAsync<Contact>(_databaseInfo, contactId);
        }
    }
}
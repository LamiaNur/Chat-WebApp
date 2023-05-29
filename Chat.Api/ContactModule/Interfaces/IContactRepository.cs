using Chat.Api.ContactModule.Models;

namespace Chat.Api.ContactModule.Interfaces
{
    public interface IContactRepository 
    {
        Task<bool> SaveContactAsync(Contact contact);
        Task<List<Contact>> GetUserContactsAsync(string userId);
        Task<List<Contact>> GetContactRequestsAsync(string userId);
        Task<List<Contact>> GetPendingContactsAsync(string userId);
        Task<Contact?> GetContactByIdAsync(string contactId);
        Task<bool> DeleteContactById(string contactId);
    }
}
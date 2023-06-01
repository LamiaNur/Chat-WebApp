using Chat.Api.CoreModule.Database.Interfaces;
using Chat.Api.IdentityModule.Models;

namespace Chat.Api.ContactModule.Models
{
    public class Contact : IRepositoryItem
    {
        public string Id { get; set; } = string.Empty;
        public UserProfile UserA {get; set;}
        public UserProfile UserB {get; set;}
        public bool IsPending {get; set;}
        public string RequestUserId {get; set;} = string.Empty;
        public DateTime CreatedAt {get; set;}
    }
}
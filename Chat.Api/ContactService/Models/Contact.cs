using Chat.Api.Core.Database.Interfaces;
using Chat.Api.IdentityService.Models;

namespace Chat.Api.ContactService.Models
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
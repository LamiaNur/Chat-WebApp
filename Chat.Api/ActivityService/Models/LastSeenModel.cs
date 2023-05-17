using Chat.Api.Core.Database.Interfaces;

namespace Chat.Api.ActivityService.Models
{
    public class LastSeenModel : IRepositoryItem
    {
        public string Id { get; set; } = string.Empty;
        public string UserId {get; set;} = string.Empty;
        public DateTime LastSeenAt {get; set;}
    }
}
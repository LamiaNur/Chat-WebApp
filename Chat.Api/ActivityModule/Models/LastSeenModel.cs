using Chat.Api.CoreModule.Database.Interfaces;

namespace Chat.Api.ActivityModule.Models
{
    public class LastSeenModel : IRepositoryItem
    {
        public string Id { get; set; } = string.Empty;
        public string UserId {get; set;} = string.Empty;
        public DateTime LastSeenAt {get; set;}
    }
}
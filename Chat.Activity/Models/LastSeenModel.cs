using Chat.Framework.Database.Interfaces;

namespace Chat.Activity.Models
{
    public class LastSeenModel : IRepositoryItem
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime LastSeenAt { get; set; }
    }
}
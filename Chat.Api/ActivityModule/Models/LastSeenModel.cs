using Chat.Api.CoreModule.Database.Interfaces;
using Chat.Api.CoreModule.Helpers;

namespace Chat.Api.ActivityModule.Models
{
    public class LastSeenModel : IRepositoryItem
    {
        public string Id { get; set; } = string.Empty;
        public string UserId {get; set;} = string.Empty;
        public DateTime LastSeenAt {get; set;}
        
        public LastSeenDto ToLastSeenDto()
        {
            return new LastSeenDto
            {
                Id = Id,
                UserId = UserId,
                LastSeenAt = LastSeenAt,
                Status = DisplayTimeHelper.GetChatListDisplayTime(LastSeenAt, "Active Now"),
                IsActive = DisplayTimeHelper.IsActive(LastSeenAt)
            };
        }
    }
}
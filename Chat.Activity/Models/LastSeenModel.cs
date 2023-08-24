using Chat.Framework.Database.Interfaces;
using Chat.Shared.Domain.Helpers;

namespace Chat.Activity.Models
{
    public class LastSeenModel : IRepositoryItem
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime LastSeenAt { get; set; }

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
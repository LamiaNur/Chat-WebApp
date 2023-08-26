using Chat.Activity.Api.Models;
using Chat.Shared.Domain.Helpers;

namespace Chat.Activity.Api.Extensions;

public static class LastSeenModelExtension
{
    public static LastSeenDto ToLastSeenDto(this LastSeenModel lastSeenModel)
    {
        return new LastSeenDto
        {
            Id = lastSeenModel.Id,
            UserId = lastSeenModel.UserId,
            LastSeenAt = lastSeenModel.LastSeenAt,
            Status = DisplayTimeHelper.GetChatListDisplayTime(lastSeenModel.LastSeenAt, "Active Now"),
            IsActive = DisplayTimeHelper.IsActive(lastSeenModel.LastSeenAt)
        };
    }
}
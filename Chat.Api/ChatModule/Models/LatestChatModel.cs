using Chat.Api.CoreModule.Helpers;

namespace Chat.Api.ChatModule.Models
{
    public class LatestChatModel : ChatModel
    {
        public int Occurrance {get; set;}
        public LatestChatDto ToLatestChatDto(string currentUserId)
        {
            return new LatestChatDto
            {
                Id = Id,
                Message = Message,
                Status = Status,
                DurationDisplayTime = DisplayTimeHelper.GetChatListDisplayTime(SentAt),
                UserId = UserId == currentUserId? SendTo : UserId,
                IsReceiver = UserId != currentUserId,
                Occurrance = UserId != currentUserId? Occurrance : 0
            };
        }
    }
}
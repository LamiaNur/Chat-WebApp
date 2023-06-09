using Chat.Api.CoreModule.Database.Interfaces;
using Chat.Api.CoreModule.Helpers;

namespace Chat.Api.ChatModule.Models
{
    public class ChatModel : IRepositoryItem
    {
        public string Id {get; set;} = string.Empty;
        public string UserId {get; set;} = string.Empty;
        public string SendTo {get; set;} = string.Empty;
        public string Message {get; set;} = string.Empty;
        public DateTime SentAt {get; set;}
        public string Status {get; set;} = string.Empty;

        public LatestChatModel ToLatestChatModel()
        {
            return new LatestChatModel
            {
                Id = Id,
                UserId = UserId,
                SendTo = SendTo,
                Message = Message,
                SentAt = SentAt,
                Status = Status
            };
        }
        public ChatDto ToChatDto()
        {
            return new ChatDto
            {
                Id = Id,
                UserId = UserId,
                Message = Message,
                Status = Status,
                SentAt = SentAt
            };
        }
    }
}
using Chat.Api.Core.Database.Interfaces;

namespace Chat.Api.ChatService.Models
{
    public class ChatModel : IRepositoryItem
    {
        public string Id {get; set;} = string.Empty;
        public string UserId {get; set;} = string.Empty;
        public string SendTo {get; set;} = string.Empty;
        public string Message {get; set;} = string.Empty;
        public DateTime SentAt {get; set;}
        public string Status {get; set;} = string.Empty;
    }
}
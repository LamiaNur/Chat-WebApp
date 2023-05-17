using Chat.Api.ChatService.Interfaces;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;

namespace Chat.Api.ChatService.Queries
{
    public class ChatListQuery : AQuery
    {
        public string UserId {get; set;} = string.Empty;

        public override void ValidateQuery()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new Exception("UserId not set");
            }
        }
    }
}
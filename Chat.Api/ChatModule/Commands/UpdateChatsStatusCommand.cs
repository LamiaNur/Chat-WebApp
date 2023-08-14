using Chat.Api.ChatModule.Models;
using Chat.Api.CoreModule.CQRS;

namespace Chat.Api.ChatModule.Commands
{
    public class UpdateChatsStatusCommand : ACommand
    {
        public string UserId {get; set;} = string.Empty;
        public string OpenedChatUserId {get; set;} = string.Empty;
        public override void ValidateCommand()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new Exception("UserId not set");
            }
            if (string.IsNullOrEmpty(OpenedChatUserId))
            {
                throw new Exception("OpenedChatUserId not set");
            }
        }
    }
}
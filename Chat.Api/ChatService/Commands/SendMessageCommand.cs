using Chat.Api.Core.Models;
using Chat.Api.ChatService.Models;

namespace Chat.Api.ChatService.Commands
{
    public class SendMessageCommand : ACommand
    {
        public ChatModel ChatModel {get; set;}
        
        public override void ValidateCommand()
        {
            if (ChatModel == null) 
            {
                throw new Exception("ChatModel not set");
            }
        }
    }
}
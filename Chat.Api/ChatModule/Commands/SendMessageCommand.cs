using Chat.Api.ChatModule.Models;
using Chat.Framework.CQRS;

namespace Chat.Api.ChatModule.Commands
{
    public class SendMessageCommand : ACommand
    {
        public ChatModel? ChatModel {get; set;}
        
        public override void ValidateCommand()
        {
            if (ChatModel == null) 
            {
                throw new Exception("ChatModel not set");
            }
        }
    }
}
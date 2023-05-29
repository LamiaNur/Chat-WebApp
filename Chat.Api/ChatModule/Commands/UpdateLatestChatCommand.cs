using Chat.Api.ChatModule.Models;
using Chat.Api.CoreModule.Models;

namespace Chat.Api.ChatModule.Commands
{
    public class UpdateLatestChatCommand : ACommand
    {
        public LatestChatModel LatestChatModel {get; set;}
        public override void ValidateCommand()
        {
            if (LatestChatModel == null)
            {
                throw new Exception("LatestChatModel not set");
            }
        }
    }
}
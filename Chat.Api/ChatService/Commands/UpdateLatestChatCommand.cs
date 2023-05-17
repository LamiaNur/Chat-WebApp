using Chat.Api.ChatService.Interfaces;
using Chat.Api.ChatService.Models;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;

namespace Chat.Api.ChatService.Commands
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
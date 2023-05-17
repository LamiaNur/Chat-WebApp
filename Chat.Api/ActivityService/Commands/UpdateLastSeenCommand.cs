using Chat.Api.Core.Helpers;
using Chat.Api.Core.Models;
using Chat.Api.ActivityService.Interfaces;
using Chat.Api.ActivityService.Models;
using Chat.Api.Core.Services;

namespace Chat.Api.ActivityService.Commands
{
    public class UpdateLastSeenCommand : ACommand
    {
        public string UserId {get; set;} = string.Empty;
        
        public override void ValidateCommand()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new Exception("UserId not set!!");
            }
        }
    }
}
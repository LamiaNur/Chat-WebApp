using Chat.Api.Core.Helpers;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;
using Chat.Api.IdentityService.Interfaces;

namespace Chat.Api.IdentityService.Commands
{
    public class LogOutCommand : ACommand
    {
        public string AppId {get; set;} = string.Empty;
        public override void ValidateCommand()
        {
            if (string.IsNullOrEmpty(AppId))
            {
                throw new Exception("AppId not set");
            }
        }
    }
}
using Chat.Api.Core.Models;

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
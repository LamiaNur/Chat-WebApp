using Chat.Api.CoreModule.Models;

namespace Chat.Api.IdentityModule.Commands
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
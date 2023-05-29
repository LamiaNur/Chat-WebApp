using Chat.Api.CoreModule.Models;

namespace Chat.Api.ActivityModule.Commands
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
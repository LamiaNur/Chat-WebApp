using Chat.Framework.CQRS;

namespace Chat.Api.IdentityModule.Commands
{
    public class LoginCommand : ACommand
    {
        public string Email {get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;
        public string AppId {get; set;} = string.Empty;
        public override void ValidateCommand()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new Exception($"Email not set");
            }
            if (string.IsNullOrEmpty(Password))
            {
                throw new Exception("Password not set");
            }
            if (string.IsNullOrEmpty(AppId))
            {
                throw new Exception("AppId not set");
            }
        }
    }
}
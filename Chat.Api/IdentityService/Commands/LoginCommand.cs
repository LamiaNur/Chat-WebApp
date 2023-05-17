using Chat.Api.ActivityService.Commands;
using Chat.Api.Core.Constants;
using Chat.Api.Core.Helpers;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;
using Chat.Api.IdentityService.Interfaces;
using Chat.Api.IdentityService.Models;

namespace Chat.Api.IdentityService.Commands
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
using Chat.Api.IdentityModule.Models;
using Chat.Framework.CQRS;

namespace Chat.Api.IdentityModule.Commands
{
    public class UpdateUserProfileCommand : ACommand
    {
        public UserModel UserModel {get; set;}
        public override void ValidateCommand()
        {
            if (UserModel == null) 
            {
                throw new Exception("UserModel not set");
            }
            if (string.IsNullOrEmpty(UserModel.Id) && string.IsNullOrEmpty(UserModel.Email))
            {
                throw new Exception("UserId or Email not set");
            }
        }
    }
}
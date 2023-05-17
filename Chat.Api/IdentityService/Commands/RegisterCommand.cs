using Chat.Api.Core.Constants;
using Chat.Api.Core.Helpers;
using Chat.Api.Core.Models;
using Chat.Api.IdentityService.Interfaces;
using Chat.Api.IdentityService.Models;
using Chat.Api.IdentityService.Repositories;
using Chat.Api.Core.Services;

namespace Chat.Api.IdentityService.Commands
{
    public class RegisterCommand : ACommand
    {
        public UserModel UserModel {get; set;}
        public override void ValidateCommand()
        {
            if (UserModel == null) 
            {
                throw new Exception("UserModel is not set");
            }
            if (string.IsNullOrEmpty(UserModel.Email))
            {
                throw new Exception("Email can't be empty.");
            }
            if (string.IsNullOrEmpty(UserModel.Password)) 
            {
                throw new Exception("Password can't Empty");
            }
            if (string.IsNullOrEmpty(UserModel.FirstName) 
            || string.IsNullOrEmpty(UserModel.LastName)) 
            {
                throw new Exception("FirstName or LastName can't be empty.");
            }
            if (UserModel.BirthDay == null) 
            {
                throw new Exception("Birthday is not set");
            }
        }
    }
}
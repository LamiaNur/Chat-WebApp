using Chat.Api.Core.Constants;
using Chat.Api.Core.Helpers;
using Chat.Api.Core.Models;
using Chat.Api.IdentityService.Interfaces;
using Chat.Api.IdentityService.Models;
using Chat.Api.IdentityService.Repositories;
using Chat.Api.Core.Services;
using System.Composition;
using Chat.Api.Core.Interfaces;

namespace Chat.Api.IdentityService.Commands
{
    [Export(typeof(ICommandHandler))]
    [Export("RegisterCommandHandler", typeof(ICommandHandler))]
    [Shared]
    public class RegisterCommandHandler : ACommandHandler<RegisterCommand>
    {
        private readonly IUserRepository _userRepository;
        
        public RegisterCommandHandler()
        {
            _userRepository = DIService.Instance.GetService<IUserRepository>();
        }
        
        
        public override async Task<CommandResponse> OnExecuteAsync(RegisterCommand command)
        {
            var response = command.CreateResponse();
            if (await _userRepository.IsUserExistAsync(command.UserModel)) 
            {
                throw new Exception("User email or id already exists!!");
            }
            command.UserModel.Id = Guid.NewGuid().ToString();
            command.UserModel.UserName = $"{command.UserModel.FirstName}_{command.UserModel.LastName}";

            if (!await _userRepository.CreateUserAsync(command.UserModel))
            {
                throw new Exception("Some anonymous problem occured!!");
            }

            response.Message = "User Created Successfully!!";
            response.SetData("UserProfile", command.UserModel.ToUserProfile());
            
            return response;
        }
    }
}
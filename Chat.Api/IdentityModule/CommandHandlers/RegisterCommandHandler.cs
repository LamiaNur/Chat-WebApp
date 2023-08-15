using System.Composition;
using Chat.Api.IdentityModule.Commands;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;
using Chat.Framework.Services;

namespace Chat.Api.IdentityModule.CommandHandlers
{
    [Export("RegisterCommandHandler", typeof(IRequestHandler))]
    [Shared]
    public class RegisterCommandHandler : ACommandHandler<RegisterCommand>
    {
        private readonly IUserRepository _userRepository;
        
        public RegisterCommandHandler()
        {
            _userRepository = DIService.Instance.GetService<IUserRepository>();
        }
                
        protected override async Task<CommandResponse> OnHandleAsync(RegisterCommand command)
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
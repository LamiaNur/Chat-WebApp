using Chat.Api.IdentityModule.Commands;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;

namespace Chat.Api.IdentityModule.CommandHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class RegisterCommandHandler : ACommandHandler<RegisterCommand>
    {
        private readonly IUserRepository _userRepository;
        
        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
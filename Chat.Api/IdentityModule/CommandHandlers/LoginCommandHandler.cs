using System.Composition;
using Chat.Api.ActivityModule.Commands;
using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Models;
using Chat.Api.CoreModule.Services;
using Chat.Api.IdentityModule.Commands;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Api.IdentityModule.Models;

namespace Chat.Api.IdentityModule.CommandHandlers
{
    [Export(typeof(ICommandHandler))]
    [Export("LoginCommandHandler", typeof(ICommandHandler))]
    [Shared]
    public class LoginCommandHandler : ACommandHandler<LoginCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler()
        {
            _userRepository = DIService.Instance.GetService<IUserRepository>();
            _tokenService = DIService.Instance.GetService<ITokenService>();
        }

        public override async Task<CommandResponse> OnHandleAsync(LoginCommand command)
        {
            var response = command.CreateResponse();
            var user = await _userRepository.GetUserAsync(command.Email, command.Password);
            if (user == null) 
            {
                throw new Exception("Email or Password error!!");
            }
            var userProfile = user.ToUserProfile();
            var token = await _tokenService.CreateTokenAsync(userProfile, command.AppId);
            if (token == null)
            {
                throw new Exception("Token Creation Failed");
            }
            response.SetData("Token", token);
            response.Message = "Logged in successfully";
            await UpdateLastSeenActivity(user);
            return response;
        }

        private async Task UpdateLastSeenActivity(UserModel user)
        {
            var updateLastSeenCommand = new UpdateLastSeenCommand
            {
                UserId = user.Id
            };
            await _commandService.HandleCommandAsync(updateLastSeenCommand);
        }
    }
}
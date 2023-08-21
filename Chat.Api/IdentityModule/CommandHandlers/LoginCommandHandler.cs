using Chat.Api.IdentityModule.Commands;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;

namespace Chat.Api.IdentityModule.CommandHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class LoginCommandHandler : ACommandHandler<LoginCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        protected override async Task<CommandResponse> OnHandleAsync(LoginCommand command)
        {
            var response = command.CreateResponse();
            var user = await _userRepository.GetUserByEmailAsync(command.Email);
            if (user == null) 
            {
                throw new Exception("Email error!!");
            }
            if (user.Password != command.Password)
            {
                throw new Exception("Password error!!");
            }
            var userProfile = user.ToUserProfile();
            var token = await _tokenService.CreateTokenAsync(userProfile, command.AppId);
            if (token == null)
            {
                throw new Exception("Token Creation Failed");
            }
            response.SetData("Token", token);
            response.Message = "Logged in successfully";
            return response;
        }
    }
}
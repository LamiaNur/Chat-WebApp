using Chat.Api.ActivityService.Commands;
using Chat.Api.Core.Helpers;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Services;
using Chat.Api.IdentityService.Interfaces;
using Microsoft.Net.Http.Headers;

namespace Chat.Api.ActivityService.Middlewares
{
    public class LastSeenMiddleware : IMiddleware
    {
        private readonly ITokenService _tokenService;
        private readonly ICommandService _commandService;

        public LastSeenMiddleware()
        {
            _tokenService = DIService.Instance.GetService<ITokenService>();
            _commandService = DIService.Instance.GetService<ICommandService>();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Executing LastSeenMiddleware\n");
            var accessToken = context.Request.Headers[HeaderNames.Authorization].ToString();
            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("AccessToken not found at LastSeenMiddleware\n");
            } 
            else
            {
                Console.WriteLine($"Access Token found : {accessToken}\n");

                if (TokenHelper.IsTokenValid(accessToken, _tokenService.GetTokenValidationParameters()))
                {
                    var userProfile = _tokenService.GetUserProfileFromAccessToken(accessToken);
                    var updateLastSeenCommand = new UpdateLastSeenCommand()
                    {
                        UserId = userProfile.Id  
                    };
                    await _commandService.ExecuteCommandAsync(updateLastSeenCommand);
                    Console.WriteLine("Last seen activity saved at LastSeenMiddleware\n");
                } 
                
            } 

            await next(context);
            Console.WriteLine("Returing from LastSeenMiddleware\n");
        }
    }
}
using Chat.Api.ActivityModule.Commands;
using Chat.Api.IdentityModule.Helpers;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Framework.Proxy;
using Chat.Framework.Services;
using Microsoft.Net.Http.Headers;

namespace Chat.Api.ActivityModule.Middlewares
{
    public class LastSeenMiddleware : IMiddleware
    {
        private readonly ITokenService _tokenService;
        private readonly ICommandQueryProxy _commandQueryService;

        public LastSeenMiddleware()
        {
            _tokenService = DIService.Instance.GetService<ITokenService>();
            _commandQueryService = DIService.Instance.GetService<ICommandQueryProxy>();
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
                    await _commandQueryService.GetCommandResponseAsync(updateLastSeenCommand);
                    Console.WriteLine("Last seen activity saved at LastSeenMiddleware\n");
                } 
                
            } 

            await next(context);
            Console.WriteLine("Returing from LastSeenMiddleware\n");
        }
    }
}
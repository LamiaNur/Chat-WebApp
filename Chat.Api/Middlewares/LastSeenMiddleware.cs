using Chat.Api.IdentityModule.Interfaces;
using Chat.Api.IdentityModule.Models;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Extensions;
using Chat.Shared.Contracts.Commands;
using Chat.Shared.Domain.Helpers;

namespace Chat.Api.Middlewares
{
    [ServiceRegister(typeof(LastSeenMiddleware), ServiceLifetime.Transient)]
    public class LastSeenMiddleware : IMiddleware
    {
        private readonly ITokenService _tokenService;
        private readonly IHttpClientFactory _httpClientFactory;

        public LastSeenMiddleware(ITokenService tokenService, IHttpClientFactory clientFactory)
        {
            _tokenService = tokenService;
            _httpClientFactory = clientFactory;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Executing LastSeenMiddleware\n");
            var accessToken = context.GetAccessToken();

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

                    // Todo: its currently synchronous. have to use message broker for async 
                    await TrackLastSeenActivityAsync(userProfile, accessToken);

                    Console.WriteLine("Last seen activity tracing from LastSeenMiddleware\n");
                }

            }

            await next(context);
            Console.WriteLine("Returning from LastSeenMiddleware\n");
        }

        private async Task TrackLastSeenActivityAsync(UserProfile userProfile, string accessToken)
        {
            var updateLastSeenCommand = new UpdateLastSeenCommand()
            {
                UserId = userProfile.Id
            };
            updateLastSeenCommand.SetData("FireAndForget", true);

            var url = "https://localhost:50502/api/Activity/track";
            var response = await _httpClientFactory
                .CreateClient()
                .AddBearerToken(accessToken)
                .PostAsync<CommandResponse>(url, updateLastSeenCommand);
        }
    }
}
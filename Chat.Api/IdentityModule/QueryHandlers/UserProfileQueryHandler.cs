using Chat.Api.IdentityModule.Interfaces;
using Chat.Api.IdentityModule.Models;
using Chat.Api.IdentityModule.Queries;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;

namespace Chat.Api.IdentityModule.QueryHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class UserProfileQueryHandler : AQueryHandler<UserProfileQuery>
    {
        private readonly IUserRepository _userRepository;
        public UserProfileQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task<QueryResponse> OnHandleAsync(UserProfileQuery query)
        {
            var response = query.CreateResponse();
            var userModels = new List<UserModel>();
            if (query.UserIds != null && query.UserIds.Any())
            {
                userModels.AddRange(await _userRepository.GetUsersByUserIdsAsync(query.UserIds));
            }
            if (query.Emails != null && query.Emails.Any())
            {
                userModels.AddRange(await _userRepository.GetUsersByEmailsAsync(query.Emails));
            }
            foreach (var userModel in userModels)
            {
                response.AddItem(userModel.ToUserProfile());
            }
            return response;
        }
    }
}
using System.Composition;
using Chat.Api.CoreModule.CQRS;
using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Services;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Api.IdentityModule.Models;
using Chat.Api.IdentityModule.Queries;

namespace Chat.Api.IdentityModule.QueryHandlers
{
    [Export("UserProfileQueryHandler", typeof(IRequestHandler))]
    [Shared]
    public class UserProfileQueryHandler : AQueryHandler<UserProfileQuery>
    {
        private readonly IUserRepository _userRepository;
        public UserProfileQueryHandler()
        {
            _userRepository = DIService.Instance.GetService<IUserRepository>();
        }

        public override async Task<QueryResponse> OnHandleAsync(UserProfileQuery query)
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
using System.Composition;
using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Models;
using Chat.Api.CoreModule.Services;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Api.IdentityModule.Queries;

namespace Chat.Api.IdentityModule.QueryHandlers
{
    [Export(typeof(IQueryHandler))]
    [Export("UserProfileQueryHandler", typeof(IQueryHandler))]
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
            var userModels = await _userRepository.GetUsersByUserIdOrEmailAsync(query.UserId, query.Email);
            foreach (var userModel in userModels)
            {
                response.AddItem(userModel.ToUserProfile());
            }
            return response;
        }
    }
}
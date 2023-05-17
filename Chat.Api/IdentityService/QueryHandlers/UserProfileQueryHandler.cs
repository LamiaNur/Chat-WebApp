using System.Composition;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;
using Chat.Api.IdentityService.Interfaces;

namespace Chat.Api.IdentityService.Queries
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
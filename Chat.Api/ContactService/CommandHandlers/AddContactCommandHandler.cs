using System.Composition;
using Chat.Api.ContactService.Interfaces;
using Chat.Api.ContactService.Models;
using Chat.Api.Core.Constants;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;
using Chat.Api.IdentityService.Models;
using Chat.Api.IdentityService.Queries;

namespace Chat.Api.ContactService.Commands
{
    [Export(typeof(ICommandHandler))]
    [Export("AddContactCommandHandler", typeof(ICommandHandler))]
    [Shared]
    public class AddContactCommandHandler : ACommandHandler<AddContactCommand>
    {
        private readonly IQueryService _queryService;
        private readonly IContactRepository _contactRepository;
        public AddContactCommandHandler()
        {
            _queryService = DIService.Instance.GetService<IQueryService>();
            _contactRepository = DIService.Instance.GetService<IContactRepository>();
        }

        public override async Task<CommandResponse> OnExecuteAsync(AddContactCommand command)
        {
            var response = command.CreateResponse();
            var userProfileQuery = new UserProfileQuery()
            {
                UserId = command.UserId,
                Email = command.ContactEmail
            };
            var queryResponse = await _queryService.HandleQueryAsync(userProfileQuery);
            if (queryResponse == null || queryResponse.Status == ResponseStatus.Error || queryResponse.ItemsCount < 2)
            {
                throw new Exception("User profile query error");
            }
            var userProfiles = queryResponse.GetItems<UserProfile>();

            var userProfile = userProfiles.FirstOrDefault(x => x.Id == command.UserId);
            if (userProfile == null)
            {
                throw new Exception("User profile error");
            }
            var contactUserProfile = userProfiles.FirstOrDefault(x => x.Email == command.ContactEmail);

            if (contactUserProfile == null)
            {
                throw new Exception("Contact User Profile error");
            }
            var userContact = new Contact()
            {
                Id = Guid.NewGuid().ToString(),
                UserA = userProfile,
                UserB = contactUserProfile,
                CreatedAt = DateTime.UtcNow,
                IsPending = true,
                RequestUserId = command.UserId
            };
            
            if (!await _contactRepository.SaveContactAsync(userContact))
            {
                throw new Exception("Saving User contact error");
            }
            
            response.Message = "Contact added successfully";
            return response;
        }
    }
}
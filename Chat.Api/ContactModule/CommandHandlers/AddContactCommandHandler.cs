using System.Composition;
using Chat.Api.ContactModule.Commands;
using Chat.Api.ContactModule.Interfaces;
using Chat.Api.ContactModule.Models;
using Chat.Api.CoreModule.CQRS;
using Chat.Api.CoreModule.Enums;
using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Services;
using Chat.Api.IdentityModule.Models;
using Chat.Api.IdentityModule.Queries;

namespace Chat.Api.ContactModule.CommandHandlers
{
    [Export("AddContactCommandHandler", typeof(IRequestHandler))]
    [Shared]
    public class AddContactCommandHandler : ACommandHandler<AddContactCommand>
    {
        private readonly IContactRepository _contactRepository;
        public AddContactCommandHandler()
        {
            _contactRepository = DIService.Instance.GetService<IContactRepository>();
        }

        protected override async Task<CommandResponse> OnHandleAsync(AddContactCommand command)
        {
            var response = command.CreateResponse();
            var userProfileQuery = new UserProfileQuery()
            {
                UserIds = new List<string> {command.UserId},
                Emails = new List<string> {command.ContactEmail}
            };
            var queryResponse = await CommandQueryService.HandleQueryAsync(userProfileQuery);
            if (queryResponse == null || queryResponse.Status != ResponseStatus.Success || queryResponse.Items.Count < 2)
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
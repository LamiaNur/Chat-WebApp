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
    [Export("AcceptOrRejectContactRequestCommandHandler", typeof(ICommandHandler))]
    [Shared]
    public class AcceptOrRejectContactRequestCommandHandler : ACommandHandler<AcceptOrRejectContactRequestCommand>
    {
        private readonly IQueryService _queryService;
        private readonly IContactRepository _contactRepository;
        public AcceptOrRejectContactRequestCommandHandler()
        {
            _queryService = DIService.Instance.GetService<IQueryService>();
            _contactRepository = DIService.Instance.GetService<IContactRepository>();
        }

        public override async Task<CommandResponse> OnHandleAsync(AcceptOrRejectContactRequestCommand command)
        {
            var response = command.CreateResponse();
            var contact = await _contactRepository.GetContactByIdAsync(command.ContactId);
            if (contact == null) 
            {
                throw new Exception("Contact not found");
            }
            if (command.IsAcceptRequest)
            {
                contact.IsPending = false;
                if (await _contactRepository.SaveContactAsync(contact))
                {
                    throw new Exception("Contact save problem");
                }
                response.Message = "Contact added";
            } 
            else 
            {
                if (await _contactRepository.DeleteContactById(command.ContactId))
                {
                    throw new Exception("Delete contact problem");
                }
                response.Message = "Contact rejected";
            }
            return response;
        }
    }
}
using System.Composition;
using Chat.Api.ContactModule.Commands;
using Chat.Api.ContactModule.Interfaces;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;
using Chat.Framework.Services;

namespace Chat.Api.ContactModule.CommandHandlers
{
    [Export("AcceptOrRejectContactRequestCommandHandler", typeof(IRequestHandler))]
    [Shared]
    public class AcceptOrRejectContactRequestCommandHandler : ACommandHandler<AcceptOrRejectContactRequestCommand>
    {
        private readonly IContactRepository _contactRepository;
        public AcceptOrRejectContactRequestCommandHandler()
        {
            _contactRepository = DIService.Instance.GetService<IContactRepository>();
        }

        protected override async Task<CommandResponse> OnHandleAsync(AcceptOrRejectContactRequestCommand command)
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
                if (!await _contactRepository.SaveContactAsync(contact))
                {
                    throw new Exception("Contact save problem");
                }
                response.Message = "Contact added";
            } 
            else 
            {
                if (!await _contactRepository.DeleteContactById(command.ContactId))
                {
                    throw new Exception("Delete contact problem");
                }
                response.Message = "Contact rejected";
            }
            return response;
        }
    }
}
using System.Composition;
using Chat.Api.ContactModule.Interfaces;
using Chat.Api.ContactModule.Queries;
using Chat.Api.CoreModule.CQRS;
using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.ContactModule.QueryHandlers
{
    [Export("ContactQueryHandler", typeof(IRequestHandler))]
    [Shared]
    public class ContactQueryHandler : AQueryHandler<ContactQuery>
    {
        private readonly IContactRepository _contactRepository;
        public ContactQueryHandler()
        {
            _contactRepository = DIService.Instance.GetService<IContactRepository>();
        }
        public override async Task<QueryResponse> OnHandleAsync(ContactQuery query)
        {
            var response = query.CreateResponse();
            if (query.IsRequestContacts)
            {
                var contacts = await _contactRepository.GetContactRequestsAsync(query.UserId);
                response.SetItems(contacts.ToList<object>());
            } 
            else if (query.IsPendingContacts)
            {
                var contacts = await _contactRepository.GetPendingContactsAsync(query.UserId);
                response.SetItems(contacts.ToList<object>());
            } 
            else 
            {
                var contacts = await _contactRepository.GetUserContactsAsync(query.UserId);
                response.SetItems(contacts.ToList<object>());
            }
            return response;
        }
    }
}
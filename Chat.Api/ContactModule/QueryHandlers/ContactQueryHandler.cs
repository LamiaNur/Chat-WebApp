using Chat.Api.ContactModule.Interfaces;
using Chat.Api.ContactModule.Queries;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;

namespace Chat.Api.ContactModule.QueryHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class ContactQueryHandler : AQueryHandler<ContactQuery>
    {
        private readonly IContactRepository _contactRepository;
        public ContactQueryHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        protected override async Task<QueryResponse> OnHandleAsync(ContactQuery query)
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
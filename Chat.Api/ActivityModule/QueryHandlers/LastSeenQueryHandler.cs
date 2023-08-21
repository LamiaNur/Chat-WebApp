using Chat.Api.ActivityModule.Interfaces;
using Chat.Api.ActivityModule.Queries;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;

namespace Chat.Api.ActivityModule.QueryHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class LastSeenQueryHandler : AQueryHandler<LastSeenQuery>
    {
        private readonly ILastSeenRepository _lastSeenRepository;        
        public LastSeenQueryHandler(ILastSeenRepository lastSeenRepository)
        {
           _lastSeenRepository = lastSeenRepository;
        }
        
        protected override async Task<QueryResponse> OnHandleAsync(LastSeenQuery query)
        {
            var response = query.CreateResponse();
            var lastSeenModels = await _lastSeenRepository.GetLastSeenModelsByUserIdsAsync(query.UserIds);
            if (lastSeenModels == null)
            {
                throw new Exception("Last Seen Models Query Problems");
            }
            foreach (var lastSeenModel in lastSeenModels)
            {
                response.AddItem(lastSeenModel.ToLastSeenDto());
            }
            return response;
        }
    }
}
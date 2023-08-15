using System.Composition;
using Chat.Api.ActivityModule.Interfaces;
using Chat.Api.ActivityModule.Queries;
using Chat.Api.CoreModule.CQRS;
using Chat.Api.CoreModule.Helpers;
using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.ActivityModule.QueryHandlers
{
    [Export("LastSeenQueryHandler", typeof(IRequestHandler))]
    [Shared]
    public class LastSeenQueryHandler : AQueryHandler<LastSeenQuery>
    {
        private readonly ILastSeenRepository _lastSeenRepository;        
        public LastSeenQueryHandler()
        {
           _lastSeenRepository = DIService.Instance.GetService<ILastSeenRepository>();
        }
        
        public override async Task<QueryResponse> OnHandleAsync(LastSeenQuery query)
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
using Chat.Api.Core.Models;
using Chat.Api.ActivityService.Interfaces;
using Chat.Api.Core.Services;
using System.Composition;
using Chat.Api.Core.Interfaces;

namespace Chat.Api.ActivityService.Queries
{
    [Export(typeof(IQueryHandler))]
    [Export("LastSeenQueryHandler", typeof(IQueryHandler))]
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
            var lastSeenModel = await _lastSeenRepository.GetLastSeenModelByUserIdAsync(query.UserId);
            if (lastSeenModel == null)
            {
                throw new Exception("Last Seen Model not found");
            }
            response.AddItem(lastSeenModel);
            return response;
        }
    }
}
using System.Composition;
using Chat.Api.ActivityModule.Interfaces;
using Chat.Api.ActivityModule.Queries;
using Chat.Api.CoreModule.Helpers;
using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Models;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.ActivityModule.QueryHandlers
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
            var displayTime = DisplayTimeHelper.GetChatListDisplayTime(lastSeenModel.LastSeenAt, "Active Now");
            var isActive = displayTime == "Active Now";
            response.SetData("Status", displayTime);
            response.SetData("IsActive", isActive);
            return response;
        }
    }
}
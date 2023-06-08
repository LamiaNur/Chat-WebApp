using System.Composition;
using Chat.Api.ActivityModule.Interfaces;
using Chat.Api.ActivityModule.Queries;
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
            var timeDifference = DateTime.UtcNow.Subtract(lastSeenModel.LastSeenAt);
            var days = ((int)timeDifference.TotalDays);
            var hours = ((int)timeDifference.TotalHours);
            var minutes =((int)timeDifference.TotalMinutes);
            response.SetData("Days", days);
            response.SetData("Hours", hours);
            response.SetData("Minitues", minutes);
            var status = "";
            var isActive = false;
            if (days == 0 && hours == 0 && minutes == 0) {
                status = "Active Now";
                isActive = true;
            }  else if (days == 0 && hours == 0) {
                status = $"{minutes} minitues ago";
            } else if (days == 0) {
                status = $"{hours} hour ago";
            } else {
                status = $"{days} days ago";
            }
            response.SetData("Status", status);
            response.SetData("IsActive", isActive);
            return response;
        }
    }
}
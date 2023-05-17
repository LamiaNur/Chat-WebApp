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
    public class AcceptOrRejectContactRequestCommand : ACommand
    {
        public string ContactId {get; set;} = string.Empty;
        public bool IsAcceptRequest {get; set;}

        public override void ValidateCommand()
        {
            if (string.IsNullOrEmpty(ContactId))
            {
                throw new Exception("ContactId not set");
            }
        }
    }
}
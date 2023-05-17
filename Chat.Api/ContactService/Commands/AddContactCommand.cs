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
    public class AddContactCommand : ACommand
    {
        public string ContactEmail {get; set;} = string.Empty;
        public string UserId {get; set;} = string.Empty;
        
        public override void ValidateCommand()
        {
            if (string.IsNullOrEmpty(ContactEmail))
            {
                throw new Exception("ContactEmail not set");
            }
            if (string.IsNullOrEmpty(UserId))
            {
                throw new Exception("UserId not set");
            }
        }
    }
}
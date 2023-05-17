using System.Composition;
using Chat.Api.ActivityService.Commands;
using Chat.Api.ActivityService.Interfaces;
using Chat.Api.ActivityService.Models;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;

namespace Chat.Api.ActivityService.CommandHandlers
{
    [Export(typeof(ICommandHandler))]
    [Export("UpdateLastSeenCommandHandler", typeof(ICommandHandler))]
    [Shared]
    public class UpdateLastSeenCommandHandler : ACommandHandler<UpdateLastSeenCommand>
    {
        private readonly ILastSeenRepository _lastSeenRepository;

        public UpdateLastSeenCommandHandler()
        {
            _lastSeenRepository = DIService.Instance.GetService<ILastSeenRepository>();
        }
        public override async Task<CommandResponse> OnExecuteAsync(UpdateLastSeenCommand command)
        {
            var response = command.CreateResponse();
            var lastSeenModel = await _lastSeenRepository.GetLastSeenModelByUserIdAsync(command.UserId);
            if (lastSeenModel == null) 
            {
                lastSeenModel = new LastSeenModel
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = command.UserId
                };
            }
            lastSeenModel.LastSeenAt = DateTime.UtcNow;
            if (!await _lastSeenRepository.SaveLastSeenModelAsync(lastSeenModel))
            {
                throw new Exception("Last Seen model Save error");
            }
            response.Message = "Last seen time set successfully";
            response.SetData("LastSeenAt", lastSeenModel.LastSeenAt);
            return response;
        }
    }
}
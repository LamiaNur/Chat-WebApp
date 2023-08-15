using System.Composition;
using Chat.Api.ActivityModule.Commands;
using Chat.Api.ActivityModule.Interfaces;
using Chat.Api.ActivityModule.Models;
using Chat.Api.CoreModule.CQRS;
using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.ActivityModule.CommandHandlers
{
    [Export("UpdateLastSeenCommandHandler", typeof(IRequestHandler))]
    [Shared]
    public class UpdateLastSeenCommandHandler : ACommandHandler<UpdateLastSeenCommand>
    {
        private readonly ILastSeenRepository _lastSeenRepository;

        public UpdateLastSeenCommandHandler()
        {
            _lastSeenRepository = DIService.Instance.GetService<ILastSeenRepository>();
        }
        public override async Task<CommandResponse> OnHandleAsync(UpdateLastSeenCommand command)
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
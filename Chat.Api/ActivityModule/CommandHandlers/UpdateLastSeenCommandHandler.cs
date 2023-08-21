using Chat.Api.ActivityModule.Commands;
using Chat.Api.ActivityModule.Interfaces;
using Chat.Api.ActivityModule.Models;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;

namespace Chat.Api.ActivityModule.CommandHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class UpdateLastSeenCommandHandler : ACommandHandler<UpdateLastSeenCommand>
    {
        private readonly ILastSeenRepository _lastSeenRepository;

        public UpdateLastSeenCommandHandler(ILastSeenRepository lastSeenRepository)
        {
            _lastSeenRepository = lastSeenRepository;
        }
        protected override async Task<CommandResponse> OnHandleAsync(UpdateLastSeenCommand command)
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
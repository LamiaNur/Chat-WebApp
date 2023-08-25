using Chat.Activity.Interfaces;
using Chat.Activity.Models;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;
using Chat.Shared.Contracts.Commands;

namespace Chat.Activity.CommandHandlers
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
            var lastSeenModel = await _lastSeenRepository.GetLastSeenModelByUserIdAsync(command.UserId) ?? new LastSeenModel
            {
                Id = Guid.NewGuid().ToString(),
                UserId = command.UserId
            };
            lastSeenModel.LastSeenAt = DateTime.UtcNow;
            if (!await _lastSeenRepository.SaveLastSeenModelAsync(lastSeenModel))
            {
                response.SetErrorResponse("Save Last Seen Model Error");
                return response;
            }
            response.SetSuccessResponse("Last seen time set successfully");
            response.SetData("LastSeenAt", lastSeenModel.LastSeenAt);
            return response;
        }
    }
}
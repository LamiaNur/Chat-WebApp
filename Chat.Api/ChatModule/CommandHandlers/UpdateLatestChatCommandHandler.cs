using System.Composition;
using Chat.Api.ChatModule.Commands;
using Chat.Api.ChatModule.Interfaces;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;
using Chat.Framework.Services;

namespace Chat.Api.ChatModule.CommandHandlers
{
    [Export("UpdateLatestChatCommandHandler", typeof(IRequestHandler))]
    [Shared]
    public class UpdateLatestChatCommandHandler : ACommandHandler<UpdateLatestChatCommand>
    {
        
        private readonly ILatestChatRepository _latestChatRepository;
        
        public UpdateLatestChatCommandHandler()
        {
            _latestChatRepository = DIService.Instance.GetService<ILatestChatRepository>();
        }

        protected override async Task<CommandResponse> OnHandleAsync(UpdateLatestChatCommand command)
        {
            var response = command.CreateResponse();
            var latestChatModel = await _latestChatRepository.GetLatestChatAsync(command.LatestChatModel.UserId, command.LatestChatModel.SendTo);
            if (latestChatModel == null)
            {
                latestChatModel = command.LatestChatModel;
                latestChatModel.Id = Guid.NewGuid().ToString();
                latestChatModel.Occurrance = 1;
            }
            latestChatModel.Message = command.LatestChatModel.Message;
            latestChatModel.SentAt = command.LatestChatModel.SentAt;
            latestChatModel.Status = command.LatestChatModel.Status;
            if (command.LatestChatModel.UserId == latestChatModel.UserId)
            {
                latestChatModel.Occurrance++;
            }
            else 
            {
                latestChatModel.Occurrance = 1;
                latestChatModel.UserId = command.LatestChatModel.UserId;
                latestChatModel.SendTo = command.LatestChatModel.SendTo;
            }
            await _latestChatRepository.SaveLatestChatModelAsync(latestChatModel);

            return response;
        }
    }
}
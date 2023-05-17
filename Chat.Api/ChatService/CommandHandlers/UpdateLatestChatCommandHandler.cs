using System.Composition;
using Chat.Api.ChatService.Interfaces;
using Chat.Api.ChatService.Models;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;

namespace Chat.Api.ChatService.Commands
{
    [Export(typeof(ICommandHandler))]
    [Export("UpdateLatestChatCommandHandler", typeof(ICommandHandler))]
    [Shared]
    public class UpdateLatestChatCommandHandler : ACommandHandler<UpdateLatestChatCommand>
    {
        
        private readonly ILatestChatRepository _latestChatRepository;
        
        public UpdateLatestChatCommandHandler()
        {
            _latestChatRepository = DIService.Instance.GetService<ILatestChatRepository>();
        }

        public override async Task<CommandResponse> OnExecuteAsync(UpdateLatestChatCommand command)
        {
            var response = command.CreateResponse();
            var latestChatModel = await _latestChatRepository.GetLatestChatAsync(command.LatestChatModel.UserId, command.LatestChatModel.SendTo);
            if (latestChatModel == null)
            {
                latestChatModel = command.LatestChatModel;
                latestChatModel.Id = Guid.NewGuid().ToString();
            }
            latestChatModel.Message = command.LatestChatModel.Message;
            latestChatModel.SentAt = command.LatestChatModel.SentAt;
            latestChatModel.Status = command.LatestChatModel.Status;
            
            await _latestChatRepository.SaveLatestChatModelAsync(latestChatModel);

            return response;
        }
    }
}
using System.Composition;
using Chat.Api.ChatModule.Commands;
using Chat.Api.ChatModule.Interfaces;
using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Models;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.ChatModule.CommandHandlers
{
    [Export(typeof(ICommandHandler))]
    [Export("UpdateChatsStatusCommandHandler", typeof(ICommandHandler))]
    [Shared]
    public class UpdateChatsStatusCommandHandler : ACommandHandler<UpdateChatsStatusCommand>
    {
        
        private readonly ILatestChatRepository _latestChatRepository;
        private readonly IChatRepository _chatRepository;
        
        public UpdateChatsStatusCommandHandler()
        {
            _latestChatRepository = DIService.Instance.GetService<ILatestChatRepository>();
            _chatRepository = DIService.Instance.GetService<IChatRepository>();
        }

        public override async Task<CommandResponse> OnHandleAsync(UpdateChatsStatusCommand command)
        {
            var response = command.CreateResponse();
            var latestChatModel = await _latestChatRepository.GetLatestChatAsync(command.UserId, command.OpenedChatUserId);
            if (latestChatModel == null)
            {
                throw new Exception("LatestChatModel not found");
            }
            if (latestChatModel.UserId != command.UserId)
            {
                latestChatModel.Occurrance = 0;
                await _latestChatRepository.SaveLatestChatModelAsync(latestChatModel);
            }
            
            var chatModels = await _chatRepository.GetSenderAndReceiverSpecificChatModelsAsync(command.OpenedChatUserId, command.UserId);
            foreach (var chatModel in chatModels)
            {
                chatModel.Status = "Seen";
                await _chatRepository.SaveChatModelAsync(chatModel); // TODO: need to save together at db
            }
            return response;
        }
    }
}
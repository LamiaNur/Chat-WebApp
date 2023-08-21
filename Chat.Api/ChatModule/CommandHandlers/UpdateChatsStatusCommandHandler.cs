using Chat.Api.ChatModule.Commands;
using Chat.Api.ChatModule.Interfaces;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;

namespace Chat.Api.ChatModule.CommandHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class UpdateChatsStatusCommandHandler : ACommandHandler<UpdateChatsStatusCommand>
    {
        
        private readonly ILatestChatRepository _latestChatRepository;
        private readonly IChatRepository _chatRepository;
        
        public UpdateChatsStatusCommandHandler(IChatRepository chatRepository, ILatestChatRepository latestChatRepository)
        {
            _latestChatRepository = latestChatRepository;
            _chatRepository = chatRepository;
        }

        protected override async Task<CommandResponse> OnHandleAsync(UpdateChatsStatusCommand command)
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
            }

            await _chatRepository.SaveChatModelsAsync(chatModels);
            return response;
        }
    }
}
using Chat.Api.ChatService.Interfaces;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;
using Chat.Api.ChatService.Models;
using Chat.Api.Core.Interfaces;
using System.Composition;
using Chat.Api.ChatService.Commands;

namespace Chat.Api.ChatService.CommandHandlers
{
    [Export(typeof(ICommandHandler))]
    [Export("SendMessageCommandHandler", typeof(ICommandHandler))]
    [Shared]
    public class SendMessageCommandHandler : ACommandHandler<SendMessageCommand>
    {
        private readonly IChatRepository _chatRepository;
        
        public SendMessageCommandHandler()
        {
            _chatRepository = DIService.Instance.GetService<IChatRepository>();
        }
        public override async Task<CommandResponse> OnHandleAsync(SendMessageCommand command)
        {
            var response = command.CreateResponse();
            if (!await _chatRepository.SaveChatModelAsync(command.ChatModel))
            {
                throw new Exception("Chat model save error");
            }
            var latestChatModel = (LatestChatModel) command.ChatModel;
            var updateLatestChatCommand = new UpdateLatestChatCommand()
            {
                LatestChatModel = latestChatModel
            };
            await _commandService.HandleCommandAsync(updateLatestChatCommand);
            
            return response;
        }
    }
}
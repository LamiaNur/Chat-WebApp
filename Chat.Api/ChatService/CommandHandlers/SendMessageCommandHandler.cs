using Chat.Api.ChatService.Interfaces;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;
using Chat.Api.ChatService.Models;
using Chat.Api.Core.Interfaces;
using System.Composition;

namespace Chat.Api.ChatService.Commands
{
    [Export(typeof(ICommandHandler))]
    [Export("SendMessageCommandHandler", typeof(ICommandHandler))]
    [Shared]
    public class SendMessageCommandHandler : ACommandHandler<SendMessageCommand>
    {
        private readonly IChatRepository _chatRepository;
        private readonly ICommandService _commandService;
        
        public SendMessageCommandHandler()
        {
            _chatRepository = DIService.Instance.GetService<IChatRepository>();
            _commandService = DIService.Instance.GetService<ICommandService>();
        }
        public override async Task<CommandResponse> OnExecuteAsync(SendMessageCommand command)
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
            await _commandService.ExecuteCommandAsync(updateLatestChatCommand);
            
            return response;
        }
    }
}
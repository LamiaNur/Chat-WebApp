using System.Composition;
using Chat.Api.ChatModule.Commands;
using Chat.Api.ChatModule.Interfaces;
using Chat.Api.ChatModule.Models;
using Chat.Api.CoreModule.CQRS;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.ChatModule.CommandHandlers
{
    [Export(typeof(ICommandHandler))]
    [Export("SendMessageCommandHandler", typeof(ICommandHandler))]
    [Shared]
    public class SendMessageCommandHandler : ACommandHandler<SendMessageCommand>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IChatHubService _chatHubService;
        
        public SendMessageCommandHandler()
        {
            _chatHubService = DIService.Instance.GetService<IChatHubService>();
            _chatRepository = DIService.Instance.GetService<IChatRepository>();
        }
        public override async Task<CommandResponse> OnHandleAsync(SendMessageCommand command)
        {
            var response = command.CreateResponse();
            command.ChatModel.Id = Guid.NewGuid().ToString();
            command.ChatModel.SentAt = DateTime.UtcNow;
            command.ChatModel.Status = "Sent";
            if (!await _chatRepository.SaveChatModelAsync(command.ChatModel))
            {
                throw new Exception("Chat model save error");
            }
            var requestContext = command.GetCurrentScope();
            await _chatHubService.SendAsync<ChatModel>(command.ChatModel.SendTo, command.ChatModel, requestContext);

            var latestChatModel = command.ChatModel.ToLatestChatModel();
            var updateLatestChatCommand = new UpdateLatestChatCommand()
            {
                LatestChatModel = latestChatModel
            };
            await _commandQueryService.HandleAsync(updateLatestChatCommand);
            response.SetData("Message", command.ChatModel.ToChatDto());
            return response;
        }
    }
}
using Chat.Api.ChatModule.Commands;
using Chat.Api.ChatModule.Interfaces;
using Chat.Api.ChatModule.Models;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;
using Chat.Framework.Proxy;

namespace Chat.Api.ChatModule.CommandHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class SendMessageCommandHandler : ACommandHandler<SendMessageCommand>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IChatHubService _chatHubService;
        private readonly ICommandQueryProxy _commandQueryProxy;
        public SendMessageCommandHandler(IChatRepository chatRepository, IChatHubService chatHubService, ICommandQueryProxy commandQueryProxy)
        {
            _chatHubService = chatHubService;
            _chatRepository = chatRepository;
            _commandQueryProxy = commandQueryProxy;
        }
        protected override async Task<CommandResponse> OnHandleAsync(SendMessageCommand command)
        {
            var response = command.CreateResponse();
            command.ChatModel.Id = Guid.NewGuid().ToString();
            command.ChatModel.SentAt = DateTime.UtcNow;
            command.ChatModel.Status = "Sent";
            if (!await _chatRepository.SaveChatModelAsync(command.ChatModel))
            {
                throw new Exception("Chat model save error");
            }
            var requestContext = command.GetRequestContext();
            await _chatHubService.SendAsync<ChatModel>(command.ChatModel.SendTo, command.ChatModel, requestContext);

            var latestChatModel = command.ChatModel.ToLatestChatModel();
            var updateLatestChatCommand = new UpdateLatestChatCommand()
            {
                LatestChatModel = latestChatModel
            };
            await _commandQueryProxy.GetCommandResponseAsync(updateLatestChatCommand);
            response.SetData("Message", command.ChatModel.ToChatDto());
            return response;
        }
    }
}
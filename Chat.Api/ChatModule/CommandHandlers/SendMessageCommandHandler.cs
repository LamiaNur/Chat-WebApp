using System.Composition;
using Chat.Api.ChatModule.Commands;
using Chat.Api.ChatModule.Interfaces;
using Chat.Api.ChatModule.Models;
using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Models;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.ChatModule.CommandHandlers
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
            command.ChatModel.Id = Guid.NewGuid().ToString();
            command.ChatModel.SentAt = DateTime.UtcNow;
            command.ChatModel.Status = "Sent";
            if (!await _chatRepository.SaveChatModelAsync(command.ChatModel))
            {
                throw new Exception("Chat model save error");
            }
            var latestChatModel = command.ChatModel.ToLatestChatModel();
            var updateLatestChatCommand = new UpdateLatestChatCommand()
            {
                LatestChatModel = latestChatModel
            };
            await _commandService.HandleCommandAsync(updateLatestChatCommand);
            response.Message = "Send message success";
            return response;
        }
    }
}
using System.Composition;
using Chat.Api.ChatService.Interfaces;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;

namespace Chat.Api.ChatService.Queries
{
    [Export(typeof(IQueryHandler))]
    [Export("ChatQueryHandler", typeof(IQueryHandler))]
    [Shared]
    public class ChatQueryHandler : AQueryHandler<ChatQuery>
    {
        private readonly IChatRepository _chatRepository;
        
        public ChatQueryHandler()
        {
            _chatRepository = DIService.Instance.GetService<IChatRepository>();
        }
        public override async Task<QueryResponse> OnHandleAsync(ChatQuery query)
        {
            var response = query.CreateResponse();
            var chatModels = await _chatRepository.GetChatModelsAsync(query.UserId, query.SendTo, query.Offset, query.Limit);
            response.AddItems(chatModels.ToList<object>());
            return response;
        }
    }
}
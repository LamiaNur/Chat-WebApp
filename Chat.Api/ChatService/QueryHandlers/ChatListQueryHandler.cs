using System.Composition;
using Chat.Api.ChatService.Interfaces;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;

namespace Chat.Api.ChatService.Queries
{
    [Export(typeof(IQueryHandler))]
    [Export("ChatListQueryHandler", typeof(IQueryHandler))]
    [Shared]
    public class ChatListQueryHandler : AQueryHandler<ChatListQuery>
    {
        private readonly ILatestChatRepository _latestChatRepository;
        public ChatListQueryHandler()
        {
            _latestChatRepository = DIService.Instance.GetService<ILatestChatRepository>();
        }
        public override async Task<QueryResponse> OnHandleAsync(ChatListQuery query)
        {
            var response = query.CreateResponse();
            var latestChatModels = await _latestChatRepository.GetLatestChatModelsAsync(query.UserId, query.Offset, query.Limit);
            response.AddItems(latestChatModels.ToList<object>());
            return response;
        }
    }
}
using System.Composition;
using Chat.Api.ChatModule.Interfaces;
using Chat.Api.ChatModule.Queries;
using Chat.Api.CoreModule.CQRS;
using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.ChatModule.QueryHandlers
{
    [Export("ChatListQueryHandler", typeof(IRequestHandler))]
    [Shared]
    public class ChatListQueryHandler : AQueryHandler<ChatListQuery>
    {
        private readonly ILatestChatRepository _latestChatRepository;
        public ChatListQueryHandler()
        {
            _latestChatRepository = DIService.Instance.GetService<ILatestChatRepository>();
        }
        protected override async Task<QueryResponse> OnHandleAsync(ChatListQuery query)
        {
            var response = query.CreateResponse();
            var latestChatModels = await _latestChatRepository.GetLatestChatModelsAsync(query.UserId, query.Offset, query.Limit);
            foreach (var latestChatModel in latestChatModels)
            {
                response.AddItem(latestChatModel.ToLatestChatDto(query.UserId));
            }
            // response.AddItems(latestChatModels.ToList<object>());
            return response;
        }
    }
}
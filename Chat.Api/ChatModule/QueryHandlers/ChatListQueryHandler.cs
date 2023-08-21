using Chat.Api.ChatModule.Interfaces;
using Chat.Api.ChatModule.Queries;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;

namespace Chat.Api.ChatModule.QueryHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class ChatListQueryHandler : AQueryHandler<ChatListQuery>
    {
        private readonly ILatestChatRepository _latestChatRepository;
        public ChatListQueryHandler(ILatestChatRepository latestChatRepository)
        {
            _latestChatRepository = latestChatRepository;
        }
        protected override async Task<QueryResponse> OnHandleAsync(ChatListQuery query)
        {
            var response = query.CreateResponse();
            var latestChatModels = await _latestChatRepository.GetLatestChatModelsAsync(query.UserId, query.Offset, query.Limit);
            foreach (var latestChatModel in latestChatModels)
            {
                response.AddItem(latestChatModel.ToLatestChatDto(query.UserId));
            }
            return response;
        }
    }
}
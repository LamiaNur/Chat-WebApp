using Chat.Api.ChatModule.Interfaces;
using Chat.Api.ChatModule.Queries;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;

namespace Chat.Api.ChatModule.QueryHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class ChatQueryHandler : AQueryHandler<ChatQuery>
    {
        private readonly IChatRepository _chatRepository;

        public ChatQueryHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }
        protected override async Task<QueryResponse> OnHandleAsync(ChatQuery query)
        {
            var response = query.CreateResponse();
            var chatModels = await _chatRepository.GetChatModelsAsync(query.UserId, query.SendTo, query.Offset, query.Limit);
            foreach (var chatModel in chatModels)
            {
                response.AddItem(chatModel.ToChatDto());   
            }
            return response;
        }
    }
}
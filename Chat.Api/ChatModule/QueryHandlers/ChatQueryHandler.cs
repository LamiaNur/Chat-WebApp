using System.Composition;
using Chat.Api.ChatModule.Interfaces;
using Chat.Api.ChatModule.Queries;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;
using Chat.Framework.Services;

namespace Chat.Api.ChatModule.QueryHandlers
{
    [Export("ChatQueryHandler", typeof(IRequestHandler))]
    [Shared]
    public class ChatQueryHandler : AQueryHandler<ChatQuery>
    {
        private readonly IChatRepository _chatRepository;
        
        public ChatQueryHandler()
        {
            _chatRepository = DIService.Instance.GetService<IChatRepository>();
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
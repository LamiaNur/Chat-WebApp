using System.Composition;
using Chat.Api.ChatModule.Interfaces;
using Chat.Api.ChatModule.Queries;
using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Models;
using Chat.Api.CoreModule.Services;
using Chat.Api.CoreModule.Helpers;

namespace Chat.Api.ChatModule.QueryHandlers
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
            foreach (var chatModel in chatModels)
            {
                response.AddItem(chatModel.ToChatDto());   
            }
            return response;
        }
    }
}
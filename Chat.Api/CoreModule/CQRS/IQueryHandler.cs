using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Interfaces
{
    public interface IQueryHandler : IRequestHandler<IQuery, QueryResponse>
    {
        
    }
}
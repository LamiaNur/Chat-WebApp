using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Interfaces
{
    public interface ICommandHandler : IRequestHandler<ICommand, CommandResponse>
    {
        
    }
}
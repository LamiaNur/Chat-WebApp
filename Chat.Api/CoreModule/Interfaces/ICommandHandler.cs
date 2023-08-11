using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Interfaces
{
    public interface ICommandHandler
    {
        Task<CommandResponse> HandleAsync(ICommand command);
    }
}
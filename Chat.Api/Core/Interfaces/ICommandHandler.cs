using Chat.Api.Core.Models;

namespace Chat.Api.Core.Interfaces
{
    public interface ICommandHandler
    {
        Task<CommandResponse> ExecuteAsync(ICommand command);
    }
}
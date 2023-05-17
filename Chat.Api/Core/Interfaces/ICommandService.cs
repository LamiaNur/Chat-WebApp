using Chat.Api.Core.Models;

namespace Chat.Api.Core.Interfaces 
{
    public interface ICommandService
    {
        Task<CommandResponse> HandleCommandAsync(ICommand command);
    }
}
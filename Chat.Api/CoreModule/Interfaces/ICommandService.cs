using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Interfaces 
{
    public interface ICommandService
    {
        Task<CommandResponse> HandleCommandAsync(ICommand command, RequestContext requestContext = null);
    }
}
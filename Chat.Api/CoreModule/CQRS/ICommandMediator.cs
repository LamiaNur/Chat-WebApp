using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Mediators;

public interface ICommandMediator
{
    Task<CommandResponse> HandleAsync(ICommand command);
}
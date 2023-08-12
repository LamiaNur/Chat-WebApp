using System.Composition;
using Chat.Api.CoreModule.Models;
using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Mediators;

namespace Chat.Api.CoreModule.Services;
[Export(typeof(ICommandQueryService))]
[Shared]
public class CommandQueryService : ICommandQueryService
{
    public async Task<CommandResponse> HandleAsync(ICommand command)
    {
        Console.WriteLine("This is from CommandQueryMediato\n");
        ICommandMediator CommandMediator = DIService.Instance.GetService<ICommandMediator>();
        return await CommandMediator.HandleAsync(command);
    }
    public async Task<QueryResponse> HandleAsync(IQuery query)
    {
       IQueryMediator QueryMediator = DIService.Instance.GetService<IQueryMediator>();
        return await QueryMediator.HandleAsync(query);
    }
}
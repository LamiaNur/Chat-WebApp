using System.Composition;
using Chat.Api.CoreModule.Models;
using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Mediators;

namespace Chat.Api.CoreModule.Services;
[Export(typeof(ICommandQueryService))]
[Shared]
public class CommandQueryService : ICommandQueryService
{
    private readonly ICommandMediator _commandMediator = DIService.Instance.GetService<ICommandMediator>();
    private readonly IQueryMediator _queryMediator = DIService.Instance.GetService<IQueryMediator>();
    public async Task<CommandResponse> HandleAsync(ICommand command)
    {
        return await _commandMediator.HandleAsync(command);
    }
    public async Task<QueryResponse> HandleAsync(IQuery query)
    {
        return await _queryMediator.HandleAsync(query);
    }
}
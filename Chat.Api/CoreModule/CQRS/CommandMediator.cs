using System.Composition;
using Chat.Api.CoreModule.Services;
using Chat.Api.CoreModule.Mediators;

namespace Chat.Api.CoreModule.CQRS;

[Export(typeof(ICommandMediator))]
[Shared]
public class CommandMediator : RequestMediator<ICommand, CommandResponse>, ICommandMediator
{
    protected override IRequestHandler<ICommand, CommandResponse>? GetHandler(string handlerName)
    {
        Console.WriteLine("This is from CommandMEdiator Before Get handler\n");
        var handler = DIService.Instance.GetService<ICommandHandler>(handlerName);
        Console.WriteLine("This is from CommandMEdiator After Get handler\n");
        return handler;
    }
}
namespace Chat.Api.CoreModule.CQRS;

public interface ICommandMediator
{
    Task<CommandResponse> HandleAsync(ICommand command);
}
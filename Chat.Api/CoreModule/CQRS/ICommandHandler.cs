using Chat.Api.CoreModule.Mediators;

namespace Chat.Api.CoreModule.CQRS
{
    public interface ICommandHandler : IRequestHandler<ICommand, CommandResponse>
    {

    }
}
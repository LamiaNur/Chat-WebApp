using Chat.Api.CoreModule.Interfaces;

namespace Chat.Api.CoreModule.CQRS
{
    public interface ICommand : IRequest
    {
        CommandResponse CreateResponse();
        void ValidateCommand();
    }
}
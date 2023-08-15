using Chat.Framework.Interfaces;

namespace Chat.Framework.CQRS
{
    public interface ICommand : IRequest
    {
        CommandResponse CreateResponse();
        void ValidateCommand();
    }
}
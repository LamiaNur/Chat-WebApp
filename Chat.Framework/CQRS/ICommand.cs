using Chat.Framework.Interfaces;

namespace Chat.Framework.CQRS
{
    public interface ICommand : IRequest
    { 
        void ValidateCommand();
        CommandResponse CreateResponse();
        CommandResponse CreateResponse(CommandResponse response);
    }
}
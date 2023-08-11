using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Interfaces 
{
    public interface ICommand : IRequest
    {
        CommandResponse CreateResponse();
        void ValidateCommand();
    }
}
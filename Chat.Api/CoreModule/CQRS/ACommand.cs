using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.CQRS
{
    public abstract class ACommand : Request, ICommand
    {
        public CommandResponse CreateResponse()
        {
            return new CommandResponse
            {
                Name = GetType().Name
            };
        }
        public abstract void ValidateCommand();
    }
}
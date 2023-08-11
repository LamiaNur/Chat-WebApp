using Chat.Api.CoreModule.Interfaces;

namespace Chat.Api.CoreModule.Models
{
    public abstract class ACommand : Request, ICommand
    {
        public CommandResponse CreateResponse()
        {
            return new CommandResponse
            {
                Name = this.GetType().Name
            };
        }
        public abstract void ValidateCommand();
    }
}
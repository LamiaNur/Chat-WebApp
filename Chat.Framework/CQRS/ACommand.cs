using Chat.Framework.Models;

namespace Chat.Framework.CQRS
{
    public abstract class ACommand : Request, ICommand
    {
        public abstract void ValidateCommand();
        public CommandResponse CreateResponse()
        {
            return new CommandResponse
            {
                Name = GetType().Name
            };
        }

        public CommandResponse CreateResponse(CommandResponse response)
        {
            response.Name = GetType().Name;
            return response;
        }
    }
}
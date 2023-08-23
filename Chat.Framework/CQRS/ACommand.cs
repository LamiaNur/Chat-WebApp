using Chat.Framework.Enums;
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
                Name = GetType().Name,
                Status = ResponseStatus.Success
            };
        }

        public CommandResponse CreateResponse(CommandResponse response)
        {
            response.Name = GetType().Name;
            return response;
        }
    }
}
using Chat.Framework.Enums;
using Chat.Framework.Mediators;

namespace Chat.Framework.CQRS
{
    public abstract class ACommandHandler<TCommand> : IRequestHandler<TCommand, CommandResponse> where TCommand : ICommand
    {
        protected abstract Task<CommandResponse> OnHandleAsync(TCommand command);

        public async Task<CommandResponse> HandleAsync(TCommand command)
        {
            Console.WriteLine($"OnHandleAsync of : {GetType().Name}\n");
            try
            {
                command.ValidateCommand();
                var response = await OnHandleAsync(command);
                return command.CreateResponse(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                var response = command.CreateResponse();
                response.Message = e.Message;
                response.Status = ResponseStatus.Error;
                return response;
            }
        }
    }
}
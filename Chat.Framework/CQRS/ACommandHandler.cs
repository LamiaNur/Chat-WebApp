using Chat.Framework.Mediators;
using Chat.Framework.Proxy;
using Chat.Framework.Services;

namespace Chat.Framework.CQRS
{
    public abstract class ACommandHandler<TCommand> : IRequestHandler<TCommand, CommandResponse> where TCommand : ICommand
    {
        protected readonly ICommandQueryProxy CommandQueryProxy = DIService.Instance.GetService<ICommandQueryProxy>();

        protected abstract Task<CommandResponse> OnHandleAsync(TCommand command);

        public async Task<CommandResponse> HandleAsync(TCommand command)
        {
            Console.WriteLine($"OnHandleAsync of : {GetType().Name}\n");
            command.ValidateCommand();
            var response = await OnHandleAsync(command);
            return command.CreateResponse(response);
        }
    }
}
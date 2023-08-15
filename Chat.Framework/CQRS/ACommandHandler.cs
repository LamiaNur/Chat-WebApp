using Chat.Framework.Mediators;
using Chat.Framework.Services;

namespace Chat.Framework.CQRS
{
    public abstract class ACommandHandler<TCommand> : IRequestHandler<TCommand, CommandResponse> where TCommand : ICommand
    {
        protected readonly ICommandQueryService CommandQueryService = DIService.Instance.GetService<ICommandQueryService>();

        protected abstract Task<CommandResponse> OnHandleAsync(TCommand command);

        public async Task<CommandResponse> HandleAsync(TCommand command)
        {
            Console.WriteLine($"OnHandleAsync of : {GetType().Name}\n");
            command.ValidateCommand();
            return await OnHandleAsync(command);
        }
    }
}
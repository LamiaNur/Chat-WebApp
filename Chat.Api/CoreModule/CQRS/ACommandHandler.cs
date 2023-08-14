using Chat.Api.CoreModule.Services;

namespace Chat.Api.CoreModule.CQRS
{
    public abstract class ACommandHandler<T> : ICommandHandler where T : ICommand
    {
        public readonly ICommandQueryService _commandQueryService = DIService.Instance.GetService<ICommandQueryService>();
        public async Task<CommandResponse> HandleAsync(ICommand command)
        {
            Console.WriteLine($"OnHandleAsync of : {GetType().Name}\n");
            command.ValidateCommand();
            var response = await OnHandleAsync((T)command);
            Console.WriteLine($"Successfully returned OnHandleAsync of : {GetType().Name}\n");
            return response;
        }
        public abstract Task<CommandResponse> OnHandleAsync(T command);
    }
}
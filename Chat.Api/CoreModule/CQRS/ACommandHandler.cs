using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Services;
using MongoDB.Driver;

namespace Chat.Api.CoreModule.CQRS
{
    public abstract class ACommandHandler<T> : IRequestHandler<T, CommandResponse> where T : ICommand
    {
        protected readonly ICommandQueryService CommandQueryService = DIService.Instance.GetService<ICommandQueryService>();
        public async Task<CommandResponse> HandleAsync(T command)
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
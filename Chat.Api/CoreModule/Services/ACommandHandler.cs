using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Services
{
    public abstract class ACommandHandler<T> : ICommandHandler where T : ICommand
    {
        public readonly ICommandService _commandService = DIService.Instance.GetService<ICommandService>();
        public readonly IQueryService _queryService = DIService.Instance.GetService<IQueryService>();
        public async Task<CommandResponse> HandleAsync(ICommand command, RequestContext requestContext = null)
        {
            Console.WriteLine($"OnHandleAsync of : {this.GetType().Name}\n");
            command.SetValue("RequestContext", requestContext);
            var response = await OnHandleAsync((T)command);
            Console.WriteLine($"Successfully returned OnHandleAsync of : {this.GetType().Name}\n");
            return response;
        }
        public abstract Task<CommandResponse> OnHandleAsync(T command);
    }
}
using System.Composition;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;

namespace Chat.Api.Core.Services
{
    public abstract class ACommandHandler<T> : ICommandHandler where T : ICommand
    {
        [Import] public ICommandService _commandService  {get; set;}
        [Import] public IQueryService _queryService {get; set;}
        public async Task<CommandResponse> HandleAsync(ICommand command)
        {
            Console.WriteLine($"OnHandleAsync of : {this.GetType().Name}\n");
            var response = await OnHandleAsync((T)command);
            Console.WriteLine($"Successfully returned OnHandleAsync of : {this.GetType().Name}\n");
            return response;
        }
        public abstract Task<CommandResponse> OnHandleAsync(T command);
    }
}
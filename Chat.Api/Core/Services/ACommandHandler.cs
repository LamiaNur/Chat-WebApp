using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;

namespace Chat.Api.Core.Services
{
    public abstract class ACommandHandler<T> : ICommandHandler where T : ICommand
    {
        public async Task<CommandResponse> HandleAsync(ICommand command)
        {
            return await OnHandleAsync((T)command);
        }
        public abstract Task<CommandResponse> OnHandleAsync(T command);
    }
}
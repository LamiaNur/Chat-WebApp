using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;

namespace Chat.Api.Core.Services
{
    public abstract class ACommandHandler<T> : ICommandHandler where T : ICommand
    {
        public async Task<CommandResponse> ExecuteAsync(ICommand command)
        {
            return await OnExecuteAsync((T)command);
        }
        public abstract Task<CommandResponse> OnExecuteAsync(T command);
    }
}
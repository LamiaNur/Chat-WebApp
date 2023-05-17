using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;
using Chat.Api.Core.Constants;
using System.Composition;

namespace Chat.Api.Core.Services 
{
    [Export(typeof(ICommandService))]
    [Export("CommandService", typeof(ICommandService))]
    [Shared]
    public class CommandService : ICommandService
    {
        public async Task<CommandResponse> HandleCommandAsync(ICommand command)
        {
            var commandName = command.GetType().Name;
            var handlerName = $"{command}Handler";
            try
            {
                Console.WriteLine($"Before handle command: {commandName}\n");
                Console.WriteLine($"Start Validating Command: {commandName}\n");
                command.ValidateCommand();
                Console.WriteLine($"Success Validating Command: {commandName}\n");
                Console.WriteLine($"Start Resolving CommandHandler: {handlerName}\n");
                var handler = DIService.Instance.GetService<ICommandHandler>(handlerName);
                if (handler == null)
                {
                    throw new Exception("Handler not found");
                }
                Console.WriteLine($"Success Resolving CommandHandler: {handlerName}\n");
                var response = await handler.HandleAsync(command);
                if (string.IsNullOrEmpty(response.Status)) 
                {
                    response.Status = ResponseStatus.Success;
                }
                response.Name = commandName;
                Console.WriteLine($"After Successful handle command : {commandName}\n");
                return response;
            }
            catch (Exception ex)
            {
                var response = command.CreateResponse();
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
                Console.WriteLine($"After failed Execution command : {command.GetType().Name}\n");
                return response;
            }
        }
    }
}
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
            try
            {
                Console.WriteLine($"Before Execute command: {command.GetType().Name}\n");
                Console.WriteLine($"Start Validating Command: {command.GetType().Name}\n");
                command.ValidateCommand();
                Console.WriteLine($"Success Validating Command: {command.GetType().Name}\n");
                
                var commandHandlerName = command.GetType().Name + "Handler";
                Console.WriteLine($"Start Resolving CommandHandler: {commandHandlerName}\n");
                var handler = DIService.Instance.GetService<ICommandHandler>(commandHandlerName);
                if (handler == null)
                {
                    throw new Exception("Handler null here");
                }
                Console.WriteLine($"Success Resolving CommandHandler: {commandHandlerName}\n");
                
                var response = await handler.HandleAsync(command);
                if (string.IsNullOrEmpty(response.Status)) 
                {
                    response.Status = ResponseStatus.Success;
                }
                
                response.Name = command.GetType().Name;
                
                Console.WriteLine($"After Successful Execution command : {command.GetType().Name}\n");
                
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
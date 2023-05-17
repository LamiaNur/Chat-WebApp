using Chat.Api.Core.Constants;
using Chat.Api.Core.Helpers;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;
using Chat.Api.FileStoreService.Interfaces;
using Chat.Api.FileStoreService.Models;

namespace Chat.Api.FileStoreService.Commands
{
    public class UploadFileCommand : ACommand
    {
        public IFormFile FormFile {get; set;}
        public override void ValidateCommand()
        {
            if (FormFile == null)
            {
                throw new Exception("File is null");
            }
        }
    }
}
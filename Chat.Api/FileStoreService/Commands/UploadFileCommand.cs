using Chat.Api.Core.Models;

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
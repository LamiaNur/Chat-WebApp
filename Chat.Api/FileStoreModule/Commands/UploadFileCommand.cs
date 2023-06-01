using Chat.Api.CoreModule.Models;

namespace Chat.Api.FileStoreModule.Commands
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
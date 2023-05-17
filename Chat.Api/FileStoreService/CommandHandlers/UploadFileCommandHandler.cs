using System.Composition;
using Chat.Api.Core.Constants;
using Chat.Api.Core.Helpers;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;
using Chat.Api.FileStoreService.Interfaces;
using Chat.Api.FileStoreService.Models;

namespace Chat.Api.FileStoreService.Commands
{
    [Export(typeof(ICommandHandler))]
    [Export("UploadFileCommandHandler", typeof(ICommandHandler))]
    [Shared]
    public class UploadFileCommandHandler : ACommandHandler<UploadFileCommand>
    {
        private IFileRepository _fileRepository;

        public UploadFileCommandHandler()
        {
            _fileRepository = DIService.Instance.GetService<IFileRepository>();
        }
        
        public override async Task<CommandResponse> OnHandleAsync(UploadFileCommand command)
        {
            var response = command.CreateResponse();
            var file = command.FormFile;
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "FileStoreService\\Store");
            if (file.Length <= 0) 
            {
                throw new Exception("File Length 0");
            }
            var fileName = file.FileName;
            var fileId = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(fileName);
            var fullPath = Path.Combine(pathToSave, fileId + extension);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var fileModel = new FileModel()
            {
                Id = fileId,
                Extension = extension,
                Url = fullPath,
                UploadedAt = DateTime.UtcNow,
                Name = fileName
            };
            if (!await _fileRepository.SaveFileModelAsync(fileModel)) 
            {
                throw new Exception("File Save error to db");
            }
            response.Message = "File uploaded successfully";
            response.SetData("FileId", fileModel.Id);
            return response;
        }
    }
}
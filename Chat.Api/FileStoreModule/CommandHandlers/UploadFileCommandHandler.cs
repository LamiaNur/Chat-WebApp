using System.Composition;
using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Models;
using Chat.Api.CoreModule.Services;
using Chat.Api.FileStoreModule.Commands;
using Chat.Api.FileStoreModule.Interfaces;
using Chat.Api.FileStoreModule.Models;

namespace Chat.Api.FileStoreModule.CommandHandlers
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
            var pathToSave = "FileStoreService\\Store";
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
            var requestContext = command.GetValue<RequestContext>("RequestContext");
            
            var fileModel = new FileModel()
            {
                Id = fileId,
                Extension = extension,
                Url = fullPath,
                UploadedAt = DateTime.UtcNow,
                Name = fileName,
                UserId = requestContext.CurrentUser.Id
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
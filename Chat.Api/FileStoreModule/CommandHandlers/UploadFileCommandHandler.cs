using System.Composition;
using Chat.Api.FileStoreModule.Commands;
using Chat.Api.FileStoreModule.Interfaces;
using Chat.Api.FileStoreModule.Models;
using Chat.Api.IdentityModule.Extensions;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;
using Chat.Framework.Services;

namespace Chat.Api.FileStoreModule.CommandHandlers
{
    [Export("UploadFileCommandHandler", typeof(IRequestHandler))]
    [Shared]
    public class UploadFileCommandHandler : ACommandHandler<UploadFileCommand>
    {
        private readonly IFileRepository _fileRepository;

        public UploadFileCommandHandler()
        {
            _fileRepository = DIService.Instance.GetService<IFileRepository>();
        }
        
        protected override async Task<CommandResponse> OnHandleAsync(UploadFileCommand command)
        {
            var response = command.CreateResponse();
            var file = command.FormFile;
            var pathToSave = "FileStoreModule\\Store";
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

            var currentUser = command.GetRequestContext()?.GetCurrentUserProfile();
            var fileModel = new FileModel()
            {
                Id = fileId,
                Extension = extension,
                Url = fullPath,
                UploadedAt = DateTime.UtcNow,
                Name = fileName,
                UserId = currentUser?.Id ?? ""
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
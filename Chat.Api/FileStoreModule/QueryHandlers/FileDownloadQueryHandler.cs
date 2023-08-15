using System.Composition;
using Chat.Api.CoreModule.CQRS;
using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Services;
using Chat.Api.FileStoreModule.Interfaces;
using Chat.Api.FileStoreModule.Models;
using Chat.Api.FileStoreModule.Queries;

namespace Chat.Api.FileStoreModule.QueryHandlers
{
    [Export("FileDownloadQueryHandler", typeof(IRequestHandler))]
    [Shared]
    public class FileDownloadQueryHandler : AQueryHandler<FileDownloadQuery>
    {
        private readonly IFileRepository _fileRepository;
        public FileDownloadQueryHandler()
        {
            _fileRepository = DIService.Instance.GetService<IFileRepository>();
        }

        protected override async Task<QueryResponse> OnHandleAsync(FileDownloadQuery query)
        {
            var response = query.CreateResponse();
            var fileModel = await _fileRepository.GetFileModelByIdAsync(query.FileId);
            if (fileModel == null)
            {
                throw new Exception("File not found");
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileModel.Url);
            var fileDownloadResult = new FileDownloadResult
            {
                FileModel = fileModel,
                ContentType = GetContentType(fileModel.Extension)
            };
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                fileDownloadResult.FileBytes = new byte[fileStream.Length];
                await fileStream.ReadAsync(fileDownloadResult.FileBytes, 0 , fileDownloadResult.FileBytes.Length);
            }
            response.AddItem(fileDownloadResult);
            return response;
        }

        private string GetContentType(string fileExtension)
        {
            fileExtension = fileExtension.Replace(".", "");
            return $"image/{fileExtension}";
        }
    }
}
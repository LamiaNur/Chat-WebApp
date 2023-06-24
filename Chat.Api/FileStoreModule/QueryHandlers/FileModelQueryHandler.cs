using System.Composition;
using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Models;
using Chat.Api.CoreModule.Services;
using Chat.Api.FileStoreModule.Interfaces;
using Chat.Api.FileStoreModule.Queries;

namespace Chat.Api.FileStoreModule.QueryHandlers
{
    [Export(typeof(IQueryHandler))]
    [Export("FileModelQueryHandler", typeof(IQueryHandler))]
    [Shared]
    public class FileModelQueryHandler : AQueryHandler<FileModelQuery>
    {
        private readonly IFileRepository _fileRepository;
        
        public FileModelQueryHandler()
        {
            _fileRepository = DIService.Instance.GetService<IFileRepository>();
        }
        
        public override async Task<QueryResponse> OnHandleAsync(FileModelQuery query)
        {
            var response = query.CreateResponse();
            var fileModel = await _fileRepository.GetFileModelByIdAsync(query.FileId);
            if (fileModel == null) 
            {
                throw new Exception("File not found");
            }
            var path = Directory.GetCurrentDirectory();
            fileModel.Url = Path.Combine(path, fileModel.Url);
            response.AddItem(fileModel);
            return response;
        }
    }
}
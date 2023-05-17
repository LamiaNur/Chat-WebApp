using Chat.Api.Core.Models;
using Chat.Api.FileStoreService.Interfaces;
using Chat.Api.Core.Services;
using Chat.Api.Core.Interfaces;
using System.Composition;

namespace Chat.Api.FileStoreService.Queries
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
            response.AddItem(fileModel);
            return response;
        }
    }
}
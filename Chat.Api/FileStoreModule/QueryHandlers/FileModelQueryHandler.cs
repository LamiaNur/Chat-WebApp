using System.Composition;
using Chat.Api.FileStoreModule.Interfaces;
using Chat.Api.FileStoreModule.Queries;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;
using Chat.Framework.Services;

namespace Chat.Api.FileStoreModule.QueryHandlers
{
    [Export("FileModelQueryHandler", typeof(IRequestHandler))]
    [Shared]
    public class FileModelQueryHandler : AQueryHandler<FileModelQuery>
    {
        private readonly IFileRepository _fileRepository;
        
        public FileModelQueryHandler()
        {
            _fileRepository = DIService.Instance.GetService<IFileRepository>();
        }
        
        protected override async Task<QueryResponse> OnHandleAsync(FileModelQuery query)
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
using Chat.Api.FileStoreModule.Interfaces;
using Chat.Api.FileStoreModule.Queries;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;

namespace Chat.Api.FileStoreModule.QueryHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class FileModelQueryHandler : AQueryHandler<FileModelQuery>
    {
        private readonly IFileRepository _fileRepository;
        
        public FileModelQueryHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
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
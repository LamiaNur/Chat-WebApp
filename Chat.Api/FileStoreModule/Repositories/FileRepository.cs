using System.Composition;
using Chat.Api.CoreModule.Database.Interfaces;
using Chat.Api.CoreModule.Database.Models;
using Chat.Api.CoreModule.Services;
using Chat.Api.FileStoreModule.Interfaces;
using Chat.Api.FileStoreModule.Models;

namespace Chat.Api.FileStoreModule.Repositories
{
    [Export(typeof(IFileRepository))]
    [Shared]
    public class FileRepository : IFileRepository
    {
        private readonly DatabaseInfo _databaseInfo;
        private readonly IMongoDbContext _dbContext;
        
        public FileRepository()
        {
            _databaseInfo = DIService.Instance.GetConfiguration().GetSection("DatabaseInfo").Get<DatabaseInfo>();
            _dbContext = DIService.Instance.GetService<IMongoDbContext>();
        }
        
        public async Task<bool> SaveFileModelAsync(FileModel fileModel) 
        {
            return await _dbContext.SaveItemAsync(_databaseInfo, fileModel);
        }

        public async Task<FileModel?> GetFileModelByIdAsync(string id) 
        {
            return await _dbContext.GetItemByIdAsync<FileModel>(_databaseInfo, id);
        }
    }
}
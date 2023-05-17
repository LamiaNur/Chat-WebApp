using System.Composition;
using Chat.Api.Core.Database.Interfaces;
using Chat.Api.Core.Database.Models;
using Chat.Api.Core.Services;
using Chat.Api.FileStoreService.Interfaces;
using Chat.Api.FileStoreService.Models;

namespace Chat.Api.FileStoreService.Repositories
{
    [Export(typeof(IFileRepository))]
    [Export("FileRepository", typeof(IFileRepository))]
    [Shared]
    public class FileRepository : IFileRepository
    {
        private readonly DatabaseInfo _databaseInfo;
        private readonly IMongoDbContext _dbContext;
        
        public FileRepository()
        {
            _databaseInfo = DIService.Instance.GetService<IConfiguration>().GetSection("DatabaseInfo").Get<DatabaseInfo>();
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
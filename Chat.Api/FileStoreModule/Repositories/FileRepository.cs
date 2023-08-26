using Chat.Api.FileStoreModule.Interfaces;
using Chat.Api.FileStoreModule.Models;
using Chat.Framework.Attributes;
using Chat.Framework.Database.Interfaces;
using Chat.Framework.Database.Models;

namespace Chat.Api.FileStoreModule.Repositories
{
    [ServiceRegister(typeof(IFileRepository), ServiceLifetime.Singleton)]
    public class FileRepository : IFileRepository
    {
        private readonly DatabaseInfo _databaseInfo;
        private readonly IMongoDbContext _dbContext;
        
        public FileRepository(IMongoDbContext mongoDbContext, IConfiguration configuration)
        {
            _databaseInfo = configuration.GetSection("DatabaseInfo").Get<DatabaseInfo>();
            _dbContext = mongoDbContext;
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
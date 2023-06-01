using Chat.Api.FileStoreModule.Models;

namespace Chat.Api.FileStoreModule.Interfaces
{
    public interface IFileRepository
    {
        Task<bool> SaveFileModelAsync(FileModel fileModel);
        Task<FileModel?> GetFileModelByIdAsync(string id);
    }
}
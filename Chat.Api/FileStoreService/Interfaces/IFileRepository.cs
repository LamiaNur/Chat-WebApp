using Chat.Api.FileStoreService.Models;

namespace Chat.Api.FileStoreService.Interfaces
{
    public interface IFileRepository
    {
        Task<bool> SaveFileModelAsync(FileModel fileModel);
        Task<FileModel?> GetFileModelByIdAsync(string id);
    }
}
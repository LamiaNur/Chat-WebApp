using Chat.Framework.Database.Models;
using MongoDB.Driver;

namespace Chat.Framework.Database.Interfaces
{
    public interface IMongoDbContext
    {
        Task<bool> SaveItemAsync<T>(DatabaseInfo databaseInfo, T item) where T : class, IRepositoryItem;
        Task<bool> DeleteItemByIdAsync<T>(DatabaseInfo databaseInfo, string id) where T : class, IRepositoryItem;
        Task<T?> GetItemByIdAsync<T>(DatabaseInfo databaseInfo, string id) where T : class, IRepositoryItem;
        Task<List<T>> GetItemsAsync<T>(DatabaseInfo databaseInfo) where T : class, IRepositoryItem;
        Task<T?> GetItemByFilterDefinitionAsync<T>(DatabaseInfo databaseInfo, FilterDefinition<T> filterDefinition) where T : class, IRepositoryItem;
        Task<bool> DeleteItemsByFilterDefinitionAsync<T>(DatabaseInfo databaseInfo, FilterDefinition<T> filterDefinition) where T : class, IRepositoryItem;
        Task<List<T>> GetItemsByFilterDefinitionAsync<T>(DatabaseInfo databaseInfo, FilterDefinition<T> filterDefinition) where T : class, IRepositoryItem;
        Task<List<T>> GetItemsByFilterDefinitionAsync<T>(DatabaseInfo databaseInfo, FilterDefinition<T> filterDefinition, int offset, int limit) where T : class, IRepositoryItem;
    }
}
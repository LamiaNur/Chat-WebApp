using System.Composition;
using Chat.Api.CoreModule.Database.Interfaces;
using Chat.Api.CoreModule.Database.Models;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Chat.Api.CoreModule.Database.Contexts
{
    [Export(typeof(IMongoDbContext))]
    [Shared]
    public class MongoDbContext : IMongoDbContext
    {
        private readonly Dictionary<string, MongoClient> _dbClients;
        private readonly int MaxLimit;
        public MongoDbContext()
        {
            _dbClients = new Dictionary<string, MongoClient>();
            MaxLimit = 1000;
        }

        private MongoClient? GetClient(string connectionString)
        {
            if (_dbClients.ContainsKey(connectionString))
            {
                return _dbClients[connectionString];
            }
            try
            {
                var mongoClient = new MongoClient(connectionString);
                _dbClients.Add(connectionString, mongoClient);
                return mongoClient;
            }
            catch (Exception)
            {
                var message = $"Client Creation Error. Connection string {connectionString}";
                Console.WriteLine(message);
                return null;
            }
        }

        private IMongoCollection<T>? GetCollection<T>(DatabaseInfo databaseInfo)
        {
            try
            {
                var client = GetClient(databaseInfo.ConnectionString);
                var database = client?.GetDatabase(databaseInfo.DatabaseName);
                return database?.GetCollection<T>(typeof(T).Name);
            }
            catch (Exception)
            {
                Console.WriteLine($"Get Collection : {typeof(T).Name} from Database : {databaseInfo.DatabaseName} Error");
                return null;
            }
        }

        public async Task<bool> SaveItemAsync<T>(DatabaseInfo databaseInfo, T item) where T : class, IRepositoryItem
        {
            try
            {
                var collection = GetCollection<T>(databaseInfo);
                if (collection == null)
                {
                    throw new Exception("Collection null");
                } 
                var filter = Builders<T>.Filter.Eq("Id", item.Id);
                await collection.ReplaceOneAsync(filter, item, new ReplaceOptions {IsUpsert = true});
                Console.WriteLine($"Successfully Save Item : {JsonConvert.SerializeObject(item)}\n");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Problem Save Item : {JsonConvert.SerializeObject(item)}\n{ex.Message}\n");
                return false;
            }
        }

        public async Task<bool> DeleteItemByIdAsync<T>(DatabaseInfo databaseInfo, string id) where T : class, IRepositoryItem
        {
            try
            {
                var collection = GetCollection<T>(databaseInfo);
                if (collection == null) 
                {
                    throw new Exception("Collection null");
                }
                var filter = Builders<T>.Filter.Eq("Id", id);
                var res = await collection.DeleteOneAsync(filter);
                if (res != null && res.DeletedCount > 0) 
                {
                    Console.WriteLine($"Successfully Item Deleted, Id: {id}\n");
                    return true;
                }
                throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine($"Problem Delete Item, Id : {id}\n");
                return false;
            }
        }

        public async Task<T?> GetItemByIdAsync<T>(DatabaseInfo databaseInfo, string id) where T : class, IRepositoryItem
        {
            try
            {
                var collection = GetCollection<T>(databaseInfo);
                if (collection == null) 
                {
                    throw new Exception("Collection null");
                }
                var filter = Builders<T>.Filter.Eq("Id", id);
                var items = await collection.FindAsync<T>(filter);
                var item = await items.FirstOrDefaultAsync<T>();
                Console.WriteLine($"Successfully Get Item : {JsonConvert.SerializeObject(item)}\n");
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Problem Get Item, id : {id}\n{ex.Message}\n");
                return default(T);
            }
        }

        public async Task<List<T>> GetItemsAsync<T>(DatabaseInfo databaseInfo) where T : class, IRepositoryItem
        {
            try
            {
                var collection = GetCollection<T>(databaseInfo);
                if (collection == null) 
                {
                    throw new Exception("Collection null");
                }
                var filter = Builders<T>.Filter.Empty;
                var itemsCursor = await collection.FindAsync<T>(filter);
                var items = await itemsCursor.ToListAsync<T>();
                Console.WriteLine($"Successfully Get items, Count: {items.Count}\n");
                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Problem Get Items\n{ex.Message}\n");
                return new List<T>();
            }
        }

        public async Task<T?> GetItemByFilterDefinitionAsync<T>(DatabaseInfo databaseInfo, FilterDefinition<T> filterDefinition) where T : class, IRepositoryItem
        {
            try
            {
                var collection = GetCollection<T>(databaseInfo);
                if (collection == null) 
                {
                    throw new Exception("Collection null");
                }
                var items = await collection.FindAsync<T>(filterDefinition);
                var item = await items.FirstAsync<T>();
                Console.WriteLine($"Successfully Get Item by filter : {JsonConvert.SerializeObject(item)}\n");
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Problem Get Item by fiter \n{ex.Message}\n");
                return default(T);
            }
        }

        public async Task<bool> DeleteItemsByFilterDefinitionAsync<T>(DatabaseInfo databaseInfo, FilterDefinition<T> filterDefinition) where T : class, IRepositoryItem
        {
            try
            {
                var collection = GetCollection<T>(databaseInfo);
                if (collection == null) 
                {
                    throw new Exception("Collection null");
                }
                var res = await collection.DeleteManyAsync(filterDefinition);
                if (res != null && res.DeletedCount > 0) 
                {
                    Console.WriteLine($"Successfully Delete Items, count : {JsonConvert.SerializeObject(res.DeletedCount)}\n");
                    return true;
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Problem Delete Items by fiter \n{ex.Message}\n");
                return false;
            }
        }

        public async Task<List<T>> GetItemsByFilterDefinitionAsync<T>(DatabaseInfo databaseInfo, FilterDefinition<T> filterDefinition) where T : class, IRepositoryItem
        {
            try
            {
                var collection = GetCollection<T>(databaseInfo);
                if (collection == null) 
                {
                    throw new Exception("Collection null");
                }
                var itemsCursor = await collection.FindAsync<T>(filterDefinition);
                var items = await itemsCursor.ToListAsync();
                Console.WriteLine($"Successfully Get Items by filter count : {items.Count}\n");
                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Problem Get Items by filter\n{ex.Message}\n");
                return new List<T>();
            }
        }

        public async Task<List<T>> GetItemsByFilterDefinitionAsync<T>(DatabaseInfo databaseInfo, FilterDefinition<T> filterDefinition, int offset, int limit) where T : class, IRepositoryItem
        {
            try
            {
                if (limit > MaxLimit) 
                {
                    throw new Exception($"Limit exceeded!");
                }
                var collection = GetCollection<T>(databaseInfo);
                if (collection == null) 
                {
                    throw new Exception("Collection null");
                }
                var itemsCursor = await collection
                    .Find<T>(filterDefinition)
                    .Skip(offset)
                    .Limit(limit)
                    .ToCursorAsync();
                var items = await itemsCursor.ToListAsync();
                Console.WriteLine($"Successfully Get Items by filter count : {items.Count}\n");
                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Problem Get Items by filter\n{ex.Message}\n");
                return new List<T>();
            }
        }
    }
}
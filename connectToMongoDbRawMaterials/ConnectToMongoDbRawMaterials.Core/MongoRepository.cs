using ConnectToMongoDbRawMaterials.Core.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToMongoDbRawMaterials.Core
{
    public class MongoRepository<T> : IMongoClient<T>
    {

        private MongoClient _mongoClient;
        private readonly string _connectionString = "mongodb+srv://tomdyd:tomdyd7583@cluster0.2ggdhx2.mongodb.net/";
        private IMongoDatabase _database;
        private string _databaseName = "LabStore";
        private string _collectionName = "RawMaterialsCollection";

        public void Connect()
        {
            try
            {
                Console.WriteLine("Nawiązywanie połączenia z bazą danych...");
                _mongoClient = new MongoClient(_connectionString);
                _database = _mongoClient.GetDatabase(_databaseName);
            }
            catch
            {
                Console.WriteLine("BŁĄD POŁĄCZENIA");
            }
        }

        public IMongoCollection<T> GetCollection()
        {
            try
            {
                return _database.GetCollection<T>(_collectionName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania kolekcji z bazy danych MongoDB: {ex.Message}");
                return null;
            }
        }
        public void addToDb(T input)
        {
            try
            {
                var collection = GetCollection();
                collection.InsertOne(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas dodawania do bazy danych MongoDB: {ex.Message}");
            }
        }
        public List<T> GetDataList()
        {
            var collection = GetCollection();
            var result = collection.Find(_ => true).ToList();
            return result;
        }

        public T GetData(string searchTerm)
        {
            var collection = GetCollection();
            var filter = Builders<T>.Filter.Eq("_index", searchTerm);
            var result = collection.Find(filter).FirstOrDefault();
            return result;

        }

        public void UpdateData(string property, string searchTerm, T updatingData)
        {
            var filter = Builders<T>.Filter.Eq(property, searchTerm);
            var collection = GetCollection();
            collection.ReplaceOne(filter, updatingData);
        }

        public void DeleteData(string searchTerm)
        {
            var filter = Builders<T>.Filter.Eq("_index", searchTerm);
            var collection = GetCollection();
            collection.DeleteOne(filter);
        }
    }
}

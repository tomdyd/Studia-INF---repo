using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Models;

public class MongoDbDatabaseConnection<T> : IDatabaseConnection<T>
{
    private MongoClient _mongoClient;
    private IMongoDatabase _database;
    private string _collectionName;

    private List<T> _users;

    public void Connect(string connectionString, string databaseName, string collectionName)
    {
        try
        {
            // Implementacja kodu do nawiązywania połączenia z bazą danych MongoDB.
            _mongoClient = new MongoClient(connectionString);
            _database = _mongoClient.GetDatabase(databaseName);
            _collectionName = collectionName;
            Console.WriteLine("Połączenie z bazą danych MongoDB nawiązane.");
        }
        catch
        {
            Console.WriteLine("BŁĄD POŁĄCZENIA");
        }
    }

    public void Disconnect()
    {
        // Implementacja kodu do zamykania połączenia z bazą danych MongoDB.
        Console.WriteLine("Połączenie z bazą danych MongoDB zamknięte.");
    }

    public IMongoCollection<T> GetCollection(string collectionName)
    {
        try
        {
            return _database.GetCollection<T>(collectionName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas pobierania kolekcji z bazy danych MongoDB: {ex.Message}");
            return null;
        }
    }

    public void AddToDb(T input)
    {
        try
        {
            var collection = GetCollection(_collectionName);
            collection.InsertOne(input);
            Console.WriteLine("Dane zostały zapisane w bazie danych.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas dodawania do bazy danych MongoDB: {ex.Message}");
        }
    }
    public T GetDataT()
    {
        var filter = Builders<T>.Filter.Eq("Username", "testUser");
        var collection = GetCollection(_collectionName);
        var result = collection.Find(filter).FirstOrDefault();
        return result;
    }

    List<T> IDatabaseConnection<T>.GetData()
    {
        return null;
    }
}

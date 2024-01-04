using MongoDB.Driver;
using projectDydaTomaszCore.Models;

namespace projectDydaTomaszCore.Interfaces
{
    public interface IDatabaseConnection<T>
    {
        public void Connect(string connectionString, string databaseName, string collectionName); // Metoda do nawiązywania połączenia z bazą danych.

        public void Disconnect(); // Metoda do zamykania połączenia z bazą danych.
        public IMongoCollection<T> GetCollection(string collectionName);
        public void AddToDb(T input);

        public List<T> ReadFromDb();

        // Dodaj inne metody związane z operacjami bazodanowymi, jeśli to konieczne.
    }
}
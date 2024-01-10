using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Models;
using System.Data.SQLite;
using System.Security.Cryptography;

namespace projectDydaTomasz.Core
{
    public class SqlitedatabaseConnection<T> : IDatabaseConnection<T>
    {
        private readonly string _connectionString = "Data Source=C:\\Users\\t.dyda\\Documents\\Studia-INF---repo\\Semestr III\\Programowanie - Szematowicz\\sqlite.db;Version=3";
        public void AddToDb(T item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "INSERT INTO users (userId, username, passwordHash, email) VALUES ((lower(hex(randomblob(4))) || '-' || lower(hex(randomblob(2))) || '-4' || substr(lower(hex(randomblob(2))),2) || '-a' || substr('89ab',abs(random()) % 4 + 1,1) || '-6' || substr(lower(hex(randomblob(2))),2) || lower(hex(randomblob(6)))), @username, @passwordHash, @email)";                                   

                    foreach (var property in typeof(T).GetProperties())
                    {
                        cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(item));
                    }                   

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Connect(string connectionString, string databaseName, string collectionName)
        {
            throw new NotImplementedException();
        }

        public void DeleteData(string property, string searchTerm)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public List<T> GetAllDataList()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                List<T> dataList;
                connection.Open();

                using(var cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "SELECT * FROM users";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            while(reader.Read())
                            {
                                foreach (var property in typeof(T).GetProperties())
                                {
                                    
                                    cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(item));
                                }
                                    T dataItem = mapFunction(reader);
                                dataList.Add(dataItem);
                            }
                        }
                    }
                }

                
            }
        }

        public IMongoCollection<T> GetCollection(string collectionName)
        {
            throw new NotImplementedException();
        }

        public T GetFilteredData(string property, string searchingTerm)
        {
            throw new NotImplementedException();
        }

        public List<T> GetFilteredDataList(string property, string searchingTerm)
        {
            throw new NotImplementedException();
        }

        public void UpdateData(string property, string searchTerm, T updatingData)
        {
            throw new NotImplementedException();
        }
    }
}

using MongoDB.Driver;
using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Models;
using SharpCompress.Common;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.SQLite;
using System.Reflection.Metadata;

namespace projectDydaTomasz.Core
{
    public class SqlitedatabaseConnection<T> : IDatabaseConnection<T>
    {
        private readonly string _connectionString = "Data Source=D:\\Dane\\Studia INF - repo\\Semestr III\\Programowanie - Szematowicz\\sqlite.db;Version=3";
        public void AddToDb(T item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "INSERT INTO users (userId, username, passwordHash, email) VALUES ((lower(hex(randomblob(4))) || '-' || lower(hex(randomblob(2))) || '-4' || substr(lower(hex(randomblob(2))),2) || '-a' || substr('89ab',abs(random()) % 4 + 1,1) || '-6' || substr(lower(hex(randomblob(2))),2) || lower(hex(randomblob(6)))), @username, @passwordHash, @email)";

                    //var data = typeof(T).GetProperty("username");
                    //var property = data.Name;
                    //var value = data.GetValue(item);

                    //Console.WriteLine($"property: {property}, value: {value}");

                    foreach (var property in typeof(T).GetProperties())
                    {
                        cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(item));
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteData(string property, string searchTerm)
        {
            var dataname = typeof(T).Name;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = $"DELETE FROM {dataname}s WHERE {property} = @searchTerm";
                    cmd.Parameters.AddWithValue("@searchTerm", searchTerm);

                    cmd.ExecuteReader();                    
                }
            }
        }

        public List<T> GetAllDataList()
        {
            List<T> dataList = new List<T>();
            var dataname = typeof(T).Name; //pobiera nazwę klasy która później przekazuje jako nazwę tabeli do przeszukiwania

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = $"SELECT * FROM {dataname}s";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T data = Activator.CreateInstance<T>();

                            foreach (var prop in typeof(T).GetProperties())
                            {
                                prop.SetValue(data, reader[prop.Name]);
                            }

                            dataList.Add(data);
                        }

                    }
                }
            }
            return dataList;
        }

        public T GetFilteredData(string property, string searchingTerm)
        {
            var dataname = typeof(T).Name;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = $"SELECT * FROM {dataname}s WHERE {property} = @searchingTerm";
                    cmd.Parameters.AddWithValue("@searchingTerm", searchingTerm);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            T data = Activator.CreateInstance<T>();

                            foreach (var prop in typeof(T).GetProperties())
                            {
                                prop.SetValue(data, reader[prop.Name]);
                            }

                            return data;

                        }
                    }
                }
            }
            return default;
        }

        public List<T> GetFilteredDataList(string property, string searchingTerm)
        {
            List<T> dataList = new List<T>();
            var dataType = typeof(T).Name;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = $"SELECT * FROM {dataType}s WHERE {property} = @searchingTerm";
                    cmd.Parameters.AddWithValue("@searchingTerm", searchingTerm);

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            T data = Activator.CreateInstance<T>();

                            foreach (var prop in typeof(T).GetProperties())
                            {
                                prop.SetValue(data, reader[prop.Name]);
                            }

                            dataList.Add(data);
                        }
                    }
                }
            }
            return dataList;
        }

        public void UpdateData(string property, string searchTerm, T updatingData)
        {
            var dataType = typeof(T).Name;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(connection))
                {
                    var properties = updatingData.GetType().GetProperties();
                    string setValues = string.Join(", ", properties.Select(prop => $"{prop.Name} = @{prop.Name}"));

                    cmd.CommandText = $"UPDATE {dataType}s SET {setValues} WHERE {property} = @{searchTerm}";

                    foreach (var prop in properties)
                    {
                        cmd.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(updatingData));
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

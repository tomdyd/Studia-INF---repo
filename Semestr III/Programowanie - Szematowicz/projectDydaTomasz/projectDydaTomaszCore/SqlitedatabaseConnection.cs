using MongoDB.Driver;
using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Models;
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
            throw new NotImplementedException();
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
                        if (reader.Read())
                        {
                            while (reader.Read())
                            {
                                T data = Activator.CreateInstance<T>();

                                if (typeof(T) == typeof(User))
                                {
                                    User user = data as User;
                                    user.username = reader["username"].ToString();
                                    user.passwordHash = reader["passwordHash"].ToString();
                                    user.email = reader["email"].ToString();
                                }

                                dataList.Add(data);
                            }
                        }
                    }
                }
            }
            return dataList;
        }

        public T GetFilteredData(string property, string searchingTerm)
        {
            List<T> dataList = new List<T>();
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

                            if (typeof(T) == typeof(User))
                            {
                                User user = data as User;
                                user.username = reader["username"].ToString();
                                user.passwordHash = reader["passwordHash"].ToString();
                                user.email = reader["email"].ToString();

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
            var dataname = typeof(T).Name;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = $"SELECT * FROM {dataname}s WHERE {property} = {searchingTerm}";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            while (reader.Read())
                            {
                                T data = Activator.CreateInstance<T>();

                                if (typeof(T) == typeof(User))
                                {
                                    User user = data as User;
                                    user.username = reader["username"].ToString();
                                    user.passwordHash = reader["passwordHash"].ToString();
                                    user.email = reader["email"].ToString();
                                }

                                dataList.Add(data);
                            }
                        }
                    }
                }
            }
            return null;
        } //to check

        public void UpdateData(string property, string searchTerm, T updatingData)
        {
            throw new NotImplementedException();
        }
    }
}

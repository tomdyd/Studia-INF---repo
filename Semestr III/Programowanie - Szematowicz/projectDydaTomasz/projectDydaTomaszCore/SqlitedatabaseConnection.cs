﻿using MongoDB.Driver;
using projectDydaTomasz.Core.Models;
using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Models;
using SharpCompress.Common;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Reflection.Metadata;

namespace projectDydaTomasz.Core
{
    public class SqlitedatabaseConnection<T> : IDatabaseConnection<T>
    {
        private readonly string _connectionString = "Data Source=" + Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\sqlite.db;Version=3";

        public void AddToDb(T item)
        {
            Console.WriteLine(_connectionString);

            var tableName = typeof(T).Name;
            var properties = item.GetType().GetProperties().ToArray();
            var setValues = string.Join(", ", properties.Select(prop => prop.Name));
            var setValues1 = "@" + string.Join(", @", properties.Select(prop => prop.Name));

            setValues1 = setValues1.Remove(0, 8);

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(connection))
                {
                    var idGenerator = "(lower(hex(randomblob(4))) || '-' || lower(hex(randomblob(2))) || '-4' || substr(lower(hex(randomblob(2))),2) || '-a' || substr('89ab',abs(random()) % 4 + 1,1) || '-6' || substr(lower(hex(randomblob(2))),2) || lower(hex(randomblob(6))))";
                    cmd.CommandText = $"INSERT INTO {tableName}s ({setValues}) VALUES ({idGenerator}, {setValues1})";
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
            var dataname = typeof(T).Name;

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

        public List<Car> GetFilteredDataList(string property, string searchingTerm)
        {
            List<Car> dataList = new List<Car>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new SQLiteCommand(connection))
                {
                    var query = "SELECT Cars.carId, Cars.carNumber, Cars.carBrand, Cars.carModel," +
                        "Cars.carProductionYear, Cars.engineCapacity, Users.userId, Users.username, Users.passwordHash, Users.email FROM Cars " +
                          "INNER JOIN Users ON Cars.user = Users.userId " +
                          "WHERE carId = @carId";

                    cmd.CommandText = $"SELECT * FROM Cars INNER JOIN Users ON Cars.user = Users.userId WHERE carId = @carId";
                    cmd.Parameters.AddWithValue("carId", searchingTerm);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string carId = reader["carId"].ToString();
                            string carNumber = reader["carNumber"].ToString();
                            string carBrand = reader["carBrand"].ToString();
                            string carModel = reader["carModel"].ToString();
                            string carProductionYear = reader["carProductionYear"].ToString();
                            string engineCapacity = reader["engineCapacity"].ToString();
                            string userId = reader["userId"].ToString();
                            string username = reader["username"].ToString();
                            string passwordHash = reader["passwordHash"].ToString();
                            string email = reader["email"].ToString();

                            var user = new User()
                            {
                                userId = userId,
                                username = username,
                                passwordHash = passwordHash,
                                email = email
                            };

                            var newCar = new Car()
                            {
                                carId = carId,
                                carNumber = int.Parse(carNumber),
                                carBrand = carBrand,
                                carModel = carModel,
                                carProductionYear = carProductionYear,
                                engineCapacity = engineCapacity,
                                user = user
                            };

                            dataList.Add(newCar);
                        }
                    }
                }
            }           

                //var dataType = typeof(T).Name;
                //using (var connection = new SQLiteConnection(_connectionString))
                //{
                //    connection.Open();

                //    using (var cmd = new SQLiteCommand(connection))
                //    {
                //        cmd.CommandText = $"SELECT * FROM {dataType}s WHERE {property} = @searchingTerm";
                //        cmd.Parameters.AddWithValue("@searchingTerm", searchingTerm);

                //        using (var reader = cmd.ExecuteReader())
                //        {

                //            while (reader.Read())
                //            {
                //                T data = Activator.CreateInstance<T>();

                //                foreach (var prop in typeof(T).GetProperties())
                //                {
                //                    prop.SetValue(data, reader[prop.Name]);
                //                }

                //                dataList.Add(data);
                //            }
                //        }
                //    }
                //}

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

﻿using System;
using System.Data.SqlClient;

namespace DatabaseConnectionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=178.157.15.66;Integrated Security=False;Initial Catalog=evodata18;User ID=userevodata18;Password=0u7zV3S(cg)b47LjhnE5YcOdA!28Lj7EW9;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Veritabanına bağlantı başarılı.");

                    // Tablo ve sütun bilgilerini tutacak listeler
                    List<string> tableNames = new List<string>();
                    Dictionary<string, List<string>> columnNames = new Dictionary<string, List<string>>();

                    // Tablo bilgilerini almak için sorgu
                    string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tableName = reader["TABLE_NAME"].ToString();
                            tableNames.Add(tableName);
                        }
                    }

                    // Her bir tablo için sütun bilgilerini almak için sorgu
                    foreach (string tableName in tableNames)
                    {
                        query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<string> columns = new List<string>();
                            while (reader.Read())
                            {
                                string columnName = reader["COLUMN_NAME"].ToString();
                                columns.Add(columnName);
                            }
                            columnNames.Add(tableName, columns);
                        }
                    }

                    // Tablo ve sütun bilgilerini yazdır
                    foreach (var kvp in columnNames)
                    {
                        Console.WriteLine($"Tablo: {kvp.Key}");
                        foreach (string columnName in kvp.Value)
                        {
                            Console.WriteLine($"  Sütun: {columnName}");
                        }
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bağlantı hatası: " + ex.Message);
                }
            }
        }
    }
}


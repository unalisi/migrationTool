using System;
using System.Data.SqlClient;                    //  AND TABLE_NAME NOT LIKE 'LOG_%'

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

                    // Tablo bilgilerini almak için sorgu
                    string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'  AND TABLE_NAME NOT LIKE 'LOG_%'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tableName = reader["TABLE_NAME"].ToString();
                            Console.WriteLine($"Tablo: {tableName}");

                            // Her bir tablo için sütun bilgilerini ve verilerini almak için sorgu
                            string columnQuery = $"SELECT * FROM {tableName}";
                            using (SqlCommand columnCommand = new SqlCommand(columnQuery, connection))
                            using (SqlDataReader columnReader = columnCommand.ExecuteReader())
                            {
                                List<string> columns = new List<string>();
                                for (int i = 0; i < columnReader.FieldCount; i++)
                                {
                                    columns.Add(columnReader.GetName(i));
                                }

                                foreach (string columnName in columns)
                                {
                                    Console.Write($"{columnName}\t");
                                }
                                Console.WriteLine();

                                while (columnReader.Read())
                                {
                                    for (int i = 0; i < columnReader.FieldCount; i++)
                                    {
                                        Console.Write($"{columnReader[i]}\t");
                                    }
                                    Console.WriteLine();
                                }
                            } // SqlDataReader'ı burada kapatıyoruz
                        }
                        connection.Close();
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
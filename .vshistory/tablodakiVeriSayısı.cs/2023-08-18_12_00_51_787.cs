using System;
using System.Data;
using System.Data.SqlClient;

namespace TableRowCountExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "your_connection_string_here";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tüm tablo adlarını al
                DataTable tableNames = connection.GetSchema("Tables");

                foreach (DataRow row in tableNames.Rows)
                {
                    string tableName = (string)row["TABLE_NAME"];

                    // Her tablo için veri sayısını al
                    using (SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM {tableName}", connection))
                    {
                        int rowCount = Convert.ToInt32(command.ExecuteScalar());
                        Console.WriteLine($"Tablo: {tableName}, Veri Sayısı: {rowCount}");
                    }
                }
            }
        }
    }
}
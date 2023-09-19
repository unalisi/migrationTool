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

                    // Tablo ve sütun bilgilerini tutacak listeler
                    List<string> tableNames = new List<string>();
                    Dictionary<string, List<string>> columnNames = new Dictionary<string, List<string>>();

                    // Tablo bilgilerini almak için sorgu
                    string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'   AND TABLE_NAME NOT LIKE 'LOG_%'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tableName = reader["TABLE_NAME"].ToString();
                            tableNames.Add(tableName);
                        }
                    }

                    // Her bir tablo için sütun bilgilerini ve verilerini almak için sorgu
                    foreach (string tableName in tableNames)
                    {
                        query = $"SELECT * FROM {tableName}";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<string> columns = new List<string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                columns.Add(reader.GetName(i));
                            }
                            columnNames.Add(tableName, columns);

                            Console.WriteLine($"Tablo: {tableName}");
                            foreach (string columnName in columns)
                            {
                                Console.Write($"{columnName}\t");
                            }
                            Console.WriteLine();

                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    Console.Write($"{reader[i]}\t");
                                }
                                Console.WriteLine();
                            }
                            Console.WriteLine();
                        }
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
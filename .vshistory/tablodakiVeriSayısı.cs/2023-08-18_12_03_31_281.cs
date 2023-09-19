using System;
using System.Data;
using System.Data.SqlClient;

namespace TableRowCountExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=178.157.15.66;Integrated Security=False;Initial Catalog=evodata18;User ID=userevodata18;Password=0u7zV3S(cg)b47LjhnE5YcOdA!28Lj7EW9;";

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
                        if (rowCount > 100000)
                        {
                            Console.WriteLine($"Tablo: {tableName}, Veri Sayısı: {rowCount}");

                        }
                    }
                }
            }
        }
    }
}
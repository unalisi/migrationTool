using System;
using System.Data.SqlClient;                    //  AND TABLE_NAME NOT LIKE 'LOG_%'
using System.Text;

namespace DatabaseConnectionExample
{
    class Program
    {
        public static StreamWriter sw;
        static void Main(string[] args)
        {
            string connectionString = "Data Source=178.157.15.66;Integrated Security=False;Initial Catalog=evodata18;User ID=userevodata18;Password=0u7zV3S(cg)b47LjhnE5YcOdA!28Lj7EW9;";
            string outputFilePath = "C:\\Users\\mtddo\\Desktop\\cikti_1.01.txt";
            sw = new StreamWriter(outputFilePath, false, Encoding.UTF8);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //try
                //{
                connection.Open();
                Console.WriteLine("Veritabanına bağlantı başarılı.");

                List<string> tableNames = new List<string>();
                Dictionary<string, List<string>> columnNames = new Dictionary<string, List<string>>();

                string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE 'LOG_%'";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tableName = reader["TABLE_NAME"].ToString();
                        tableNames.Add(tableName);
                    }
                }



                foreach (string tableName in tableNames)
                {
                    List<string> columns = new List<string>();
                    query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}' AND TABLE_NAME NOT LIKE 'LOG_%'";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string columnName = reader["COLUMN_NAME"].ToString();
                            columns.Add(columnName);
                        }
                    }

                    columnNames.Add(tableName, columns);
                }

                foreach (var kvp in columnNames)
                {
                    sw.WriteLine($"Tablo: {kvp.Key}");
                    Console.WriteLine($"Tablo: {kvp.Key}");

                    string selectColumns = string.Join(", ", kvp.Value); // Sütun isimlerini yan yana yazdırmak için
                    string selectQuery = $"SELECT {selectColumns} FROM {kvp.Key}";
                    //string insertQuery = $"INSERT INTO {kvp.Key} ({kvp.Value}) VALUES (@value1)";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string rowValues = "";
                            for (int i = 0; i < reader.FieldCount; i++)
                            {


                                rowValues += $"{reader[i]}          ";
                                //sw.WriteLine(rowValues);
                                //Console.WriteLine(rowValues);
                            }

                            sw.WriteLine(rowValues);
                            Console.WriteLine(rowValues);

                        }
                    }

                    sw.WriteLine();
                    Console.WriteLine();
                }
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine("Bağlantı hatası: " + ex.Message);
                //}
            }

        }
    }
}






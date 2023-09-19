using Npgsql;
using System;
using System.Data.SqlClient;                    //  AND TABLE_NAME NOT LIKE 'LOG_%'
using System.Text;
using Npgsql;


namespace DatabaseConnectionExample
{
    class Program
    {
        public static StreamWriter sw;
        public static NpgsqlConnection targetConnection;
        public static SqlConnection connection;
        static void Main(string[] args)
        {
            string connectionString = "Data Source=178.157.15.66;Integrated Security=False;Initial Catalog=evodata18;User ID=userevodata18;Password=0u7zV3S(cg)b47LjhnE5YcOdA!28Lj7EW9;";
            string targetConnectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=admin";
            string outputFilePath = "C:\\Users\\mtddo\\Desktop\\cikti_1.01.txt";
            sw = new StreamWriter(outputFilePath, false, Encoding.UTF8);

            targetConnection = new NpgsqlConnection(targetConnectionString);
            targetConnection.Open();
            connection = new SqlConnection(connectionString);
            connection.Open();

            //try
            //{
            Console.WriteLine("Veritabanına bağlantı başarılı.");

            List<string> tableNames = new List<string>();
            Dictionary<string, List<string>> columnNames = new Dictionary<string, List<string>>();

            string query =
                "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE 'LOG_%'";

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
                query =
                    $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}' AND TABLE_NAME NOT LIKE 'LOG_%'";
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

            int j = 0;
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
                        InsertData(tableNames[0 + j], selectColumns, rowValues);
                    }

                    j++;
                }

                sw.WriteLine();
                Console.WriteLine();
            }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Bağlantı hatası: " + ex.Message);
            //}
            connection.Close();
            targetConnection.Close();

        }

        public static void InsertData(string tableName, string columns, string words)
        {
            words.Trim();
            string[] lineWords = words.Split("          ");
            lineWords = lineWords.Where(s => !string.IsNullOrEmpty(s)).ToArray(); // Remove empty elements
            string insertQuery = $"INSERT INTO {tableName} ({columns}) VALUES ";
            if (lineWords[0].EndsWith("_id"))
                insertQuery += "(@p1:: integer";
            else
                insertQuery += "(@p1";
            for (int i = 1; i < lineWords.Count(); i++)
            {
                if (lineWords[i].EndsWith("_id"))
                {
                    insertQuery += $", @p{i + 1}::integer";
                }
                else
                    insertQuery += $", @p{i + 1}";
            }
            insertQuery += ")";
            words.Trim();

            using (var cmd = new NpgsqlCommand(insertQuery, targetConnection))
            {
                int i = 1;
                foreach (var word in lineWords)
                {
                    if (word.EndsWith("_id"))
                    {
                        cmd.Parameters.AddWithValue($"@p{i}::integer", int.Parse(word));

                    }
                    else
                        cmd.Parameters.AddWithValue($"@p{i}", word);
                    i++;
                }

                cmd.ExecuteNonQuery();

            }
        }
    }
}






using System;
using System.Data;
using System.Data.SqlClient;
using Npgsql;

class Program
{
    static void Main()
    {
        string sourceConnectionString = "your_sql_server_connection_string";
        

        string targetConnectionString = "your_postgre_connection_string";

        using (var sourceConnection = new SqlConnection(sourceConnectionString))
        using (var targetConnection = new NpgsqlConnection(targetConnectionString))
        {
            sourceConnection.Open();
            targetConnection.Open();

            List<string> tableNames = new List<string>();

            using (var sourceCommand = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", sourceConnection))
            using (var reader = sourceCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    string tableName = reader["TABLE_NAME"].ToString();
                    tableNames.Add(tableName);
                }
            }

            foreach (string tableName in tableNames)
            {
                try
                {
                    TransferData(sourceConnection, targetConnection, tableName);
                    Console.WriteLine($"Veri aktarımı başarılı: {tableName}");
                }
                catch (Npgsql.PostgresException ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                    // Hata durumunu daha ayrıntılı incelemek için ex hatasını kullanın.
                }
            }
        }

        Console.WriteLine("Veri aktarım işlemi tamamlandı.");
    }

    static void TransferData(SqlConnection sourceConnection, NpgsqlConnection targetConnection, string tableName)
    {
        using (var sourceDataAdapter = new SqlDataAdapter($"SELECT * FROM {tableName}", sourceConnection))
        {
            var dataSet = new DataSet();
            sourceDataAdapter.Fill(dataSet, tableName);

            using (var writer = targetConnection.BeginBinaryImport($"COPY {tableName} FROM STDIN BINARY"))
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    var values = new object[row.ItemArray.Length];
                    row.ItemArray.CopyTo(values, 0);
                    writer.WriteRow(values);
                }
                writer.Complete();
            }
        }
    }
}
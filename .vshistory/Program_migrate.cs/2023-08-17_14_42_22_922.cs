using System;
using System.Data;
using System.Data.SqlClient;
using Npgsql;

class Program
{
    static void Main()
    {
        string sourceConnectionString = "Data Source=178.157.15.66;Integrated Security=False;Initial Catalog=evodata18;User ID=userevodata18;Password=0u7zV3S(cg)b47LjhnE5YcOdA!28Lj7EW9;";
        //string targetConnectionString = "Server=161.35.202.107; Port=5432; User Id=postgres; Password=enasoft1453; Database=postgres; Pooling=false;";

        //string sourceConnectionString = "Data Source=DESKTOP-M4RCH0O;Initial Catalog=YourMSSQLDatabase;Integrated Security=True";
        string targetConnectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=admin";

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
                TransferData(sourceConnection, targetConnection, tableName);
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
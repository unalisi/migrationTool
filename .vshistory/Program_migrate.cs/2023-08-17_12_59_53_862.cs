using System;
using System.Data;
using System.Data.SqlClient;
using Npgsql;

class Program
{
    static void Main()
    {
        string sqlServerConnectionString = "your_sql_server_connection_string";
        string postgreConnectionString = "your_postgresql_connection_string";

        using (SqlConnection sqlConnection = new SqlConnection(sqlServerConnectionString))
        using (NpgsqlConnection postgreConnection = new NpgsqlConnection(postgreConnectionString))
        {
            sqlConnection.Open();
            postgreConnection.Open();

            string sqlQuery = "SELECT * FROM TableName"; // Tablonuzu ve sütunlarınızı güncelleyin
            using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    // Veriyi çekme ve PostgreSQL'e aktarma işlemleri
                    int id = (int)sqlDataReader["Id"];
                    string name = sqlDataReader["Name"].ToString();
                    // Diğer sütunları da burada çekebilirsiniz

                    string insertQuery = "INSERT INTO target_table (id, name) VALUES (@id, @name)"; // Hedef tabloyu güncelleyin
                    using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, postgreConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@id", id);
                        insertCommand.Parameters.AddWithValue("@name", name);
                        // Diğer sütunları da burada ekleyin

                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
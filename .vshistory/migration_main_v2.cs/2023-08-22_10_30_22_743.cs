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
                "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE 'LOG_%'" +
                "AND TABLE_NAME NOT LIKE '%_023%' AND TABLE_NAME NOT LIKE '%_022%' AND TABLE_NAME NOT LIKE '%_021%' AND TABLE_NAME NOT LIKE '%_020%'" +
                "AND TABLE_NAME NOT LIKE '%_019%' AND TABLE_NAME NOT LIKE '%_018%' AND TABLE_NAME NOT LIKE '%_017%'  AND TABLE_NAME NOT LIKE '%_016%' AND TABLE_NAME NOT LIKE '%_015%'" +
                "AND TABLE_NAME NOT LIKE '%_014%' AND TABLE_NAME NOT LIKE '%_013%' AND TABLE_NAME NOT LIKE '%_012%' AND TABLE_NAME NOT LIKE '%_011%' AND TABLE_NAME NOT LIKE '%_010%'";

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
                try
                {


                    List<string> columns = new List<string>();
                    query =
                        $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}' " +
                        $"AND TABLE_NAME NOT LIKE 'LOG_%' AND TABLE_NAME NOT LIKE '%_023%' AND TABLE_NAME NOT LIKE '%_022%' AND TABLE_NAME NOT LIKE '%_021%' AND TABLE_NAME NOT LIKE '%_020%' " +
                        $"AND TABLE_NAME NOT LIKE '%_019%' AND TABLE_NAME NOT LIKE '%_018%' AND TABLE_NAME NOT LIKE '%_017%'  AND TABLE_NAME NOT LIKE '%_016%'  AND TABLE_NAME NOT LIKE '%_015%' " +
                        $"AND TABLE_NAME NOT LIKE '%_014%' AND TABLE_NAME NOT LIKE '%_013%' AND TABLE_NAME NOT LIKE '%_012%' AND TABLE_NAME NOT LIKE '%_011%' AND TABLE_NAME NOT LIKE '%_010%'"/*AND TABLE_NAME NOT LIKE '%_023%' AND TABLE_NAME NOT LIKE '%_022%'*/;
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
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            foreach (var kvp in columnNames)
            {
                if (kvp.Key != "tbl_stok_depo_miktar" && kvp.Key != "tbl_001_grup_rehber" && kvp.Key != "tbl_001_rehber" && kvp.Key != "tbl_pot_is_amac"
                    && kvp.Key != "api_test" && kvp.Key != "_ana_tablo" && kvp.Key != "_ana2" && kvp.Key != "tbl_uts_ulkeler" &&
                    kvp.Key != "tbl_efatura_brm" && kvp.Key != "tbl_001_grup_1622" && kvp.Key != "tbl_hayvan_yonetimi" && kvp.Key != "tbl_gib_earsiv_fat" && kvp.Key != "tbl_cms_veri_tag" && 
                    kvp.Key != "tbl_vkn_cari" && kvp.Key != "tbl_kisayol_menu_bilgi" && kvp.Key != "_voltaj" && kvp.Key != "tbl_pot_is_drm" && kvp.Key != "tbl_pot_is_grp" && kvp.Key != "tbl_pot_is_amac" && 
                    kvp.Key != "tbl_potansiyel_isler" && kvp.Key != "tbl_kampanya_master" && kvp.Key != "tbl_finans_turler")
                /*&& kvp.Key != "tbl_017_muh_banka" && kvp.Key != "tbl_015_muh_cari" && kvp.Key != "tbl_018_muh_proje"*/
                //         && kvp.Key != "tbl_017_muh_stok" && kvp.Key != "tbl_015_muh_kasa" && kvp.Key != "tbl_013_muhmas" && kvp.Key != "tbl_023_muh_plan" 
                //         && kvp.Key != "tbl_021_muh_plan" && kvp.Key != "tbl_015_muh_banka"
                //         && kvp.Key != "tbl_015_muh_banka"
                //         && kvp.Key != "tbl_015_muh_banka"
                //         && kvp.Key != "tbl_015_muh_banka"
                //         && kvp.Key != "tbl_015_muh_banka"
                //         && kvp.Key != "tbl_015_muh_banka"
                //         && kvp.Key != "tbl_015_muh_banka"
                //         && kvp.Key != "tbl_015_muh_banka"
                //         && kvp.Key != "tbl_015_muh_banka"
                //         && kvp.Key != "tbl_015_muh_banka"
                //         && kvp.Key != "tbl_015_muh_banka" ) // && kvp.Key != "tbl_finans_turler"
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
                            int k = 0;
                            int l = 0;
                            int r = reader.FieldCount;
                            string m = kvp.Value[k];
                            string rowValues = "";
                            //for (int i = 0; i < reader.FieldCount; i++)
                            //{

                            string insertQuery = $"INSERT INTO {kvp.Key} ({selectColumns}) VALUES ({string.Join(", ", kvp.Value.Select(v => $"@{v}"))})";
                            using (var cmd = new NpgsqlCommand(insertQuery, targetConnection))
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    cmd.Parameters.AddWithValue($"@{kvp.Value[i]}", reader[i]);
                                }
                                cmd.ExecuteNonQuery();
                            }
                            //k++;
                            //rowValues += $"{reader[i]}          ";

                            //}

                            //sw.WriteLine(rowValues);
                            //Console.WriteLine(rowValues);
                        }

                    }

                    sw.WriteLine();
                    Console.WriteLine();
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Bağlantı hatası: " + ex.Message);
            //}
            //connection.Close();
            //targetConnection.Close();

        }

        //public static void InsertData(string tableName, List<string> columns, object reader,int k)
        //{
        //    string insertQuery = $"INSERT INTO {tableName} ({columns[k]}) VALUES (@p1) ";



        //    //    insertQuery += "(@p1:: integer";
        //    //for (int i = 1; i < lineWords.Count(); i++)
        //    //{
        //    //    if (lineWords[i].EndsWith("_id"))
        //    //    {
        //    //        insertQuery += $", @p{i + 1}::integer";
        //    //    }
        //    //    else
        //    //        insertQuery += $", @p{i + 1}";
        //    //}
        //    //insertQuery += ")";
        //    //words.Trim();

        //    using (var cmd = new NpgsqlCommand(insertQuery, targetConnection))
        //    {
        //        cmd.Parameters.AddWithValue("@p1", reader);

        //        cmd.ExecuteNonQuery();

        //    }
        //}
    }
}








//MS SQL 18 Test Şirketi
//Data Source=178.157.15.66;Integrated Security=False;Initial Catalog=evodata18;User ID=userevodata18;Password=0u7zV3S(cg)b47LjhnE5YcOdA!28Lj7EW9;


//PG SQL Test Sunucusu
//Server=161.35.202.107; Port=5432; User Id=postgres; Password=enasoft1453; Database=postgres; Pooling=false;




using Npgsql;
using System.Data;

public DataTable SQL_Tablo(string SQL)
{

    DataTable functionReturnValue = null;

    NpgsqlDataAdapter DA = null;
    DataSet DS = new DataSet();
    NpgsqlConnection NC = new NpgsqlConnection(BAG);

    //Son_SQL_Hata.Temizle();


    try
    {
        NC.Open();
    }
    catch (Exception ex)
    {
        //Son_SQL_Hata.Hata_Var = true;
        //Son_SQL_Hata.Hata_Kodu = 1;
        //Son_SQL_Hata.Hata_Metni = ex.Message;
        //Son_SQL_Hata.Hata_Metni = Son_SQL_Hata.Hata_Metni.Replace("\"", "´"); ;
        //Son_SQL_Hata.Hata_SQL = "Bağlantı Hatası";
        NC.Close();
        NC.Dispose();

        //SQL_LOG_REC(ex.Message, SQL, "Connection", prm, BAG);

        return new DataTable();
    }


    DA = new NpgsqlDataAdapter(SQL, NC);
    try
    {
        DA.SelectCommand.CommandTimeout = 0;
        DA.Fill(DS, "Tablo");
        DS.Tables["Tablo"].DefaultView.AllowDelete = true;
        DS.Tables["Tablo"].DefaultView.AllowNew = true;
        DS.Tables["Tablo"].DefaultView.AllowEdit = true;
        NC.Close();
        NC.Dispose();
        return DS.Tables["Tablo"];
    }
    catch (Exception ex)
    {
        //Son_SQL_Hata.Hata_Var = true;
        //Son_SQL_Hata.Hata_Kodu = 2;
        //Son_SQL_Hata.Hata_Metni = ex.Message;
        //Son_SQL_Hata.Hata_Metni = Son_SQL_Hata.Hata_Metni.Replace("\"", "´"); ;
        //Son_SQL_Hata.Hata_SQL = "";
        //SQL
        NC.Close();
        NC.Dispose();
        //SQL_LOG_REC(ex.Message, SQL, "SQL_Tablo", prm, BAG);
        functionReturnValue = new DataTable();
    }
    return functionReturnValue;

}
public bool SQL_Kos(string SQL)
{

    NpgsqlParameter[] prm = new NpgsqlParameter[1];
    prm[0] = new NpgsqlParameter("@a_id", "");

    NpgsqlConnection NC = new NpgsqlConnection(BAG);
    NpgsqlCommand COMUT = null;

    int SNC = 0;
    try
    {
        NC.Open();
    }
    catch (Exception ex)
    {
        //Son_SQL_Hata.Hata_Var = true;
        //Son_SQL_Hata.Hata_Kodu = 1;
        //Son_SQL_Hata.Hata_Metni = ex.Message;
        //Son_SQL_Hata.Hata_Metni = Son_SQL_Hata.Hata_Metni.Replace("\"", "´"); ;
        //Son_SQL_Hata.Hata_SQL = "Bağlantı Hatası";
        NC.Close();
        NC.Dispose();
        //SQL_LOG_REC(ex.Message, SQL, "Connection", prm, BAG);
        return false;
    }
    try
    {
        COMUT = new NpgsqlCommand(SQL, NC);
        COMUT.CommandTimeout = 0;
        SNC = COMUT.ExecuteNonQuery();
        if (SNC != 0)
        {
            COMUT.Dispose();
            NC.Close();
            NC.Dispose();
            return true;
        }
        else
        {
            COMUT.Dispose();
            NC.Close();
            NC.Dispose();
            return false;
        }
    }
    catch (Exception ex)
    {
        //Son_SQL_Hata.Hata_Var = true;
        //Son_SQL_Hata.Hata_Kodu = 2;
        //Son_SQL_Hata.Hata_Metni = ex.Message;
        //Son_SQL_Hata.Hata_Metni = Son_SQL_Hata.Hata_Metni.Replace("\"", "´"); ;
        //Son_SQL_Hata.Hata_SQL = "";
        //SQL
        NC.Close();
        NC.Dispose();
        //SQL_LOG_REC(ex.Message, SQL, "SQL_Kos", prm, BAG);
        return false;
    }
}
public void SQL_LOG_REC(string Mesaj, string SQL, string TIP, NpgsqlParameter[] prmX, string SQL_CONN)
{

}

//----------------------------------------------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------------------------------------------------

 using  System.Data.SqlClient

 public DataTable SQL_Tablo(string SQL)
{


    SQL = "SET LANGUAGE Turkish; " + SQL;

    DataTable functionReturnValue = null;

    System.Data.SqlClient.SqlDataAdapter DA = null;
    DataSet DS = new DataSet();
    System.Data.SqlClient.SqlConnection NC = new System.Data.SqlClient.SqlConnection(BAG);

    //Son_SQL_Hata.Temizle();

    SqlParameter[] prm = new SqlParameter[1];
    prm[0] = new SqlParameter("@a_id", "");

    try
    {
        NC.Open();
    }
    catch (Exception ex)
    {
        //Son_SQL_Hata.Hata_Var = true;
        //Son_SQL_Hata.Hata_Kodu = 1;
        //Son_SQL_Hata.Hata_Metni = ex.Message;
        //Son_SQL_Hata.Hata_SQL = "Bağlantı Hatası";
        NC.Close();
        NC.Dispose();

        //SQL_LOG_REC(ex.Message, SQL, "Connection", prm, BAG);
        return new DataTable();
    }


    DA = new System.Data.SqlClient.SqlDataAdapter(SQL, NC);
    try
    {
        DA.SelectCommand.CommandTimeout = 0;

        DataTable DT = new DataTable();
        DT.DefaultView.AllowDelete = true;
        DT.DefaultView.AllowNew = true;
        DT.DefaultView.AllowEdit = true;
        DT.TableName = "Tablo";
        DA.Fill(DT);
        //DS.Tables["Tablo"].DefaultView.AllowDelete = true;
        //DS.Tables["Tablo"].DefaultView.AllowNew = true;
        //DS.Tables["Tablo"].DefaultView.AllowEdit = true;
        NC.Close();
        NC.Dispose();

        return DT;

    }
    catch (Exception ex)
    {
        //Son_SQL_Hata.Hata_Var = true;
        //Son_SQL_Hata.Hata_Kodu = 2;
        //Son_SQL_Hata.Hata_Metni = ex.Message;
        //Son_SQL_Hata.Hata_SQL = "";
        //SQL
        NC.Close();
        NC.Dispose();

        //string func = "";
        //string classx = "";
        //func = new StackFrame(1, true).GetMethod().Name;
        //classx = new StackFrame(1, true).GetMethod().ReflectedType.Name;

        //SQL_LOG_REC(ex.Message, SQL, "SQL_Tablo" + " Class: " + classx + " func: " + func, prm, BAG);
        functionReturnValue = new DataTable();
    }
    return functionReturnValue;

}


public bool SQL_Kos(string SQL)
{


    SQL = "SET LANGUAGE Turkish; " + SQL;

    SqlParameter[] prm = new SqlParameter[1];
    prm[0] = new SqlParameter("@a_id", "");


    //Son_SQL_Hata.Temizle();

    System.Data.SqlClient.SqlConnection NC = new System.Data.SqlClient.SqlConnection(BAG);
    System.Data.SqlClient.SqlCommand COMUT = null;
    int SNC = 0;
    try
    {
        NC.Open();
    }
    catch (Exception ex)
    {
        //Son_SQL_Hata.Hata_Var = true;
        //Son_SQL_Hata.Hata_Kodu = 1;
        //Son_SQL_Hata.Hata_Metni = ex.Message;
        //Son_SQL_Hata.Hata_SQL = "Bağlantı Hatası";
        NC.Close();
        NC.Dispose();
        //SQL_LOG_REC(ex.Message, SQL, "Connection", prm, BAG);
        return false;
    }
    try
    {
        COMUT = new SqlCommand(SQL, NC);
        COMUT.CommandTimeout = 0;
        SNC = COMUT.ExecuteNonQuery();
        if (SNC != 0)
        {
            COMUT.Dispose();
            NC.Close();
            NC.Dispose();
            return true;
        }
        else
        {
            COMUT.Dispose();
            NC.Close();
            NC.Dispose();
            return false;
        }
    }
    catch (Exception ex)
    {
        //Son_SQL_Hata.Hata_Var = true;
        //Son_SQL_Hata.Hata_Kodu = 2;
        //Son_SQL_Hata.Hata_Metni = ex.Message;
        //Son_SQL_Hata.Hata_SQL = "";
        //SQL
        NC.Close();
        NC.Dispose();

        //string func = "";
        //string classx = "";
        //func = new StackFrame(1, true).GetMethod().Name;
        //classx = new StackFrame(1, true).GetMethod().ReflectedType.Name;
        //SQL_LOG_REC(ex.Message, SQL, "SQL_Kos" + " Class: " + classx + " func: " + func, prm, BAG);
        return false;
    }
}
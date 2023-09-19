using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

class Program3
{
    public static StreamReader sr;
    public static StreamWriter sw;

    static void Main()
    {
        string filePath = "your_file_path";
        string outputfilePath = "your_file_path";

        string targetConnectionString = "your_target_connection_string";

        sr = new StreamReader(filePath, Encoding.UTF8);
        sw = new StreamWriter(outputfilePath, false, Encoding.UTF8);


        List<string> words = new List<string>();


        string line;
        string tableName;

        while ((line = sr.ReadLine()) != null)
        {
            line = line.Trim();
            if (line.StartsWith("Tablo: _ana_tablo"))
            {
                string[] FirstLineWords = line.Split(" ");
                FirstLineWords = FirstLineWords.Where(s => !string.IsNullOrEmpty(s)).ToArray(); // Remove empty elements
                tableName = FirstLineWords[1];

                bool flag = false;
                while ((line = sr.ReadLine()) != null || !line.StartsWith("Tablo:"))
                {

                    line = line.Trim();

                    string[] lineWords = line.Split("          ");
                    lineWords = lineWords.Where(s => !string.IsNullOrEmpty(s)).ToArray(); // Remove empty elements
                    foreach (string word in lineWords)
                    {
                        if (word.ToUpper() == "Tablo:")
                        {
                            flag = true;
                            break;
                        }
                        words.Add(word.Trim());
                    }

                    if (words.Count > 0)
                        InsertData(tableName, words);

                    if (flag)
                        break;
                }


            }
        }
        sr.Close();
    }

    public static void InsertData(string tableName, List<string> words)
    {


    }

}



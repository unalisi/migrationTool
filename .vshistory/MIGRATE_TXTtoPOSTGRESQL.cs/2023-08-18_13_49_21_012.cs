using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

class Program3
{
    public static StreamWriter sw;
    public static StreamReader sr;

    static void Main()
    {
        string filePath = "C:\\Users\\mtddo\\Desktop\\cikti.txt";
        string targetConnectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=admin";

        sw = new StreamWriter(outputFilePath, false, Encoding.UTF8);
        sr = new StreamReader(filePath, Encoding.UTF8);


        List<string> words = new List<string>();


        string line;

        while ((line = sr.ReadLine()) != null)
        {
            line = line.Trim();
            line = RemoveOrReplaceWords(line);
            if (line.StartsWith("CREATE"))
            {
                bool flag = false;
                do
                {

                    line = line.Trim();
                    line = RemoveOrReplaceWords(line);

                    string[] lineWords = line.Split(' ');
                    lineWords = lineWords.Where(s => !string.IsNullOrEmpty(s)).ToArray(); // Remove empty elements
                    foreach (string word in lineWords)
                    {
                        words.Add(word.Trim());
                        if (word.ToUpper() == "GO")
                            flag = true;
                    }

                    if (flag)
                        break;

                } while ((line = sr.ReadLine()) != null);

                if (words[1].ToUpper() == "TABLE")
                    CreateTable(words);
                else if (words[1].ToUpper() == "TRIGGER")
                    CreateTrigger(words);
                else
                    Console.WriteLine("HATA");
                words.Clear();

            }
            else if (line.StartsWith("INSERT"))
            {
                bool flag = false;
                do
                {
                    line = line.Trim();
                    line = RemoveOrReplaceWords(line);
                    if (!string.IsNullOrWhiteSpace(line) && !string.IsNullOrEmpty(line))
                    {

                        string[] lineWords = line.Split(' ');
                        lineWords = lineWords.Where(s => !string.IsNullOrEmpty(s)).ToArray(); // Remove empty elements
                        foreach (string word in lineWords)
                        {
                            if (!string.IsNullOrWhiteSpace(word))
                            {
                                if (word.ToUpper() == "GO")
                                {
                                    flag = true;
                                    break;
                                }
                                words.Add(word.Trim());
                            }

                        }
                        if (flag)
                            break;
                    }
                    else
                        break;
                } while ((line = sr.ReadLine()) != null);

                CreateInsertInto(words);
                sw.WriteLine(";");
                words.Clear();
            }
        }
        sw.Flush();
        sw.Close();
        sr.Close();
    }

}



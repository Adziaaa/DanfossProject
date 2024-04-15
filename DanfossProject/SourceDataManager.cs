using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;

class Program
{
    static void ReadCSV()
    {
        string csvFilePath = "Source Data Manager.csv";

        if (!File.Exists(csvFilePath))
        {
            Console.WriteLine("The CSV file does not exist.");
            return;
        }

        using (TextFieldParser parser = new TextFieldParser(csvFilePath))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            if (!parser.EndOfData)
            {
                string[] headerFields = parser.ReadFields();
            }

            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                
                foreach (string field in fields)
                {
                    Console.Write(field + "\t");
                }
                Console.WriteLine();
            }
        }
    }

    static void WriteCSV()
    {
        string csvFilePath = "Source Data Manager.csv";

        Console.WriteLine("Enter data to append to the CSV file (comma delimited):");
        string userInput = Console.ReadLine();
        string[] fields = userInput.Split(',');
        try
        {
            using (StreamWriter writer = new StreamWriter(csvFilePath, true))
            {
                writer.WriteLine(string.Join(",", fields));
            }

            Console.WriteLine("Data has been added to the CSV file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred");
        }
    }
}
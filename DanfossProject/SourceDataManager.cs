using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace DanfossProject
{ 
public class CsvRecord
{
    public string TimeFrom { get; set; }
    public string TimeTo { get; set; }
    public double HeatDemand { get; set; }
    public double ElectricityPrice { get; set; }
}

public class CsvManager
{
    private string csvfilePath = "SourceDataManager.csv";

    public List<CsvRecord> ReadCsv()
    {
        using (var reader = new StreamReader(csvfilePath))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
                Console.WriteLine("It worked");
                Console.Read();
                return csv.GetRecords<CsvRecord>().ToList();
        }

        }

    public bool CheckCsvFile()
    {
        if (!File.Exists(csvfilePath))
        {
            Console.WriteLine($"CSV file '{csvfilePath}' does not exist.");
            return false;
        }

        return true;
    }
    public void WriteCsv(List<CsvRecord> records)
    {
        using (var writer = new StreamWriter(csvfilePath))
        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.WriteRecords(records);
        }
    }
}
}





/*
using System;
using System.Globalization;
using System.IO;
using CsvHelper;
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
*/


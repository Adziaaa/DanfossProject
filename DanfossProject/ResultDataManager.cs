using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace DanfossProject
{

    public class ResultDataCsvManager
    {
        public void CO2RdmCsvWriter(List<ResultData> records, string ResultData)
        {
            using (var writer = new StreamWriter("CO2ResultData.csv"))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(records);
            }
        }

        public void RdmCsvWriter(List<ResultData> records, string ResultData)
        {
            using (var writer = new StreamWriter("ResultData.csv"))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(records);
            }
        }

        public static void RdmCsvReader()
        {
            if (File.Exists("ResultData.csv"))
            {
                List<ResultData> outputRecords = new();

                using (var reader = new StreamReader("ResultData.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<ResultData>();

                    foreach (var record in records)
                    {
                        Console.WriteLine(record);
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        public static void CO2RdmCsvReader()
        {
            if (File.Exists("CO2ResultData.csv"))
            {
                List<ResultData> outputRecords = new();

                using (var reader = new StreamReader("CO2ResultData.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<ResultData>();

                    foreach (var record in records)
                    {
                        Console.WriteLine(record);
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
    }
}
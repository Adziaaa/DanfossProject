using System;
using System.Collections.Generic;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace DanfossProject
{
    public class RdmRecord
    {
        //string efficiencyByCost.Modle.Name { get; set; }
        //string lowestC02Emmisions.Name { get; set; }
    }

    public class ResultDataCsvManager
    {
        string filename = @"ResultData.csv";
        public void RdmCsvWriter(List<RdmRecord> records, string ResultData)
        {
            using (var writer = new StreamWriter(filename))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(records);
            }
        }

        public static void RdmCsvReader()
        {
            if(!File.Exists("ResultData.csv"))
            {
                Console.WriteLine(".csv file not found");
                return;
            }
            else
            {
                List<RdmRecord> outputRecords = new();

                using (var reader = new StreamReader("ResultData.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<RdmRecord>();

                    foreach(var record in records)
                    {
                        Console.WriteLine(record);
                    }
                }
            }
        }
    }
}
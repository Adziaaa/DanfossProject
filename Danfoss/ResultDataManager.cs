using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Danfoss
{

    public class ResultDataCsvManager
    {
        public void RdmCsvWriter(List<ResultData> records, string RDMCsvName)
        {
            using (var writer = new StreamWriter(RDMCsvName))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(records);
            }
        }

        public static void RdmCsvReader(string RDMPath)
        {
            if (File.Exists(RDMPath))
            {
                List<ResultData> outputRecords = new();

                using (var reader = new StreamReader(RDMPath))
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
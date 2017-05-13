using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace CsvToJson
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonString = GetJsonStringFroamCsv();
            Console.WriteLine(jsonString);
        }

        /// <summary>
        /// convert csv to json using powershell cmdlet
        /// file encoding must utf-8!
        /// to-be : encoding check before convert 
        /// </summary>
        /// <returns></returns>
        private static string GetJsonStringFroamCsv()
        {
            Console.WriteLine("drag csv file hear and press enter.(Only UTF-8!)");
            string filePath = Console.ReadLine();

            StringBuilder result = new StringBuilder();
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript("param($filePath); " +
                    "$csv = Import-Csv -path $filePath; " +
                    "ConvertTo-Json $csv;");
                ps.AddParameter("filePath", filePath.Replace("\"", ""));

                Collection<PSObject> ouput = ps.Invoke();

                foreach (var item in ouput)
                    if (item != null && item.BaseObject is string)
                        result.Append(item.BaseObject.ToString());

                return result.ToString();
            }
        }
    }
}

using CsvHelper;
using pit38_tasty_ibkr.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace pit38_tasty_ibkr
{
     public class ImportCSV
    {
        private const string CSV_DIR = @"D:\ibkr-tasty-history";
        public static IEnumerable<TradeTT> LoadTastyTradeCSV()
        {
            // Get the current directory

            var files = GetDirectoryFiles().Select(x => new FileInfo(x));

            var file = files.FirstOrDefault(x => x.Name.StartsWith("tastytrade_transactions"));

            if (file == null)
            {
                Console.WriteLine("No tastytrade CSV found!");
                return new List<TradeTT>();
            }
            using (var reader = new StreamReader(file.FullName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<TradeTT>().ToList();
            }
        }

        public static IEnumerable<TradeIBKR> LoadIBKRTradeCSV()
        {
            var files = GetDirectoryFiles().Select(x => new FileInfo(x));

            var file = files.FirstOrDefault(x => x.Name.StartsWith("TradeConfirmation"));

            if (file == null)
            {
                Console.WriteLine("No IBKR CSV found!");
                
                return new List<TradeIBKR>();
            }
            using (var reader = new StreamReader(file.FullName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<TradeIBKR>().ToList();
            }
        }
        public static string[] GetDirectoryFiles(string dir = null)
        {
            // Get the current directory
            string currentDirectory = dir ?? CSV_DIR;

            // Get files from the current directory
            string[] files = Directory.GetFiles(currentDirectory);

            return files;
        }

    }
}

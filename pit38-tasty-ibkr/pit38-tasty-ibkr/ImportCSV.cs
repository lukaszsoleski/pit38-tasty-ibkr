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
        public static IEnumerable<TradeTT> LoadTastyTradeCSV()
        {
            // Get the current directory

            var file = GetDirectoryFiles().FirstOrDefault(x => x.StartsWith("tastytrade_transactions"));

            if (string.IsNullOrEmpty(file)) return new List<TradeTT>();

            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<TradeTT>();
            }
        }

        public static string[] GetDirectoryFiles(string dir = null)
        {
            // Get the current directory
            string currentDirectory = dir ?? Directory.GetCurrentDirectory();

            // Get files from the current directory
            string[] files = Directory.GetFiles(currentDirectory);

            return files;
        }

    }
}

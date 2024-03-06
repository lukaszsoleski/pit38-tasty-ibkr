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
    public class TransactionsCSV
    {
        private const string CSV_DIR = @"D:\ibkr-tasty-history";

        public static TransactionsCSV Inst = new TransactionsCSV();

        public List<Transaction> GetTransactionsHistory()
        {
            var all = new List<Transaction>();

            var ibkr = GetIBKRTransactions();

            var tasty = GetTastyTransactions();

            all.AddRange(tasty);
            all.AddRange(ibkr);

            all = all.OrderByDescending(x => x.TransactionDate).ToList();

            return all;
        }

        private List<Transaction> GetIBKRTransactions()
        {
            var csv = LoadIBKRTradeCSV().Where(x => x.LevelOfDetail == "EXECUTION").ToList();

            var transactions = new List<Transaction>();

            foreach (var line in csv)
            {
                AssetClassEnum assetClass = GetInstrumentType(line);
                var transaction = new Transaction()
                {
                    TransactionDate = line.TradeDateT,
                    TickerSymbol = string.IsNullOrEmpty(line.UnderlyingSymbol) ? line.Symbol : line.UnderlyingSymbol,
                    Amount =  line.ProceedsNum,
                    Quantity = line.QuantityNum,
                    Commitions = line.CommissionNum,
                    Price = line.PriceNum,
                    TransactionType = GetTransactionType(line),
                    AssetClass = assetClass,
                    Currency = line.CurrencyPrimary,
                    CommissionCurrency = line.CommissionCurrency,
                    SettlementDate = line.SettleDateT,
                    Description = line.Description
                };
                if(assetClass != AssetClassEnum.Undefined)
                    transactions.Add(transaction);

            }
            return transactions;
        }
        private List<Transaction> GetTastyTransactions()
        {
            var csv = LoadTastyTradeCSV();

            var transactions = new List<Transaction>();

            foreach(var line in csv)
            {
                AssetClassEnum assetClass = GetInstrumentType(line);
                var transaction = new Transaction()
                {
                    TransactionDate = DateTime.Parse(line.Date),
                    TickerSymbol = line.UnderlyingSymbol,
                    Amount = line.ValueNum,
                    Quantity = line.QuantityNum,
                    Commitions = line.FeesNum + line.CommissionsNum,
                    Price = line.AveragePriceNum,
                    TransactionType = GetTransactionType(line),
                    AssetClass = assetClass,
                    Currency = "USD",
                    CommissionCurrency = "USD",
                    Description = line.Description
                };

                if(transaction.AssetClass != AssetClassEnum.Undefined && transaction.TransactionType != TransactionTypeEnum.UNDEFINED)
                {
                    transaction.SettlementDate = SettlementDateAPI.Inst.GetSettlementDate(DateTime.Parse(line.Date), assetClass);

                    transactions.Add(transaction);
                }
            }

            return transactions;
        }

        private static TransactionTypeEnum GetTransactionType(TradeTT line)
        {
            switch (line.Action)
            {
                case "BUY_TO_CLOSE":
                case "BUY_TO_OPEN":
                    return TransactionTypeEnum.BUY;
                case "SELL_TO_OPEN":
                case "SELL_TO_CLOSE":
                    return TransactionTypeEnum.SELL;
                default:
                    {
                        return TransactionTypeEnum.UNDEFINED;
                    }
            }
        }
        private static TransactionTypeEnum GetTransactionType(TradeIBKR line)
        {
            switch (line.BuySell)
            {
                case "BUY":
                    return TransactionTypeEnum.BUY;
                case "SELL":
                    return TransactionTypeEnum.SELL;
                default:
                    {
                        return TransactionTypeEnum.UNDEFINED;
                    }
            }
        }
        private static AssetClassEnum GetInstrumentType(TradeTT line)
        {
            switch (line.InstrumentType)
            {
                case "Equity Option":
                    return AssetClassEnum.Option;
                case "Equity":
                    return AssetClassEnum.Stock;
                default:
                    return AssetClassEnum.Undefined;
            }
        }

        private static AssetClassEnum GetInstrumentType(TradeIBKR line)
        {
            switch (line.AssetClass)
            {
                case "OPT":
                    return AssetClassEnum.Option;
                case "STK":
                    return AssetClassEnum.Stock;
                case "BILL":
                    return AssetClassEnum.Option;
                default:
                    return AssetClassEnum.Undefined;
            }
        }

        private static IEnumerable<TradeTT> LoadTastyTradeCSV()
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

        private static IEnumerable<TradeIBKR> LoadIBKRTradeCSV()
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
        private static string[] GetDirectoryFiles(string dir = null)
        {
            // Get the current directory
            string currentDirectory = dir ?? CSV_DIR;

            // Get files from the current directory
            string[] files = Directory.GetFiles(currentDirectory);

            return files;
        }
    }
}

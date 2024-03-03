using pit38_tasty_ibkr.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr
{
    public class TransactionBL
    {
        public static TransactionBL Inst = new TransactionBL();

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
            var csv = ImportCSV.LoadIBKRTradeCSV().Where(x => !string.IsNullOrEmpty(x.TradeID)).ToList();

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
                    Commitions = line.GetCommitions(),
                    Price = line.PriceNum,
                    TransactionType = GetTransactionType(line),
                    AssetClass = assetClass,
                    Currency = line.CurrencyPrimary,
                    CommissionCurrency = line.CommissionCurrency,
                    SettlementDate = line.SettleDateT
                };

                transaction.SetPLN();

                transactions.Add(transaction);

                Console.WriteLine(transaction);
            }
            return transactions;
        }
        private List<Transaction> GetTastyTransactions()
        {
            var csv = ImportCSV.LoadTastyTradeCSV();

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
                    SettlementDate = SettlementDateBL.Inst.GetSettlementDate(DateTime.Parse(line.Date), assetClass)
                };
                transaction.SetPLN();
                
                Console.WriteLine(transaction);

                transactions.Add(transaction);
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
    }
}

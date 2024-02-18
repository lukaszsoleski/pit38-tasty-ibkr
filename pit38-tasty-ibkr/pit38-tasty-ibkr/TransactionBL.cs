using pit38_tasty_ibkr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr
{
    public class TransactionBL
    {
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
                    TransactionDate = line.TradeDate,
                    TickerSymbol = line.UnderlyingSymbol,
                    Amount = line.Proceeds,
                    Quantity = line.Quantity,
                    Fees = line.GetCommitions(),
                    Price = line.Price,
                    TransactionType = GetTransactionType(line),
                    AssetClass = assetClass,
                    Currency = line.CurrencyPrimary,
                    CommissionCurrency = line.CommissionCurrency,
                    SettlementDate = line.SettleDate
                };

                transactions.Add(transaction);
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
                    TransactionDate = line.Date,
                    TickerSymbol = line.UnderlyingSymbol,
                    Amount = line.Value,
                    Quantity = line.Quantity,
                    Fees = line.Fees,
                    Price = line.AveragePrice,
                    TransactionType = GetTransactionType(line),
                    AssetClass = assetClass,
                    Currency = "USD",
                    CommissionCurrency = "USD",
                    SettlementDate = SettlementDateBL.Inst.GetSettlementDate(line.Date, assetClass)
                };

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

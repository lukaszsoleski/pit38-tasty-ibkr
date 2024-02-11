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
        public List<Transaction> GetTastyTransactions()
        {
            var csv = ImportCSV.LoadTastyTradeCSV();

            var transactions = new List<Transaction>();

            foreach(var line in csv)
            {
                var transaction = new Transaction()
                {
                    TransactionDate = line.Date,
                    TickerSymbol = line.UnderlyingSymbol,
                    Amount = line.Value,
                    Quantity = line.Quantity,
                    Fees = line.Fees,
                    Price = line.AveragePrice,
                    TransactionType = GetTransactionType(line),
                    AssetClass = GetInstrumentType(line),
                    Currency = "USD",
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
    }
}

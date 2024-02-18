using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr.Model
{
    public class Transaction
    {
        public TransactionTypeEnum TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime SettlementDate { get; set; }

        public string Currency { get; set; }
        public string CommissionCurrency { get; set; }

        public decimal Amount { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Fees { get; set; }

        public AssetClassEnum AssetClass { get; set; }

        public string TickerSymbol { get; set; }

        public Rate Rate { get; set; }
        
        public decimal AmountPLN { get; private set; }
        public decimal PricePLN { get; private set; }
        public decimal FeesPLN { get; private set; }
        public decimal ProfitLossPLN { get; private set; }
        
        public void SetPLN()
        {
            if (Rate == null) return;

            AmountPLN = Math.Round(Amount * Rate.Mid, 4);
            PricePLN = Math.Round(Price * Rate.Mid, 4);
            FeesPLN = Math.Round(Price * Rate.Mid, 4);
        }
        public void SetProfitLossPLN(decimal profitLoss)
        {
            ProfitLossPLN = profitLoss;
        }
        public static void PrintTransactions(List<Transaction> transactions)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{0,-20} {1,-20} {2,-20} {3,-10} {4,-10} {5,-10} {6,-10} {7,-10} {8,-10} {9,-15} {10,-15} {11,-15} {12,-10}");
            foreach (var transaction in transactions)
            {
                sb.AppendLine(GetFormattedString(transaction));
            }
            Console.WriteLine(sb.ToString());
        }
        public static string GetFormattedString(Transaction transaction)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0,-20} {1,-20} {2,-20} {3,-10} {4,-10} {5,-10} {6,-10} {7,-10} {8,-10} {9,-15} {10,-15} {11,-15} {12,-10} ",
                transaction.TransactionType,
                transaction.TransactionDate.ToString("yyyy-MM-dd"),
                transaction.SettlementDate.ToString("yyyy-MM-dd"),
                transaction.Currency,
                transaction.CommissionCurrency,
                transaction.Amount,
                transaction.Quantity,
                transaction.Price,
                transaction.Fees,
                transaction.AssetClass,
                transaction.TickerSymbol,
                transaction.Rate,
                transaction.AmountPLN);
            return sb.ToString();
        }

    }
}

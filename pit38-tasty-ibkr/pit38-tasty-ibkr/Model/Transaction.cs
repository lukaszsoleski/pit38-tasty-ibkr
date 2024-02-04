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

        public DateTime? SettlementDate { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Fees { get; set; }

        public AssetClassEnum AssetClass { get; set; }

        public string TickerSymbol { get; set; }

        
        public DateTime GetCurrencyExchangeDate()
        {
            if (SettlementDate.HasValue)
            {
                return SettlementDate.Value.AddDays(-1);
            }
            else
            {
                return TransactionDate.AddDays(1);
            }
        }

    }
}

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

        public decimal Commitions { get; set; }

        public AssetClassEnum AssetClass { get; set; }

        public string TickerSymbol { get; set; }

        public string Description { get; set; }
        public Rate Rate { get; private set; }
        
        public decimal AmountPLN { get; private set; }
        public decimal PricePLN { get; private set; }
        public decimal FeesPLN { get; private set; }
        public decimal ProfitLossPLN { get; private set; }

        public List<Transaction> BuyReference { get; set; } = new List<Transaction>();

        
        public void SetRate(Rate rate)
        {
            Rate = rate;
            
            AmountPLN = Math.Round(Amount * Rate.Mid, 4);
            PricePLN = Math.Round(Price * Rate.Mid, 4);
            FeesPLN = Math.Round(Commitions * Rate.Mid, 4);
        }
        public void SetProfitLossPLN(decimal profitLoss)
        {
            ProfitLossPLN = profitLoss;
        }
        public Transaction Copy()
        {
            return new Transaction()
            {
                Amount = this.Amount,
                AmountPLN = this.AmountPLN,
                AssetClass = this.AssetClass,
                CommissionCurrency = CommissionCurrency,
                Commitions = Commitions,
                Currency = Currency,
                Description = Description,
                FeesPLN = FeesPLN,
                Price = Price,
                PricePLN = PricePLN,
                Quantity = Quantity,
                Rate = Rate,
                SettlementDate = SettlementDate,
                TickerSymbol = TickerSymbol,
                TransactionDate = TransactionDate,
                TransactionType = TransactionType
            };
        }
        public override string ToString()
        {
            return $"Transaction Type: {TransactionType}, " +
                   $"Ticker Symbol: {TickerSymbol}, " +
                   $"Transaction Date: {TransactionDate}, " +
                   $"Settlement Date: {SettlementDate}, " +
                   $"Currency: {Currency}, " +
                   $"Com. Curr.: {CommissionCurrency}, " +
                   $"Amount: {Amount}, " +
                   $"Quantity: {Quantity}, " +
                   $"Price: {Price}, " +
                   $"Commissions: {Commitions}, " +
                   $"Asset Class: {AssetClass}, " +
                   $"Rate: {Rate}, ";
        }
    }
}

using pit38_tasty_ibkr.BL.Models;
using pit38_tasty_ibkr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr
{
    public class FIFOStockCalculator
    {
        private List<Transaction> transactions;
        public List<Transaction> Transactions => transactions ?? new List<Transaction>();
        public FIFOStockCalculator()
        {
            transactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            if(transaction.AssetClass == AssetClassEnum.Stock)
                transactions.Add(transaction);

        }
        public TransactionsSummary GetTotalCostAndProfit(DateTime startDate, DateTime endDate)
        {
            var sells = transactions.Where(x => x.TransactionDate.Date >= startDate && x.TransactionDate.Date <= endDate && x.ProfitLossPLN != 0);

            var totalProfit = sells.Sum(x => Math.Abs(x.AmountPLN));

            var totalCost = sells.Sum(x => Math.Abs(x.FeesPLN)) + sells.SelectMany(x => x.BuyReference).Sum(x => Math.Abs(x.AmountPLN) + Math.Abs(x.FeesPLN));

            var result = new TransactionsSummary()
            {
                Cost = totalCost,
                Profit = totalProfit,
                FromDate = startDate,
                ToDate = endDate
            };

            return result;
        }
        public void CalculateProfitOrLoss()
        {
   
            var sellTransactions = transactions.Where(t => t.TransactionType == TransactionTypeEnum.SELL).OrderBy(t => t.TransactionDate);
            
            foreach (var sellTransaction in sellTransactions)
            {
                var buyTransactions = transactions.Where(t => t.TransactionType == TransactionTypeEnum.BUY && t.TickerSymbol == sellTransaction.TickerSymbol && t.Quantity > 0).OrderBy(t => t.TransactionDate);
               
                decimal remainingQuantityToSell = sellTransaction.Quantity;
                
                decimal profitOrLoss = 0;
                
                foreach (var buyTransaction in buyTransactions)
                {
                    if (remainingQuantityToSell <= 0)
                        break;

                    decimal quantityToSellFromThisBuy = Math.Min(remainingQuantityToSell, buyTransaction.Quantity);
          
                    profitOrLoss += (sellTransaction.PricePLN - buyTransaction.PricePLN) * quantityToSellFromThisBuy;

                    remainingQuantityToSell -= quantityToSellFromThisBuy;
                    buyTransaction.Quantity -= quantityToSellFromThisBuy;

                    if(buyTransaction.Quantity == 0)
                    {
                        profitOrLoss -= buyTransaction.FeesPLN;
                    }

                    var buyReference = buyTransaction.Copy();

                    buyReference.Quantity = quantityToSellFromThisBuy;

                    buyReference.Amount = quantityToSellFromThisBuy * buyReference.Price;

                    buyReference.Commitions = buyTransaction.Quantity > 0 ? 0 : buyTransaction.Commitions;

                    buyReference.CalculatePLN();

                    sellTransaction.BuyReference.Add(buyReference);
                }

                profitOrLoss -= sellTransaction.FeesPLN;

                sellTransaction.SetProfitLossPLN(profitOrLoss);
            }
           
        }
    }
}

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

        public FIFOStockCalculator()
        {
            transactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
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
          
                    profitOrLoss += (sellTransaction.Price - buyTransaction.Price) * quantityToSellFromThisBuy;

                    remainingQuantityToSell -= quantityToSellFromThisBuy;
                    buyTransaction.Quantity -= quantityToSellFromThisBuy;

                    if(buyTransaction.Quantity == 0)
                    {
                        profitOrLoss -= buyTransaction.Fees;
                    }
                }

                profitOrLoss -= sellTransaction.Fees;

                sellTransaction.ProfitLoss = profitOrLoss;
            }
           
        }
    }
}

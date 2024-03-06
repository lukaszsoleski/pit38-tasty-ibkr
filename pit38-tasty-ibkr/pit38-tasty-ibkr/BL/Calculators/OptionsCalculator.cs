using pit38_tasty_ibkr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr
{
    public class OptionsCalculator
    {
        private List<Transaction> transactions;

        public List<Transaction> Transactions => transactions ?? new List<Transaction>();
        public OptionsCalculator()
        {
            transactions = new List<Transaction>();
        }
        public void AddTransaction(Transaction transaction)
        {
            if(transaction.AssetClass == AssetClassEnum.Option)
                transactions.Add(transaction);

        }
        public void CalculateProfitOrLoss()
        {
            foreach(var t in transactions)
            {
                decimal profitOrLoss;
                if (t.TransactionType == TransactionTypeEnum.BUY)
                {
                    // cost:
                    profitOrLoss = Math.Abs(t.AmountPLN) + Math.Abs(t.FeesPLN);
                    profitOrLoss = profitOrLoss * -1;
                    
                    t.SetProfitLossPLN(profitOrLoss);
                }else if(t.TransactionType == TransactionTypeEnum.SELL)
                {
                    // profit:
                    profitOrLoss = Math.Abs(t.AmountPLN) - Math.Abs(t.FeesPLN);

                    t.SetProfitLossPLN(profitOrLoss);
                }
            }
        }
    }
}

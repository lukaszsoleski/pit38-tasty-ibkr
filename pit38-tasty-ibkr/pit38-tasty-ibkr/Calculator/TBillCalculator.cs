using pit38_tasty_ibkr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr
{
    class TBillCalculator
    {
        private List<Transaction> transactions;

        public TBillCalculator()
        {
            transactions = new List<Transaction>();

        }
        public void AddTransaction(Transaction transaction)
        {
            if (transaction.AssetClass == AssetClassEnum.Bill)
                transactions.Add(transaction);

        }
        public void CalculateProfitOrLoss()
        {
            foreach (var t in transactions)
            {
                decimal profitOrLoss = t.AmountPLN - t.FeesPLN;

                t.SetProfitLossPLN(profitOrLoss);
            }
        }
    }
}

using pit38_tasty_ibkr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr
{
    class Program
    {
        static DateTime StartDate = new DateTime(2022, 12, 29);

        static DateTime EndDate = new DateTime(2023, 12, 27);
        static void Main(string[] args)
        {
            var transactionsHitory = TransactionsCSV.Inst.GetTransactionsHistory();

            foreach(var t in transactionsHitory)
            {
                var rate = ExchangeRateAPI.Inst.GetTradeExchangeRate(t.SettlementDate, t.Currency, FallbackRateEnum.Backward);

                if (rate == null) throw new Exception("Rate cannot be null");

                t.SetRate(rate);
            }

            var fifoStockCalculator = new FIFOStockCalculator();

            var optionsCalculator = new OptionsCalculator();


            foreach(var transation in transactionsHitory)
            {
                fifoStockCalculator.AddTransaction(transation);

                optionsCalculator.AddTransaction(transation);
            }

            optionsCalculator.CalculateProfitOrLoss();

            fifoStockCalculator.CalculateProfitOrLoss();

            var optionsTransactions = optionsCalculator.Transactions.Where(x => x.TransactionDate.Date >= StartDate && x.TransactionDate.Date <= EndDate).ToList();

            var stockTransactions = fifoStockCalculator.Transactions.Where(x => x.TransactionDate.Date >= StartDate && x.TransactionDate.Date <= EndDate && x.ProfitLossPLN != 0);

            var totalOptionsCost = optionsTransactions.Where(x => x.ProfitLossPLN < 0).Sum(x => x.ProfitLossPLN);

            var totalOptionsProfit = optionsTransactions.Where(x => x.ProfitLossPLN > 0).Sum(x => x.ProfitLossPLN);



            var optionsProfitOrLoss = optionsTransactions.Sum(x => x.ProfitLossPLN);
            var stockProfitOrLoss = stockTransactions.Sum(x => x.ProfitLossPLN);

            ExchangeRateCache.Inst.Dispose();

            Console.Read();
        }
    }
}

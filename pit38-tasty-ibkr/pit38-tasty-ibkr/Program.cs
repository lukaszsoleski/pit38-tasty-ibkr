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

            var optionsCostProfit = optionsCalculator.GetTotalCostAndProfit(StartDate,EndDate);

            var stocksCostProfit = fifoStockCalculator.GetTotalCostAndProfit(StartDate, EndDate);



            ExchangeRateCache.Inst.Dispose();

            Console.Read();
        }
    }
}

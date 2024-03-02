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
            var transactionsHitory = TransactionBL.Inst.GetTransactionsHistory();

            var fifoStockCalculator = new FIFOStockCalculator();

            var optionsCalculator = new OptionsCalculator();

            foreach(var transation in transactionsHitory)
            {
                fifoStockCalculator.AddTransaction(transation);

                optionsCalculator.AddTransaction(transation);
            }

            fifoStockCalculator.CalculateProfitOrLoss();

            optionsCalculator.CalculateProfitOrLoss();



            ExchangeRateDC.Inst.Dispose();

            Console.Read();
        }
    }
}

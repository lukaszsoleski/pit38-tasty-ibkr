using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr.BL.Models
{
    public class TransactionsSummary
    {
        public decimal Profit { get; set; }
        public decimal Cost { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public override string ToString()
        {
            decimal total = Math.Abs(Profit) - Math.Abs(Cost);
           
            string status = total >= 0 ? "made" : "lost";
           
            string taxInfo = "";
            
            if (total >= 0)
            {
                decimal taxAmount = total * 0.19m; // 19% tax rate
                taxInfo = $", 19% tax rate: {Math.Round(taxAmount,2)} PLN";
            }
            return $"Profit: {Math.Round(Profit, 2)},\nCost: {Math.Round(Cost, 2)},\nFrom Date: {FromDate.Date},\nTo Date: {ToDate.Date},\nYou {status} {Math.Abs(total)}{taxInfo}";

        }
    }
}

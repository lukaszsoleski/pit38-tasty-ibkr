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
    }
}

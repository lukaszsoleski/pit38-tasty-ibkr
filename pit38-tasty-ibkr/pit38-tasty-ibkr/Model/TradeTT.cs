using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr.Model
{
    public class TradeTT
    {
        public DateTime Date { get; set; }
        public string Action { get; set; }
        public string Symbol { get; set; }
        public string InstrumentType { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal Commissions { get; set; }
        public decimal Fees { get; set; }
        public int Multiplier { get; set; }
        public string RootSymbol { get; set; }
        public string UnderlyingSymbol { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal StrikePrice { get; set; }
        public string CallOrPut { get; set; }
        public long OrderNumber { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
    }
}

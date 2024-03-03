using pit38_tasty_ibkr.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr.Model
{
    public class TradeTT
    {
        // Your properties...
        public string Date { get; set; }
        public string Action { get; set; }
        public string Symbol { get; set; }
        public string InstrumentType { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Quantity { get; set; }
        public string AveragePrice { get; set; }
        public string Commissions { get; set; }
        public string Fees { get; set; }
        public string Multiplier { get; set; }
        public string RootSymbol { get; set; }
        public string UnderlyingSymbol { get; set; }
        public string ExpirationDate { get; set; }
        public string StrikePrice { get; set; }
        public string CallOrPut { get; set; }
        public string OrderNumber { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        // Other properties...

        public decimal ValueNum => Value.ConvertToDecimal();
        public decimal QuantityNum => Quantity.ConvertToDecimal();
        public decimal AveragePriceNum => AveragePrice.ConvertToDecimal();
        public decimal CommissionsNum => Commissions.ConvertToDecimal();
        public decimal FeesNum => Fees.ConvertToDecimal();
        public decimal MultiplierNum => Multiplier.ConvertToDecimal();
        public decimal StrikePriceNum => StrikePrice.ConvertToDecimal();
    }
}

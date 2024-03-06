using CsvHelper.Configuration.Attributes;
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
        [Name("Instrument Type")]
        public string InstrumentType { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Quantity { get; set; }
        [Name("Average Price")]
        public string AveragePrice { get; set; }
        public string Commissions { get; set; }
        public string Fees { get; set; }
        public string Multiplier { get; set; }
        [Name("Root Symbol")]
        public string RootSymbol { get; set; }
        [Name("Underlying Symbol")]
        public string UnderlyingSymbol { get; set; }
        [Name("Expiration Date")]
        public string ExpirationDate { get; set; }
        [Name("Strike Price")]
        public string StrikePrice { get; set; }
        [Name("Call or Put")]
        public string CallOrPut { get; set; }
        [Name("Order #")]
        public string OrderNumber { get; set; }
        public string Type { get; set; }
        [Name("Sub Type")]
        public string SubType { get; set; }
        // Other properties...

        public decimal ValueNum => Math.Abs(Value.ConvertToDecimal());
        public decimal QuantityNum => Math.Abs(Quantity.ConvertToDecimal());
        public decimal AveragePriceNum => Math.Abs(AveragePrice.ConvertToDecimal());
        public decimal CommissionsNum => Math.Abs(Commissions.ConvertToDecimal());
        public decimal FeesNum => Math.Abs(Fees.ConvertToDecimal());
        public decimal MultiplierNum => Math.Abs(Multiplier.ConvertToDecimal());
        public decimal StrikePriceNum => Math.Abs(StrikePrice.ConvertToDecimal());
    }
}

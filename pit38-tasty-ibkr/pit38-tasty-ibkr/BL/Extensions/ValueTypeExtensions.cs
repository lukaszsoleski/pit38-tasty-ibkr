using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr.Extensions
{
    public static class ValueTypeExtensions
    {
        public static decimal ConvertToDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0m;

            decimal result;

            string valTmp = value;

            if (valTmp.Contains(","))
            {
                if(valTmp.IndexOf(",") == valTmp.Length - 3)// Sold 2 SPY 03/28/24 Call 498.00 FOR "1,120.00"
                {
                    throw new Exception("Invalid number formatting");
                }

                valTmp = value.Replace(",", "").Replace(" ", "");
            }

            if (decimal.TryParse(valTmp, out result))
                return result;

            return 0m;
        }
    }
}

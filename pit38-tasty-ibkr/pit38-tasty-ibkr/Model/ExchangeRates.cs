using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace pit38_tasty_ibkr.Model
{
    public class ExchangeRates
    {
        public ExchangeRates()
        {
            Rates = new List<Rate>();
        }
        public string Table { get; set; }
        public string Currency { get; set; }
        public string Code { get; set; }
        public List<Rate> Rates { get; set; }

        public void PrintExchangeRates()
        {
            Console.WriteLine($"Table: {Table}");
            Console.WriteLine($"Currency: {Currency}");
            Console.WriteLine($"Code: {Code}");

            if (Rates != null && Rates.Count > 0)
            {
                Console.WriteLine("Rates:");

                foreach (var rate in Rates)
                {
                    Console.WriteLine($"  Rate No: {rate.No}");
                    Console.WriteLine($"  Effective Date: {rate.EffectiveDate}");
                    Console.WriteLine($"  Mid: {rate.Mid}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No rates available.");
            }
        }

        public void Add(Rate rate)
        {
            if (Rates == null) Rates = new List<Rate>();

            if(!Rates.Any(x => x.EffectiveDate == rate.EffectiveDate))
            {
                Rates.Add(rate);
            }
        }



       
    }

    public class Rate
    {
        public string No { get; set; }

        public DateTime EffectiveDate { get; set; }

        public double Mid { get; set; }
    }
}

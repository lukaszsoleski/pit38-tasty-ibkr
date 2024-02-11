using CsvHelper;
using Newtonsoft.Json;
using pit38_tasty_ibkr.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr
{
    public class ExchangeRateDC : IDisposable
    {
        public const string FileName = "ExchangeRates";

        public static ExchangeRateDC Inst = new ExchangeRateDC();

        private Dictionary<string, ExchangeRates> exchangeRates = new Dictionary<string, ExchangeRates>();

        public Rate GetRate(string code, DateTime date)
        {
            if (!exchangeRates.ContainsKey(code))
            {
                var loaded = Load(code);

                if (loaded != null)
                {
                    exchangeRates.Add(code, loaded);
                }
            }

            if (!exchangeRates.ContainsKey(code) || exchangeRates[code].Rates == null || !exchangeRates[code].Rates.Any()) return null;

            return exchangeRates[code].Rates.FirstOrDefault(x => x.EffectiveDate.Date == date.Date);
        }

        public void AddRates(ExchangeRates rates)
        {
            if (!exchangeRates.ContainsKey(rates.Code))
            {
                var loaded = Load(rates.Code);

                if(loaded != null)
                {
                    exchangeRates.Add(rates.Code, loaded);
                }
            }
            if (!exchangeRates.TryGetValue(rates.Code, out var currencyRates))
            {
                currencyRates = new ExchangeRates();
            }
            foreach (var newRate in rates.Rates)
            {
                var exRate = currencyRates.Rates.FirstOrDefault(x => x.EffectiveDate.Date == newRate.EffectiveDate.Date);
                
                if(exRate != null)
                {
                    currencyRates.Rates.Remove(exRate);
                }
              
                currencyRates.Rates.Add(newRate);
            }
        }

        public void Dispose()
        {
            foreach(var r in exchangeRates)
            {
                Save(r.Value);
            }
        }

        private void Save(ExchangeRates r)
        {
            if (string.IsNullOrEmpty(r.Code)) throw new Exception("Empty Code");

            if (r.Rates != null)
            {
                r.Rates = r.Rates.OrderByDescending(x => x.EffectiveDate).ToList();
            }
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);

            File.WriteAllText($"{FileName}_{r.Code}.json", json);
        }

        private ExchangeRates Load(string code)
        {
            try
            {
                string path = $"{FileName}_{code}.json";

                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);

                    return JsonConvert.DeserializeObject<ExchangeRates>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

   
    }
}
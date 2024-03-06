using pit38_tasty_ibkr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using System.Net;

namespace pit38_tasty_ibkr
{
    internal class ExchangeRateAPI
    {
        public static ExchangeRateAPI Inst = new ExchangeRateAPI();

        private const string nbp_base_url = "http://api.nbp.pl/api/exchangerates/rates/a/";

        private ExchangeRates GetResultFromNbpApi(string currencyCode, DateTime date)
        {
            var link = nbp_base_url + $"{currencyCode.ToLower()}/{date:yyyy-MM-dd}/";

            ExchangeRates r = null;
            try
            {
                var tmp = link.GetJsonAsync<ExchangeRates>();

                r = tmp.Result;
            }
            catch (Exception ex)
            {
                if (ex is FlurlHttpException && ((FlurlHttpException)ex).StatusCode == (int)HttpStatusCode.NotFound)
                {
                    throw new BankHolidayException();
                }
                else
                {
                    throw ex;
                }
            }

            return r;
        }

        public Rate GetTradeExchangeRate(DateTime day, string currency, FallbackRateEnum fallbackDirection)
        {
            try
            {
                var rate = ExchangeRateCache.Inst.GetRate(currency, day);
                
                if (rate == null)
                {
                    var apiExchangeRates = GetResultFromNbpApi(currency, day);

                    ExchangeRateCache.Inst.AddRates(apiExchangeRates);

                    rate = apiExchangeRates.Rates.Single();
                }

                return rate;

            }catch (BankHolidayException)
            {
                if (fallbackDirection == FallbackRateEnum.Backward)
                {
                    return GetTradeExchangeRate(day.AddDays(-1), currency, fallbackDirection);
                }
                else if (fallbackDirection == FallbackRateEnum.Forward)
                {
                    return GetTradeExchangeRate(day.AddDays(1), currency, fallbackDirection);
                }
                else throw new NotImplementedException();
            }
        }
    }
}

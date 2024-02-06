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
    internal class ExchangeRateBL
    {
        public static ExchangeRateBL Inst = new ExchangeRateBL();

        private const string nbp_base_url = "http://api.nbp.pl/api/exchangerates/rates/a/";

        private async Task<ExchangeRates> GetResultFromNbpApi(string currencyCode, DateTime date)
        {
            var link = nbp_base_url + $"{currencyCode.ToLower()}/{date:yyyy-MM-dd}/";
            try
            {
                return await link.GetJsonAsync<ExchangeRates>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    throw new BankHolidayException();
                }
                else
                {
                    throw ex;
                }
            }
        }

        public Rate GetTradeExchangeRate(DateTime day, string currency, FallbackRateEnum fallbackDirection)
        {
            try
            {
                var rate = ExchangeRateDC.Inst.GetRate(currency, day);
                
                if (rate == null)
                {
                    var apiExchangeRates = GetResultFromNbpApi(currency, day).Result;

                    ExchangeRateDC.Inst.AddRates(apiExchangeRates);
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

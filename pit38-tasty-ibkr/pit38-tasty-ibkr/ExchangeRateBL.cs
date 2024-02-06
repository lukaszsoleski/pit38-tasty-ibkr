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
        private const string nbp_base_url = "http://api.nbp.pl/api/exchangerates/rates/a/"; 
 
        public async Task<ExchangeRates> GetResultFromNbpApi(string currencyCode, DateTime date)
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
    }
}

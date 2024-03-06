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
    public class SettlementDateAPI
    {
        public static SettlementDateAPI Inst = new SettlementDateAPI();

        public const string HolidayAPI = "https://date.nager.at/api/v3/publicholidays/{0}/{1}";

        private List<Holiday> Holidays = new List<Holiday>();
        public DateTime GetSettlementDate(DateTime transactionDate, AssetClassEnum assetClass)
        {
            var forwardDays = 0;
            if(assetClass == AssetClassEnum.Option)
            {
                forwardDays = 1;
            }else if(assetClass == AssetClassEnum.Stock)
            {
                forwardDays = 2;
            }

            var currentDate =transactionDate;
            int i = 0; 
            
            while(i < forwardDays)
            {
                currentDate = currentDate.AddDays(1);

                if(currentDate.Date.DayOfWeek == DayOfWeek.Saturday 
                    || currentDate.Date.DayOfWeek == DayOfWeek.Sunday
                    || IsHoliday("US", currentDate))
                {
                    continue;
                }

                i++;
            }

            return currentDate;
        }
        private bool IsHoliday(string country, DateTime dateTime)
        {
            var holidays = GetHolidays(country, dateTime.Year.ToString());

            var holiday = holidays.FirstOrDefault(x => x.Global && x.Date.Date == dateTime.Date);

            return holiday != null;
        }

        private string GetHolidayAPILink(string country, string year)
        {
            return string.Format(HolidayAPI, country, year);
        }

        private List<Holiday> GetHolidaysFromAPI(string country, string year)
        {
            return GetHolidayAPILink(year, country).GetJsonAsync<List<Holiday>>().Result;
        }

        private List<Holiday> GetHolidays(string country, string year)
        {
            if(Holidays.Any(x => x.Date.Year == int.Parse(year) && x.CountryCode == country))
            {
                return Holidays.Where(x => x.Date.Year == int.Parse(year) && x.CountryCode == country).ToList();
            }
            else
            {
                var apiResult = GetHolidaysFromAPI(country, year);

                apiResult.ForEach(x => Holidays.Add(x));

                return apiResult;
            }
        }
    }
}

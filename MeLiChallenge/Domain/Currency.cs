using MeLiChallenge.Models;
using System.Linq;

namespace MeLiChallenge.Domain
{
    public class Currency
    {
        private decimal _usdRate;

        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Rate { get { return GetRate(); } }

        public Currency(ExchangeData exchangeData, CountryData countryData, string baseCurrencyCode)
        {
            Code = countryData.Currencies.FirstOrDefault().Code;
            Name = countryData.Currencies.FirstOrDefault().Name;
            Symbol = countryData.Currencies.FirstOrDefault().Symbol;

            exchangeData.Rates.TryGetValue(baseCurrencyCode, out _usdRate);

        }

        private decimal GetRate()
        {
            return 1 / _usdRate;
        }
    }
}

using System.Collections.Generic;

namespace MeLiChallenge.Models
{
    public class CountryData
    {
        public string Name { get; set; }

        public IEnumerable<double> Latlng { get; set; }

        public IEnumerable<CurrencyData> Currencies { get; set; }

        public string Alpha2Code { get; set; }

        public IEnumerable<string> Timezones { get; set; }

        public IEnumerable<LanguageData> Languages { get; set; }
    }
}

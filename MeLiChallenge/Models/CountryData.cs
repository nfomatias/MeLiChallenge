using System;
using System.Collections.Generic;
using System.Text;

namespace MeLiChallenge.Models
{
    public class CountryData
    {
        public string Name { get; set; }

        public IEnumerable<decimal> Latlng { get; set; }

        public IEnumerable<CurrencyData> Currencies { get; set; }

        public string Alpha2Code { get; set; }

        public IEnumerable<string> Timezones { get; set; }
    }
}

using MeLiChallenge.Models;
using System.Collections.Generic;

namespace MeLiChallenge.Domain
{
    public class Country
    {
        private decimal _referenceLat;
        private decimal _referenceLng;
        
        public string Code { get; private set; }
        public string Name { get; private set; }
        public decimal Lat { get; private set; }
        public decimal Lng { get; private set; }
        public IEnumerable<string> Timezones { get; set; }
        public Currency Currency { get; set; }

        public int RelativeDistance
        {
            get { return GetRelativeDistance(); }
        }

        public Country(CountryData countryData)
        {
            Name = countryData.Name;
            Code = countryData.Alpha2Code;
            Timezones = countryData.Timezones;
        }

        public Country(CountryData countryData, Currency currency, decimal referenceLat, decimal referenceLng)
        {
            _referenceLat = referenceLat;
            _referenceLng = referenceLng;
            Name = countryData.Name;
            Code = countryData.Alpha2Code;
            Timezones = countryData.Timezones;
            Currency = currency;
        }

        private int GetRelativeDistance()
        {
            return 0;
        }
    }
}

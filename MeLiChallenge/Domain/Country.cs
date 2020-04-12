using GeoCoordinatePortable;
using MeLiChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;

namespace MeLiChallenge.Domain
{
    public class Country
    {
        private double _referenceLat;
        private double _referenceLng;
        
        public string Code { get; private set; }
        public string Name { get; private set; }
        public double Lat { get; private set; }
        public double Lng { get; private set; }
        public IEnumerable<string> Timezones { get; set; }
        public Currency Currency { get; set; }

        public List<string> Languages { get; set; }

        public int RelativeDistance
        {
            get { return GetRelativeDistance(); }
        }

        public Country(CountryData countryData)
        {
            Name = countryData.Name;
            Code = countryData.Alpha2Code;
            Timezones = countryData.Timezones;
            var latlng = countryData.Latlng.ToList<double>();
            Lat = latlng[0];
            Lng = latlng[1];   
        }

        public Country(CountryData countryData, Currency currency, double referenceLat, double referenceLng)
        {
            _referenceLat = referenceLat;
            _referenceLng = referenceLng;
            Name = countryData.Name;
            Code = countryData.Alpha2Code;
            Timezones = countryData.Timezones;
            Currency = currency;

            var latlng = countryData.Latlng.ToList<double>();
            Lat = latlng[0];
            Lng = latlng[1];

            Languages = new List<string>(countryData.Languages.Count());

            foreach (var item in countryData.Languages)
                Languages.Add(item.Name);
        }

        private int GetRelativeDistance()
        {
            var pin1 = new GeoCoordinate(Lat, Lng);
            var pin2 = new GeoCoordinate(_referenceLat, _referenceLng);

            return Convert.ToInt32(pin1.GetDistanceTo(pin2)/1000); //distancia en km
        }
    }
}

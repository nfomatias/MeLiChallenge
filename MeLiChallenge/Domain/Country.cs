﻿using GeoCoordinatePortable;
using MeLiChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeLiChallenge.Domain
{
    public class Country
    {
        private readonly double _referenceLat;
        private readonly double _referenceLng;

        public string Code { get; private set; }
        public string Name { get; private set; }
        public double Lat { get; private set; }
        public double Lng { get; private set; }
        private IEnumerable<string> _timezones { get; set; }
        public Currency Currency { get; set; }
        public List<string> Languages { get; set; }
        public List<DateTime> CurrentDateTimes { get { return GetDateTimes(); } }

        public static Country Default
        {
            get
            {
                return new Country();
            }
        }

        public int ReferenceDistance
        {
            get { return GetReferenceDistance(); }
        }

        private Country()
        {

        }

        public Country(CountryData countryData)
        {
            _timezones = countryData.Timezones;
            Name = countryData.Name;
            Code = countryData.Alpha2Code;
            var latlng = countryData.Latlng.ToList<double>();
            Lat = latlng[0];
            Lng = latlng[1];
        }

        public Country(CountryData countryData, Currency currency, double referenceLat, double referenceLng)
        {
            _timezones = countryData.Timezones;
            _referenceLat = referenceLat;
            _referenceLng = referenceLng;
            Name = countryData.Name;
            Code = countryData.Alpha2Code;
            Currency = currency;

            var latlng = countryData.Latlng.ToList<double>();
            Lat = latlng[0];
            Lng = latlng[1];

            Languages = new List<string>(countryData.Languages.Count());

            foreach (var item in countryData.Languages)
                Languages.Add(item.Name);
        }

        private int GetReferenceDistance()
        {
            var pin1 = new GeoCoordinate(Lat, Lng);
            var pin2 = new GeoCoordinate(_referenceLat, _referenceLng);

            return Convert.ToInt32(pin1.GetDistanceTo(pin2) / 1000); //distancia en km
        }

        private List<DateTime> GetDateTimes()
        {
            var retVal = new List<DateTime>();

            if (_timezones == null)
                return retVal;

            foreach (var item in _timezones)
            {
                var timezone = item;
                if (item == "UTC")
                    timezone = "UTC+00:00"; //Fix: cuando un pais es UTC+00:00, en el valor viene "UTC"

                var hours = GetHours(timezone);
                var minutes = GetMinutes(timezone);

                retVal.Add(DateTime.UtcNow.AddMinutes(hours * 60 + minutes));
            }
            return retVal;
        }

        private int GetMinutes(string timezone)
        {
            var positiveOrNegative = 1;

            if (timezone.Substring(3, 1) == "-")
                positiveOrNegative = -1;

            return Convert.ToInt32(timezone.Substring(7, 2)) * positiveOrNegative;
        }

        private int GetHours(string timezone)
        {
            var positiveOrNegative = 1;

            if (timezone.Substring(3, 1) == "-")
                positiveOrNegative = -1;

            return Convert.ToInt32(timezone.Substring(4, 2)) * positiveOrNegative;
        }
    }
}

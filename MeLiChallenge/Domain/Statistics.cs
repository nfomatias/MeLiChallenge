using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeLiChallenge.Domain
{
    public class Statistics
    {

        public static string FartestCountryKey = "FartestCountryKey";
        public static string NearestCountryKey = "NearestCountryKey";
        public static string AverageDistanceKey = "AverageDistance";

        public Country FarthestCountry { get; set; }
        public Country NearestCountry { get; set; }

        public int AverageDistance { get; set; }
    }
}

using System;

namespace MeLiChallenge.Domain
{
    public class Statistics
    {
        public Country FarthestCountry { get; set; }
        public Country NearestCountry { get; set; }

        public UInt32 AverageDistance { get; set; }

        public Statistics(Country farthest, Country nearest, UInt32 average)
        {
            FarthestCountry = farthest;
            NearestCountry = nearest;
            AverageDistance = average;
        }
    }
}

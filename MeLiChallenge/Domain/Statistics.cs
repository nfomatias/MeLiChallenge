using System;

namespace MeLiChallenge.Domain
{
    public class Statistics
    {
        public Country FarthestCountry { get; set; }
        public Country NearestCountry { get; set; }
        public ulong AverageDistance { get; set; }

        public Statistics(Country farthest, Country nearest, ulong average)
        {
            FarthestCountry = farthest;
            NearestCountry = nearest;
            AverageDistance = average;
        }
    }
}

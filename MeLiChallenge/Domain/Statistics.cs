using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeLiChallenge.Domain
{
    public class Statistics
    {
        public Country FarthestCountry { get; set; }
        public Country NearestCountry { get; set; }

        public int AverageDistance { get; set; }
    }
}

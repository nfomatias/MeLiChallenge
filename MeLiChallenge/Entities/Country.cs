using System;
using System.Collections.Generic;
using System.Text;

namespace MeLiChallenge.Entities
{
    public class Country
    {
        public string Name { get; set; }

        public IEnumerable<decimal> Latlng { get; set; }

        public IEnumerable<Currency> Currencies { get; set; }

        public int DistanceOf(Country country)
        {
            return 0;
        }
    }
}

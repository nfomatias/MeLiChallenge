using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeLiChallenge.Models
{
    public class ExchangeData
    {
        public string Base { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}

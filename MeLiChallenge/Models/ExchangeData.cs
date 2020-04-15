using System.Collections.Generic;

namespace MeLiChallenge.Models
{
    public class ExchangeData
    {
        public string Base { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}

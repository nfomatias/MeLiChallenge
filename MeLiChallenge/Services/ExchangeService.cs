using MeLiChallenge.Entities;
using MeLiChallenge.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public class ExchangeService : IExchangeService
    {
        public async Task<Exchange> GetExchangeData(string currencyCode)
        {

            //http://data.fixer.io/api/latest?access_key=8cbb50d0f402f6d992091a432a902636&base=EUR&symbols=USD

            var baseCurrency = "USD";
            var accesKey = "8cbb50d0f402f6d992091a432a902636";

            var apiClient = new ApiClient();

            var query = string.Format("latest?access_key={0}&base={1}&symbols={2}", 
                accesKey, currencyCode, baseCurrency);

            var uri = new Uri("http://data.fixer.io/api/" + query);
            
            return await apiClient.GetAsync<Exchange>(uri);
        }

    }
}

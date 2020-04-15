using MeLiChallenge.Models;
using MeLiChallenge.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace MeLiChallenge.Services.Externals
{
    public class ExchangeService : IExchangeService
    {
        private IConfiguration _configuration { get; }

        public ExchangeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ExchangeData> GetExchangeData(string currencyCode)
        {
            //http://data.fixer.io/api/latest?access_key=8cbb50d0f402f6d992091a432a902636&base=EUR&symbols=USD

            var baseCurrency = _configuration.GetValue<string>(key: SettingKeys.BaseCurrency); //"USD";
            var accesKey = _configuration.GetValue<string>(key: SettingKeys.FixerKey); //"8cbb50d0f402f6d992091a432a902636";

            var apiClient = new ApiClient();

            var query = string.Format("latest?access_key={0}&base={1}&symbols={2}",
                accesKey, currencyCode, baseCurrency);

            var uri = new Uri("http://data.fixer.io/api/" + query);

            return await apiClient.GetAsync<ExchangeData>(uri);
        }

    }
}

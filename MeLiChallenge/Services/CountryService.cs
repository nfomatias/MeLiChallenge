using MeLiChallenge.Entities;
using MeLiChallenge.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public class CountryService : ICountryService
    {
        public async Task<Country> GetCountryData(string countryCode)
        {
            var apiClient = new ApiClient();

            var baseuri = new Uri("https://restcountries.eu/rest/v2/alpha/");
            var uri = new Uri(baseuri, countryCode);

            return await apiClient.GetAsync<Country>(uri);
        }
    }
}

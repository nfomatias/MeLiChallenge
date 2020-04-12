using MeLiChallenge.Domain;
using MeLiChallenge.Models;
using MeLiChallenge.Services.Externals;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public class IPGuardService : IIPGurardService
    {
        IIPService _ipService;
        ICountryService _countryService;
        IExchangeService _exchangeService;
        IReferenceCountryService _referenceCountryService;
        IConfiguration _configuration;

        public IPGuardService(IIPService ipService, ICountryService countryService, IExchangeService exchangeService, IReferenceCountryService referenceCountryService, IConfiguration configuration)
        {
            _ipService = ipService;
            _countryService = countryService;
            _exchangeService = exchangeService;
            _referenceCountryService = referenceCountryService;
            _configuration = configuration;
        }

        public async Task<Country> GetCountry(string ipAddress)
        {
            var ipData = _ipService.GetIpData(ipAddress).Result;

            var countryData = _countryService.GetCountryData(ipData.CountryCode).Result;

            var exchangeData = _exchangeService.GetExchangeData(countryData.Currencies.FirstOrDefault().Code).Result;

            var currency = new Currency(exchangeData, countryData, _configuration.GetValue<string>(key: SettingKeys.BaseCurrency));


            var referenceCountry = _referenceCountryService.GetReferenceCountry();


            var Country = new Country(countryData, currency, referenceCountry.Lat, referenceCountry.Lng);

            return Country;
        }
    }
}

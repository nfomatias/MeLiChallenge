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
        ICacheService _cacheService;
        IIPService _ipService;
        ICountryService _countryService;
        IExchangeService _exchangeService;
        IReferenceCountryService _referenceCountryService;
        IConfiguration _configuration;
        IStatisticService _statisticService;

        public IPGuardService(ICacheService cacheService, IIPService ipService, ICountryService countryService, IExchangeService exchangeService, IReferenceCountryService referenceCountryService, IConfiguration configuration, IStatisticService statisticService)
        {
            _cacheService = cacheService;
            _ipService = ipService;
            _countryService = countryService;
            _exchangeService = exchangeService;
            _referenceCountryService = referenceCountryService;
            _configuration = configuration;
            _statisticService = statisticService;
        }

        public async Task<Country> GetCountry(string ipAddress)
        {
            var ipData = GetIpData(ipAddress);
            var countryData = GetCountryData(ipData);
            var exchangeData = GetExchangeData(countryData);
            var currency = new Currency(exchangeData, countryData, _configuration.GetValue<string>(key: SettingKeys.BaseCurrency));
            var referenceCountry = _referenceCountryService.GetReferenceCountry();
            var Country = new Country(countryData, currency, referenceCountry.Lat, referenceCountry.Lng);
            _statisticService.Notify(Country);
            return Country;
        }

        public async Task<Statistics> GetStatistics()
        {
            return await _statisticService.GetStatistics();
        }

        private ExchangeData GetExchangeData(CountryData countryData)
        {
            var currencyCode = countryData.Currencies.FirstOrDefault().Code;

            var retVal = _cacheService.GetCacheValueAsync<ExchangeData>(currencyCode).Result;

            var ttl = _configuration.GetValue<int>(SettingKeys.TimeToLiveMinutesExchange);

            if(retVal == null)
            {
                retVal = _exchangeService.GetExchangeData(currencyCode).Result;
                _cacheService.SetCacheValueAsync<ExchangeData>(currencyCode, retVal, new TimeSpan(0, ttl, 0));
            }

            return retVal;
        }

        private IPData GetIpData(string ipAddress)
        {
            var retVal = _cacheService.GetCacheValueAsync<IPData>(ipAddress).Result;

            var ttl = _configuration.GetValue<int>(SettingKeys.TimeToLiveMinutesIP);

            if(retVal == null)
            {
                retVal = _ipService.GetIpData(ipAddress).Result;
                _cacheService.SetCacheValueAsync<IPData>(ipAddress, retVal, new TimeSpan(0, ttl, 0));
            }

            return retVal;
        }

        private CountryData GetCountryData(IPData ipData)
        {
            var retVal = _cacheService.GetCacheValueAsync<CountryData>(ipData.CountryCode).Result;

            if (retVal == null)
            {
                retVal = _countryService.GetCountryData(ipData.CountryCode).Result;
                _cacheService.SetCacheValueAsync(ipData.CountryCode, retVal);
            }

            return retVal;
        }
    }
}

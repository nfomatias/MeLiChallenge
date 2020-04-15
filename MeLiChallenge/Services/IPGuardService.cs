using MeLiChallenge.Domain;
using MeLiChallenge.Models;
using MeLiChallenge.Services.Externals;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace MeLiChallenge.Services
{
    public class IPGuardService : IIPGurardService
    {
        readonly ICacheService _cacheService;
        readonly IIPService _ipService;
        readonly ICountryService _countryService;
        readonly IExchangeService _exchangeService;
        readonly IReferenceCountryService _referenceCountryService;
        readonly IConfiguration _configuration;
        readonly IStatisticService _statisticService;

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

        public Country GetCountry(string ipAddress)
        {
            var referenceCountry = _referenceCountryService.GetReferenceCountry();
            var ipData = GetIpData(ipAddress);
            var countryData = GetCountryData(ipData);
            var exchangeData = GetExchangeData(countryData);

            var currency = new Currency(exchangeData, countryData, _configuration.GetValue<string>(key: SettingKeys.BaseCurrency));

            var Country = new Country(countryData, currency, referenceCountry.Lat, referenceCountry.Lng);
            _statisticService.Notify(Country);
            return Country;
        }

        public Statistics GetStatistics()
        {
            return _statisticService.GetStatistics();
        }

        private ExchangeData GetExchangeData(CountryData countryData)
        {
            var currencyCode = countryData.Currencies.FirstOrDefault().Code;

            var retVal = _cacheService.GetCacheValueAsync<ExchangeData>(currencyCode).Result;

            var ttl = _configuration.GetValue<int>(SettingKeys.TimeToLiveMinutesExchange);

            if (retVal == null)
            {
                retVal = _exchangeService.GetExchangeData(currencyCode).Result;
                _cacheService.SetCacheValueAsync(currencyCode, retVal, new TimeSpan(0, ttl, 0));
            }

            return retVal;
        }

        private IPData GetIpData(string ipAddress)
        {
            var retVal = _cacheService.GetCacheValueAsync<IPData>(ipAddress).Result;

            if (retVal == null)
            {
                var ttl = _configuration.GetValue<int>(SettingKeys.TimeToLiveMinutesIP);
                retVal = _ipService.GetIpData(ipAddress).Result;
                _cacheService.SetCacheValueAsync(ipAddress, retVal, new TimeSpan(0, ttl, 0));
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

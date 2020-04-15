using MeLiChallenge.Domain;
using MeLiChallenge.Models;
using MeLiChallenge.Services.Externals;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;

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
        private readonly ILogger<IPGuardService> _logger;

        public IPGuardService(ICacheService cacheService, IIPService ipService, ICountryService countryService, IExchangeService exchangeService, IReferenceCountryService referenceCountryService, IConfiguration configuration, IStatisticService statisticService, ILogger<IPGuardService> logger)
        {
            _cacheService = cacheService;
            _ipService = ipService;
            _countryService = countryService;
            _exchangeService = exchangeService;
            _referenceCountryService = referenceCountryService;
            _configuration = configuration;
            _statisticService = statisticService;
            _logger = logger;
        }

        public Country GetCountry(string ipAddress)
        {
            if (!IsValidIpSintax(ipAddress))
                throw new Exception("Invalid IP address: " + ipAddress);

            var referenceCountry = _referenceCountryService.GetReferenceCountry();
            var ipData = GetIpData(ipAddress);
            var countryData = GetCountryData(ipData);
            var exchangeData = GetExchangeData(countryData);

            var currency = new Currency(exchangeData, countryData, _configuration.GetValue<string>(key: SettingKeys.BaseCurrency));

            var Country = new Country(countryData, currency, referenceCountry.Lat, referenceCountry.Lng);
            _statisticService.Notify(Country);
            return Country;
        }

        private bool IsValidIpSintax(string ipAddress)
        {
            if (ipAddress.Count(c => c == '.') != 3) return false;
            IPAddress address;
            return IPAddress.TryParse(ipAddress, out address);
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
                _logger.LogInformation("Calling exchangeService....");

                retVal = _exchangeService.GetExchangeData(currencyCode).Result;

                _logger.LogInformation("ExchangeService call finished.");
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

                _logger.LogInformation("Calling ipService....");

                retVal = _ipService.GetIpData(ipAddress).Result;

                _logger.LogInformation("IpService call finished.");
                _cacheService.SetCacheValueAsync(ipAddress, retVal, new TimeSpan(0, ttl, 0));
            }

            return retVal;
        }

        private CountryData GetCountryData(IPData ipData)
        {
            var retVal = _cacheService.GetCacheValueAsync<CountryData>(ipData.CountryCode).Result;

            if (retVal == null)
            {
                _logger.LogInformation("Calling countryService....");

                retVal = _countryService.GetCountryData(ipData.CountryCode).Result;

                _logger.LogInformation("CountryService call finished.");
                _cacheService.SetCacheValueAsync(ipData.CountryCode, retVal);
            }

            return retVal;
        }
    }
}

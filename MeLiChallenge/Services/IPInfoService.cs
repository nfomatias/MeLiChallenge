using MeLiChallenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public class IPInfoService : IIPInfoService
    {

        IIPService _ipService;
        ICountryService _countryService;
        IExchangeService _exchangeService;

        public IPInfoService(IIPService ipService, ICountryService countryService, IExchangeService exchangeService)
        {
            _ipService = ipService;
            _countryService = countryService;
            _exchangeService = exchangeService;
        }

        public async Task<IPInfo> GetIPInfo(string ipAddress)
        {
            var ipInfo = new IPInfo();

            ipInfo.IP = _ipService.GetIpData(ipAddress).Result;

            ipInfo.Country = _countryService.GetCountryData(ipInfo.IP.CountryCode).Result;

            ipInfo.Exchange = _exchangeService.GetExchangeData(ipInfo.Country.Currencies.FirstOrDefault().Code).Result;



            return ipInfo;
        }
    }
}

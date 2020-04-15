using MeLiChallenge.Domain;
using MeLiChallenge.Services.Externals;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MeLiChallenge.Services
{
    public class ReferenceCountryService : IReferenceCountryService
    {
        private readonly ILogger<ReferenceCountryService> _logger;

        private IConfiguration _configuration { get; }
        private ICountryService _countryService { get; }
        private Country _country { get; set; }

        public ReferenceCountryService(IConfiguration configuration, ICountryService countryService, ILogger<ReferenceCountryService> logger)
        {
            _configuration = configuration;
            _countryService = countryService;
            _logger = logger;
        }

        public Country GetReferenceCountry()
        {
            if (_country == null)
            {
                var countryCode = _configuration.GetValue<string>(key: SettingKeys.ReferenceCountryCode);
                var countryData = _countryService.GetCountryData(countryCode);
                _country = new Country(countryData.Result);
            }
            return _country;
        }
    }
}

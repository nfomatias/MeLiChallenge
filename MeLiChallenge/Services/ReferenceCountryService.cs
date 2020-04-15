using MeLiChallenge.Domain;
using MeLiChallenge.Services.Externals;
using Microsoft.Extensions.Configuration;

namespace MeLiChallenge.Services
{
    public class ReferenceCountryService : IReferenceCountryService
    {
        private IConfiguration _configuration { get; }
        private ICountryService _countryService { get; }

        private Country _country { get; set; }

        public ReferenceCountryService(IConfiguration configuration, ICountryService countryService)
        {
            _configuration = configuration;
            _countryService = countryService;
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

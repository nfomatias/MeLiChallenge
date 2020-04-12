using MeLiChallenge.Models;
using System.Threading.Tasks;

namespace MeLiChallenge.Services.Externals
{
    public interface ICountryService
    {
        Task<CountryData> GetCountryData(string countryCode);
    }
}
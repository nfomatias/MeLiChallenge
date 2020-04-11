using MeLiChallenge.Entities;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public interface ICountryService
    {
        Task<Country> GetCountryData(string countryCode);
    }
}
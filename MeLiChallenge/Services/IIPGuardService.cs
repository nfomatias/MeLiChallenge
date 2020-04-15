using MeLiChallenge.Domain;
using System.Threading.Tasks;

namespace MeLiChallenge.Services.Externals
{
    public interface IIPGurardService
    {
        Task<Country> GetCountry(string ipAddress);

        Task<Statistics> GetStatistics();
    }
}

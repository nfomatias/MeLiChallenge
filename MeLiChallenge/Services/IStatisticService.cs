using MeLiChallenge.Domain;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public interface IStatisticService
    {
        void Notify(Country country);

        Task<Statistics> GetStatistics();
    }
}

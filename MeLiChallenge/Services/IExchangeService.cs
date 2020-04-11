using MeLiChallenge.Entities;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public interface IExchangeService
    {
        Task<Exchange> GetExchangeData(string currencyCode);
    }
}